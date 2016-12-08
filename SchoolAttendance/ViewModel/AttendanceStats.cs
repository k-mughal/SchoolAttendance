using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAttendance.ViewModel
{
    public class AttendanceStats
    {
        public string StudentID { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public double Total_Lectures { get; set; }
        public double TotalAttendant { get; set; }
        public int TotalMedicalReports { get; set; }
        public byte[] Image { get; set; }
        public string percentage { get; set; }
      
    }
}