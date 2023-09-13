namespace CMSManagement_Web.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public string Mobile_No { get; set; }
        public DateTime Birthdate { get; set; }
        public int Role { get; set; }
        public int Is_status { get; set; }
        public int Is_delete { get; set; }
        public int Created_by { get; set; }
        public DateTime Created_date { get; set; }
        public int Modified_by { get; set; }
        public DateTime Modified_date { get; set; }
    }
}
