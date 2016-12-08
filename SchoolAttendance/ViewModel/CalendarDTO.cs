using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAttendance.ViewModel
{
    public class CalendarDTO
    {
       public int id { get; set; }
        public DateTime start { get; set; }
        public string title { get; set; }
        public string color { get; set; }
       
    }
}