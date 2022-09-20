using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class ListModel : PageModel
    {
        private readonly BoardExDbContext boardExDbContext;

        public List<BoardAd> BoardAds { get; set; }

        public ListModel(BoardExDbContext boardExDbContext)
        {
            this.boardExDbContext = boardExDbContext;
        }


        public void OnGet()
        {
            BoardAds = boardExDbContext.BoardAds.ToList();
        }
    }
}
