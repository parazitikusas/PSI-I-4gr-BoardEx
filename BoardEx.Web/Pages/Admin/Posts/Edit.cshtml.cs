using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "User")]
    public class EditModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;
        private readonly ILogsRepository logsRepository;

        [BindProperty]
        public BoardAd BoardAd { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        public EditModel(IBoardAdRepository boardAdRepository, ILogsRepository logsRepository)
        {
            this.boardAdRepository = boardAdRepository;
            this.logsRepository = logsRepository;
        }


        public async Task OnGet(Guid Id)
        {
            Lazy<Task<BoardAd>> getAsync = new Lazy<Task<BoardAd>>(() => boardAdRepository.GetAsync(Id));  // LAZY IMPLEMENTATION

            BoardAd = await getAsync.Value;

            if (BoardAd != null && BoardAd.Tags != null)
            {
                Tags = String.Join(',', BoardAd.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()               // reikėtų padaryti, kad po sėkimingo atnaujinimo, redirectintu į listą ir ten rašytu notificationa.
        {

            try
            {
                BoardAd.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }));

                await boardAdRepository.UpdateAsync(BoardAd);

                logsRepository.CreateLog(" Atnaujintas skelbimas ID: ", BoardAd.Id.ToString());

                ViewData["Notification"] = new Notification
                {
                    Message = "Sėkmingai atnaujinta!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {

                logsRepository.CreateLog(" Nepavyko atnaujinti skelbimo ID: ", BoardAd.Id.ToString());


                ViewData["Notification"] = new Notification
                {
                    Message = "Ooops! Kažkas negerai!",
                    Type = Enums.NotificationType.Error
                };

                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {

            var deleted = await boardAdRepository.DeleteAsync(BoardAd.Id);
            if (deleted)
            {
                //var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
                // using StreamWriter logFile = new(file, append: true);
                //await logFile.WriteLineAsync("<br/>" + DateTime.Now + "" + (BoardAd.Id));

                logsRepository.CreateLog(" Ištrintas skelbimas ID: ", BoardAd.Id.ToString());




                var notification = new Notification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Skelbimas sėkmingai ištrintas!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/admin/posts/list");
            }

            return Page();
        }
    }
}
