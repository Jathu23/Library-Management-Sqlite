namespace Library_Api_Sqlite.Dto_s.Notifaction_Dtos
{
    public class Notifaction
    {
        public Notifaction(int lentid, string isbn, int usernic, int copies, DateTime lentDate, DateTime returnDate, string status)
        {
            Lentid = lentid;
            this.isbn = isbn;
            this.usernic = usernic;
            this.copies = copies;
            this.lentDate = lentDate;
            ReturnDate = returnDate;
            this.status = status;
        }

        public int Lentid { get; set; }
        public string isbn { get; set; }
        public int usernic { get; set; }
        public int copies { get; set; }
        public DateTime lentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string status { get; set; }
    }
}
