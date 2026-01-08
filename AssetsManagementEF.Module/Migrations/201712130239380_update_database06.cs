namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database06 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PurchaseRequests", "OriginDocNumSeq");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PurchaseRequests", "OriginDocNumSeq", c => c.Long(nullable: false));
        }
    }
}
