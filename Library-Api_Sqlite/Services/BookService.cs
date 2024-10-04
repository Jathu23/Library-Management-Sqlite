using Library_Api_Sqlite.Dto_s;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Repository;
using LMS.rest_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api_Sqlite.Services
{
    public class BookService
    {
        private readonly BookRepo _bookRepo;
        private readonly IWebHostEnvironment _environment; 
       
        public BookService(BookRepo bookRepo, IWebHostEnvironment environment)
        {
            _bookRepo = bookRepo;
            _environment = environment;
        }

       
        public async Task<Book> AddBook(Book_Req_Dto Reqbook)
        {
            var images = Reqbook.Images;
            var imagePaths = await SaveImages(images);

            Book book = new Book
            {
                Id = Reqbook.Id,
                ISBN = Reqbook.ISBN,
                Title = Reqbook.Title,
                Author = Reqbook.Author,
                Genre = Reqbook.Genre,
                Copies = Reqbook.Copies,
                AviCopies = Reqbook.Copies,
                PublishYear = Reqbook.PublishYear,
                AddDateTime = DateTime.Now,
                Images = imagePaths,
                RentCount = 0

            };

          var data = await _bookRepo.AddBook(book);

         return data;

       
        }

        public async Task<Book> GetBook(int id)
        {
            return await _bookRepo.GetBook(id);
        }

        public async Task<bool> DeleteById(int id)
        {
            return await _bookRepo.DeleteById(id);  
        }

        public async Task<int> CountTotalBooks()
        {
            return await _bookRepo.CountTotalBooks();
        }

        public async Task<List<Book>> GetBooksByPublishYear(int publishYear)
        {
            return await GetBooksByPublishYear(publishYear);
        }

        public async Task<List<string>> GetBooksByGenre(string genre)
        {
            return await GetBooksByGenre(genre);
        }

        public async Task<List<Book>> GetBooksOrderedByPublishYear(bool ascending)
        {
            return await GetBooksOrderedByPublishYear(ascending);
        }

        public async Task<List<string>> GetAllAuthors()
        {
            return await _bookRepo.GetAllAuthors();
        }

        public async Task<List<Book>> SearchBooksByTitle(string title)
        {
            return await _bookRepo.SearchBooksByTitle(title);   
        }

        public async Task<List<string>> GetAllGenres()
        {
            return await _bookRepo.GetAllGenres();
        }

        public async Task<List<int>> GetAllPublishYears()
        {
            return await _bookRepo.GetAllPublishYears();
        }















        public async Task<List<string>> SaveImages(List<IFormFile> imageFiles)
        {
            if (imageFiles == null || imageFiles.Count == 0)
            {
                throw new ArgumentException("No files uploaded.");
            }

           
            var imagePaths = new List<string>();

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");

           
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

                    var imagePath = Path.Combine("images", uniqueFileName).Replace("\\", "/");

                    imagePaths.Add(imagePath);
                }
            }

            return imagePaths;
        }
    }

}
