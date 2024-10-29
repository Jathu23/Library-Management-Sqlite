using Library_Api_Sqlite.Dto_s;
using Library_Api_Sqlite.Dto_s.Book_Dtos;
using Library_Api_Sqlite.Dto_s.Reports_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.FileService;
using Library_Api_Sqlite.Repository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Services
{
    public class BookService
    {
        private readonly BookRepo _bookRepo;
        private readonly RootOprations _Rootoprations;

        public BookService(BookRepo bookRepo, RootOprations rootoprations)
        {
            _bookRepo = bookRepo;
            _Rootoprations = rootoprations;
        }

        public async Task<Book> AddBook(Book_Req_Dto Reqbook)
        {
            List<string> imagePaths;


            if (Reqbook.Images == null)
            {
                imagePaths = new List<string> { "bookimages/defaultimg.jpg" };
            }
            else
            {
                var images = Reqbook.Images;

                imagePaths = await _Rootoprations.SaveImages(images, "bookimages");
            }

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

        public async Task<bool> UpdateBook(string isbn, Book_Update_Dto Reqbook )
        {
            List<string> imgpaths = new List<string>();

          
            var oldbook = await _bookRepo.GetBook(isbn);

            int newavicopies = oldbook.AviCopies+(oldbook.Copies - Reqbook.Copies);

            if (Reqbook.Images == null)
            {
                imgpaths = oldbook.Images;
            }
            else
            {
                imgpaths = await _Rootoprations.SaveImages(Reqbook.Images, "bookimages");

                 _Rootoprations.DeleteImages(oldbook.Images, "bookimages");
            }


            var uBook = new UpdateBook
            {
                Title = Reqbook.Title,
                Author = Reqbook.Author,
                Genre= Reqbook.Genre,
                Copies= Reqbook.Copies,
                AviCopies= newavicopies,
                PublishYear = Reqbook.PublishYear,
                Images = imgpaths
            };

           return await _bookRepo.UpdateBook(uBook, isbn);
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
        

        public async Task<bool> DeleteById(string id)
        {
             var oldbook = await _bookRepo.GetBook(id);
            _Rootoprations.DeleteImages(oldbook.Images, "bookimages");
            return await _bookRepo.DeleteById(id);  
        }

        public async Task<int> CountTotalBooks()
        {
            //var imagePaths = new List<string> { "bookimages/40533406-0348-45ba-b80a-c4c4868347fbth.jpg" };
            //_Rootoprations.DeleteImages(imagePaths, "bookimages");

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

        public async Task<List<Book>> GetByAuthor(string author)
        {
            return await _bookRepo.GetByAuthor(author);
        }

        public async Task<IEnumerable<Book>> Categorization(string? genre, string? author, int? publishYear)
        {
            return await _bookRepo.Categorization(genre, author, publishYear);
        }


        public async Task<IEnumerable<Book_report_Dto>> BookReport()
        {
            var books = await _bookRepo.GetAllBooks();
            var repots = new List<Book_report_Dto>();
            foreach (var book in books)
            {
                repots.Add(new Book_report_Dto(book.ISBN,book.Title,book.Copies,book.AviCopies,book.RentCount));
            }
            return repots;
        }


        public async Task<List<Book>> GetPopularBooks(int limit)
        {
            if (limit > 0)
            {
                var books = await _bookRepo.GetPopularBooks(limit);
                return books;
            }
            else
            {
                throw new Exception("limit is grater than 0");
            }

        }













    }

}
