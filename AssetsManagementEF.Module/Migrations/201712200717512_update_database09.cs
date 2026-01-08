namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database09 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequests", "PlannerGroup_ID", c => c.Int());
            CreateIndex("dbo.PurchaseRequests", "PlannerGroup_ID");
            AddForeignKey("dbo.PurchaseRequests", "PlannerGroup_ID", "dbo.PlannerGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequests", "PlannerGroup_ID", "dbo.PlannerGroups");
            DropIndex("dbo.PurchaseRequests", new[] { "PlannerGroup_ID" });
            DropColumn("dbo.PurchaseRequests", "PlannerGroup_ID");
        }
    }
}
