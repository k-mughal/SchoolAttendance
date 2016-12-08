using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using SchoolAttendance.ViewModel;
using SchoolAttendance.DAL;
using System.Data;



namespace SchoolAttendance.Controllers
{
    public class UpdateAttendanceController : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();

        public ActionResult ShowClassForAttendance()
        {
            ViewBag.AttndMsg = TempData["attdMsg"];
            ViewBag.Classlist = new SelectList(db.Classes, "ClassID", "Name");
        //      string number = String.Format("{0:d5}", (DateTime.Now.Ticks / 10) % 1000000000);
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
                                            { start = atd.Date, color = "Green", title = "Done" }

                                         ).Distinct().ToList();

            return Json(attdCheck, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowCalender()
        {
            string _classid = TempData["_classid"].ToString();
            ViewBag.Class_id = _classid;
            return View();

        }

        public ActionResult UpdateAttendanceCalender(string date, string classid)
        {
            DateTime newDate = DateTime.Parse(date);
            TempData["_newDate"] = newDate;
            TempData["_classid"] = classid;

            var attdCheck = (from atd in db.Student_Attendance
                             where (atd.ClassID == classid && atd.Date == newDate)
                             select (atd.ClassID)).FirstOrDefault();

            if (attdCheck == null)
            {
                return RedirectToAction("ShowCalender");

            }
            else
            {
                return RedirectToAction("UpdateAttend");
            }
        }

        [HttpGet]
        public ActionResult UpdateAttend()
        {

            string _classid = TempData["_classid"].ToString();
            string newDate = TempData["_newDate"].ToString();
            TempData["_dateAdd"] = newDate;
            TempData["_classidAdd"] = _classid;
            DateTime mydate = Convert.ToDateTime(newDate);
            ViewBag.mydate = mydate.ToString("dd-MM-yyyy");

            List<UpdateAttendance> updateAttendance = (from attend in db.Student_Attendance
                                                        join student in db.Students on attend.StudentID equals student.StudentID
                                                        join cls in db.Classes on attend.ClassID equals cls.ClassID
                                                        where attend.ClassID == _classid && attend.Date == mydate
                                                        select new UpdateAttendance
                                                        {
                                                            id = attend.id,
                                                            StudentID = student.StudentID,
                                                            FristName = student.FristName,
                                                            LastName = student.LastName,
                                                            ClassID = cls.ClassID,
                                                            Image = student.Image,
                                                            is_present = attend.IsPresent
                                                        }).ToList();

            List<UpdateAttendance> updateattendancd2 = new List<UpdateAttendance>();

            foreach (var items in updateAttendance)
            {
                updateattendancd2.Add(new UpdateAttendance
                {
                    id = items.id,
                    StudentID = items.StudentID,
                    Image64 = Convert.ToBase64String(items.Image),
                    FristName = items.FristName,
                    LastName = items.LastName,
                    IsPresent = Convert.ToBoolean(items.is_present)
        
                });
            }


            var className = (from cls in db.Classes
                             where cls.ClassID == _classid
                             select cls.Name).FirstOrDefault();

            ViewBag.Classname = className;

            return View(updateattendancd2);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAttend(List<UpdateAttendance> updateAttendance)//, HttpPostedFileBase Image1)
        {

            string _classid = TempData["_classidAdd"].ToString();
            TempData["_classid"] = _classid;

            foreach (var i in updateAttendance)
            {
               
                var std_attd = db.Student_Attendance.Where(s => s.id == i.id).FirstOrDefault();
                std_attd.IsPresent = Convert.ToInt32(i.IsPresent);
                db.SaveChanges();
             
            }

    
            return RedirectToAction("Confirm");


        }
        public ActionResult Confirm()
        {
            string _classid = TempData["_classid"].ToString();
            TempData["_classid"] = _classid;

            ViewBag.ConfirmMsg = "Updated Successfully";
            return View();
        }

    }
}
