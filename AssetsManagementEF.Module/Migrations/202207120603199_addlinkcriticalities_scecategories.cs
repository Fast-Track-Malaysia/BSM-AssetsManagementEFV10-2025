namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlinkcriticalities_scecategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SCECategories", "Criticality_ID", c => c.Int());
            CreateIndex("dbo.SCECategories", "Criticality_ID");
            AddForeignKey("dbo.SCECategories", "Criticality_ID", "dbo.Criticalities", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SCECategories", "Criticality_ID", "dbo.Criticalities");
            DropIndex("dbo.SCECategories", new[] { "Criticality_ID" });
            DropColumn("dbo.SCECategories", "Criticality_ID");
        }
    }
}
