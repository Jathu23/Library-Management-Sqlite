namespace Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos
{
    public class Custom_Lent_Rec
    {
        public Custom_Lent_Rec(int lentId, string bookTitle, int copies, DateTime lentDate, string status)
        {
            this.lentId = lentId;
            this.bookTitle = bookTitle;
            this.copies = copies;
            this.lentDate = lentDate;
            this.status = status;
        }

        public int lentId { get; set; }
        public string bookTitle { get; set; }
        public int copies { get; set; }
        public DateTime lentDate { get; set; }
        public string status { get; set; }
    }
}
