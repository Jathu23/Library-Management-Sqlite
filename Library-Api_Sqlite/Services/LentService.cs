using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Repository;

namespace Library_Api_Sqlite.Services
{
    public class LentService
    {
        private readonly LentRepo _lentRepo;
        private readonly BookRepo _bookRepo;
        private readonly UserRepo _userRepo;

        public LentService(LentRepo lentRepo)
        {
            _lentRepo = lentRepo;
        }

        public async Task<LentRecode> Add(Lent_Req_Dto recode)
        {
            var book = await _bookRepo.GetBook(recode.isbn);
            var user = await _userRepo.Getuser(recode.usernic);
            if (book != null)
            {
                if (book.AviCopies > 1)
                {
                    if (user != null)
                    {
                        var lentrec = new LentRecode
                        {
                            isbn = recode.isbn,
                            id = recode.id,
                            usernic = recode.usernic,
                            copies = recode.copies,
                            ReturnDate = DateTime.Now,
                            lentDate = DateTime.Now
                        };

                        _lentRepo.AddlentHistory(lentrec);
                        return await _lentRepo.Add(lentrec);
                    }
                    else
                    {
                        throw new Exception("invalid user NIC number");
                    }

                }
                else
                {
                    throw new Exception("only one book aviable");
                }

            }
            else
            {
                throw new Exception("no book!");
            }
           
        }
    }
}
