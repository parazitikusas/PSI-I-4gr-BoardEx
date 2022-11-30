using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserRepository userRepository;

        public List<User> Users { get; set; }

        [BindProperty]
        public AddUser AddUserRequest { get; set; }

        public IndexModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            var users = await userRepository.GetAll();

            Users = new List<User>();
            foreach (var user in users)
            {
                Users.Add(new Models.ViewModels.User()
                {
                    Id = Guid.Parse(user.Id),
                    Username = user.UserName,
                    Email = user.Email
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var identityUser = new IdentityUser
            {
                UserName = AddUserRequest.Username,
                Email = AddUserRequest.Email
            };

            var roles = new List<string> { "User" };

            if (AddUserRequest.AdminCheckbox)
            {
                roles.Add("Admin");
            }

            var result = await userRepository.Add(identityUser, AddUserRequest.Password, roles);

            if (result)
            {
                return RedirectToPage("/Admin/Users/Index");
            }
            return Page();
        }
    }
}
