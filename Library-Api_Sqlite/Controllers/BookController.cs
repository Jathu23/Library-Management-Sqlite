using Library_Api_Sqlite.Dto_s;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookservics;
        private readonly string _connectionString;

        public BookController(BookService bookservics, string connectionString)
        {
            _bookservics = bookservics;
            _connectionString = connectionString;
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

        [HttpGet ("GetByAuthor")]
        public async Task<List<Book>> GetByAuthor(string author)
        {
           

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount FROM Books WHERE Author LIKE @genre";
                command.Parameters.AddWithValue("@genre", "%" + author + "%");
                var books = new List<Book>();
                using (var reader = await command.ExecuteReaderAsync())
                {
                  
                    while (await reader.ReadAsync())
                    {
                        var book = new Book
                        {
                            Id = reader.GetInt32(0),
                            ISBN = reader.GetString(1),
                            Title = reader.GetString(2),
                            Author = reader.GetString(3),
                            Genre = reader.IsDBNull(4) ? new List<string>() : reader.GetString(4).Split(',').ToList(),
                            Copies = reader.GetInt32(5),
                            AviCopies = reader.GetInt32(6),
                            PublishYear = reader.GetInt32(7),
                            AddDateTime = DateTime.Parse(reader.GetString(8)),
                            Images = reader.IsDBNull(9) ? new List<string>() : reader.GetString(9).Split(',').ToList(),
                            RentCount = reader.GetInt32(10)
                        };

                        books.Add(book);
                    }
                }
                return books;
            }
        }
    }
}
