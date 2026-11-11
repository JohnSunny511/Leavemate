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

        public IActionResult ManageLeavesLoad(int id, string status = "All")
        {
            ViewBag.AdminId = id;
            ViewBag.SelectedStatus = status;

            List<Leavecls> lst = dbobj.AllLeavesDB(status);

            return View(lst);
        }


        [HttpPost]
        public IActionResult UpdateLeaveStatus(int id,int leave_id,string status,string admin_remark)
        {
            if (status != "Approved" && status != "Rejected")
            {
                return RedirectToAction(
                    "ManageLeavesLoad",
                    new { id = id }
                );
            }

            int result = dbobj.UpdateLeaveStatusDB(
                leave_id,
                status,
                admin_remark,
                id
            );

            if (result == 1)
            {
                TempData["msg"] = "Leave status updated successfully.";
            }
            else
            {
                TempData["msg"] = "Leave request could not be updated.";
            }

            return RedirectToAction(
                "ManageLeavesLoad",
                new { id = id }
            );
        }


        public IActionResult MyProfileLoad(int id)
        {
            Adminclss obj = dbobj.AdminProfileDB(id);

            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
    }
}
