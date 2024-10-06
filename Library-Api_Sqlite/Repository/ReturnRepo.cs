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

      


    }
}
