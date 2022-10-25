using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;
        private readonly ITagRepository tagRepository;

        public List<BoardAd> Boards { get; set; }
        public List<Tag> Tags { get; set; }

        public DetailsModel(IBoardAdRepository boardAdRepository,
            ITagRepository tagRepository)
        {
            this.boardAdRepository = boardAdRepository;
            this.tagRepository = tagRepository;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            Boards = (await boardAdRepository.GetAllAsync(tagName)).ToList();
            Tags = (await tagRepository.GetAllAsync()).ToList();

            return Page();
        }
    }
}
