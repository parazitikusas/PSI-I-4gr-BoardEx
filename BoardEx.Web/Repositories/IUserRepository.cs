using Microsoft.AspNetCore.Identity;

namespace BoardEx.Web.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<IdentityUser>> GetAll();
	}
}
