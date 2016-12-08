using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Mvc;
using SchoolAttendance.ViewModel;
using SchoolAttendance.DAL;
using System.Data;

namespace SchoolAttendance.Controllers
{
    public class HomeController : Controller
    {
        private SchoolAttendanceContext db = new SchoolAttendanceContext();
        public ActionResult Index()
        {


            return View();
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CalendarData()
        {
            DateTime BeginDate, EndDate;
            BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //.ToShortDateString();
            EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)); //ToShortDateString();
            

            IList<CalendarDTO> tasksList = new List<CalendarDTO>();

            tasksList.Add(new CalendarDTO
            {
              id = 1,
              title = "Done",
              start = DateTime.Today  //ToUnixTimespan(DateTime.Now),
              //end = DateTime.Today.AddDays(1), //ToUnixTimespan(DateTime.Now.AddHours(4)),
             // url = "My Test"
            });
            tasksList.Add(new CalendarDTO
            {
                id = 2,
               title = "Done",
                start = DateTime.Today.AddDays(2)// ToUnixTimespan(DateTime.Now.AddDays(1)),
                                                 //   end =   DateTime.Today.AddDays(2), //ToUnixTimespan(DateTime.Now.AddDays(1).AddHours(4)),
                                                 // url = "www.bing.com"
            });
            //  IList<CalendarDTO> tasksList = new List<CalendarDTO>();

            //IList<CalendarDTO> attdCheck = (from atd in db.Student_Attendance
            //                               where (atd.ClassID == "HD16")// && atd.Date >= BeginDate && atd.Date <= EndDate)
            //                               select new CalendarDTO
            //                               {  start = atd.Date, color = "Red"}

            //                               ).Distinct().ToList(); 

            //foreach(var item in attdCheck)
            //{
            //    attdCheck.Add(item);
            //}

          //  return View();
            return Json(tasksList, JsonRequestBehavior.AllowGet);
         //   return Json(attdCheck, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        
        //  public ActionResult getSelectedDate(string value)
        public JsonResult getSelectedDate(string value)
        {
            DateTime date = DateTime.Parse(value);
         

            return Json(value, JsonRequestBehavior.AllowGet);
           
        }




    }
}



