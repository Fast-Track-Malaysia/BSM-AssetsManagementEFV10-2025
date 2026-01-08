namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "DocStatus", c => c.String());
            AddColumn("dbo.WorkOrders", "DocStatus", c => c.String());
            AddColumn("dbo.WorkRequests", "DocStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkRequests", "DocStatus");
            DropColumn("dbo.WorkOrders", "DocStatus");
            DropColumn("dbo.PurchaseRequests", "DocStatus");
        }
    }
}
