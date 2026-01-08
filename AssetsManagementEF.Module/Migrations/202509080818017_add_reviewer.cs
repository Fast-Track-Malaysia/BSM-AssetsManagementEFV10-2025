namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_reviewer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviationReviewers", "Reviewer_ID", c => c.Int());
            CreateIndex("dbo.DeviationReviewers", "Reviewer_ID");
            AddForeignKey("dbo.DeviationReviewers", "Reviewer_ID", "dbo.PermissionPolicyUsers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviationReviewers", "Reviewer_ID", "dbo.PermissionPolicyUsers");
            DropIndex("dbo.DeviationReviewers", new[] { "Reviewer_ID" });
            DropColumn("dbo.DeviationReviewers", "Reviewer_ID");
        }
    }
}
