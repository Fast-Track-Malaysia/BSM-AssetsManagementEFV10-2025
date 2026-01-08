namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_MitigationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviationMitigations", "MitigationUser_ID", c => c.Int());
            CreateIndex("dbo.DeviationMitigations", "MitigationUser_ID");
            AddForeignKey("dbo.DeviationMitigations", "MitigationUser_ID", "dbo.PermissionPolicyUsers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviationMitigations", "MitigationUser_ID", "dbo.PermissionPolicyUsers");
            DropIndex("dbo.DeviationMitigations", new[] { "MitigationUser_ID" });
            DropColumn("dbo.DeviationMitigations", "MitigationUser_ID");
        }
    }
}
