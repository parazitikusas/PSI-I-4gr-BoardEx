using Microsoft.AspNetCore.Identity;

namespace BoardEx.Web.Repositories
{
	public interface IUserRepository
	{
		Task<IEnumerable<IdentityUser>> GetAll();

		Task<bool> Add(IdentityUser identityUser, string password, List<string> roles);

		Task Delete(Guid userId);
	}
}
