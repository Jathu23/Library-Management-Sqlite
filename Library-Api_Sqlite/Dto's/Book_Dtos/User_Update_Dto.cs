namespace Library_Api_Sqlite.Dto_s.Book_Dtos
{
    public class User_Update_Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
