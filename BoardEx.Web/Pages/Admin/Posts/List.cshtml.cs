using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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


        public async Task OnGet()
        {
            BoardAds = await boardExDbContext.BoardAds.ToListAsync();
        }
    }
}
