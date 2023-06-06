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
		// Trả về tên file được upload nếu thành công, ngược lại ném Exception chứa thông báo lỗi
		public string UploadFile(IFormFile file, string relativeFolderPath)
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
				throw new Exception("Cannot upload this file name " + file.FileName
					+ ", something went wrong!");
			}
		}
        // Thực hiện upload file với extension tương ứng
        // Trả về tên file được upload nếu thành công
        // Ném exception nếu file extension ko đúng hoặc upload file lỗi
        public string UploadFile(IFormFile audio, string relativeFolderPath, string[] acceptExtensions)
        {
            // Validate file extension
            string fileExtension = Path.GetExtension(audio.FileName);
            if (!acceptExtensions.Contains(fileExtension))
            {
                throw new Exception("Can only upload files with the extension "
                    + string.Join(", ", acceptExtensions));
            }
            string fileName = UploadFile(audio, relativeFolderPath);
            return fileName;
        }

        // Thực hiện upload file AUDIO
        // Trả về tên file được upload nếu thành công
        // Ném exception nếu file extension ko đúng hoặc upload file lỗi
        public string UploadAudio(IFormFile audio, string relativeFolderPath)
		{
			return UploadFile(audio, relativeFolderPath, new string[] { ".mp3", ".wav" });
		}

        // Thực hiện upload file IMAGE
        // Trả về tên file được upload nếu thành công
        // Ném exception nếu file extension ko đúng hoặc upload file lỗi
        public string UploadImage(IFormFile image, string relativeFolderPath)
        {
			return UploadFile(image, relativeFolderPath, new string[] { ".jpg", ".png" });
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
