using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance.Models
{
   // public enum genderType { Male, Female};
    public class User
    {

        [Required(ErrorMessage = "Minimum 2 and Maximum 25 Characters required")]
        [MinLength(2)]
        [MaxLength(20)]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Minimum 2 and Maximum 25 Characters required")]
        [MinLength(2)]
        [MaxLength(25)]
        public string FristName { get; set; }

        [Required(ErrorMessage = "Minimum 2 and Maximum 25 Characters required")]
        [MinLength(2)]
        [MaxLength(25)]
        public string LastName { get; set; }

        public string Gender { get; set; }

        public string IsAdmin { get; set; }

    //    public string ClassID { get; set; } // FK

      //  public virtual Class Class { get; set; }

    //    public virtual ICollection<Student_Attendance> Student_Attendance { get; set; }

    }
}