using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SchoolAttendance.Models
{
    public class Class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Minimum 4 and Maximum 10 Characters required")]
        [MinLength(4)]
        [MaxLength(10)]
        public string ClassID { get; set; }


        [Required(ErrorMessage = "Minimum 5 and Maximum 30 Characters required")]
        [MinLength(5)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Start Date Required ! ")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date Required ! ")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
      //  public string StudentID { get; set; } // FK
        public string DepartmentID { get; set; } // FK
        public virtual Department Department { get; set; }

         
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Student_Attendance> Student_Attendance { get; set; }
    }
}