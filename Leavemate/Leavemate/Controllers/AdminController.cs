using Leavemate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Leavemate.Controllers
{
    public class AdminController : Controller
    {
        DBclss dbobj = new DBclss();
        public IActionResult RegisterLoad()
        {
            return View();
        }

        // Admin Registration Submit
        [HttpPost]
        public IActionResult Register_Click(Adminclss objcls)
        {
            try
            {
                string resp = dbobj.AdminRegistration(objcls);

                TempData["msg"] = resp;
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error: " + ex.Message;
            }

            return RedirectToAction("RegisterLoad");
        }

        // Admin Dashboard
        public IActionResult DashboardLoad()
        {
            if (HttpContext.Session.GetString("role") != "Admin")
            {
                return RedirectToAction("LoginLoad", "Account");
            }

            return View();
        }
    }
}
