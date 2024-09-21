using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; } // المفتاح الأساسي
        public string Name { get; set; }

        // علاقة One-to-Many مع Teacher
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        // علاقة Many-to-Many مع Student
        public virtual ICollection<Student> Students { get; set; }
        // علاقة One-to-Many مع Task
        public virtual ICollection<Task> Tasks { get; set; }  // أضف العلاقة العكسية
    }
}