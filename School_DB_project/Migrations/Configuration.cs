namespace School_DB_project.Migrations
{
    using global::School_DB_project.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<School_DB_project.Models.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        // تهيئة قاعدة البيانات
        protected override void Seed(School_DB_project.Models.SchoolContext context)
        {
            // إدخال البيانات بدون تحقق
            context.Teachers.AddOrUpdate(new Teacher[]
            {
        new Teacher
        {
            Name = "Ali Al-Smadi",
            Password = PasswordHasher.HashPassword("123456") // تشفير كلمة المرور
        },
        new Teacher
        {
            Name = "Sara Al-Smadi",
            Password = PasswordHasher.HashPassword("password123") // تشفير كلمة المرور
        }
            });
            context.SaveChanges();

            // إدخال بيانات الطلاب
            context.Students.AddOrUpdate(new Student[]
            {
        new Student { Name = "Ahmed Salah" },
        new Student { Name = "Mona Khaled" }
            });
            context.SaveChanges();

            // إدخال بيانات الفصول
            context.Classes.AddOrUpdate(new Class[]
            {
        new Class { Name = "Class 1", Location = "Room A1", TeacherId = 1 },
        new Class { Name = "Class 2", Location = "Room B1", TeacherId = 2 }
            });
            context.SaveChanges();
        }
    }
}
