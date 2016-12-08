using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolAttendance.Controllers
{
    public class FullCalenderController : Controller
    {
        // GET: FullCalender
        public ActionResult Index()
        {
            return View();
        }
    }
}