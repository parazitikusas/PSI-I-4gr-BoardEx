namespace BoardEx.Web.Models.Domain
{
	public class BoardAdComment
	{
		public Guid Id { get; set; }

		public string Description { get; set; }

		public Guid BoardAdId { get; set; }

		public Guid UserId { get; set; }

		public DateTime DateAdded { get; set; }
	}
}
