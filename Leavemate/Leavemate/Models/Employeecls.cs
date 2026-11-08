namespace Leavemate.Models
{
    public class Employeecls
    {
        public int emp_id { get; set; }

        public string? emp_name { get; set; }
        public int emp_age { get; set; }

        public string? emp_email { get; set; }

        public string? emp_department { get; set; }

        public string? emp_password { get; set; }
    }

    public class DepartmentClass
    {
        public string deptName { get; set; }
    }
}
