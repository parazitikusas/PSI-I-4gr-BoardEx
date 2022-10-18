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
            return await boardExDbContext.BoardAds.Include(nameof(BoardAd.Tags)).ToListAsync();
        }

        public async Task<IEnumerable<BoardAd>> GetAllAsync(string tagName)
        {
            return await (boardExDbContext.BoardAds.Include(nameof(BoardAd.Tags))
                .Where(x => x.Tags.Any(x => x.Name == tagName)))
                .ToListAsync();
        }

        public async Task<BoardAd> GetAsync(Guid id)
        {
            return await boardExDbContext.BoardAds.Include(nameof(BoardAd.Tags))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BoardAd> GetAsync(string urlHandler)
        {
            return await boardExDbContext.BoardAds.Include(nameof(BoardAd.Tags))
                .FirstOrDefaultAsync(x => x.UrlHandler == urlHandler);
        }

        public async Task<BoardAd> UpdateAsync(BoardAd boardAd)
        {
            var existingBoardAd = await boardExDbContext.BoardAds.Include(nameof(BoardAd.Tags)).FirstOrDefaultAsync(x => x.Id == boardAd.Id);

            if (existingBoardAd != null)
            {
                existingBoardAd.Name = boardAd.Name;
                existingBoardAd.City = boardAd.City;
                existingBoardAd.Content = boardAd.Content;
                existingBoardAd.Price = boardAd.Price;
                existingBoardAd.UrlHandler = boardAd.UrlHandler;
                existingBoardAd.FeaturedImageUrl = boardAd.FeaturedImageUrl;
                existingBoardAd.PublishedDate = boardAd.PublishedDate;
                existingBoardAd.Author = boardAd.Author;
                existingBoardAd.IsSold = boardAd.IsSold;
                
   
                if (boardAd.Tags != null && boardAd.Tags.Any())
                {
                    // Delete the existing tags
                    boardExDbContext.Tags.RemoveRange(existingBoardAd.Tags);

                    // Add new tags
                    boardAd.Tags.ToList().ForEach(x => x.BoardAdId = existingBoardAd.Id);

                    await boardExDbContext.Tags.AddRangeAsync(boardAd.Tags);
                }

            }

            await boardExDbContext.SaveChangesAsync();
            return existingBoardAd;
        }
    }
}
