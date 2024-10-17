namespace Library_Api_Sqlite.Dto_s.Reports_Dtos
{
    public class Lent_user_books_Dtos
    {
        public Lent_user_books_Dtos(int nic,  List<string> books)
        {
            this.nic = nic;
          
            this.books = books;
        }

        public int nic {  get; set; }
       
        public List<string> books { get; set; }
    }
}
