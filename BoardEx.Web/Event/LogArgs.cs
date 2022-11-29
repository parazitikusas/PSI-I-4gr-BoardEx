namespace BoardEx.Web.Event
{
    public delegate void LogAddDelegate(LogArgs l);
    public class LogArgs : EventArgs
    {
        public Log _log;
        public LogArgs (Log log)
        {
            this._log = log;
        }
    }
}
