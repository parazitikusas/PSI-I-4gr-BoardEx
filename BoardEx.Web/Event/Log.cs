namespace BoardEx.Web.Event
{
    public class Log
    {
        public string Message { get; set; }

        public static void AddLog(LogArgs l)
        {
            Console.WriteLine(l._log.Message);
        }
    }
}
