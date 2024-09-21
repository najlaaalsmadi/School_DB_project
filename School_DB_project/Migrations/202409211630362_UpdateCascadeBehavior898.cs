namespace School_DB_project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCascadeBehavior898 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Task", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Task", "CourseId");
            AddForeignKey("dbo.Task", "CourseId", "dbo.Course", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Task", "CourseId", "dbo.Course");
            DropIndex("dbo.Task", new[] { "CourseId" });
            DropColumn("dbo.Task", "CourseId");
        }
    }
}
