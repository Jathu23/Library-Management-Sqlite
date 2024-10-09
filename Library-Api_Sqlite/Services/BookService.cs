using Library_Api_Sqlite.Dto_s;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.FileSaver;
using Library_Api_Sqlite.Repository;

using Microsoft.AspNetCore.Mvc;

namespace Library_Api_Sqlite.Services
{
    public class BookService
    {
        private readonly BookRepo _bookRepo;
        private readonly SaveToRoot _saveToRoot;

        public BookService(BookRepo bookRepo, SaveToRoot saveToRoot)
        {
            _bookRepo = bookRepo;
            _saveToRoot = saveToRoot;
        }

        public async Task<Book> AddBook(Book_Req_Dto Reqbook)
        {
            var images = Reqbook.Images;
            var imagePaths = await _saveToRoot.SaveImages(images, "bookimages");

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

        public async Task<Book> GetBook(string isbn)
        {
            var data = await _bookRepo.GetBook(isbn);
            if (data != null)
            {
                return data;
            }
            else
            {
                throw new Exception("no book!__");
            }

        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepo.GetAllBooks();
        }
        

        public async Task<bool> DeleteById(int id)
        {
            return await _bookRepo.DeleteById(id);  
        }

        public async Task<int> CountTotalBooks()
        {
            return await _bookRepo.CountTotalBooks();
        }

        public async Task<List<int>> GetAllPublishYears()
        {
            return await _bookRepo.GetAllPublishYears();
        }
        public async Task<List<string>> GetAllGenres()
        {
            return await _bookRepo.GetAllGenres();
        }
        public async Task<List<string>> GetAllAuthors()
        {
            return await _bookRepo.GetAllAuthors();
        }




        public async Task<List<Book>> GetBooksByGenre(string genre)
        {
            return await _bookRepo.GetBooksByGenre(genre);
        }

        public async Task<List<Book>> GetBooksByPublishYear(int publishYear)
        {
            return await _bookRepo.GetBooksByPublishYear(publishYear);
        }

        public async Task<List<Book>> GetBooksOrderedByPublishYear(bool ascending)
        {
            return await _bookRepo.GetBooksOrderedByPublishYear(ascending);
        }

        public async Task<List<Book>> SearchBooksByTitle(string title)
        {
            return await _bookRepo.SearchBooksByTitle(title);
        }

      

















    }

}
