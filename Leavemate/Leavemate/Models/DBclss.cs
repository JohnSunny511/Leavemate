using System.Data.SqlClient;
using System.Data;

namespace Leavemate.Models
{
    public class DBclss
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-QK9QR22V\SQLEXPRESS;database=LeaveManagementDB;integrated security = true");

        public string EmployeeRegistration(Employeecls obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_EmployeeRegister", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@emp_name", obj.emp_name);
                cmd.Parameters.AddWithValue("@emp_age", obj.emp_age);
                cmd.Parameters.AddWithValue("@emp_email", obj.emp_email);
                cmd.Parameters.AddWithValue("@emp_department", obj.emp_department);
                cmd.Parameters.AddWithValue("@password", obj.emp_password);

                con.Open();

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                if (id == 1)
                {
                    return "Registered Successfully";
                }
                else
                {
                    return "Username already exists";
                }


            }
            catch (Exception ex) { 
                if(con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return ex.Message;
            }
        }



        public string AdminRegistration(Adminclss obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_AdminRegister", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@admin_name", obj.admin_name);
                cmd.Parameters.AddWithValue("@admin_email", obj.admin_email);
                cmd.Parameters.AddWithValue("@admin_password", obj.admin_password);

                con.Open();

                int id = Convert.ToInt32(cmd.ExecuteScalar());

                if (id == 1)
                {
                    return "Registered Successfully";
                }
                else
                {
                    return "Username already exists";
                }


            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                return ex.Message;
            }
        }


        public Login LoginDB(Login objcls)
        {
            try
            {
                Login user = null;

                SqlCommand cmd = new SqlCommand("sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", objcls.username);
                cmd.Parameters.AddWithValue("@password", objcls.password);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    user = new Login();

                    user.reg_id = Convert.ToInt32(dr["reg_id"]);
                    user.role = dr["role"].ToString();
                }

                con.Close();

                return user;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                throw;
            }
        }


        public Employeecls DashboardDB(int id)
        {
            try
            {
                Employeecls obj = new Employeecls();

                SqlCommand cmd = new SqlCommand("sp_EmployeeDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@emp_id", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    obj.emp_id = id;
                    obj.emp_name = dr["emp_name"].ToString();
                    obj.total_allowance = Convert.ToInt32(dr["total_allowance"]);
                    obj.used_days = Convert.ToInt32(dr["used_days"]);
                    obj.available_days = Convert.ToInt32(dr["available_days"]);
                    obj.pending_requests = Convert.ToInt32(dr["pending_requests"]);
                }

                con.Close();

                return obj;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                throw;
            }
        }


        public Adminclss AdminDashboardDB(int id)
        {
            try
            {
                Adminclss obj = new Adminclss();

                SqlCommand cmd = new SqlCommand("sp_AdminDashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@admin_id", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    obj.admin_id = id;
                    obj.admin_name = dr["admin_name"].ToString();
                    obj.total_employees = Convert.ToInt32(dr["total_employees"]);
                    obj.pending_requests = Convert.ToInt32(dr["pending_requests"]);
                    obj.approved_requests = Convert.ToInt32(dr["approved_requests"]);
                }

                con.Close();

                return obj;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                throw;
            }
        }

        public int ApplyLeaveDB(Leavecls obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_ApplyLeave", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@emp_id", obj.emp_id);
                cmd.Parameters.AddWithValue("@leave_type", obj.leave_type);
                cmd.Parameters.AddWithValue("@start_date", obj.start_date);
                cmd.Parameters.AddWithValue("@end_date", obj.end_date);
                cmd.Parameters.AddWithValue("@total_days", obj.total_days);
                cmd.Parameters.AddWithValue("@reason", obj.reason);

                con.Open();

                int result = Convert.ToInt32(cmd.ExecuteScalar());

                con.Close();

                return result;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                throw;
            }
        }

        public List<Leavecls> MyLeavesDB(int id)
        {
            try
            {
                List<Leavecls> lst = new List<Leavecls>();

                SqlCommand cmd = new SqlCommand("sp_MyLeaves", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@emp_id", id);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Leavecls obj = new Leavecls();

                    obj.leave_id = Convert.ToInt32(dr["leave_id"]);
                    obj.emp_id = Convert.ToInt32(dr["emp_id"]);
                    obj.leave_type = dr["leave_type"].ToString();
                    obj.start_date = Convert.ToDateTime(dr["start_date"]);
                    obj.end_date = Convert.ToDateTime(dr["end_date"]);
                    obj.total_days = Convert.ToInt32(dr["total_days"]);
                    obj.reason = dr["reason"].ToString();
                    obj.status = dr["status"].ToString();
                    obj.admin_remark = dr["admin_remark"].ToString();

                    lst.Add(obj);
                }

                con.Close();

                return lst;
            }
            catch (Exception)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                throw;
            }
        }
    }
}
