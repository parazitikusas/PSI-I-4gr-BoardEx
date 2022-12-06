using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BoardEx.Web.Pages.Board
{
    public class DetailsModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;
        private readonly IBoardAdLikeRepository boardAdLikeRepository;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBoardAdCommentRepository boardAdCommentRepository;

        public BoardAd BoardAd { get; set; }

        public List<BoardComment> Comments { get; set; }
        public int TotalLikes { get; set; }
        public bool Liked { get; set; }

        [BindProperty]
        public Guid BoardAdId { get; set; }
        
        [BindProperty]
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string CommentDescription { get; set; }

        public DetailsModel(IBoardAdRepository boardAdRepository, 
            IBoardAdLikeRepository boardAdLikeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IBoardAdCommentRepository boardAdCommentRepository)
        {
            this.boardAdRepository = boardAdRepository;
            this.boardAdLikeRepository = boardAdLikeRepository;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.boardAdCommentRepository = boardAdCommentRepository;
        }

        public async Task<IActionResult> OnGet(string urlHandle)
        {
            await GetBoard(urlHandle);
            
            return Page();
            
        }

        public async Task<IActionResult> OnPost(string urlHandle)
        {
            if (ModelState.IsValid)
            {
                if (signInManager.IsSignedIn(User) && !string.IsNullOrEmpty(CommentDescription))
                {
                    var userId = userManager.GetUserId(User);

                    var comment = new BoardAdComment()
                    {
                        BoardAdId = BoardAdId,
                        Description = CommentDescription,
                        DateAdded = DateTime.Now,
                        UserId = Guid.Parse(userId)
                    };

                    await boardAdCommentRepository.AddAsync(comment);
                }


                return RedirectToPage("/skelbimai/details", new { urlHandle = urlHandle });
            }

            await GetBoard(urlHandle);
            return Page();
        }

        private async Task GetComments()
        {
            var boardAdComments = await boardAdCommentRepository.GetAllAsync(BoardAd.Id);

            var boardAdCommentsViewModel = new List<BoardComment>();

            foreach (var boardAdComment in boardAdComments)
            {
                boardAdCommentsViewModel.Add(new BoardComment
                {
                    DateAdded = boardAdComment.DateAdded,
                    Description = boardAdComment.Description,
                    Username = (await userManager.FindByIdAsync(boardAdComment.UserId.ToString())).UserName
                });
            }

            Comments = boardAdCommentsViewModel;
        }

        private async Task GetBoard(string urlHandle)
        {
            BoardAd = await boardAdRepository.GetAsync(urlHandle);

            if (BoardAd != null)
            {
                BoardAdId = BoardAd.Id;

                if (signInManager.IsSignedIn(User))
                {
                    var likes = await boardAdLikeRepository.GetLikesForBoard(BoardAd.Id);

                    var userId = userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));

                    await GetComments();
                }

                TotalLikes = await boardAdLikeRepository.GetTotalLikesForBoardAd(BoardAd.Id);
            }
        }
    }
}
