using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace School_DB_project.Models
{
    public class StudentClass
    {
        [Key]
        public int StudentClassId { get; set; } // المفتاح الأساسي

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int ClassId { get; set; }
        public virtual Class Class { get; set; }
    }

}