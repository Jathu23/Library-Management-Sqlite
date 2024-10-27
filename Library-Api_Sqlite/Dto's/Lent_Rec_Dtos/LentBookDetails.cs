namespace Library_Api_Sqlite.Dto_s.Lent_Rec_Dtos
{
    public class LentBookDetails
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int UserNIC { get; set; }
        public DateTime LentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Copies { get; set; }
    }
}
