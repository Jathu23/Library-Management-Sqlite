namespace Library_Api_Sqlite.Dto_s.Return_Rec_Dto
{
    public class ReturnBookDetails
    {
        public int Id { get; set; }
        public int UserNIC { get; set; }
        public int LentId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int LentCopies { get; set; }
        public int ReturnCopies { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
