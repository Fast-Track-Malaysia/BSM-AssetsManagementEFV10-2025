namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_deviation_eq : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deviation2025", "Component_ID", c => c.Int());
            AddColumn("dbo.Deviation2025", "Equipment_ID", c => c.Int());
            CreateIndex("dbo.Deviation2025", "Component_ID");
            CreateIndex("dbo.Deviation2025", "Equipment_ID");
            AddForeignKey("dbo.Deviation2025", "Component_ID", "dbo.EquipmentComponents", "ID");
            AddForeignKey("dbo.Deviation2025", "Equipment_ID", "dbo.Equipments", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Deviation2025", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.Deviation2025", "Component_ID", "dbo.EquipmentComponents");
            DropIndex("dbo.Deviation2025", new[] { "Equipment_ID" });
            DropIndex("dbo.Deviation2025", new[] { "Component_ID" });
            DropColumn("dbo.Deviation2025", "Equipment_ID");
            DropColumn("dbo.Deviation2025", "Component_ID");
        }
    }
}
