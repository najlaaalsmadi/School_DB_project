using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; } // المفتاح الأساسي

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }

        // علاقة Many-to-One مع Class
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        // علاقة Many-to-One مع Student
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        // علاقة Many-to-One مع Course
        public int CourseId { get; set; }  // أضف الحقل CourseId
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }  // العلاقة مع Course
    }
}