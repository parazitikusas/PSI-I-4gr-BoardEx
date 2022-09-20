using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class AddModel : PageModel
    {
        private readonly BoardExDbContext boardExDbContext;

        [BindProperty]
        public AddBoardAd AddBoardAdRequest { get; set; }

        public AddModel(BoardExDbContext boardExDbContext)
        {
            this.boardExDbContext = boardExDbContext;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            var boardAd = new BoardAd()
            {
                Name = AddBoardAdRequest.Name,
                City = AddBoardAdRequest.City,
                Content = AddBoardAdRequest.Content,
                Price = AddBoardAdRequest.Price,
                FeaturedImageUrl = AddBoardAdRequest.FeaturedImageUrl,
                PublishedDate = AddBoardAdRequest.PublishedDate,
                Author = AddBoardAdRequest.Author,
                IsSold = AddBoardAdRequest.IsSold
            };

            boardExDbContext.BoardAds.Add(boardAd);
            boardExDbContext.SaveChanges();
            return RedirectToPage("/admin/posts/list");
        }
    }
}
