namespace Library_Api_Sqlite.FileSaver
{
    public class SaveToRoot
    {
        private readonly IWebHostEnvironment _environment;

        public SaveToRoot(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<string>> SaveImages(List<IFormFile> imageFiles,string folderName)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                throw new ArgumentException("No files uploaded.");
            }


            var imagePaths = new List<string>();

            var uploadsFolder = Path.Combine(_environment.WebRootPath, folderName);


            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }


            foreach (var imageFile in imageFiles)
            {
                if (imageFile.Length > 0)
                {

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetFileName(imageFile.FileName);

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }

                    var imagePath = Path.Combine(folderName, uniqueFileName).Replace("\\", "/");

                    imagePaths.Add(imagePath);
                }
            }

            return imagePaths;
        }
    }
}
