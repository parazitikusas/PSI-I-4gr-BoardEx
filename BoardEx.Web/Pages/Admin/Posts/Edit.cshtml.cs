using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class EditModel : PageModel
    {
        private readonly BoardExDbContext boardExDbContext;

        [BindProperty]
        public BoardAd BoardAd { get; set; }

        public EditModel(BoardExDbContext boardExDbContext)
        {
            this.boardExDbContext = boardExDbContext;
        }


        public void OnGet(Guid Id)
        {
            BoardAd = boardExDbContext.BoardAds.Find(Id);

        }

        public IActionResult OnPostEdit()
        {
            var existingBoardAd = boardExDbContext.BoardAds.Find(BoardAd.Id);

            if (existingBoardAd != null)
            {
                existingBoardAd.Name = BoardAd.Name;
                existingBoardAd.City = BoardAd.City;
                existingBoardAd.Content = BoardAd.Content;
                existingBoardAd.Price = BoardAd.Price;
                existingBoardAd.FeaturedImageUrl = BoardAd.FeaturedImageUrl;
                existingBoardAd.PublishedDate = BoardAd.PublishedDate;
                existingBoardAd.Author = BoardAd.Author;
                existingBoardAd.IsSold = BoardAd.IsSold;
            }

            boardExDbContext.SaveChanges();
            return RedirectToPage("/admin/posts/list");
        }

        public IActionResult OnPostDelete()
        {
            var existingBoardAd = boardExDbContext.BoardAds.Find(BoardAd.Id);

            if (existingBoardAd != null)
            {
                boardExDbContext.BoardAds.Remove(existingBoardAd);
                boardExDbContext.SaveChanges();

                return RedirectToPage("/admin/posts/list");
            }

            return Page();
        }
    }
}
