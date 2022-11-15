namespace BoardEx.Web.Models.Domain
{
    public class BoardAdLike
    {
        public Guid Id { get; set; }
        public Guid BoardAdId { get; set; }
        public Guid UserId { get; set; }
    }
}
