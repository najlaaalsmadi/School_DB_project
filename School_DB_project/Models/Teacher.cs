using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School_DB_project.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; } // المفتاح الأساسي
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; } // كلمة المرور

        // علاقة One-to-Many مع Course
        public virtual ICollection<Course> Courses { get; set; }
    }
}
