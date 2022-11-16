using BoardEx.Web.Models.Domain;

namespace BoardEx.Web.Repositories
{
    public interface IBoardAdLikeRepository
    {
        Task<int> GetTotalLikesForBoardAd(Guid boardAdId);
    
        Task AddLikeForBoard(Guid boardAdId, Guid userId);

        Task<IEnumerable<BoardAdLike>> GetLikesForBoard(Guid boardAdId);
    }
}
