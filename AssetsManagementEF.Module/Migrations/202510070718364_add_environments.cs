namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_environments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deviation2025", "People2_ID", "dbo.RiskPeoples");
            DropIndex("dbo.Deviation2025", new[] { "People2_ID" });
            CreateTable(
                "dbo.RiskEnvironments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Deviation2025", "EscalationComment", c => c.String());
            AddColumn("dbo.Deviation2025", "Environment_ID", c => c.Int());
            CreateIndex("dbo.Deviation2025", "Environment_ID");
            AddForeignKey("dbo.Deviation2025", "Environment_ID", "dbo.RiskEnvironments", "ID");
            DropColumn("dbo.Deviation2025", "People2_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deviation2025", "People2_ID", c => c.Int());
            DropForeignKey("dbo.Deviation2025", "Environment_ID", "dbo.RiskEnvironments");
            DropIndex("dbo.Deviation2025", new[] { "Environment_ID" });
            DropColumn("dbo.Deviation2025", "Environment_ID");
            DropColumn("dbo.Deviation2025", "EscalationComment");
            DropTable("dbo.RiskEnvironments");
            CreateIndex("dbo.Deviation2025", "People2_ID");
            AddForeignKey("dbo.Deviation2025", "People2_ID", "dbo.RiskPeoples", "ID");
        }
    }
}
