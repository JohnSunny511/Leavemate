using Leavemate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Leavemate.Controllers
{
    public class AccountController : Controller
    {


        DBclss dbobj = new DBclss();


        //Login PageLoad
        public IActionResult LoginLoad()
        {
            return View();
        }


        // Login Submit
        [HttpPost]
        public IActionResult Login_Click(Login objcls)
        {
            try
            {
                int id = dbobj.LoginDB(objcls);

                if (id > 0)
                {
                    HttpContext.Session.SetInt32("reg_id", id);

                    HttpContext.Session.SetString("role", objcls.role);

                    if (objcls.role == "Employee")
                    {
                        return RedirectToAction(
                            "DashboardLoad", "Employee");
                    }
                    else if (objcls.role == "Admin")
                    {
                        return RedirectToAction(
                            "DashboardLoad", "Admin");
                    }
                }
                else
                {
                    TempData["msg"] = "Invalid Username or Password";
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error: " + ex.Message;
            }

            return RedirectToAction("LoginLoad");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("LoginLoad");
        }
    }
}
