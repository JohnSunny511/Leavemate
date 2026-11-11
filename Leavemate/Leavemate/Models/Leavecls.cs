namespace Leavemate.Models
{
    public class Leavecls
    {
        public int leave_id { get; set; }
        public int emp_id { get; set; }

        public string emp_name { get; set; }
        public string emp_department { get; set; }


        public string leave_type { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int total_days { get; set; }
        public string reason { get; set; }
        public string status { get; set; }
        public string admin_remark { get; set; }
        public int admin_id { get; set; }
    }
}
