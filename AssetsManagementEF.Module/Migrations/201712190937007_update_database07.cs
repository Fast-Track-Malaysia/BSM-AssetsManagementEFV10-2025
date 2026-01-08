namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database07 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Positions", "PlannerGroup_ID", "dbo.PlannerGroups");
            DropIndex("dbo.Positions", new[] { "PlannerGroup_ID" });
            DropColumn("dbo.Positions", "PlannerGroup_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Positions", "PlannerGroup_ID", c => c.Int());
            CreateIndex("dbo.Positions", "PlannerGroup_ID");
            AddForeignKey("dbo.Positions", "PlannerGroup_ID", "dbo.PlannerGroups", "ID");
        }
    }
}
