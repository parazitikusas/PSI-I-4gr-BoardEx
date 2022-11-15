using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardEx.Web.Repositories
{
    public class BoardAdLikeRepository : IBoardAdLikeRepository
    {
        private readonly BoardExDbContext boardExDbContext;

        public BoardAdLikeRepository(BoardExDbContext boardExDbContext)
        {
            this.boardExDbContext = boardExDbContext;
        }

        public async Task AddLikeForBoard(Guid boardAdId, Guid userId)
        {
            var like = new BoardAdLike
            {
                Id = Guid.NewGuid(),
                BoardAdId = boardAdId,
                UserId = userId
            };

            await boardExDbContext.BoardAdLike.AddAsync(like);
            await boardExDbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<BoardAdLike>> GetLikesForBoard(Guid boardAdId)
        {
            return await boardExDbContext.BoardAdLike.Where(x => x.BoardAdId == boardAdId)
                .ToListAsync();
        }

        public async Task<int> GetTotalLikesForBoardAd(Guid boardAdId)
        {
            return await boardExDbContext.BoardAdLike
                .CountAsync(x => x.BoardAdId == boardAdId);
        }
    }
}
