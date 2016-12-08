using System.Web.Mvc;
using SchoolAttendance.DAL;
using SchoolAttendance.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using System.Data.Entity;
using System.Web;

namespace SchoolAttendance.Controllers
{
    public class SickNoteController : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();
        // GET: SickNote
        public ActionResult ShowStudent()
        {
            ViewBag.StudentList = new SelectList(db.Students, "StudentID", "StudentID");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowStudent(FormCollection form)
        {
            string _student_id;
            _student_id = (form[1]).ToString();
            TempData["_student_id"] = _student_id;
            return RedirectToAction("ShowSickNoteCalender");
        }
        public ActionResult ShowSickNoteCalender()
        {
            string _student_id = TempData["_student_id"].ToString();
            ViewBag.Student_ID = _student_id;
            return View();
        }
        public ActionResult CalenderClassData(string studentid)
        {
            IList<CalendarDTO> attdCheck = (from atd in db.Student_Attendance
                                            where (atd.StudentID == studentid && atd.IsPresent == 0 && atd.MedicalReportStatus == 0)// && atd.Date >= BeginDate && atd.Date <= EndDate)
                                            select new CalendarDTO
                                            { start = atd.Date, color = "Green", title = "Add" }
                                            ).Distinct().ToList();
            return Json(attdCheck, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddSickNoteCalender(string date, string studentid)
        {
            DateTime newDate = DateTime.Parse(date);
            TempData["_newDate"] = newDate;
            TempData["_student_id"] = studentid;
            var attdCheck = (from atd in db.Student_Attendance
                             where (atd.StudentID == studentid && atd.Date == newDate && atd.MedicalReportStatus == 0)
                             select (atd.StudentID)).FirstOrDefault();

            if (attdCheck != null)
            {
                return RedirectToAction("AddSickNote");
            }
            else
            {
                return RedirectToAction("ShowSickNoteCalender");
            }

        }
        [HttpGet]
        public ActionResult AddSickNote()
        {
            string _student_id = TempData["_student_id"].ToString();
            string _newDate = TempData["_newDate"].ToString();
            DateTime newDate = Convert.ToDateTime(_newDate).Date;
            TempData["_newDate"] = _newDate;
            TempData["_student_id"] = _student_id;
            //var students = db.Students.Include(s => s.Class);//.Where(s => s.ClassID == "6THA11");
            var student_Attendance = db.Student_Attendance.Include(s => s.Class).Include(s => s.Student);
            List<SickNote> addSickNote = (from student in db.Students
                                          join attend in db.Student_Attendance on student.StudentID equals attend.StudentID
                                          join cls in db.Classes on student.ClassID equals cls.ClassID
                                          //  join cls in db.Classes on attend.ClassID equals cls.ClassID
                                          where attend.StudentID == _student_id && attend.Date == newDate && attend.sick_note_image == null
                                          select new SickNote
                                          {
                                              StudentImage = student.Image,
                                              StudentID = student.StudentID,
                                              FristName = student.FristName,
                                              LastName = student.LastName,
                                              className = cls.Name,
                                              note = attend.note

                                          }).ToList();

            List<SickNote> addSickNote2 = new List<SickNote>();
            foreach (var items in addSickNote)
            {
                addSickNote2.Add(new SickNote
                {

                    StudentID = items.StudentID,
                    Image64 = Convert.ToBase64String(items.StudentImage),
                    MedicalReportStatus = 1,
                    FristName = items.FristName,
                    LastName = items.LastName,
                    className = items.className,
                    note = items.note


                });
            }
            return View(addSickNote2);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSickNote(List<SickNote> addsicknote, HttpPostedFileBase ImageSickNote)//, HttpPostedFileBase Image1)
        {

            string _classid = TempData["_student_id"].ToString();
            string _newDate = TempData["_newDate"].ToString();
            DateTime newDate = Convert.ToDateTime(_newDate).Date;
            TempData["_classid"] = _classid;

            foreach (var i in addsicknote)
            {

                var std_attd = db.Student_Attendance.Where(s => s.StudentID == i.StudentID && s.Date == newDate).FirstOrDefault();
                std_attd.sick_note_image = new byte[ImageSickNote.ContentLength];
                ImageSickNote.InputStream.Read(std_attd.sick_note_image, 0, ImageSickNote.ContentLength);
                std_attd.note = i.note;
                std_attd.MedicalReportStatus = 1;
                db.SaveChanges();

            }
            return RedirectToAction("Confirm");

        }

        public ActionResult Confirm()
        {
            string _student_id = TempData["_student_id"].ToString();
            TempData["_student_id"] = _student_id;

            //string _newDate = TempData["_newDate"].ToString();
            //TempData["_newDate"] = _newDate;

            return View();
        }

        public ActionResult ViewStudent()
        {
            ViewBag.StudentList = new SelectList(db.Students, "StudentID", "StudentID");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewStudent(FormCollection form)
        {
            string _student_id;
            _student_id = (form[1]).ToString();
            TempData["_student_id"] = _student_id;
            return RedirectToAction("ViewSickNoteCalender");
        }
        public ActionResult ViewSickNoteCalender()
        {
            string _student_id = TempData["_student_id"].ToString();
            ViewBag.Student_ID = _student_id;
            return View();
        }
        public ActionResult CalenderViewStudentData(string studentid)
        {
            IList<CalendarDTO> attdCheck = (from atd in db.Student_Attendance
                                            where (atd.StudentID == studentid && atd.IsPresent == 0 && atd.MedicalReportStatus == 1)// && atd.Date >= BeginDate && atd.Date <= EndDate)
                                            select new CalendarDTO
                                            { start = atd.Date, color = "Green", title = "View" }
                                            ).Distinct().ToList();
            return Json(attdCheck, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewSickNoteCalenderData(string date, string studentid)
        {
            DateTime newDate = DateTime.Parse(date);
            TempData["_newDate"] = newDate;
            TempData["_student_id"] = studentid;
            var attdCheck = (from atd in db.Student_Attendance
                             where (atd.StudentID == studentid && atd.Date == newDate && atd.MedicalReportStatus == 1)
                             select (atd.StudentID)).FirstOrDefault();

            if (attdCheck != null)
            {
                return RedirectToAction("ViewSickNote");
            }
            else
            {
                return RedirectToAction("ViewSickNoteCalender");
            }

        }
        [HttpGet]
        public ActionResult ViewSickNote()
        {
            string _student_id = TempData["_student_id"].ToString();
            string _newDate = TempData["_newDate"].ToString();
            DateTime newDate = Convert.ToDateTime(_newDate).Date;
            TempData["_newDate"] = _newDate;
            TempData["_student_id"] = _student_id;
         
            List<SickNote> viewSickNote = (from student in db.Students
                                          join attend in db.Student_Attendance on student.StudentID equals attend.StudentID
                                          join cls in db.Classes on attend.ClassID equals cls.ClassID
                                          where attend.StudentID == _student_id && attend.Date == newDate && attend.MedicalReportStatus == 1
                                          select new SickNote
                                          {
                                              StudentImage = student.Image,
                                              StudentID = student.StudentID,
                                              FristName = student.FristName,
                                              LastName = student.LastName,
                                              className = cls.Name,
                                              ImageSickReport =  attend.sick_note_image,
                                              MedicalReportStatus = attend.MedicalReportStatus,
                                              note = attend.note

                                          }).ToList();

            List<SickNote> viewSickNote2 = new List<SickNote>();
            foreach (var items in viewSickNote)
            {
                viewSickNote2.Add(new SickNote
                {

                    StudentID = items.StudentID,
                    Image64 = Convert.ToBase64String(items.StudentImage),
                    Image64Medi_Rpt = Convert.ToBase64String(items.ImageSickReport),
                    FristName = items.FristName,
                    LastName = items.LastName,
                    className = items.className,
                    note = items.note


                });
            }
            return View(viewSickNote2);
        }


    }
}