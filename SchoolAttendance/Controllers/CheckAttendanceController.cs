using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;
using SchoolAttendance.ViewModel;
using SchoolAttendance.DAL;
using System.Globalization;

namespace SchoolAttendance.Controllers
{
    public class CheckAttendanceController : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();
        public ActionResult Index()
        {
            ViewBag.ClassList = new SelectList(db.Classes , "ClassID", "Name");
           
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {

            string _startDate, _endDate, _classId;

            _startDate = (form[1]).ToString();
            TempData["_startDate"] = _startDate;

            _endDate = form[2].ToString();
            TempData["_endDate"] = _endDate;

            _classId = form[3].ToString();
            TempData["_classId"] = _classId;

            return RedirectToAction("ShowAttend");

        }

        [HttpGet]
        public ActionResult ShowAttend()
        {

            string _startDate = TempData["_startDate"].ToString();
            DateTime newStartDate = Convert.ToDateTime(_startDate);

            string _endtDate = TempData["_endDate"].ToString();
            DateTime newEndDate = Convert.ToDateTime(_endtDate);

            string ClassId = TempData["_classId"].ToString();

            List<AttendanceStats> attendanceSatats1 = (from atnd in db.Student_Attendance
                                                       where (atnd.ClassID == ClassId && atnd.Date >= newStartDate && atnd.Date <= newEndDate) //
                                                       group atnd by atnd.StudentID into grpStdId
                                               join std in db.Students on grpStdId.Key equals std.StudentID
                                               join atd in db.Student_Attendance on std.StudentID equals atd.StudentID
                   
                                                       select new AttendanceStats
                                               {
                                                   Image = std.Image,
                                                   StudentID = std.StudentID,
                                                   FristName = std.FristName,
                                                   LastName = std.LastName,
                                                   TotalAttendant = grpStdId.Sum(z => z.IsPresent),
                                                   Total_Lectures = grpStdId.Select(t => t.Date).Count(),
                                                   TotalMedicalReports = grpStdId.Sum(t => t.MedicalReportStatus)
                                               }).Distinct().ToList();

         

            
            List<AttendanceStats> attendanceSatats2 = new List<AttendanceStats>();

            foreach (var items in attendanceSatats1)
            {
                attendanceSatats2.Add(new AttendanceStats
                {
                    Image = items.Image,
                    StudentID = items.StudentID,
                    FristName = items.FristName,
                    LastName = items.LastName,
                    TotalAttendant = Convert.ToInt32(items.TotalAttendant),
                    Total_Lectures = Convert.ToInt32(items.Total_Lectures),
                    percentage = Convert.ToString(Math.Round((Convert.ToDouble(items.TotalAttendant) / Convert.ToDouble(items.Total_Lectures) * 100) , 2)) + "%",
                    TotalMedicalReports = items.TotalMedicalReports

                });
            }

            return View(attendanceSatats2);

        }

    }
}
