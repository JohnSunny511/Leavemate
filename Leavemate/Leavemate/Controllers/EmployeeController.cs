using Leavemate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public IActionResult DashboardLoad(int id)
        {
            Employeecls obj = dbobj.DashboardDB(id);

            return View(obj);
        }


        public IActionResult ApplyLeaveLoad(int id)
        {
            ViewBag.LeaveTypes = new SelectList(
                new List<string>
                {
            "Casual Leave",
            "Sick Leave",
            "Earned Leave"
                }
            );

            ViewBag.EmpId = id;

            return View();
        }


        [HttpPost]
        public IActionResult ApplyLeave_Click(int id, Leavecls obj)
        {
            if (obj.end_date < obj.start_date)
            {
                ViewBag.msg = "End date cannot be before start date.";

                ViewBag.LeaveTypes = new SelectList(
                    new List<string>
                    {
                "Casual Leave",
                "Sick Leave",
                "Earned Leave"
                    }
                );

                ViewBag.EmpId = id;

                return View("ApplyLeaveLoad", obj);
            }

            obj.emp_id = id;
            obj.total_days = (obj.end_date - obj.start_date).Days + 1;

            int result = dbobj.ApplyLeaveDB(obj);

            if (result == 1)
            {
                TempData["msg"] = "Leave applied successfully.";
            }
            else
            {
                TempData["msg"] = "You already have a leave request for these dates.";
            }

            return RedirectToAction("MyLeavesLoad", new { id = id });
        }


        public IActionResult MyLeavesLoad(int id)
        {
            ViewBag.EmpId = id;

            List<Leavecls> lst = dbobj.MyLeavesDB(id);

            return View(lst);
        }
    }
}
