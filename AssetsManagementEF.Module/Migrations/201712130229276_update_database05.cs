namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "OriginID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "OriginID");
        }
    }
}
