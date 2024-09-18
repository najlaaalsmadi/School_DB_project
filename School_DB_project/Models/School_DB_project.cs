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
        }
    }
}
