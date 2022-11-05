using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Board
{
    public class DetailsModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;
        private readonly IBoardAdLikeRepository boardAdLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public BoardAd BoardAd { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }

        public DetailsModel(IBoardAdRepository boardAdRepository, 
            IBoardAdLikeRepository boardAdLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            this.boardAdRepository = boardAdRepository;
            this.boardAdLikeRepository = boardAdLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string urlHandler)
        {
            BoardAd = await boardAdRepository.GetAsync(urlHandler);

            if (BoardAd != null)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var likes = await boardAdLikeRepository.GetLikesForBoard(BoardAd.Id);

                    var userId = userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                }
                

                TotalLikes = await boardAdLikeRepository.GetTotalLikesForBoardAd(BoardAd.Id);
            }
            return Page();
            
        }
    }
}
