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
            public int StudentId { get; set; } // المفتاح الأساسي
            public string Name { get; set; }

            // علاقة One-to-One مع StudentDetails
            public virtual StudentDetails StudentDetails { get; set; }

            // علاقة Many-to-Many مع Course
            public virtual ICollection<Course> Courses { get; set; }
        }
    }

