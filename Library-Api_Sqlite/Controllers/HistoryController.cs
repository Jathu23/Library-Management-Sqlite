using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.Dto_s.Return_Rec_Dto;
using Library_Api_Sqlite.EntityModals;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly string _connectionString;

        public HistoryController(string connectionString)
        {
            _connectionString = connectionString;
        }

        [HttpGet ("Lent")]
        public async Task<IEnumerable<LentRecode>> GetAllLentHistory()
        {
            var lentHistoryRecords = new List<LentRecode>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
        SELECT Id, ISBN, UserNIC, LentDate, ReturnDate, Copies
        FROM LentHistory";

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var lentHistory = new LentRecode
                        {
                            id = reader.GetInt32(0),
                            isbn = reader.GetString(1),
                            usernic = reader.GetInt32(2),
                            lentDate = reader.GetDateTime(3),
                            ReturnDate = reader.GetDateTime(4),
                            copies = reader.GetInt32(5),
                        };
                        lentHistoryRecords.Add(lentHistory);
                    }
                }
            }

            return lentHistoryRecords;
        }

        [HttpGet("LentByUserNIC")]
        public async Task<IEnumerable<LentBookDetails>> GetByUserNIC(int usernic)
        {
            var lentBookDetails = new List<LentBookDetails>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
        SELECT LentHistory.Id, LentHistory.ISBN, Books.Title, LentHistory.UserNIC, 
               LentHistory.LentDate, LentHistory.ReturnDate, LentHistory.Copies
        FROM LentHistory
        JOIN Books ON LentHistory.ISBN = Books.ISBN
        WHERE LentHistory.UserNIC = @usernic";

                command.Parameters.AddWithValue("@usernic", usernic);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var lentBookDetail = new LentBookDetails
                        {
                            Id = reader.GetInt32(0),
                            ISBN = reader.GetString(1),
                            Title = reader.GetString(2),
                            UserNIC = reader.GetInt32(3),
                            LentDate = reader.GetDateTime(4),
                            ReturnDate = reader.GetDateTime(5),
                            Copies = reader.GetInt32(6),
                        };
                        lentBookDetails.Add(lentBookDetail);
                    }
                }
            }

            return lentBookDetails;
        }


        [HttpGet("Return")]
        public async Task<IEnumerable<Return_Recode>> GetAllReturnRecords()
        {
            var returnRecords = new List<Return_Recode>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
        SELECT Id, UserNIC, LentId, ISBN, LentCopies, ReturnCopies, ReturnDate
        FROM ReturnRecords";

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

        [HttpGet("ReturnByUserNIC")]
        public async Task<IEnumerable<ReturnBookDetails>> GetReturnRecordsWithBookDetailsByUserNIC(int usernic)
        {
            var returnBookDetails = new List<ReturnBookDetails>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"
        SELECT ReturnRecords.Id, ReturnRecords.UserNIC, ReturnRecords.LentId, ReturnRecords.ISBN, 
               Books.Title, ReturnRecords.LentCopies, ReturnRecords.ReturnCopies, ReturnRecords.ReturnDate
        FROM ReturnRecords
        JOIN Books ON ReturnRecords.ISBN = Books.ISBN
        WHERE ReturnRecords.UserNIC = @usernic";

                command.Parameters.AddWithValue("@usernic", usernic);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var returnBookDetail = new ReturnBookDetails
                        {
                            Id = reader.GetInt32(0),
                            UserNIC = reader.GetInt32(1),
                            LentId = reader.GetInt32(2),
                            ISBN = reader.GetString(3),
                            Title = reader.GetString(4),
                            LentCopies = reader.GetInt32(5),
                            ReturnCopies = reader.GetInt32(6),
                            ReturnDate = reader.GetDateTime(7)
                        };
                        returnBookDetails.Add(returnBookDetail);
                    }
                }
            }

            return returnBookDetails;
        }


    }
}
