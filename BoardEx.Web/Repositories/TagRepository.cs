using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BoardEx.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BoardExDbContext boardExDbContext;

        public TagRepository(BoardExDbContext boardExDbContext)
        {
            this.boardExDbContext = boardExDbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await boardExDbContext.Tags.ToListAsync();

            return tags.DistinctBy(x => x.Name.ToLower());
        }

        public async Task<IEnumerable<Tag>> GetGetCertainNumber(int number)
        {
            var tags = await boardExDbContext.Tags.Take(number).ToListAsync();

            return tags.DistinctBy(x => x.Name.ToLower());
        }
    }
}
