namespace BoardEx.Web.Models.ViewModels
{
	public class AddBoardAd
	{
        public string Name { get; set; }
        public string City { get; set; }
        public string Content { get; set; }
        public string UrlHandler { get; set; }
        public int Price { get; set; }
        public string FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsSold { get; set; }
    }
}
