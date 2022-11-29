using BoardEx.Web.Data;
using BoardEx.Web.Event;
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
        public static event LogAddDelegate logEventHandler;
        public static LogListener logListener = null;
        public static Log log;

        public List<BoardAd> BoardAds { get; set; }

        public ListModel(IBoardAdRepository boardAdRepository, ILogsRepository logsRepository)
        {
            this.boardAdRepository = boardAdRepository;
            this.logsRepository = logsRepository;
        }


        public async Task OnGet()
        {

            //logsRepository.CreateLog(" Peržiūrėti visi skelbimai");
            logEventHandler += new LogAddDelegate(logListener.AddLog);
            log = new Log { Message = " Peržiūrėti visi skelbimai" };
            AddNewLog(log);


            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }

            BoardAds = (await boardAdRepository.GetAllAsync())?.ToList();
        }

        private static void AddNewLog(Log log)
        {
            if(log != null)
            {
                LogArgs l = new LogArgs(log);
                OnLogAdd(l);
            }
        }

        private static void OnLogAdd(LogArgs l)
        {
            if(logEventHandler != null)
            {
                logEventHandler(l);
            }
        }
    }
}
