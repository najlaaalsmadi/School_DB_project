namespace School_DB_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentDetails",
                c => new
                    {
                        StudentDetailsId = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.StudentDetailsId)
                .ForeignKey("dbo.Student", t => t.StudentDetailsId)
                .Index(t => t.StudentDetailsId);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TeacherId);
            
            CreateTable(
                "dbo.StudentCourse",
                c => new
                    {
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CourseId })
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.StudentDetails", "StudentDetailsId", "dbo.Student");
            DropForeignKey("dbo.StudentCourse", "CourseId", "dbo.Course");
            DropForeignKey("dbo.StudentCourse", "StudentId", "dbo.Student");
            DropIndex("dbo.StudentCourse", new[] { "CourseId" });
            DropIndex("dbo.StudentCourse", new[] { "StudentId" });
            DropIndex("dbo.StudentDetails", new[] { "StudentDetailsId" });
            DropIndex("dbo.Course", new[] { "TeacherId" });
            DropTable("dbo.StudentCourse");
            DropTable("dbo.Teacher");
            DropTable("dbo.StudentDetails");
            DropTable("dbo.Student");
            DropTable("dbo.Course");
        }
    }
}
