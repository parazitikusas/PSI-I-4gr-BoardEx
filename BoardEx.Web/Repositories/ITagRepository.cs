using BoardEx.Web.Models.Domain;

namespace BoardEx.Web.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
    }
}
