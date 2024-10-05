namespace Library_Api_Sqlite.EntityModals
{
    public class Return_Recode
    {
        public int id { get; set; }
        public int usernic {  get; set; }
        public int lentId { get; set; }
        public string isbn { get; set; }
        public int lentcopies { get; set; }
        public int returncopies { get; set; }
        public DateTime returndate { get; set; }


    }
}
