using SchoolAttendance.Models;
using System.Data.Entity;
using SchoolAttendance.ViewModel;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SchoolAttendance.DAL
{
    public class SchoolAttendanceContext : DbContext
    {
        public SchoolAttendanceContext() : base("SchoolAttendanceContext")
        {
            Database.SetInitializer<SchoolAttendanceContext>(new DropCreateDatabaseIfModelChanges<SchoolAttendanceContext>());
        }

       // public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Department> Departments { get; set; }
        //public DbSet<User> Users { get; set; }
    
        
      

        public System.Data.Entity.DbSet<SchoolAttendance.Models.Student_Attendance> Student_Attendance { get; set; }

        public System.Data.Entity.DbSet<SchoolAttendance.Models.Student> Students { get; set; }

        public System.Data.Entity.DbSet<SchoolAttendance.Models.User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}