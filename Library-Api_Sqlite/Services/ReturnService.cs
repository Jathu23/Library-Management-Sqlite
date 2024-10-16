using Library_Api_Sqlite.Dto_s.Return_Rec_Dto;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Repository;

namespace Library_Api_Sqlite.Services
{
    public class ReturnService
    {
       private readonly ReturnRepo _returnrepo;
        private readonly LentRepo _lentRepo;
        private readonly UserRepo _userRepo;
        private readonly BookRepo _bookRepo;

        public ReturnService(ReturnRepo returnrepo, LentRepo lentRepo, UserRepo userRepo, BookRepo bookRepo)
        {
            _returnrepo = returnrepo;
            _lentRepo = lentRepo;
            _userRepo = userRepo;
            _bookRepo = bookRepo;
        }

        public async Task<Return_Recode> ReciveBook(Return_Req_Dto recode)
        {
            try {

                var lenrec = await _lentRepo.GetRecode(recode.lentId);
                var user = await _userRepo.Getuser(recode.usernic);
                var book = await _bookRepo.GetBook(lenrec.isbn);

                if (lenrec == null)
                {
                    throw new Exception("Recode not found.");
                }
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                if (lenrec.copies < recode.returncopies)
                {
                    throw new Exception("Returned copies cannot be greater than lent copies.");
                }

                var rec = new Return_Recode
                {
                    id = recode.id,
                    lentId = recode.lentId,
                    usernic = recode.usernic,
                    isbn = lenrec.isbn,
                    lentcopies = lenrec.copies,
                    returncopies = recode.returncopies,
                    returndate = DateTime.Now
                };

              await  _bookRepo.updatecopies(book.AviCopies + recode.returncopies, lenrec.isbn);
                await _lentRepo.DeleteRecode(recode.lentId);
               await _returnrepo.AddReturnHistory(rec);
                return await _returnrepo.ReciveBook(rec);

            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while creating recode: " + ex.Message);
            }

        }
        public async Task<IEnumerable<Return_Recode>> GetAllReturnRecords()
        {
            return await _returnrepo.GetAllReturnRecords();
        }
    }
}
