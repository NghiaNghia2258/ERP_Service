using Microsoft.AspNetCore.Http;

namespace ERP_Service.Shared.Utilities;

public class UploadHelper
{

	public static string ROOT_IMAGE { get => Path.Combine(Directory.GetCurrentDirectory(), "Public\\Images\\"); }
	public static string ROOT_VIDEO { get => Path.Combine(Directory.GetCurrentDirectory(), "Public\\Videos\\"); }

	struct MaxResolution
	{
		public int Width;
		public int Height;
		public MaxResolution(int Width, int Height)
		{
			this.Width = Width;
			this.Height = Height;
		}
	}

	private readonly string[] imageExtensions = new[] { "jpg", "jpeg", "png" };
	private readonly string[] videoExtensions = new[] { "mp4" };
	private const int MAX_VIDEO_DURATION = 60;
	private const double MAX_FILE_SIZE = 3 * 1024 * 1024; // 3Mb
	private readonly MaxResolution _maxResolution = new MaxResolution(1280, 1280);
	private string GetExtension(byte[] bytes)
	{
		if (bytes[0] == 0xFF && bytes[1] == 0xD8 && bytes[2] == 0xFF)
		{
			// JPEG
			return "jpeg";
		}
		else if (bytes[0] == 0x89 && bytes[1] == 0x50 && bytes[2] == 0x4E && bytes[3] == 0x47)
		{
			// PNG
			return "png";
		}
		else if (bytes[0] == 0x47 && bytes[1] == 0x49 && bytes[2] == 0x46 && bytes[3] == 0x38)
		{
			// GIF
			return "gif";
		}
		else if (bytes[0] == 0x42 && bytes[1] == 0x4D)
		{
			// BMP
			return "bmp";
		}
		if (bytes[0] == 0x00 && bytes[1] == 0x00 && bytes[2] == 0x00 && bytes[3] == 0x18)
		{
			// MPEG-4 (MP4)
			return "mp4";
		}
		else
		{
			return "";
		}
	}
	public void DeleteFile(string nameFile)
	{
		string filepath = ROOT_IMAGE + nameFile;

		if (File.Exists(filepath))
		{
			File.Delete(filepath);
		}
	}
	public bool IsImage(byte[] bytes)
	{
		if (bytes == null)
		{
			return false;
		}
		if (bytes.Length <= 1)
		{
			return false;
		}
		var extension = GetExtension(bytes);
		bool isImage = Array.Exists(imageExtensions, e => e.Equals(extension, StringComparison.OrdinalIgnoreCase));
		return isImage && ValidateImage(bytes);
	}
	public async Task<bool> IsVideo(byte[] bytes)
	{
		var extension = GetExtension(bytes);
		bool isVideo = Array.Exists(videoExtensions, e => e.Equals(extension, StringComparison.OrdinalIgnoreCase));
		//return isVideo && (await ValidateVideoAsync(bytes));
		return true;
	}
	public bool ValidateImage(byte[] bytes)
	{
		if (bytes.Length * 4 / 3 > MAX_FILE_SIZE) { return false; }
		return true;
	}
	public async Task<bool> ValidateVideoAsync(byte[] bytes)
	{
		return bytes.Length <= MAX_FILE_SIZE * 40 / 3; // 30Mb
	}
	public async Task<string> UploadVideoAsync(IFormFile file)
	{
		if (!Directory.Exists(ROOT_VIDEO))
		{
			Directory.CreateDirectory(ROOT_VIDEO);
		}
		string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
		string filePath = Path.Combine(ROOT_VIDEO, fileName);
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}
		return fileName;
	}
	public async Task<string> UploadImageAsync(IFormFile file)
	{
		if (!Directory.Exists(ROOT_IMAGE))
		{
			Directory.CreateDirectory(ROOT_IMAGE);
		}
		string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
		string filePath = Path.Combine(ROOT_IMAGE, fileName);
		using (var stream = new FileStream(filePath, FileMode.Create))
		{
			await file.CopyToAsync(stream);
		}
		return fileName;
	}
	public async Task<string> UploadImageWithBytes(byte[] bytes)
	{
		if (!IsImage(bytes)) { throw new ArgumentException("Image does not validate"); }
		if (!Directory.Exists(ROOT_IMAGE))
		{
			Directory.CreateDirectory(ROOT_IMAGE);
		}
		string extention = GetExtension(bytes);
		string uniqueFileName = Guid.NewGuid().ToString() + $".{extention}";
		string filePath = Path.Combine(ROOT_IMAGE, uniqueFileName);
		await File.WriteAllBytesAsync(filePath, bytes);
		return uniqueFileName;
	}
	public async Task<string> UploadVideoWithBytes(byte[] bytes)
	{
		if (!(await IsVideo(bytes))) { throw new ArgumentException("Video does not validate"); }
		if (!Directory.Exists(ROOT_VIDEO))
		{
			Directory.CreateDirectory(ROOT_VIDEO);
		}
		string extention = GetExtension(bytes);
		string uniqueFileName = Guid.NewGuid().ToString() + $".{extention}";
		string filePath = Path.Combine(ROOT_VIDEO, uniqueFileName);
		await File.WriteAllBytesAsync(filePath, bytes);
		return uniqueFileName;
	}
}
