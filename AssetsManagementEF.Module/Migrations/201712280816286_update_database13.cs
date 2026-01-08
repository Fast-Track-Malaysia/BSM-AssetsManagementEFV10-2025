namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PermissionPolicyUsers", "UserEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PermissionPolicyUsers", "UserEmail");
        }
    }
}
