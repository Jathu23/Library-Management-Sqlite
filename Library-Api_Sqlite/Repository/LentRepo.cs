﻿using Library_Api_Sqlite.EntityModals;
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

    }

}
