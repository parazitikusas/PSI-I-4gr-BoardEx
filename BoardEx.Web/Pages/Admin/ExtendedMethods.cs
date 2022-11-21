using BoardEx.Web.Models.Domain;
using System.Xml.Linq;

namespace BoardEx.Web.Pages.Admin
{
    public static class ExtentionMethods
    {
        public static async void logOutput(this BoardAd boardAd, string message)
        {

            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
            using StreamWriter logFile = new(file, append: true);
            await logFile.WriteLineAsync("<br/>" + DateTime.Now + message + boardAd.Id);
        }
    }
}
