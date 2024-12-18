﻿using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.Dto_s.Notifaction_Dtos;
using Library_Api_Sqlite.Dto_s.Reports_Dtos;
using Library_Api_Sqlite.EntityModals;
using Library_Api_Sqlite.Repository;
using System.ComponentModel.DataAnnotations;
using static Library_Api_Sqlite.Repository.LentRepo;

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
                var Date = DateTime.Now.AddDays(0);
                var lentrec = new LentRecode
                {
                    isbn = recode.isbn,
                    id = recode.id,
                    usernic = recode.usernic,
                    copies = recode.lentcopies,
                    lentDate = Date,
                    ReturnDate = Date.AddDays(7)
                  
                };

                await _bookRepo.updatecopies(book.AviCopies - recode.lentcopies, recode.isbn);
                await _bookRepo.updaterentcount( recode.isbn, book.RentCount + 1);
                await _userRepo.UpdateLentCount(recode.usernic);
                _lentRepo.AddlentHistory(lentrec);
              

                return await _lentRepo.Add(lentrec);
            }
            catch (Exception ex)
            {
               
                throw new Exception("An error occurred while lending the book: " + ex.Message);
            }
        }
        public async Task<IEnumerable<Lent__Res_Dto>> GetAllLendRecords()
        {
            var lentRec = new List<Lent__Res_Dto>();
            var rec = await _lentRepo.GetAllLendRecords();
            foreach (var item in rec)
            {
                var recS = new Lent__Res_Dto(item.isbn, item.id, item.usernic, item.copies, item.lentDate, item.ReturnDate, DateDifference(item.ReturnDate, true));
                lentRec.Add(recS);
            }

            return lentRec;
        }

        public async Task<IEnumerable<Lent__Res_Dto>> GetRecordsby_Nic(int nic)
        {
            var lentRec = new List<Lent__Res_Dto>();
            var rec = await _lentRepo.GetRecordsby_Nic(nic);
            foreach (var item in rec)
            {
                var recS = new Lent__Res_Dto(item.isbn, item.id, item.usernic, item.copies, item.lentDate, item.ReturnDate, DateDifference(item.ReturnDate, true));
                lentRec.Add(recS);
            }

            return lentRec;
        }
        public async Task<List<Notifaction>> findoverdueAll()
        {
            var lentrecods = await _lentRepo.GetAllLendRecords();
            List<Notifaction> Notifactions = new List<Notifaction>();

            foreach (var rec in lentrecods)
            {
                int diff = int.Parse(DateDifference(rec.ReturnDate, false));
                if (diff < 0)
                {
                    var Nrec = new Notifaction(rec.id, rec.isbn, rec.usernic, rec.copies, rec.lentDate, rec.ReturnDate, DateDifference(rec.ReturnDate, true));
                    Notifactions.Add(Nrec);
                }
            }
            return Notifactions;

        }

        public async Task<List<Notifaction>> findoverdue_user(int nic)
        {

            var lentrecods = await _lentRepo.GetAllLendRecords();
            List<Notifaction> Notifactions = new List<Notifaction>();

            foreach (var rec in lentrecods)
            {
                if (rec.usernic == nic)
                {
                    int diff = int.Parse(DateDifference(rec.ReturnDate, false));
                    if (diff < 0)
                    {
                        var Nrec = new Notifaction(rec.id, rec.isbn, rec.usernic, rec.copies, rec.lentDate, rec.ReturnDate, DateDifference(rec.ReturnDate, true));
                        Notifactions.Add(Nrec);
                    }
                }

            }
            return Notifactions;

        }


        public async Task<IEnumerable<string>> GetLentBooks(int nic)
        {

            var orBooks = await _bookRepo.GetAllBooks();
            var records = await _lentRepo.GetRecordsby_Nic(nic);

            var books = new List<string>();

            foreach (var rec in records)
            {
                if (rec.usernic == nic)
                {
                    var isbn = rec.isbn;
                    var book = orBooks.FirstOrDefault(x => x.ISBN == isbn);
                    if (book != null)
                    {
                        books.Add(book.Title);
                    }
                }
            }
            return books.Distinct();
        }


        public async Task<List<UserLentBook>> GetUserLentBooks_R()
        {
            var data = await _lentRepo.GetUserLentBooks_R();

            return data;
        }


        public string DateDifference(DateTime inputDate, bool outFormat)
        {
            DateTime today = DateTime.Now;

            TimeSpan diff = inputDate - today;
            int difference = (int)Math.Floor(diff.TotalHours);
            int days = difference / 24;
            int hours = difference % 24;

            if (outFormat)
            {
                if (days < 0 && hours < 0)
                {
                    return $"{Math.Abs(days)} Days {Math.Abs(hours)} hours over";
                }
                else
                {
                    return $"{days} Days {hours} hours more";
                }
            }
            else
            {
                return difference.ToString();
            }
        }

        public async Task<int> CountTotalLendBooks()
        {

            return await _lentRepo.CountTotalLendBooks();
        }
        public async Task<List<Custom_Lent_Rec>> customInfo(int userid)
        {
            var lentrecods =await _lentRepo.GetRecordsby_Nic(userid);
            var Books = await _bookRepo.GetAllBooks();
            var customrecods = new List<Custom_Lent_Rec>();
          
            foreach (var rec in lentrecods)
            {
                if (rec.usernic == userid)
                {
                  
                    foreach (var book in Books)
                    {
                        if (rec.isbn == book.ISBN)
                        {
                           var title = book.Title;
                            var cusRec = new Custom_Lent_Rec(rec.id, title, rec.copies,rec.lentDate, DateDifference(rec.ReturnDate, true));
                            customrecods.Add(cusRec);
                        }
                    }
                   
                }
            }
            return customrecods;
           

        }
    }
}
