namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ContractorID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PermissionPolicyUsers", "ContractorID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PermissionPolicyUsers", "ContractorID");
        }
    }
}
