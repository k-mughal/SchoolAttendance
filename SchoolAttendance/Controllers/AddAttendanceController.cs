using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using SchoolAttendance.Models;
using SchoolAttendance.ViewModel;
using SchoolAttendance.DAL;
using System.Data;

namespace SchoolAttendance.Controllers
{
    public class AddAttendanceController : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();
        public ActionResult ShowClassAddAttendance()
        {
            ViewBag.AttndMsg = TempData["attdMsg"];
            ViewBag.Classlist = new SelectList(db.Classes, "ClassID", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowClassForAttendance(FormCollection form)
        {
            string _classid;
            _classid = (form[1]).ToString();
            TempData["_classid"] = _classid;
            return RedirectToAction("ShowCalender");
        }
        public ActionResult CalenderClassData(string classid)
        {
            IList<CalendarDTO> attdCheck = (from atd in db.Student_Attendance
                                            where (atd.ClassID == classid)// && atd.Date >= BeginDate && atd.Date <= EndDate)
                                            select new CalendarDTO
                                            { start = atd.Date, color = "Red", title = "Done" }
                                             ).Distinct().ToList();
             return Json(attdCheck, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowCalender()
        {
            string _classid = TempData["_classid"].ToString();
            ViewBag.Class_id = _classid;
            return View();

        }
        public ActionResult AddAttendanceCalender(string date, string classid)
        {
            DateTime newDate = DateTime.Parse(date);
            TempData["_newDate"] = newDate;
            TempData["_classid"] = classid;
            var attdCheck = (from atd in db.Student_Attendance
                             where (atd.ClassID == classid && atd.Date == newDate)
                             select (atd.ClassID)).FirstOrDefault();

            if (attdCheck == null)
            {
                return RedirectToAction("AddAttendance");
             }
            else
            {
                return RedirectToAction("ShowCalender");
            }
            
        }
        [HttpGet]
        public ActionResult AddAttendance()
        {
            string _classid = TempData["_classid"].ToString();
            string newDate = TempData["_newDate"].ToString();
            TempData["_dateAdd"] = newDate;
            TempData["_classidAdd"] = _classid;
            List<AddAttendance> addattendance = (from student in db.Students
                                                     //join  attend in db.Student_Attendance on student.StudentID equals attend.StudentID
                                                 join cls in db.Classes on student.ClassID equals cls.ClassID
                                                 where student.ClassID == _classid
                                                 select new AddAttendance
                                                 {
                                                     Image = student.Image,
                                                     StudentID = student.StudentID,
                                                     FristName = student.FristName,
                                                     LastName = student.LastName,
                                                     ClassID = cls.ClassID

                                                 }).ToList();
            var className = (from cls in db.Classes
                             where cls.ClassID == _classid
                             select cls.Name).FirstOrDefault();
            ViewBag.Classname = className;
            List<AddAttendance> addattendance2 = new List<AddAttendance>();
            foreach (var items in addattendance)
            {
                addattendance2.Add(new AddAttendance
                {
                    id = items.id,
                    StudentID = items.StudentID,
                    Image64 = Convert.ToBase64String(items.Image),
                    FristName = items.FristName,
                    LastName = items.LastName,
                    IsPresent = true //Convert.ToBoolean(items.is_present)
                });
            }
            return View(addattendance2);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAttendance(List<AddAttendance> addattendance)//, HttpPostedFileBase Image1)
        {
            string _date = TempData["_dateAdd"].ToString();
            DateTime var_Date = Convert.ToDateTime(_date);

            string _classid = TempData["_classidAdd"].ToString();

            TempData["_classid"] = _classid;

            Student_Attendance std_atttd = new Student_Attendance();

            foreach (var item in addattendance)
            {
                db.Student_Attendance.Add(std_atttd).StudentID = item.StudentID;
                db.Student_Attendance.Add(std_atttd).Date = var_Date;//DateTime.Today ;
                db.Student_Attendance.Add(std_atttd).ClassID = _classid;
                db.Student_Attendance.Add(std_atttd).UserID = "Admin";
                db.Student_Attendance.Add(std_atttd).IsPresent = Convert.ToInt16(item.IsPresent);
                db.Student_Attendance.Add(std_atttd).Created_DateTime = DateTime.Now;
                db.Student_Attendance.Add(std_atttd).LastModified_DateTime = DateTime.Now;
                db.SaveChanges();
            }

            ViewBag.ConfirmMsg = "Successfully Saved";
            return RedirectToAction("Confirm");


        }
        public ActionResult Confirm()
        {
            string _classid = TempData["_classid"].ToString();
            TempData["_classid"] = _classid;
            return View();
        }

    }
}
