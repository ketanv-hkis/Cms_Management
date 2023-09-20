namespace CMSManagement_API.Models
{
    public class Team
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public int Is_status { get; set; }
        public int Is_delete { get; set; }
        public int Created_by { get; set; }
        public DateTime Created_date { get; set; }
        public int Modified_by { get; set; }
        public DateTime Modified_date { get; set; }

    }
}
