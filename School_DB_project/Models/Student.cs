using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{

    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string Name { get; set; }

        // علاقة One-to-One مع StudentDetails
        public virtual StudentDetails StudentDetails { get; set; }

        // علاقة Many-to-Many مع Class من خلال الجدول الوسيط StudentClass
        public virtual ICollection<StudentClass> StudentClasses { get; set; }

        // علاقة Many-to-Many مع Course
        public virtual ICollection<Course> Courses { get; set; }

        // علاقة One-to-Many مع Task
        public virtual ICollection<Task> Tasks { get; set; }
    }

}

