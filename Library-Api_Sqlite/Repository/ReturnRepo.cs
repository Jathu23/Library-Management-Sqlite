using Library_Api_Sqlite.EntityModals;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Repository
{
    public class ReturnRepo
    {
        private readonly string _connectionString;

        public ReturnRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Return_Recode> ReciveBook(Return_Recode recode)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
   INSERT INTO ReturnRecords (Id, UserNIC, LentId, ISBN, LentCopies, ReturnCopies, ReturnDate) 
   VALUES (@id, @usernic, @lentId, @isbn, @lentcopies, @returncopies, @returndate)";
                command.Parameters.AddWithValue("@id", recode.id);
                command.Parameters.AddWithValue("@usernic", recode.usernic);
                command.Parameters.AddWithValue("@lentId", recode.lentId);
                command.Parameters.AddWithValue("@isbn", recode.isbn);
                command.Parameters.AddWithValue("@lentcopies", recode.lentcopies);
                command.Parameters.AddWithValue("@returncopies", recode.returncopies);
                command.Parameters.AddWithValue("@returndate", recode.returndate);

                command.ExecuteNonQuery();



            }
            return recode;
        }
        public async Task AddReturnHistory(Return_Recode recode)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
   INSERT INTO ReturnHistory (Id, UserNIC, LentId, ISBN, LentCopies, ReturnCopies, ReturnDate) 
   VALUES (@id, @usernic, @lentId, @isbn, @lentcopies, @returncopies, @returndate)";
                command.Parameters.AddWithValue("@id", recode.id);
                command.Parameters.AddWithValue("@usernic", recode.usernic);
                command.Parameters.AddWithValue("@lentId", recode.lentId);
                command.Parameters.AddWithValue("@isbn", recode.isbn);
                command.Parameters.AddWithValue("@lentcopies", recode.lentcopies);
                command.Parameters.AddWithValue("@returncopies", recode.returncopies);
                command.Parameters.AddWithValue("@returndate", recode.returndate);

                command.ExecuteNonQuery();



            }
          
        }
        public async Task<IEnumerable<Return_Recode>> GetAllReturnRecords()
        {
            var returnRecords = new List<Return_Recode>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT id, usernic, lentId, isbn, lentcopies, returncopies, returndate
            FROM ReturnRecords"; // Adjust the table name as needed

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var returnRecord = new Return_Recode
                        {
                            id = reader.GetInt32(0),
                            usernic = reader.GetInt32(1),
                            lentId = reader.GetInt32(2),
                            isbn = reader.GetString(3),
                            lentcopies = reader.GetInt32(4),
                            returncopies = reader.GetInt32(5),
                            returndate = reader.GetDateTime(6)
                        };

                        returnRecords.Add(returnRecord);
                    }
                }
            }

            return returnRecords;
        }

        public async Task<IEnumerable<Return_Recode>> GetRecordsby_nic(int nic)
        {
            var returnRecords = new List<Return_Recode>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
            SELECT id, usernic, lentId, isbn, lentcopies, returncopies, returndate
            FROM ReturnRecords where usernic = @nic"; 
                command.Parameters.AddWithValue("@nic", nic);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var returnRecord = new Return_Recode
                        {
                            id = reader.GetInt32(0),
                            usernic = reader.GetInt32(1),
                            lentId = reader.GetInt32(2),
                            isbn = reader.GetString(3),
                            lentcopies = reader.GetInt32(4),
                            returncopies = reader.GetInt32(5),
                            returndate = reader.GetDateTime(6)
                        };

                        returnRecords.Add(returnRecord);
                    }
                }
            }

            return returnRecords;
        }
        public async Task<int> CountTotalReturnBooks()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM ReturnRecords";


                var result = await command.ExecuteScalarAsync();

                return Convert.ToInt32(result);
            }
        }





    }
}
