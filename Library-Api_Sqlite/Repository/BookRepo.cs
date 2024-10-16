using Library_Api_Sqlite.Dto_s.Book_Dtos;
using Library_Api_Sqlite.EntityModals;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Repository
{
    public class BookRepo
    {
        private readonly string _connectionString;

        public BookRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Book> AddBook(Book book)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
            INSERT INTO Books (Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount) 
            VALUES (@ID, @isbn, @title, @author, @genre, @copies, @aviCopies, @publishYear, @addDateTime, @images, @rentCount)";
                command.Parameters.AddWithValue("@ID", book.Id);
                command.Parameters.AddWithValue("@isbn", book.ISBN);
                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@genre", string.Join(",", book.Genre)); 
                command.Parameters.AddWithValue("@copies", book.Copies);
                command.Parameters.AddWithValue("@aviCopies", book.AviCopies);
                command.Parameters.AddWithValue("@publishYear", book.PublishYear);
                command.Parameters.AddWithValue("@addDateTime", book.AddDateTime);
                command.Parameters.AddWithValue("@images", string.Join(",", book.Images));
                command.Parameters.AddWithValue("@rentCount", book.RentCount);

                command.ExecuteNonQuery();
            }

            return book;
        }



        public async Task<bool> UpdateBook(UpdateBook book ,string ISBN)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText = @"
        UPDATE Books
        SET 
            Title = @title,
            Author = @author,
            Genre = @genre,
            Copies = @copies,
            AviCopies = @aviCopies,
            PublishYear = @publishYear,
            Images = @images
        WHERE ISBN = @ISBN";

                command.Parameters.AddWithValue("@title", book.Title);
                command.Parameters.AddWithValue("@author", book.Author);
                command.Parameters.AddWithValue("@genre", string.Join(",", book.Genre)); 
                command.Parameters.AddWithValue("@copies", book.Copies);
                command.Parameters.AddWithValue("@aviCopies", book.AviCopies);
                command.Parameters.AddWithValue("@publishYear", book.PublishYear);
                command.Parameters.AddWithValue("@images", string.Join(",", book.Images));
                command.Parameters.AddWithValue("@ISBN", ISBN);
              

                var result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }















        public async Task<Book> GetBook(string isbn)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount FROM Books WHERE isbn = @isbn";
                command.Parameters.AddWithValue("@isbn", isbn);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Book
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
                            // Split the comma-separated Images string into a List<string>
                            Images = reader.IsDBNull(9) ? new List<string>() : reader.GetString(9).Split(',').ToList(),

                            RentCount = reader.GetInt32(10)
                        };
                    }
                }
            }
            return null;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            var books = new List<Book>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount
            FROM Books ";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
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
            }


            return books;
        }
        public async Task<bool> DeleteById(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "" +
                    "DELETE FROM Books WHERE Id = @id ";
                command.Parameters.AddWithValue("@id", id);

                var result = await command.ExecuteNonQueryAsync();

                return result > 0;
            }
        }

        public async Task<int> CountTotalBooks()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM Books";

               
                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result);
            }
        }

        public async Task<List<int>> GetAllPublishYears()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT PublishYear FROM Books";

                var publishYears = new List<int>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            publishYears.Add(reader.GetInt32(0));
                        }
                    }
                }

                return publishYears; 
            }
        }
        public async Task<List<string>> GetAllGenres()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT  Genre FROM Books";

                var genres = new List<string>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            var genreField = reader.GetString(0);
                            var genreList = genreField.Split(',');

                            foreach (var genre in genreList)
                            {
                                genres.Add(genre.Trim());
                            }
                        }
                    }
                }

                return genres.ToList();
            }
        }
        public async Task<List<string>> GetAllAuthors()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT Author FROM Books";

                var authors = new List<string>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        authors.Add(reader.GetString(0));
                    }
                }

                return authors;
            }
        }


        public async Task<List<Book>> GetBooksByPublishYear(int publishYear)
        {
            Console.WriteLine(publishYear);
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount FROM Books WHERE PublishYear = @publishYear";
                command.Parameters.AddWithValue("@publishYear", publishYear);


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


        public async Task<List<Book>> GetBooksByGenre(string genre)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount FROM Books WHERE Genre LIKE @genre";
                command.Parameters.AddWithValue("@genre", "%" + genre + "%");

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

        public async Task<List<Book>> GetBooksOrderedByPublishYear(bool ascending)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();

                command.CommandText = $"SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount FROM Books ORDER BY PublishYear {(ascending ? "ASC" : "DESC")}";

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

        public async Task<List<Book>> SearchBooksByTitle(string title)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, ISBN, Title, Author, Genre, Copies, AviCopies, PublishYear, AddDateTime, Images, RentCount FROM Books WHERE Title LIKE @title";
                command.Parameters.AddWithValue("@title", "%" + title + "%");

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

        public async Task<IEnumerable<Book>> Categorization(string? genre, string? author, int? publishYear)
        {
            List<Book> books = new List<Book>();
            string sqlQuery = "SELECT * FROM Books WHERE 1=1";
            if (!string.IsNullOrEmpty(genre))
            {
                sqlQuery += " AND Genre LIKE @genre";
            }
            if (!string.IsNullOrEmpty(author))
            {
                sqlQuery += " AND Author LIKE @author";
            }
            if (publishYear.HasValue)
            {
                sqlQuery += " AND PublishYear = @publishYear";
            }

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqliteCommand(sqlQuery, connection))
                {
                    if (!string.IsNullOrEmpty(genre))
                    {
                        command.Parameters.AddWithValue("@genre", $"%{genre}%");
                    }
                    if (!string.IsNullOrEmpty(author))
                    {
                        command.Parameters.AddWithValue("@author", $"%{author}%");
                    }
                    if (publishYear.HasValue)
                    {
                        command.Parameters.AddWithValue("@publishYear", publishYear.Value);
                    }


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
                }
            }

            return books;
        }






        public async Task updatecopies(int copies ,string isbn)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"UPDATE books SET AviCopies = @copies WHERE isbn = @isbn";
                command.Parameters.AddWithValue("@copies", copies);
                command.Parameters.AddWithValue("@isbn", isbn);
                command.ExecuteNonQuery();
            }
        }



       

       


    }
}
