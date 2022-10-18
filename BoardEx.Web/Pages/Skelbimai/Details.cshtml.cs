using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Board
{
    public class DetailsModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        public BoardAd BoardAd { get; set; }

        public DetailsModel(IBoardAdRepository boardAdRepository)
        {
            this.boardAdRepository = boardAdRepository;
        }

        public async Task<IActionResult> OnGet(string urlHandler)
        {
            BoardAd = await boardAdRepository.GetAsync(urlHandler);
            return Page();
            
        }
    }
}
