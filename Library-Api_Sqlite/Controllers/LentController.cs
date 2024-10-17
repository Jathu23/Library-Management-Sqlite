using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.Dto_s.Notifaction_Dtos;
using Library_Api_Sqlite.Dto_s.Reports_Dtos;
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


        [HttpGet("GetAllLendRecods")]
        public async Task<IActionResult> GetAllLendRecords()
        {
            var lend= await _lentService.GetAllLendRecords();
            return Ok(lend);
        }

        [HttpGet("GetRecordsby_Nic")]
        public async Task<IActionResult> GetRecordsby_Nic(int nic)
        {
            var lend = await _lentService.GetRecordsby_Nic(nic);
            return Ok(lend);
        }

        [HttpGet("findoverdueAll")]
        public async Task<IActionResult> findoverdueAll()
        {
            var data = await _lentService.findoverdueAll();
            return Ok(data);
        }
        [HttpGet("findoverdue_user")]
        public async Task<IActionResult> findoverdue_user(int nic)
        {
            if (nic == null)
            {
                return BadRequest("nic invalid");
            }
            var data = await _lentService.findoverdue_user(nic);
            return Ok(data);
        }

        [HttpGet("Getlentbooks_nic")]
        public async Task<IActionResult> Getlentbooks(int nic)
        {
            var data = await _lentService.GetLentBooks(nic);
            return Ok(data);
        }

        [HttpGet("R_getuserandlentbooks")]
        public async Task<IActionResult> R_getuserandlentbooks()
        {
            var data = await _lentService.R_getuserandlentbooks();
            return Ok(data);
        }
    }

}
