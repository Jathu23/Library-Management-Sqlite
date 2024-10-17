namespace Library_Api_Sqlite.Dto_s.Reports_Dtos
{
    public class Book_report_Dto
    {
        public Book_report_Dto(string isbn, string title, int totalcopies, int avicopies, int lentcount)
        {
            this.isbn = isbn;
            this.title = title;
            this.totalcopies = totalcopies;
            this.avicopies = avicopies;
            this.lentcount = lentcount;
        }

        public string isbn { get; set; }
        public string title { get; set; }
        public int totalcopies { get; set; }
        public int avicopies { get; set; }
        public int lentcount { get; set; }

    }
}
