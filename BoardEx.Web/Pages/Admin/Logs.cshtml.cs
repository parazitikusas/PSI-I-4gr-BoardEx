using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace BoardEx.Web.Pages.Admin
{
    public class LogsModel : PageModel
    {
        private IHostingEnvironment env;
        public LogsModel(IHostingEnvironment env)
        {
            this.env = env;    
        }
        public void OnGet()
        {
            var webRoot = this.env.WebRootPath;
            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
            ViewData["test"] = System.IO.File.ReadAllText(file);
        }
    }
}
