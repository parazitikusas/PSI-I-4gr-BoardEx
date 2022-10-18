using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IBoardAdRepository boardAdRepository;


        public List<BoardAd> Boards { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBoardAdRepository boardAdRepository)
        {
            _logger = logger;
            this.boardAdRepository = boardAdRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            Boards = (await boardAdRepository.GetAllAsync()).ToList();
            return Page();
        }
    }
}