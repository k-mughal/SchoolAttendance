using System;
using System.Collections.Generic;
using System.Linq;
using SchoolAttendance.Models;
using System.Web;

namespace SchoolAttendance.ViewModel
{
    public class ShowAttendance
    {
      
        public String ClassID { get; set; }
        //public List<Class> ClassesName { get; set; }

        public String className { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String SelectedClassId { get; set; }


    }
}