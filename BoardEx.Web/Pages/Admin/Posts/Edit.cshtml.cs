﻿using BoardEx.Web.Data;
using BoardEx.Web.Models.Domain;
using BoardEx.Web.Models.ViewModels;
using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text.Json;

namespace BoardEx.Web.Pages.Admin.Posts
{
    public class EditModel : PageModel
    {
        private readonly IBoardAdRepository boardAdRepository;

        [BindProperty]
        public BoardAd BoardAd { get; set; }

        public EditModel(IBoardAdRepository boardAdRepository)
        {
            this.boardAdRepository = boardAdRepository;
        }


        public async Task OnGet(Guid Id)
        {
            BoardAd = await boardAdRepository.GetAsync(Id);
        }

        public async Task<IActionResult> OnPostEdit()               // reikėtų padaryti, kad po sėkimingo atnaujinimo, redirectintu į listą ir ten rašytu notificationa.
        {
            try
            {
                //throw new Exception();

                await boardAdRepository.UpdateAsync(BoardAd);
                
                ViewData["Notification"] = new Notification
                {
                    Message = "Sėkmingai atnaujinta!",
                    Type = Enums.NotificationType.Success
                };
            }
            catch (Exception)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Ooops! Kažkas negerai!",
                    Type = Enums.NotificationType.Error
                };

                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await boardAdRepository.DeleteAsync(BoardAd.Id);
            if (deleted)
            {
                var notification = new Notification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Skelbimas sėkmingai ištrintas!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/admin/posts/list");
            }

            return Page();
        }
    }
}
