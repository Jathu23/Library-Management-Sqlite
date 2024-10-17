namespace Library_Api_Sqlite.Dto_s.Book_Dtos
{
    public class Book_Update_Dto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int Copies { get; set; }
        public int PublishYear { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
