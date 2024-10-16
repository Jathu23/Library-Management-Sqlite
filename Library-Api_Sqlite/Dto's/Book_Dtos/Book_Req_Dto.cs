namespace Library_Api_Sqlite.Dto_s
{
    public class Book_Req_Dto
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<string> Genre { get; set; }
        public int Copies { get; set; }
        public int PublishYear { get; set; }
        public List<IFormFile>? Images { get; set; }
  

    }
}
