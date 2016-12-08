using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolAttendance.ViewModel
{
    public class SickNote
    {
        public string StudentID { get; set; }
        public byte[] StudentImage { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Image64 { get; set; }
        public string Image64Medi_Rpt { get; set; }
        public byte[] ImageSickReport { get; set; }
        [DataType(DataType.MultilineText)]
        public string note { get; set; }
        public string classID { get; set; }
        public string className { get; set; }
        public int MedicalReportStatus { get; set; }

    }
}