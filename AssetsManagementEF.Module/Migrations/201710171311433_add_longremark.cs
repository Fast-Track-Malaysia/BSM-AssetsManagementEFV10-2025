namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_longremark : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WOLongRemarks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LongRemarks = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WRLongRemarks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LongRemarks = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.WorkOrders", "DetailDescription_ID", c => c.Int());
            AddColumn("dbo.WorkRequests", "DetailDescription_ID", c => c.Int());
            CreateIndex("dbo.WorkOrders", "DetailDescription_ID");
            CreateIndex("dbo.WorkRequests", "DetailDescription_ID");
            AddForeignKey("dbo.WorkOrders", "DetailDescription_ID", "dbo.WOLongRemarks", "ID");
            AddForeignKey("dbo.WorkRequests", "DetailDescription_ID", "dbo.WRLongRemarks", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkRequests", "DetailDescription_ID", "dbo.WRLongRemarks");
            DropForeignKey("dbo.WorkOrders", "DetailDescription_ID", "dbo.WOLongRemarks");
            DropIndex("dbo.WorkRequests", new[] { "DetailDescription_ID" });
            DropIndex("dbo.WorkOrders", new[] { "DetailDescription_ID" });
            DropColumn("dbo.WorkRequests", "DetailDescription_ID");
            DropColumn("dbo.WorkOrders", "DetailDescription_ID");
            DropTable("dbo.WRLongRemarks");
            DropTable("dbo.WOLongRemarks");
        }
    }
}
