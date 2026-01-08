namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PermissionPolicyUsers", "B1EmployeeID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PermissionPolicyUsers", "B1EmployeeID");
        }
    }
}
