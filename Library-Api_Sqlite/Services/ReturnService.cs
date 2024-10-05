using Library_Api_Sqlite.Dto_s.Return_Rec_Dto;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Repository;

namespace Library_Api_Sqlite.Services
{
    public class ReturnService
    {
       private readonly ReturnRepo _repo;

        public ReturnService(ReturnRepo repo)
        {
            _repo = repo;
        }

        public async Task<Return_Recode> ReciveBook(Return_Req_Dto recode)
        {
            var rec = new Return_Recode
            {
                id = recode.id,
                lentId = recode.lentId,
                usernic = recode.usernic,
                isbn = recode.isbn,
                lentcopies = 0,
                returncopies = recode.returncopies,
                returndate = DateTime.Now
            };

            _repo.AddReturnHistory(rec);
            return await _repo.ReciveBook(rec);
        }
    }
}
