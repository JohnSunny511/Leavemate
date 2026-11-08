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


        public int LoginDB(Login objcls)
        {
            try
            {
                int id = 0;

                SqlCommand cmd = new SqlCommand("sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", objcls.username);
                cmd.Parameters.AddWithValue("@password", objcls.password);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    id = Convert.ToInt32(dr["reg_id"]);
                    objcls.role = dr["role"].ToString();
                }

                con.Close();

                return id;
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
