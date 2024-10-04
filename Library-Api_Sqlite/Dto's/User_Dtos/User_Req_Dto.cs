namespace Library_Api_Sqlite.Dto_s.User_Dtos
{
    public class User_Req_Dto
    {
        public int NIC { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public IFormFile profileimg { get; set; }
    }
}
