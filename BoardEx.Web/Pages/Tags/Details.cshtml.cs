using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        public List<BoardAd> Boards { get; set; }

        public DetailsModel(IBoardAdRepository boardAdRepository)
        {
            this.boardAdRepository = boardAdRepository;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            Boards = (await boardAdRepository.GetAllAsync(tagName)).ToList();
            return Page();
        }
    }
}
