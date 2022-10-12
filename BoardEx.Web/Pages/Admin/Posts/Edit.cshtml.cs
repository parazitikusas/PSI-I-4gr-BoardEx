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
            try
            {
                //throw new Exception();

                await boardAdRepository.UpdateAsync(BoardAd);

                var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
                using StreamWriter logFile = new(file, append: true);
                await logFile.WriteLineAsync("<br/>" + DateTime.Now + " Atnaujintas skelbimas ID: " + (BoardAd.Id));

                ViewData["Notification"] = new Notification
                {
                    Message = "Sėkmingai atnaujinta!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception)
            {
                var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
                using StreamWriter logFile = new(file, append: true);
                await logFile.WriteLineAsync("<br/>" + DateTime.Now + "Nepavyko atnaujinti skelbimo ID: " + (BoardAd.Id));

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
                var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
                using StreamWriter logFile = new(file, append: true);
                await logFile.WriteLineAsync("<br/>" + DateTime.Now + " Ištrintas skelbimas ID: " + (BoardAd.Id));

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
