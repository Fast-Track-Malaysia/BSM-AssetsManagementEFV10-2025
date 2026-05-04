namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_WorkOrderTrades : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkOrderTrades",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.WorkOrderManHours", "EManCount", c => c.Int(nullable: false));
            AddColumn("dbo.WorkOrderManHours", "EManHours", c => c.Long(nullable: false));
            AddColumn("dbo.WorkOrderManHours", "WorkOrderTrade_ID", c => c.Int());
            CreateIndex("dbo.WorkOrderManHours", "WorkOrderTrade_ID");
            AddForeignKey("dbo.WorkOrderManHours", "WorkOrderTrade_ID", "dbo.WorkOrderTrades", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrderManHours", "WorkOrderTrade_ID", "dbo.WorkOrderTrades");
            DropIndex("dbo.WorkOrderManHours", new[] { "WorkOrderTrade_ID" });
            DropColumn("dbo.WorkOrderManHours", "WorkOrderTrade_ID");
            DropColumn("dbo.WorkOrderManHours", "EManHours");
            DropColumn("dbo.WorkOrderManHours", "EManCount");
            DropTable("dbo.WorkOrderTrades");
        }
    }
}
