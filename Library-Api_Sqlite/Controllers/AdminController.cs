using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly string _connectionString;

        public AdminController(string connectionString)
        {
            _connectionString = connectionString;
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateAdmin([FromBody] AdminLoginRequest request)
        {
            
            using (var connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = @"SELECT COUNT(1) FROM ADMINS WHERE USERID = @UserID AND password = @Password";
                    command.Parameters.AddWithValue("@UserID", request.UserID);
                    command.Parameters.AddWithValue("@Password", request.Password);

                var result = (long)await command.ExecuteScalarAsync();

                if (result >0)
                    {
                        return Ok(true);
                }
                else
                {
                    return Ok(false);
                }  
            }
        }
    }

   
    public class AdminLoginRequest
    {
        public string UserID { get; set; }
        public string Password { get; set; }
    }

}
