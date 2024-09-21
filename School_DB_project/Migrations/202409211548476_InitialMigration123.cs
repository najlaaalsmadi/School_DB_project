namespace School_DB_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration123 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Class",
                c => new
                    {
                        ClassId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId)
                .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.TeacherId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TeacherId = c.Int(nullable: false),
                        Class_ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Teacher", t => t.TeacherId, cascadeDelete: true)
                .ForeignKey("dbo.Class", t => t.Class_ClassId)
                .Index(t => t.TeacherId)
                .Index(t => t.Class_ClassId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.StudentClass",
                c => new
                    {
                        StudentClassId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        ClassId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StudentClassId)
                .ForeignKey("dbo.Class", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ClassId);
            
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
                "dbo.Task",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        ClassId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Class", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Teacher",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(nullable: false, maxLength: 100),
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
            DropForeignKey("dbo.Class", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.Course", "Class_ClassId", "dbo.Class");
            DropForeignKey("dbo.Course", "TeacherId", "dbo.Teacher");
            DropForeignKey("dbo.Task", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Task", "ClassId", "dbo.Class");
            DropForeignKey("dbo.StudentDetails", "StudentDetailsId", "dbo.Student");
            DropForeignKey("dbo.StudentClass", "StudentId", "dbo.Student");
            DropForeignKey("dbo.StudentClass", "ClassId", "dbo.Class");
            DropForeignKey("dbo.StudentCourse", "CourseId", "dbo.Course");
            DropForeignKey("dbo.StudentCourse", "StudentId", "dbo.Student");
            DropIndex("dbo.StudentCourse", new[] { "CourseId" });
            DropIndex("dbo.StudentCourse", new[] { "StudentId" });
            DropIndex("dbo.Task", new[] { "StudentId" });
            DropIndex("dbo.Task", new[] { "ClassId" });
            DropIndex("dbo.StudentDetails", new[] { "StudentDetailsId" });
            DropIndex("dbo.StudentClass", new[] { "ClassId" });
            DropIndex("dbo.StudentClass", new[] { "StudentId" });
            DropIndex("dbo.Course", new[] { "Class_ClassId" });
            DropIndex("dbo.Course", new[] { "TeacherId" });
            DropIndex("dbo.Class", new[] { "TeacherId" });
            DropTable("dbo.StudentCourse");
            DropTable("dbo.Teacher");
            DropTable("dbo.Task");
            DropTable("dbo.StudentDetails");
            DropTable("dbo.StudentClass");
            DropTable("dbo.Student");
            DropTable("dbo.Course");
            DropTable("dbo.Class");
        }
    }
}
