using Library_Api_Sqlite.EntityModals;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Repository
{
    public class LentRepo
    {
        private readonly string _connectionString;

        public LentRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<LentRecode> Add(LentRecode lentRecode)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
              var command = connection.CreateCommand();
                command.CommandText = @"
   INSERT INTO LentRecords (Id, ISBN, UserNIC, Copies, LentDate, ReturnDate) 
   VALUES (@id, @isbn, @usernic, @copies, @lentDate, @returnDate)";
                command.Parameters.AddWithValue("@id", lentRecode.id);
                command.Parameters.AddWithValue("@isbn", lentRecode.isbn);
                command.Parameters.AddWithValue("@usernic", lentRecode.usernic);
                command.Parameters.AddWithValue("@copies", lentRecode.copies);
                command.Parameters.AddWithValue("@lentDate", lentRecode.lentDate);
                command.Parameters.AddWithValue("@returnDate", lentRecode.ReturnDate);

                command.ExecuteNonQuery();


            }
            return lentRecode;
        }
        public async void AddlentHistory(LentRecode lentRecode)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
   INSERT INTO LentHistory (Id, ISBN, UserNIC, Copies, LentDate, ReturnDate) 
   VALUES (@id, @isbn, @usernic, @copies, @lentDate, @returnDate)";
                command.Parameters.AddWithValue("@id", lentRecode.id);
                command.Parameters.AddWithValue("@isbn", lentRecode.isbn);
                command.Parameters.AddWithValue("@usernic", lentRecode.usernic);
                command.Parameters.AddWithValue("@copies", lentRecode.copies);
                command.Parameters.AddWithValue("@lentDate", lentRecode.lentDate);
                command.Parameters.AddWithValue("@returnDate", lentRecode.ReturnDate);

                command.ExecuteNonQuery();


            }
        }

        public async Task<LentRecode> GetRecode(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"SELECT Id, ISBN, UserNIC, Copies, LentDate, ReturnDate 
                                FROM LentRecords WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var lentRecode = new LentRecode
                        {
                            id = reader.GetInt32(0),           
                            isbn = reader.GetString(1),         
                            usernic = reader.GetInt32(2),      
                            copies = reader.GetInt32(3),        
                            lentDate = reader.GetDateTime(4), 
                            ReturnDate = reader.GetDateTime(5)  
                        };

                        return lentRecode;
                    }
                    
                }
            }
            return null;

        }

        public async Task DeleteRecode(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = @"DELETE FROM LentRecords WHERE ID = @id";
                command.Parameters.AddWithValue("@id", id);

                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<LentRecode>> GetAllLendRecords()
        {
            var lendRecords = new List<LentRecode>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT id, isbn, usernic, lentDate, ReturnDate,Copies
            FROM LentRecords"; 

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var lendRecord = new LentRecode
                        {
                            id = reader.GetInt32(0),
                            isbn = reader.GetString(1),
                            usernic = reader.GetInt32(2),
                            lentDate = reader.GetDateTime(3),
                            ReturnDate = reader.GetDateTime(4),
                            copies = reader.GetInt32(5),


                        };
                        lendRecords.Add(lendRecord);
                    }
                }
            }

            return lendRecords;
        }






        public async Task<IEnumerable<LentRecode>> GetRecordsby_Nic(int nic)
        {
            var lendRecords = new List<LentRecode>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT id, isbn, usernic, lentDate, ReturnDate,Copies
            FROM LentRecords where usernic = @nic"; 

                command.Parameters.AddWithValue("@nic", nic);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var lendRecord = new LentRecode
                        {
                            id = reader.GetInt32(0),
                            isbn = reader.GetString(1),
                            usernic = reader.GetInt32(2),
                            lentDate = reader.GetDateTime(3),
                            ReturnDate = reader.GetDateTime(4),
                            copies = reader.GetInt32(5),


                        };
                        lendRecords.Add(lendRecord);
                    }
                }
            }

            return lendRecords;
        }

        public async Task<int> CountTotalLendBooks()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM LentRecords";


                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result);
            }
        }

        public async Task<List<UserLentBook>> GetUserLentBooks_R()
        {
            var userLentBooks = new List<UserLentBook>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT Users.NIC, Users.fullName, Books.Title 
            FROM LentRecords 
            JOIN Users ON LentRecords.UserNIC = Users.NIC 
            JOIN Books ON LentRecords.ISBN = Books.ISBN";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var userNIC = reader.GetInt32(reader.GetOrdinal("NIC"));
                        var userFullName = reader.GetString(reader.GetOrdinal("fullName"));
                        var bookTitle = reader.GetString(reader.GetOrdinal("Title"));

                        // Check if the user already exists in the list
                        var existingUser = userLentBooks.FirstOrDefault(ulb => ulb.NIC == userNIC);

                        if (existingUser != null)
                        {
                            // Use HashSet to avoid duplicate titles
                            if (!existingUser.BookTitles.Contains(bookTitle))
                            {
                                existingUser.BookTitles.Add(bookTitle);
                            }
                        }
                        else
                        {
                            // If user doesn't exist, create a new UserLentBook object and add it to the list
                            var userLentBook = new UserLentBook
                            {
                                NIC = userNIC,
                                FullName = userFullName,
                                BookTitles = new List<string> { bookTitle }
                            };
                            userLentBooks.Add(userLentBook);
                        }
                    }
                }
            }

            return userLentBooks;
        }


        public class UserLentBook
        {
            public int NIC { get; set; }             
            public string FullName { get; set; }      
            public List<string> BookTitles { get; set; } 
            public UserLentBook()
            {
                BookTitles = new List<string>();  
            }
        }



    }

}
