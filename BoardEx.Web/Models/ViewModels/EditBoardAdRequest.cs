using System.ComponentModel.DataAnnotations;

namespace BoardEx.Web.Models.ViewModels
{
	public class EditBoardAdRequest
	{
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string City { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UrlHandler { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public string FeaturedImageUrl { get; set; }


        public DateTime PublishedDate { get; set; }

        [Required]
        public string Author { get; set; }
        public bool IsSold { get; set; }

        public Guid UserId { get; set; }
    }
}
