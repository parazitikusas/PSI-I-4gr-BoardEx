using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardEx.Web.Repositories
{
    public class BoardAdRepository : IBoardAdRepository
    {
        private readonly BoardExDbContext boardExDbContext;

        public BoardAdRepository(BoardExDbContext boardExDbContext)
        {
            this.boardExDbContext = boardExDbContext;
        }

        public async Task<BoardAd> AddAsync(BoardAd boardAd)
        {
            await boardExDbContext.BoardAds.AddAsync(boardAd);
            await boardExDbContext.SaveChangesAsync();
            return boardAd;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingBoardAd = await boardExDbContext.BoardAds.FindAsync(id);

            if (existingBoardAd != null)
            {
                boardExDbContext.BoardAds.Remove(existingBoardAd);
                await boardExDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<BoardAd>> GetAllAsync()
        {
            return await boardExDbContext.BoardAds.ToListAsync();
        }

        public async Task<BoardAd> GetAsync(Guid id)
        {
            return await boardExDbContext.BoardAds.FindAsync(id);
        }

        public async Task<BoardAd> UpdateAsync(BoardAd boardAd)
        {
            var existingBoardAd = await boardExDbContext.BoardAds.FindAsync(boardAd.Id);

            if (existingBoardAd != null)
            {
                existingBoardAd.Name = boardAd.Name;
                existingBoardAd.City = boardAd.City;
                existingBoardAd.Content = boardAd.Content;
                existingBoardAd.Price = boardAd.Price;
                existingBoardAd.FeaturedImageUrl = boardAd.FeaturedImageUrl;
                existingBoardAd.PublishedDate = boardAd.PublishedDate;
                existingBoardAd.Author = boardAd.Author;
                existingBoardAd.IsSold = boardAd.IsSold;
            }

            await boardExDbContext.SaveChangesAsync();
            return existingBoardAd;
        }
    }
}
