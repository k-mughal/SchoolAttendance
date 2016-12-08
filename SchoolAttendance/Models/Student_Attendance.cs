using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAttendance.Models
{
    public class Student_Attendance
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        
        [Key]
        [Column(Order = 1)]
        public string StudentID { get; set; }

        [Key] // StudentID ,Date (Composite Key)
        [Column(Order = 2)]
        [Required(ErrorMessage = "Date is Required ! ")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public string ClassID { get; set; } // FK

        public string UserID { get; set; } // FK

        public byte[] sick_note_image { get; set; }
        public int MedicalReportStatus { get; set; }

        [DataType(DataType.MultilineText)]
        public string note { get; set; }

        [Required(ErrorMessage = " Required ! ")]
        public int IsPresent { get; set; }

        public DateTime Created_DateTime { get; set; }

        public DateTime LastModified_DateTime { get; set; }


        public virtual Student Student { get; set; }
        public virtual Class Class { get; set; }
      //  public virtual User User { get; set; }


    }
}