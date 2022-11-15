using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BoardEx.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BoardAdLikeController : Controller
	{
		private readonly IBoardAdLikeRepository boardAdLikeRepository;

		public BoardAdLikeController(IBoardAdLikeRepository boardAdLikeRepository)
		{
			this.boardAdLikeRepository = boardAdLikeRepository;
		}

		[Route("Add")]
		[HttpPost]
        public async Task<IActionResult> AddLike([FromBody] AddBoardAdLikeRequest addBoardAdLikeRequest)
		{
			await boardAdLikeRepository.AddLikeForBoard(addBoardAdLikeRequest.BoardAdId, 
				addBoardAdLikeRequest.UserId);

			return Ok();
        }

		[HttpGet]
		[Route("{boardAdId:Guid}/totalLikes")]
		public async Task<IActionResult> GetTotalLikes([FromRoute] Guid boardAdId)
		{
			var totalLikes = await boardAdLikeRepository.GetTotalLikesForBoardAd(boardAdId);

			return Ok(totalLikes);
		}
	}
}
