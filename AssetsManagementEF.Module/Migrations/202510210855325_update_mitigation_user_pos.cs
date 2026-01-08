namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_mitigation_user_pos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeviationMitigations", "MitigationUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationMitigations", "Position_ID", "dbo.Positions");
            DropIndex("dbo.DeviationMitigations", new[] { "MitigationUser_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "Position_ID" });
            AddColumn("dbo.DeviationMitigations", "MitigationUser", c => c.String());
            AddColumn("dbo.DeviationMitigations", "Position", c => c.String());
            DropColumn("dbo.DeviationMitigations", "MitigationUser_ID");
            DropColumn("dbo.DeviationMitigations", "Position_ID");
            DropColumn("dbo.DeviationReviewers", "KeyReviewer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviationReviewers", "KeyReviewer", c => c.Boolean(nullable: false));
            AddColumn("dbo.DeviationMitigations", "Position_ID", c => c.Int());
            AddColumn("dbo.DeviationMitigations", "MitigationUser_ID", c => c.Int());
            DropColumn("dbo.DeviationMitigations", "Position");
            DropColumn("dbo.DeviationMitigations", "MitigationUser");
            CreateIndex("dbo.DeviationMitigations", "Position_ID");
            CreateIndex("dbo.DeviationMitigations", "MitigationUser_ID");
            AddForeignKey("dbo.DeviationMitigations", "Position_ID", "dbo.Positions", "ID");
            AddForeignKey("dbo.DeviationMitigations", "MitigationUser_ID", "dbo.PermissionPolicyUsers", "ID");
        }
    }
}
