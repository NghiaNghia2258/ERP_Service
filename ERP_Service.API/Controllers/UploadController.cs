using ERP_Service.Domain.ApiResult;
using ERP_Service.Shared.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UploadController : ControllerBase
	{
		[HttpGet("images/{filename}")]
		public IActionResult getImage(string filename)
		{
			string filePath = UploadHelper.ROOT_IMAGE + filename;
			if (!System.IO.File.Exists(filePath))
				return BadRequest("Not found image");
			byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

			return File(fileBytes, "image/jpeg");
		}
		[HttpGet("videos/{filename}")]
		public IActionResult getVideo(string filename)
		{
			string filePath = UploadHelper.ROOT_VIDEO + filename;
			if (!System.IO.File.Exists(filePath))
				return BadRequest("Not found video");
			byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

			return File(fileBytes, "video/mp4");
		}
		[HttpPost("upload-image")]
		public async Task<IActionResult> uploadImage(IFormFile file)
		{
			UploadHelper upload = new();
			string path = await upload.UploadImageAsync(file);
			return Ok(new ApiSuccessResult<string>(path));
		}
		[HttpPost("upload-video")]
		public async Task<IActionResult> uploadVideo(IFormFile file)
		{
			UploadHelper upload = new();
			string path = await upload.UploadVideoAsync(file);
			return Ok(new ApiSuccessResult<string>(path));
		}
	}
}
