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

    }
}
