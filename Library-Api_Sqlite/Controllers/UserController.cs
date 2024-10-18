using Library_Api_Sqlite.Dto_s.Book_Dtos;
using Library_Api_Sqlite.Dto_s.User_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _services;

        public UserController(UserServices services)
        {
            _services = services;
        }

        [HttpPost("AddUser")]
        public async Task<User> Add(User_Req_Dto Requser)
        {
            return await _services.Add(Requser);
        }
        [HttpDelete("Remove")]
        public async Task<bool> RemoveUser(int id)
        {
            return await _services.RemoveUser(id);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _services.GetAll();
        }

        [HttpGet("GetByUser")]
        public async Task<User> Getuser(int nic)
        {
        return await _services.Getuser(nic);
        }

        [HttpPut("Updateuser")]
        public async Task<IActionResult> UpdateUser(int nic, User_Update_Dto reqUser)
        {
            try
            {
                var result = await _services.UpdateUser(nic, reqUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet("ValidateUser")]
        public async Task<IActionResult> ValidateUser( [FromQuery] int nic , [FromQuery] string password)
        {
            try
            {
                User_Req_Dto user = new User_Req_Dto
                {
                    password = password
                };

                bool isValid = await _services.ValidDateUser(user, nic);

               return Ok(isValid);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }
        [HttpGet("CountTotalUsers")]
        public async Task<IActionResult> CountTotalUsers()
        {
            var count = await _services.CountTotalUsers();
            return Ok(count);
        }



    }
}
