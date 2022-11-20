namespace BoardEx.Web.Repositories
{
    public class LogsRepository : ILogsRepository
    {
        public async void CreateLog(string message, string id = "")
        {
            var file = System.IO.Path.Combine(Environment.CurrentDirectory, "./logs/logs.txt");
            using StreamWriter logFile = new(file, append: true);
            await logFile.WriteLineAsync("<br/>" + DateTime.Now + message + id);
        }
    }
}
