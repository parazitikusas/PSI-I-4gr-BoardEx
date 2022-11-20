using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "Admin")]
    public class ListModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;
        private readonly ILogsRepository logsRepository;

        public List<BoardAd> BoardAds { get; set; }

        public ListModel(IBoardAdRepository boardAdRepository, ILogsRepository logsRepository)
        {
            this.boardAdRepository = boardAdRepository;
            this.logsRepository = logsRepository;
        }


        public async Task OnGet()
        {

            logsRepository.CreateLog(" Peržiūrėti visi skelbimai");


            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BoardAds = (await boardAdRepository.GetAllAsync())?.ToList();
        }
    }
}
