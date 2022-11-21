namespace BoardEx.Web.Repositories
{
    public interface ILogsRepository
    {
        void CreateLog(string message, string id = "");
    }
}
