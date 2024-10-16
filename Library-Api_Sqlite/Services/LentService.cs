﻿using Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos;
using Library_Api_Sqlite.Dto_s.Notifaction_Dtos;
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
                    ReturnDate = DateTime.Now.AddDays(-14), 
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

        public async Task<IEnumerable<LentRecode>> GetRecordsby_Nic(int nic)
        {
            return await _lentRepo.GetRecordsby_Nic(nic);
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
                    var Nrec = new Notifaction(rec.id,rec.isbn,rec.usernic,rec.copies, rec.lentDate,rec.ReturnDate, DateDifference(rec.ReturnDate, true));
                    Notifactions.Add(Nrec);
                }
            }
            return Notifactions;

        }

        public static string DateDifference(DateTime inputDate, bool outFormat)
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
    }
}
