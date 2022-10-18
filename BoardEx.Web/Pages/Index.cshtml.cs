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
        private readonly ITagRepository tagRepository;

        public List<BoardAd> Boards { get; set; }
        public List<Tag> Tags { get; set; }

        public IndexModel(ILogger<IndexModel> logger, 
            IBoardAdRepository boardAdRepository,
            ITagRepository tagRepository)
        {
            _logger = logger;
            this.boardAdRepository = boardAdRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            Boards = (await boardAdRepository.GetAllAsync()).ToList();
            Tags = (await tagRepository.GetAllAsync()).ToList();

            return Page();
        }
    }
}