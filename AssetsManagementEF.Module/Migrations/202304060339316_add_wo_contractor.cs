namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_wo_contractor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "Contractor_ID", c => c.Int());
            CreateIndex("dbo.WorkOrders", "Contractor_ID");
            AddForeignKey("dbo.WorkOrders", "Contractor_ID", "dbo.Contractors", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrders", "Contractor_ID", "dbo.Contractors");
            DropIndex("dbo.WorkOrders", new[] { "Contractor_ID" });
            DropColumn("dbo.WorkOrders", "Contractor_ID");
        }
    }
}
