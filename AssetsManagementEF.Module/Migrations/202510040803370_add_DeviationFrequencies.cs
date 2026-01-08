namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_DeviationFrequencies : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeviationMitigations", "Frequency_ID", "dbo.PMFrequencies");
            DropIndex("dbo.DeviationMitigations", new[] { "Frequency_ID" });
            CreateTable(
                "dbo.DeviationFrequencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.DeviationMitigations", "DeviationFrequency_ID", c => c.Int());
            CreateIndex("dbo.DeviationMitigations", "DeviationFrequency_ID");
            AddForeignKey("dbo.DeviationMitigations", "DeviationFrequency_ID", "dbo.DeviationFrequencies", "ID");
            DropColumn("dbo.DeviationMitigations", "Frequency_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeviationMitigations", "Frequency_ID", c => c.Int());
            DropForeignKey("dbo.DeviationMitigations", "DeviationFrequency_ID", "dbo.DeviationFrequencies");
            DropIndex("dbo.DeviationMitigations", new[] { "DeviationFrequency_ID" });
            DropColumn("dbo.DeviationMitigations", "DeviationFrequency_ID");
            DropTable("dbo.DeviationFrequencies");
            CreateIndex("dbo.DeviationMitigations", "Frequency_ID");
            AddForeignKey("dbo.DeviationMitigations", "Frequency_ID", "dbo.PMFrequencies", "ID");
        }
    }
}
