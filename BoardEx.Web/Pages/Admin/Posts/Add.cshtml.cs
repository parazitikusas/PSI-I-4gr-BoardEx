using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class AddModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        [BindProperty]
        public AddBoardAd AddBoardAdRequest { get; set; }

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

            await boardAdRepository.AddAsync(boardAd);

            var notification = new Notification
            {
                Type = Enums.NotificationType.Success,
                Message = "Skelbimas sėkmingai pridėtas!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/admin/posts/list");
        }
    }
}
