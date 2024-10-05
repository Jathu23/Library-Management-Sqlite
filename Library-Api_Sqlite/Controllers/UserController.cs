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

        [HttpPut("AddUser")]
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
    }
}
