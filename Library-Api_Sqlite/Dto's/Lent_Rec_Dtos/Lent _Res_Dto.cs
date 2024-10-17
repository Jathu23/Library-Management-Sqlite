namespace Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos
{
    public class Lent__Res_Dto
    {
        public Lent__Res_Dto(string isbn, int id, int usernic, int lentcopies, DateTime lentDate, DateTime returnDate, string status)
        {
            this.isbn = isbn;
            this.id = id;
            this.usernic = usernic;
            this.lentcopies = lentcopies;
            this.lentDate = lentDate;
            ReturnDate = returnDate;
            this.status = status;
        }

        public string isbn { get; set; }
        public int id { get; set; }
        public int usernic { get; set; }
        public int lentcopies { get; set; }
        public DateTime lentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string status { get; set; }
    }
}
