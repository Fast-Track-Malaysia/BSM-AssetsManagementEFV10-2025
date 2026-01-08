namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database03 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "OriginDocNumSeq", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequests", "OriginDocNumSeq");
        }
    }
}
