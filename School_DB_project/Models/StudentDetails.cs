using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{
    public class StudentDetails
    {
        [Key]
        public int StudentDetailsId { get; set; } // المفتاح الأساسي

        // معلومات إضافية عن الطالب
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        // علاقة One-to-One مع Student
        public virtual Student Student { get; set; }
    }
}