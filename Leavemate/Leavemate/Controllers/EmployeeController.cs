using Leavemate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Leavemate.Controllers
{
    public class EmployeeController : Controller
    {
        DBclss dbobj = new DBclss();
        public IActionResult RegisterLoad()
        {
            List<DepartmentClass> deptList = GetDepartments();

            ViewBag.selDepartments = new SelectList(
                deptList, "deptName", "deptName");

            return View();
        }

        public List<DepartmentClass> GetDepartments()
        {
            List<DepartmentClass> departments = new List<DepartmentClass>()
            {
                new DepartmentClass { deptName = "IT" },
                new DepartmentClass { deptName = "HR" },
                new DepartmentClass { deptName = "Finance" },
                new DepartmentClass { deptName = "Marketing" },
                new DepartmentClass { deptName = "Sales" },
                new DepartmentClass { deptName = "Operations" }
            };

            return departments;
        }


        // Employee Registration Submit
        [HttpPost]
        public IActionResult Register_Click(Employeecls objcls)
        {
            try
            {
                string resp = dbobj.EmployeeRegistration(objcls);

                TempData["msg"] = resp;
            }
            catch (Exception ex)
            {
                TempData["msg"] = "Error: " + ex.Message;
            }

            return RedirectToAction("RegisterLoad");
        }

        // Employee Dashboard
        public IActionResult DashboardLoad()
        {
            if (HttpContext.Session.GetString("role") != "Employee")
            {
                return RedirectToAction("LoginLoad", "Account");
            }

            return View();
        }
    }
}
