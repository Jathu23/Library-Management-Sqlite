using Library_Api_Sqlite.Dto_s;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookservics;

        public BookController(BookService bookservics)
        {
            _bookservics = bookservics;
        }

        [HttpPost("AddNewBook")]
        public async Task<IActionResult> Add(Book_Req_Dto book)
        {
            try
            {
                var mainBook = await _bookservics.AddBook(book);
                return CreatedAtAction(nameof(GetBooK), new { isbn = mainBook.ISBN }, mainBook);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookservics.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("GetBook")]
        public async Task<IActionResult> GetBooK(string isbn)
        {
            try
            {
                var book = await _bookservics.GetBook(isbn);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var deleted = await _bookservics.DeleteById(id);
            if (!deleted)
            {
                return NotFound(new { message = "Book not found!" });
            }
            return NoContent();
        }

        [HttpGet("CountTotalBooks")]
        public async Task<IActionResult> CountTotalBooks()
        {
            var count = await _bookservics.CountTotalBooks();
            return Ok(count);
        }

        [HttpGet("GetAllPublishYears")]
        public async Task<IActionResult> GetAllPublishYears()
        {
            var years = await _bookservics.GetAllPublishYears();
            return Ok(years);
        }

        [HttpGet("GetAllGenres")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _bookservics.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _bookservics.GetAllAuthors();
            return Ok(authors);
        }

        [HttpGet("GetByPublishYear")]
        public async Task<IActionResult> GetBooksByPublishYear(int year)
        {
            var books = await _bookservics.GetBooksByPublishYear(year);
            return Ok(books);
        }

        [HttpGet("GetByGenre")]
        public async Task<IActionResult> GetBooksByGenre(string genre)
        {
            var books = await _bookservics.GetBooksByGenre(genre);
            return Ok(books);
        }

        [HttpGet("SearchByTitle")]
        public async Task<IActionResult> SearchBooksByTitle(string title)
        {
            var books = await _bookservics.SearchBooksByTitle(title);
            return Ok(books);
        }

        [HttpGet("GetOrderedByPublishYear")]
        public async Task<IActionResult> GetBooksOrderedByPublishYear(bool ascending = true)
        {
            var books = await _bookservics.GetBooksOrderedByPublishYear(ascending);
            return Ok(books);
        }
    }
}
