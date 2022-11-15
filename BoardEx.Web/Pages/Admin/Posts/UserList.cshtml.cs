using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class UserListModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        public List<BoardAd> BoardAds { get; set; }
        private readonly UserManager<IdentityUser> userManager;



        public UserListModel(IBoardAdRepository boardAdRepository, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.boardAdRepository = boardAdRepository;
        }

        public async Task OnGet()
        {
            LogsModel logsModel = new LogsModel();


            logsModel.createLog("Peržiūrėti visi skelbimai ");

            var userId = userManager.GetUserId(User);


            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BoardAds = (await boardAdRepository.GetAllAsync())?.ToList()
                .Where(item => item.UserId == Guid.Parse(userId))
                .ToList();
        }


    //    public async Task<IEnumerable<BoardAd>> GetAllAsync(string tagName)
    //    {
    //        return await (boardExDbContext.BoardAds.Include(nameof(BoardAd.Tags))
    //            .Where(x => x.Tags.Any(x => x.Name == tagName)))
    //            .ToListAsync();
    //    }
    }
}
