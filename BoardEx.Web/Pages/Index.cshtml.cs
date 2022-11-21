using BoardEx.Web.Models.Domain;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoardEx.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IBoardAdRepository boardAdRepository;
        private readonly ITagRepository tagRepository;

        public List<BoardAd> Boards { get; set; }
        public List<Tag> Tags { get; set; }

        /*Button button = new Button();
        button.Click += (s, e) => {
            //your code;
        };
        //button.Click += new EventHandler(button_Click);
        container.Controls.Add(button);*/

        int x = 2;

        public IndexModel(ILogger<IndexModel> logger, 
            IBoardAdRepository boardAdRepository,
            ITagRepository tagRepository)
        {
            _logger = logger;
            this.boardAdRepository = boardAdRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> OnGet()
        {

            Boards = (await boardAdRepository.GetCertainNumber(x)).ToList();
            Tags = (await tagRepository.GetGetCertainNumber(x)).ToList();

            //Boards = (await boardAdRepository.GetAllAsync()).ToList();
            //Tags = (await tagRepository.GetAllAsync()).ToList();

            return Page();
        }

        public async Task<IActionResult> UpdateList()
        {
            Console.Write("test");
            x += 2;
            Boards = (await boardAdRepository.GetCertainNumber(x)).ToList();
            Tags = (await tagRepository.GetGetCertainNumber(x)).ToList();

            return Page();
        }

        public string Message { get; set; }
        public void OnGetOnClick()
        {

            Message = "Hello";

        }

    }
    
}