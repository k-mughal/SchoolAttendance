using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SchoolAttendance.Models
{
    public class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [Required(ErrorMessage = "Minimum 4 and Maximum 10 Characters required")]
        [MinLength(4)]
        [MaxLength(10)]
        public string DepartmentID { get; set; }

        [Required(ErrorMessage = "Invalid")]
        [MinLength(2)]
        [MaxLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}