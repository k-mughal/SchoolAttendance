using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SchoolAttendance.Models;

namespace SchoolAttendance.DAL
{
    public class SchoolAttendanceInitializer : DropCreateDatabaseIfModelChanges<SchoolAttendanceContext>
    {
        protected override void Seed(SchoolAttendanceContext context)
        {
            var departments = new List<Department>
            {
                new Department { DepartmentID = "CMT01", Name = "Computing"},
                new Department {DepartmentID = "ART01" , Name = "Arts"}
            };
            departments.ForEach(d => context.Departments.Add(d));
            context.SaveChanges();


            //var classes = new List<Class>
            //{
            //    new Class {  ClassID = "HDP04", DepartmentID = "CMT01", StartDate = DateTime.Parse("2005-01-01"), EndDate = DateTime.Parse("2005-09-01") }
            //};
            //classes.ForEach(d => context.Classes.Add(d));
            //context.SaveChanges();


        }

    }
}