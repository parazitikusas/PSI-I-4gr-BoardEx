using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace BoardEx.Web.Pages.Admin.Posts
{
    [Authorize(Roles = "User")]
    public class AddModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;
        private readonly UserManager<IdentityUser> userManager;

        [BindProperty]
        public AddBoardAd AddBoardAdRequest {get;set;}

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }


            [BindProperty]
            public string Tags { get; set; }
        

        public AddModel(IBoardAdRepository boardAdRepository, UserManager<IdentityUser> userManager)
        {
            this.boardAdRepository = boardAdRepository;
            this.userManager = userManager;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {

            LogsModel logsModel = new LogsModel();

            var userId = userManager.GetUserId(User);

            var boardAd = new BoardAd(){ };


            try { //exception catch doesnt work needs fix               
                if (Tags == null || AddBoardAdRequest.Name==null || AddBoardAdRequest.City == null || AddBoardAdRequest.Content == null || AddBoardAdRequest.Price == null || AddBoardAdRequest.FeaturedImageUrl == null)
                    throw new ArgumentNullException();

                boardAd = new BoardAd()
                {
                    Name = AddBoardAdRequest.Name,
                    City = AddBoardAdRequest.City,
                    Content = AddBoardAdRequest.Content,
                    UrlHandler = convert(name: AddBoardAdRequest.Name),
                    Price = AddBoardAdRequest.Price,
                    FeaturedImageUrl = AddBoardAdRequest.FeaturedImageUrl,
                    PublishedDate = DateTime.Today,
                    Author = AddBoardAdRequest.Author,
                    IsSold = AddBoardAdRequest.IsSold,
                    Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() })),
                    UserId = Guid.Parse(userId)
                }; 
            }
            catch (Exception ex)
            {
                boardAd.logOutput("NullException, ID: ");
                errorMessage("Užpildykite visus laukelius!");
                return Page();
            }
        
            if (!nameFormatCheck(AddBoardAdRequest.Author))
            {
                errorMessage("Blogas vardo formatas!");

                return Page();
            }


            await boardAdRepository.AddAsync(boardAd);


            var notification = new Notification
            {
                Type = Enums.NotificationType.Success,
                Message = "Skelbimas sėkmingai pridėtas!!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            //await boardAdRepository.UpdateAsync(boardAd);

            //logsModel.createLog(" Sukurtas naujas skelbimas ID: ", boardAd.Id.ToString()); // kvieciamas logsu sukurimo METODAS SU OPTIONAL parameter.

            //ExtentionMethods.logOutput(boardAd, " Sukurtas naujas skelbimas, ID: "); // kvieciamas logsu sukurimo EXTENDED METODAS

            boardAd.logOutput(" Sukurtas naujas skelbimas, ID: ");

            //return RedirectToPage("/admin/posts/list"); changed
            return RedirectToPage("/admin/posts/userList");
        }

        public bool nameFormatCheck (String name) { // tikrina skelbimo autoriaus vardo formata
            if (name == null)
            {
                errorMessage("Užpildykite savo vardą");
                return false;
            }
            if (!Regex.Match(name, "^[A-Z][a-zA-Z]*\\s[A-Z][a-zA-Z]*$").Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public void errorMessage(string messageText)
        {
            var notf = new Notification
            {
                Type = Enums.NotificationType.Error,
                Message = messageText
            };

            TempData["Notification"] = JsonSerializer.Serialize(notf);

            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }


        }

        public string convert(String name) // konvertuoja zaidimo pavadinima i tinkama formata linkui
        {
            if (name == null)
            {
                return null;
            }
            char[] charArr = name.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {

                if (charArr[charArr.Length - 1] == ' ')
                {
                    List<char> list = new List<char>(charArr);
                    list.RemoveAt(charArr.Length - 1);
                    charArr = list.ToArray();
                }
                if (charArr[i] == ' ')
                {
                    charArr[i] = '-';
                }
                
            }
            
            name = new string(charArr);
            return name;
        }

    }
}
