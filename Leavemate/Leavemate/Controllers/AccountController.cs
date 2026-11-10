using Leavemate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            Login user = dbobj.LoginDB(objcls);

            if (user != null)
            {
                if (user.role == "Employee")
                {
                    return RedirectToAction(
                        "DashboardLoad",
                        "Employee",
                        new { id = user.reg_id }
                    );
                }
                else if (user.role == "Admin")
                {
                    return RedirectToAction(
                        "DashboardLoad",
                        "Admin",
                        new { id = user.reg_id }
                    );
                }
            }

            ViewBag.msg = "Invalid Username or Password";

            return View("LoginLoad");
        }

        // Logout
        public IActionResult Logout()
        {
            TempData.Clear();

            return RedirectToAction("LoginLoad", "Account");
        }
    }
}
