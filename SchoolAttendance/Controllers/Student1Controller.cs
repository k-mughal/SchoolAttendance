using SchoolAttendance.DAL;
using SchoolAttendance.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
namespace SchoolAttendance.Controllers
{
    public class Student1Controller : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();
        // GET: Student1
        public ActionResult Index(string searchString)
        {
            if (searchString != null)
            {
                var students = db.Students.Include(s => s.Class).Where(s => s.StudentID == searchString);
                return View(students.ToList());
            }
            else
            {               
                var students = db.Students.Include(s => s.Class);
                return View(students.ToList());
            }
        }
        // GET: Student1/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }
        // GET: Student1/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name");
            return View();
        }
        // POST: Student1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public ActionResult Create([Bind(Include = "StudentID,FristName,LastName,StartDate,DOB,Address,ClassID,file")] Student student, HttpPostedFileBase Image1)
        public ActionResult Create(Student student, HttpPostedFileBase Image1)
        {
            if (ModelState.IsValid)
            {
                student.Image = new byte[Image1.ContentLength];
                Image1.InputStream.Read(student.Image, 0, Image1.ContentLength);

                string _fname = student.FristName;
                string _lname = student.LastName;

                var now = DateTime.Now;
                var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
                int uniqueId = (int)(zeroDate.Ticks / 10000);

                string _unqid = _fname.Substring(0, 1).ToUpper() + _lname.Substring(0, 1).ToUpper() + uniqueId;

                db.Students.Add(student).StudentID = _unqid;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", student.ClassID);
            return View(student);
        }
        // GET: Student1/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", student.ClassID);
            return View(student);
        }
        // POST: Student1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FristName,LastName,StartDate,DOB,Address,ClassID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Classes, "ClassID", "Name", student.ClassID);
            return View(student);
        }

        // GET: Student1/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
