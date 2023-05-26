namespace PBL3.Infrastructures
{
	public class FileService
	{
		private readonly IWebHostEnvironment environment;
		public FileService(IWebHostEnvironment env)
		{
			this.environment = env;
		}

		// Upload file vào đường dẫn tương ứng
		// Trả về tên file được upload nếu thành công, ngược lại trả về null
		public string? UploadFile(IFormFile file, string relativeFolderPath)
		{
			try
			{
				string wwwPath = this.environment.WebRootPath;
				string folderPath = Path.Combine(wwwPath, relativeFolderPath);
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
				string absoluteFilePath = Path.Combine(folderPath, uniqueFileName);
				using (var fileStream = new FileStream(absoluteFilePath, FileMode.Create))
				{
					file.CopyTo(fileStream);
				}
				return uniqueFileName;
			}
			catch (Exception) 
			{
				return null;
			}
		}

		// Thực hiện xóa file có đương dẫn tương ứng
		// Trả về true nếu xóa file thành công
		// Trả về false nếu xóa file thất bại hoặc file không tồn tại
		public bool DeleteFile(string relativeFilePath)
		{
			try
			{
				string wwwPath = this.environment.WebRootPath;
				string absolutePath = Path.Combine(wwwPath, relativeFilePath);
				if (System.IO.File.Exists(absolutePath))
				{
					System.IO.File.Delete(absolutePath);
					return true;
				}
				return false;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
