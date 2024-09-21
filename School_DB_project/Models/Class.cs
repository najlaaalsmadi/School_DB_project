using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        // علاقة One-to-Many مع Task
        public virtual ICollection<Task> Tasks { get; set; }

        // علاقة Many-to-Many مع الطلاب من خلال الجدول الوسيط StudentClass
        public virtual ICollection<StudentClass> StudentClasses { get; set; }

        // علاقة One-to-Many مع المدرسين
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        // علاقة One-to-Many مع Course (الدورات)
        public virtual ICollection<Course> Courses { get; set; }
    }

}