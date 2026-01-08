namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addscecategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SCECategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SCESubCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        SCECategory_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SCECategories", t => t.SCECategory_ID)
                .Index(t => t.SCECategory_ID);
            
            AddColumn("dbo.WorkOrders", "IsDeviationApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkOrders", "IsDeviationNotApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.Equipments", "SCECategory_ID", c => c.Int());
            AddColumn("dbo.Equipments", "SCESubCategory_ID", c => c.Int());
            AddColumn("dbo.EquipmentComponents", "SCECategory_ID", c => c.Int());
            AddColumn("dbo.EquipmentComponents", "SCESubCategory_ID", c => c.Int());
            AddColumn("dbo.WorkRequests", "IsDeviationApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkRequests", "IsDeviationNotApproved", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Equipments", "SCECategory_ID");
            CreateIndex("dbo.Equipments", "SCESubCategory_ID");
            CreateIndex("dbo.EquipmentComponents", "SCECategory_ID");
            CreateIndex("dbo.EquipmentComponents", "SCESubCategory_ID");
            AddForeignKey("dbo.EquipmentComponents", "SCECategory_ID", "dbo.SCECategories", "ID");
            AddForeignKey("dbo.EquipmentComponents", "SCESubCategory_ID", "dbo.SCESubCategories", "ID");
            AddForeignKey("dbo.Equipments", "SCECategory_ID", "dbo.SCECategories", "ID");
            AddForeignKey("dbo.Equipments", "SCESubCategory_ID", "dbo.SCESubCategories", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipments", "SCESubCategory_ID", "dbo.SCESubCategories");
            DropForeignKey("dbo.Equipments", "SCECategory_ID", "dbo.SCECategories");
            DropForeignKey("dbo.EquipmentComponents", "SCESubCategory_ID", "dbo.SCESubCategories");
            DropForeignKey("dbo.EquipmentComponents", "SCECategory_ID", "dbo.SCECategories");
            DropForeignKey("dbo.SCESubCategories", "SCECategory_ID", "dbo.SCECategories");
            DropIndex("dbo.SCESubCategories", new[] { "SCECategory_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "SCESubCategory_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "SCECategory_ID" });
            DropIndex("dbo.Equipments", new[] { "SCESubCategory_ID" });
            DropIndex("dbo.Equipments", new[] { "SCECategory_ID" });
            DropColumn("dbo.WorkRequests", "IsDeviationNotApproved");
            DropColumn("dbo.WorkRequests", "IsDeviationApproved");
            DropColumn("dbo.EquipmentComponents", "SCESubCategory_ID");
            DropColumn("dbo.EquipmentComponents", "SCECategory_ID");
            DropColumn("dbo.Equipments", "SCESubCategory_ID");
            DropColumn("dbo.Equipments", "SCECategory_ID");
            DropColumn("dbo.WorkOrders", "IsDeviationNotApproved");
            DropColumn("dbo.WorkOrders", "IsDeviationApproved");
            DropTable("dbo.SCESubCategories");
            DropTable("dbo.SCECategories");
        }
    }
}
