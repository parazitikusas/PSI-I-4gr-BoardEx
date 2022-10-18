﻿using BoardEx.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BoardEx.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ImagesController : Controller
	{
		private readonly IImageRepository imageRepository;

		public ImagesController(IImageRepository imageRepository)
		{
			this.imageRepository = imageRepository;
		}


		[HttpPost]
		public async Task<IActionResult> UploadAsync(IFormFile file)
		{
			var imageUrl = await imageRepository.UploadAsync(file);

			if (imageUrl == null)
			{
				return Problem("Kažkas negerai...!", null, (int)HttpStatusCode.InternalServerError);
			}
			return Json(new { link = imageUrl });
		}
	}
}