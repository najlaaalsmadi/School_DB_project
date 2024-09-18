using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{
   
        public class Teacher
        {
            [Key]
            public int TeacherId { get; set; } // المفتاح الأساسي
            public string Name { get; set; }

            // علاقة One-to-Many مع Course
            public virtual ICollection<Course> Courses { get; set; }
        }
    }
