using Library_Api_Sqlite.Dto_s;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Services;
using LMS.rest_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly string _connectionString;
        BookService _bookservics;

        public BookController(string connectionString, BookService bookservics)
        {
            _connectionString = connectionString;
            _bookservics = bookservics;
        }


        [HttpPost ("Addnewbook")]
        public async Task<IActionResult> Add(Book_Req_Dto book)
        {
            var mainBook = await _bookservics.AddBook(book);
            if (mainBook == null)
            {
                return BadRequest("Unable to add the book.");
            }
            return Ok(mainBook);
        }

        [HttpGet("GetAllbook")]
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookservics.GetAllBooks();
        }

        [HttpGet("GetBook")]
        public async Task<Book> GetBooK(int id)
        {
            return await _bookservics.GetBook(id);
        }

        [HttpDelete ("deleteBook")]
        public async Task<bool> DeleteById(int id)
        {
            return await _bookservics.DeleteById(id);
        }

        [HttpGet("CountTotalBooks")]
        public async Task<int> CountTotalBooks()
        {
            return await _bookservics.CountTotalBooks();
        }


        [HttpGet("GetAllPublishYears")]
        public async Task<List<int>> GetAllPublishYears()
        {
            return await _bookservics.GetAllPublishYears();
        }

        [HttpGet("GetAllGenres")]
        public async Task<List<string>> GetAllGenres()
        {
            return await _bookservics.GetAllGenres();
        }

        [HttpGet("GetAllAuthors")]
        public async Task<List<string>> GetAllAuthors()
        {
            return await _bookservics.GetAllAuthors();
        }

        [HttpGet("GetBooksByPublishYear")]
        public async Task<List<Book>> GetBooksByPublishYear(int publishYear)
        {
            return await GetBooksByPublishYear(publishYear);
        }

        [HttpGet("GetBooksByGenre")]
        public async Task<List<string>> GetBooksByGenre(string genre)
        {
            return await GetBooksByGenre(genre);
        }

        [HttpGet("GetBooksOrderedByPublishYear")]
        public async Task<List<Book>> GetBooksOrderedByPublishYear(bool ascending)
        {
            return await GetBooksOrderedByPublishYear(ascending);
        }



        [HttpGet("SearchBooksByTitle")]
        public async Task<List<Book>> SearchBooksByTitle(string title)
        {
            return await SearchBooksByTitle(title);
        }

       










    }
}

