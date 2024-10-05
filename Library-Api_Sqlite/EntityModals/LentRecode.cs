namespace Library_Api_Sqlite.EntityModals
{
    public class LentRecode
    {
        public string isbn {  get; set; }
        public int id { get; set; }
        public int usernic {  get; set; }
        public int copies { get; set; }
        public DateTime lentDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
