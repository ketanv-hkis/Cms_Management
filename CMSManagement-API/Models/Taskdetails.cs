namespace CMSManagement_API.Models
{
    public class Taskdetails
    {
        public int Task_Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Video { get; set; }

        public DateTime Estimatedtime { get; set; }

        public string Comment { get; set; }

        public int TaskStatus { get; set; }

        public int Is_Status { get; set; }

        public int Is_Delete { get; set; }

        public int Created_by { get; set; }

        public DateTime Created_date { get; set; }

        public int Modified_by { get; set; }

        public DateTime Modified_date { get; set; }
    }
}
