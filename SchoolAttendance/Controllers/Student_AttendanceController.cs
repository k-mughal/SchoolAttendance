using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolAttendance.DAL;
using SchoolAttendance.Models;

namespace SchoolAttendance.Controllers
{
    public class Student_AttendanceController : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();

        // GET: Student_Attendance
        public ActionResult Index()//(FormCollection col)
        {

            //  string name = col["go"];

            var student_Attendance = db.Student_Attendance.Include(s => s.Class).Include(s => s.Student);

            return View(student_Attendance.ToList());
       
        }
    

        public ActionResult IndexAttendance()
        {

            // var students = db.Students.Include(s => s.Class); 6THA11
            ViewBag.varClassList = new SelectList(db.Classes.Where(s => s.EndDate <= DateTime.Today).Select(s => s.ClassID).ToList());
             var students = db.Students.Include(s => s.Class);//.Where(s => s.ClassID == "6THA11");
         //   var student_Attendance = db.Student_Attendance.Include(s => s.Class).Include(s => s.Student);

          // var var_student_Attendance = from student  in db.Students
                     //                join student_attendance in db.Student_Attendance
                                 //    on student.StudentID equals student_attendance.StudentID
                                  //    select new { student.FristName , student.LastName };

            return View(students.ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(List<Student> ci)
        {
            if (ModelState.IsValid)
            {
                using (SchoolAttendanceContext sa = new SchoolAttendanceContext())

                {
                    foreach (var i in ci)
                    {
                        // sa.Student_Attendance.Add().StudentID = i.StudentID;

                        var st_atd = new Student_Attendance { StudentID = i.StudentID };
                        sa.Student_Attendance.Add(st_atd);

                    }
                    sa.SaveChanges();
                    ViewBag.Message = "Data successfully saved!";
                    ModelState.Clear();
                    // ci = new List<ContactInfo> { new ContactInfo { ID = 0, ContactName = "", ContactNo = "" } };
                }
            }
            return View(ci);
        }

        // GET: Student_Attendance/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Attendance student_Attendance = db.Student_Attendance.Find(id);
            if (student_Attendance == null)
            {
                return HttpNotFound();
            }
            return View(student_Attendance);
        }

        // GET: Student_Attendance/Create
        public ActionResult Create(string Go)
        {
          //  if (btnSubmit != null)
       // do something for the Button btnSubmit
   // else
                // do something for the Button btnProcess
           //     end if
               
                ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FristName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FristName");
            return View();
        }

        // POST: Student_Attendance/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //   [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Date,ClassID,UserID,IsPresent,Createed_DateTime,LastModified_DateTime")] Student_Attendance student_Attendance)
        //  public ActionResult Create(Student_Attendance student_Attendance)
        {
            if (ModelState.IsValid)
            {
                db.Student_Attendance.Add(student_Attendance).StudentID = student_Attendance.StudentID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", student_Attendance.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FristName", student_Attendance.StudentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FristName", student_Attendance.UserID);
            return View(student_Attendance);
        }


        // GET: Student_Attendance/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Attendance student_Attendance = db.Student_Attendance.Find(id);
            if (student_Attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", student_Attendance.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FristName", student_Attendance.StudentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FristName", student_Attendance.UserID);
            return View(student_Attendance);
        }

        // POST: Student_Attendance/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Date,ClassID,UserID,IsPresent,Createed_DateTime,LastModified_DateTime")] Student_Attendance student_Attendance)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Attendance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", student_Attendance.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FristName", student_Attendance.StudentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "FristName", student_Attendance.UserID);
            return View(student_Attendance);
        }

        // GET: Student_Attendance/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Attendance student_Attendance = db.Student_Attendance.Find(id);
            if (student_Attendance == null)
            {
                return HttpNotFound();
            }
            return View(student_Attendance);
        }

        // POST: Student_Attendance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student_Attendance student_Attendance = db.Student_Attendance.Find(id);
            db.Student_Attendance.Remove(student_Attendance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
