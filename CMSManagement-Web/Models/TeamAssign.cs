namespace CMSManagement_Web.Models
{
    public class TeamAssign
    {
        public int? TeamAssignId { get; set; }
        public int TeamId { get; set; }
        public string EmpId { get; set; }
        public int? Is_status { get; set; }
        public int? Is_delete { get; set; }
        public int? Created_by { get; set; }
        public DateTime? Created_date { get; set; }
        public int? Modified_by { get; set; }
        public DateTime? Modified_date { get; set; }

    }
}
