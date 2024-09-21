using System.Data.Entity;

namespace School_DB_project.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // تعطيل الجمع التلقائي لأسماء الجداول
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            // علاقة One-to-One بين Student و StudentDetails
            modelBuilder.Entity<Student>()
                .HasOptional(s => s.StudentDetails)
                .WithRequired(sd => sd.Student);

            // علاقة One-to-Many بين Teacher و Course
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithRequired(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId);

            // علاقة Many-to-Many بين Student و Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .Map(cs =>
                {
                    cs.MapLeftKey("StudentId");
                    cs.MapRightKey("CourseId");
                    cs.ToTable("StudentCourse");
                });

            // علاقة Many-to-One بين Task و Course مع منع الحذف المتتابع
            modelBuilder.Entity<Task>()
                .HasRequired(t => t.Course)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CourseId)
                .WillCascadeOnDelete(false);  // منع الحذف المتتابع

            // علاقات أخرى...

            base.OnModelCreating(modelBuilder);
        }


        public System.Data.Entity.DbSet<School_DB_project.Models.Class> Classes { get; set; }

        public System.Data.Entity.DbSet<School_DB_project.Models.StudentClass> StudentClasses { get; set; }

        public System.Data.Entity.DbSet<School_DB_project.Models.Task> Tasks { get; set; }
    }
}
