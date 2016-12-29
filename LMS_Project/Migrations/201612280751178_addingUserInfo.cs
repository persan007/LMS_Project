namespace LMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileObjectModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        CourseID = c.String(maxLength: 128),
                        Filename = c.String(nullable: false, maxLength: 255),
                        ContentType = c.String(nullable: false, maxLength: 100),
                        Data = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .ForeignKey("dbo.CourseModels", t => t.CourseID)
                .Index(t => t.UserID)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.CourseModels",
                c => new
                    {
                        CourseID = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(),
                        CourseLevel = c.String(),
                        SubjectStartsAt = c.DateTime(nullable: false),
                        SubjectEndsAt = c.DateTime(nullable: false),
                        DisplayColor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            AddColumn("dbo.AspNetUsers", "Firstname", c => c.String());
            AddColumn("dbo.AspNetUsers", "Lastname", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfileImage", c => c.String());
            AddColumn("dbo.AspNetUsers", "CourseModels_CourseID", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "CourseModels_CourseID");
            AddForeignKey("dbo.AspNetUsers", "CourseModels_CourseID", "dbo.CourseModels", "CourseID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FileObjectModels", "CourseID", "dbo.CourseModels");
            DropForeignKey("dbo.AspNetUsers", "CourseModels_CourseID", "dbo.CourseModels");
            DropForeignKey("dbo.FileObjectModels", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "CourseModels_CourseID" });
            DropIndex("dbo.FileObjectModels", new[] { "CourseID" });
            DropIndex("dbo.FileObjectModels", new[] { "UserID" });
            DropColumn("dbo.AspNetUsers", "CourseModels_CourseID");
            DropColumn("dbo.AspNetUsers", "ProfileImage");
            DropColumn("dbo.AspNetUsers", "Lastname");
            DropColumn("dbo.AspNetUsers", "Firstname");
            DropTable("dbo.CourseModels");
            DropTable("dbo.FileObjectModels");
        }
    }
}
