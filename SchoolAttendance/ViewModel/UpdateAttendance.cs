using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAttendance.ViewModel
{
    public class UpdateAttendance
    {
        public int id { get; set; }
        public string StudentID { get; set; }
        public string FristName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public string ClassID { get; set; }
        public string Name { get; set; }
        public bool IsPresent { get; set; }
        public int is_present { get; set; }
        public byte[] Image { get; set; }
        public dynamic Image64 { get; set; }
        public DateTime Createed_DateTime { get; set; }
        public DateTime LastModified_DateTime { get; set; }

    }
}