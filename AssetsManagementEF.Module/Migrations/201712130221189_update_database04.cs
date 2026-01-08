namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database04 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "OriginRejectRemarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "OriginRejectRemarks");
        }
    }
}
