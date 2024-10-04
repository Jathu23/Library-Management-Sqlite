namespace Library_Api_Sqlite.EntityModals
{
    public class User
    {
        public int NIC { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public  string phoneNumber { get; set; }
        public DateTime joinDate { get; set; }
        public DateTime lastLoginDate { get; set; }
        public int rentCount { get; set; }
        public string profileimg { get; set; }


    }
}
