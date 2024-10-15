namespace Library_Api_Sqlite.FileService
{
    public class RootOprations
    {
        private readonly IWebHostEnvironment _environment;

        public RootOprations(IWebHostEnvironment environment)
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

        public void DeleteImages(List<string> imagePaths, string folderName)
        {
            if (imagePaths == null || imagePaths.Count == 0)
            {
                throw new ArgumentException("No image paths provided.");
            }

         
            var uploadsFolder = _environment.WebRootPath;

            foreach (var imagePath in imagePaths)
            {
              
                string fullPath = Path.Combine(uploadsFolder, imagePath);

               
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                else
                {
                   
                    Console.WriteLine($"File not found: {fullPath}");
                }
            }
        }


    }
}
