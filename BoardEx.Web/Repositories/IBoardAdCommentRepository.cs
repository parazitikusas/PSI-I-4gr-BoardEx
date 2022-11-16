using BoardEx.Web.Models.Domain;

namespace BoardEx.Web.Repositories
{
	public interface IBoardAdCommentRepository
	{
		Task<BoardAdComment> AddAsync(BoardAdComment boardAdComment);

		Task<IEnumerable<BoardAdComment>> GetAllAsync(Guid boardAdId);
	}
}
