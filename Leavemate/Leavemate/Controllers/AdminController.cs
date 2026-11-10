using Leavemate.Models;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult DashboardLoad(int id)
        {
            Adminclss obj = dbobj.AdminDashboardDB(id);

            return View(obj);
        }
    }
}
