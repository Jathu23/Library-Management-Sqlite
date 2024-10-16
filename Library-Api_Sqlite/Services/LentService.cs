using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Repository;
using System.ComponentModel.DataAnnotations;

namespace Library_Api_Sqlite.Services
{
    public class LentService
    {
        private readonly LentRepo _lentRepo;
        private readonly BookRepo _bookRepo;
        private readonly UserRepo _userRepo;

        public LentService(LentRepo lentRepo, BookRepo bookRepo, UserRepo userRepo)
        {
            _lentRepo = lentRepo;
            _bookRepo = bookRepo;
            _userRepo = userRepo;
        }

        public async Task<LentRecode> Add(Lent_Req_Dto recode)
        {
            try
            {
                var book = await _bookRepo.GetBook(recode.isbn);
                var user = await _userRepo.Getuser(recode.usernic);

                if (book == null)
                {
                    throw new Exception("Book not found.");
                }

                if (user == null)
                {
                    throw new Exception("User not found.");
                }

               
                if (book.AviCopies < recode.lentcopies)
                {
                    throw new Exception("Not enough copies available.");
                }

                var lentrec = new LentRecode
                {
                    isbn = recode.isbn,
                    id = recode.id,
                    usernic = recode.usernic,
                    copies = recode.lentcopies,
                    ReturnDate = DateTime.Now.AddDays(14), 
                    lentDate = DateTime.Now
                };

                await _bookRepo.updatecopies(book.Copies - recode.lentcopies, recode.isbn);
                _lentRepo.AddlentHistory(lentrec);
              

                return await _lentRepo.Add(lentrec);
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while lending the book: " + ex.Message);
            }
        }
        public async Task<IEnumerable<LentRecode>> GetAllLendRecords()
        {
            return await _lentRepo.GetAllLendRecords();
        }

    }
}
