using Library_Api_Sqlite.Dto_s.Return_Rec_Dto;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : ControllerBase
    {
        private readonly ReturnService _returnService;

        public ReturnController(ReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpPost ("ReciveBook")]
        public async Task<Return_Recode> ReciveBook(Return_Req_Dto recode)
        {
            return await _returnService.ReciveBook(recode);
        }
    }
}
