using BoardEx.Web.Enums;

namespace BoardEx.Web.Models.ViewModels
{
	public class Notification
	{
		public string Message { get; set; }

		public NotificationType Type { get; set; }
	}
}
