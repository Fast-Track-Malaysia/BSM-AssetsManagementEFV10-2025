namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database02 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "RevisionNo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "RevisionNo");
        }
    }
}
