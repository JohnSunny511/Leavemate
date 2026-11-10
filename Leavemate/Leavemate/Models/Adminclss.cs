namespace Leavemate.Models
{
    public class Adminclss
    {
        public int admin_id { set; get; }
        public string? admin_name { set; get; }
        public string? admin_email { set; get; }
        public string? admin_password { get; set; }

        public int total_employees { get; set; }
        public int pending_requests { get; set; }
        public int approved_requests { get; set; }
    }
}
