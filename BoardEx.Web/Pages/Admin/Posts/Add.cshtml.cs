using BoardEx.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public AddBoardAd AddBoardAdRequest { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("/admin/posts/list");
        }
    }
}
