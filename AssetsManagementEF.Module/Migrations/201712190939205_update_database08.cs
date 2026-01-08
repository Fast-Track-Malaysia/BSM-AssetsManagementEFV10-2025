namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database08 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionsPlannerGroups",
                c => new
                    {
                        Positions_ID = c.Int(nullable: false),
                        PlannerGroups_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Positions_ID, t.PlannerGroups_ID })
                .ForeignKey("dbo.Positions", t => t.Positions_ID, cascadeDelete: true)
                .ForeignKey("dbo.PlannerGroups", t => t.PlannerGroups_ID, cascadeDelete: true)
                .Index(t => t.Positions_ID)
                .Index(t => t.PlannerGroups_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PositionsPlannerGroups", "PlannerGroups_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.PositionsPlannerGroups", "Positions_ID", "dbo.Positions");
            DropIndex("dbo.PositionsPlannerGroups", new[] { "PlannerGroups_ID" });
            DropIndex("dbo.PositionsPlannerGroups", new[] { "Positions_ID" });
            DropTable("dbo.PositionsPlannerGroups");
        }
    }
}
