using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class EditModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        [BindProperty]
        public BoardAd BoardAd { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        public EditModel(IBoardAdRepository boardAdRepository)
        {
            this.boardAdRepository = boardAdRepository;
        }


        public async Task OnGet(Guid Id)
        {
            BoardAd = await boardAdRepository.GetAsync(Id);
        }

        public async Task<IActionResult> OnPostEdit()               // reikėtų padaryti, kad po sėkimingo atnaujinimo, redirectintu į listą ir ten rašytu notificationa.
        {


            LogsModel logsModel = new LogsModel();


            try
            {
                await boardAdRepository.UpdateAsync(BoardAd);

                logsModel.createLog(" Atnaujintas skelbimas ID: ", BoardAd.Id.ToString());

                ViewData["Notification"] = new Notification
                {
                    Message = "Sėkmingai atnaujinta!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception ex)
            {

                logsModel.createLog(" Nepavyko atnaujinti skelbimo ID: ", BoardAd.Id.ToString());


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

            LogsModel logsModel = new LogsModel();

            var deleted = await boardAdRepository.DeleteAsync(BoardAd.Id);
            if (deleted)
            {
                //var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
               // using StreamWriter logFile = new(file, append: true);
                //await logFile.WriteLineAsync("<br/>" + DateTime.Now + "" + (BoardAd.Id));

                logsModel.createLog(" Ištrintas skelbimas ID: ", BoardAd.Id.ToString());




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
