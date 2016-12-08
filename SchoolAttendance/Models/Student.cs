using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SchoolAttendance.Models
{
    public class Student
    {
   
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Required(ErrorMessage = "Minimum 4 and Maximum 10 Characters required")]
        //[MinLength(4)]
        //[MaxLength(10)]
        public string StudentID { get; set; }


        [Required(ErrorMessage = "Minimum 2 and Maximum 25 Characters required")]
        [MinLength(2)]
        [MaxLength(25)]
        public string FristName { get; set; }

        [Required(ErrorMessage = "Minimum 2 and Maximum 25 Characters required")]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Start Date Required ! ")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Date of Birth is Required ! ")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = "Minimum 2 and Maximum 25 Characters required")]
        [MinLength(2)]
        [MaxLength(50)]        
        public String Address { get; set; }

        public byte[] Image { get; set; }


        public string ClassID { get; set; } //FK
        public virtual Class Class { get; set; }

        public virtual ICollection<Student_Attendance> Student_Attendance { get; set; }
    }
}