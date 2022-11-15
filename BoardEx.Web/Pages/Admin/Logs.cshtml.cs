using BoardEx.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace BoardEx.Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class LogsModel : PageModel
    {
        private IHostingEnvironment env;
        public void OnGet()
        {
            //var webRoot = this.env.WebRootPath;
            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
            ViewData["test"] = System.IO.File.ReadAllText(file);
        }


        public async void createLog (string message, string id = "")
        {
            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
            using StreamWriter logFile = new(file, append: true);
            await logFile.WriteLineAsync("<br/>" + DateTime.Now + message + id);
        }

    }
}
