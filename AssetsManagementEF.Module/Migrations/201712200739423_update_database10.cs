namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "IsPreventiveMaintenance", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "IsPreventiveMaintenance");
        }
    }
}
