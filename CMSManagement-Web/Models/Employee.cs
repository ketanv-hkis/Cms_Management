namespace CMSManagement_Web.Models
{
    public class Employee
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int gender { get; set; }
        public string mobile_No { get; set; }
        public DateTime birthdate { get; set; }
        public int role { get; set; }
        public int is_status { get; set; }
        public int is_delete { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public int modified_by { get; set; }
        public DateTime modified_date { get; set; }
    }
}
