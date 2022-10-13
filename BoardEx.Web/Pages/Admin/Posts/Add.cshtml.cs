using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class AddModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        [BindProperty]
        public AddBoardAd AddBoardAdRequest { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        public AddModel(IBoardAdRepository boardAdRepository)
        {
            this.boardAdRepository = boardAdRepository;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            var boardAd = new BoardAd()
            {
                Name = AddBoardAdRequest.Name,
                City = AddBoardAdRequest.City,
                Content = AddBoardAdRequest.Content,
                Price = AddBoardAdRequest.Price,
                FeaturedImageUrl = AddBoardAdRequest.FeaturedImageUrl,
                PublishedDate = AddBoardAdRequest.PublishedDate,
                Author = AddBoardAdRequest.Author,
                IsSold = AddBoardAdRequest.IsSold
            };

            if (!nameFormatCheck(AddBoardAdRequest.Author))
            {
                var notf = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Blogas vardo formatas!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notf);

                var notificationJson = (string)TempData["Notification"];
                if (notificationJson != null)
                {
                    ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
                }

                return Page();
            }

            await boardAdRepository.AddAsync(boardAd);


            var notification = new Notification
            {
                Type = Enums.NotificationType.Success,
                Message = "Skelbimas sėkmingai pridėtas!!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            //await boardAdRepository.UpdateAsync(boardAd);

            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
            using StreamWriter logFile = new(file, append: true);
            await logFile.WriteLineAsync("<br/>" + DateTime.Now + " Sukurtas naujas skelbimas ID: " + (boardAd.Id));

            return RedirectToPage("/admin/posts/list");
        }

        public bool nameFormatCheck (String name) {

            if (!Regex.Match(name, "^[A-Z][a-zA-Z]*\\s[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
