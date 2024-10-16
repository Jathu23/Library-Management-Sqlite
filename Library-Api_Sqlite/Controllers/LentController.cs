using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Api_Sqlite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LentController : ControllerBase
    {
        private readonly LentService _lentService;

        public LentController(LentService lentService)
        {
            _lentService = lentService;
        }

        [HttpPost ("Add")]
        public async Task<LentRecode> Add(Lent_Req_Dto recode)
        {
            return await _lentService.Add (recode);
        }


        [HttpGet("Get All Lend Recods")]
        public async Task<IActionResult> GetAllLendRecords()
        {
            var lend= await _lentService.GetAllLendRecords();
            return Ok(lend);
        }
    }

}
