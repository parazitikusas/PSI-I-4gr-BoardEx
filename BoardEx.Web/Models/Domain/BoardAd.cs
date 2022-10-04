﻿namespace BoardEx.Web.Models.Domain
{
	public class BoardAd
	{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Content { get; set; }
        public int Price { get; set; }
        public string FeaturedImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool IsSold { get; set; }
    }
}