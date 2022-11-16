using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardEx.Web.Repositories
{
	public class BoardAdCommentRepository : IBoardAdCommentRepository
	{
		private readonly BoardExDbContext boardExDbContext;

		public BoardAdCommentRepository(BoardExDbContext boardExDbContext)
		{
			this.boardExDbContext = boardExDbContext;
		}

		public async Task<BoardAdComment> AddAsync(BoardAdComment boardAdComment)
		{
			await boardExDbContext.BoardAdComment.AddAsync(boardAdComment);
			await boardExDbContext.SaveChangesAsync();
			return boardAdComment;
		}

		public async Task<IEnumerable<BoardAdComment>> GetAllAsync(Guid boardAdId)
		{
            return await boardExDbContext.BoardAdComment.Where(x => x.BoardAdId == boardAdId )
				.ToListAsync();

        }
	}
}
