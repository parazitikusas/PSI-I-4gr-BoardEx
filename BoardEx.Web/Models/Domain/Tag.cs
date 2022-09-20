namespace BoardEx.Web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid BoardAdId { get; set; }
    }
}
