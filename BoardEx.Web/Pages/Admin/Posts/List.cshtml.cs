using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class ListModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        public List<BoardAd> BoardAds { get; set; }

        public ListModel(IBoardAdRepository boardAdRepository)
        {
            this.boardAdRepository = boardAdRepository;
        }


        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BoardAds = (await boardAdRepository.GetAllAsync())?.ToList();
        }
    }
}
