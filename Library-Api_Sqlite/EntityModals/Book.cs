namespace Library_Api_Sqlite.EntityModals
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int Copies { get; set; }
        public int AviCopies { get; set; }
        public int PublishYear { get; set; }
        public DateTime AddDateTime { get; set; }
        public List<string> Images { get; set; }
        public int RentCount { get; set; }
    }
}
