namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMPatches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.WorkOrders", "PMPatch_ID", c => c.Int());
            CreateIndex("dbo.WorkOrders", "PMPatch_ID");
            AddForeignKey("dbo.WorkOrders", "PMPatch_ID", "dbo.PMPatches", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrders", "PMPatch_ID", "dbo.PMPatches");
            DropIndex("dbo.WorkOrders", new[] { "PMPatch_ID" });
            DropColumn("dbo.WorkOrders", "PMPatch_ID");
            DropTable("dbo.PMPatches");
        }
    }
}
