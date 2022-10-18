using BoardEx.Web.Models.Domain;

namespace BoardEx.Web.Repositories
{
    public interface IBoardAdRepository
    {
        Task<IEnumerable<BoardAd>> GetAllAsync();   //get all
        Task<BoardAd> GetAsync(Guid id);            // get one board ad
        Task<BoardAd> GetAsync(string urlHandler);
        Task<BoardAd> AddAsync(BoardAd boardAd);    // add board ad
        Task<BoardAd> UpdateAsync(BoardAd boardAd); // edit board ad
        Task<bool> DeleteAsync(Guid id);            // delete board ad
    }
}
