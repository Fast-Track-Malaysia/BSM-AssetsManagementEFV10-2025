namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database32 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "FullCode", c => c.String());
            AddColumn("dbo.EquipmentComponents", "FullCode", c => c.String());
            AddColumn("dbo.EquipmentComponents", "ComponentFullCode", c => c.String());
            AddColumn("dbo.PMSchedules", "FullCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMSchedules", "FullCode");
            DropColumn("dbo.EquipmentComponents", "ComponentFullCode");
            DropColumn("dbo.EquipmentComponents", "FullCode");
            DropColumn("dbo.Equipments", "FullCode");
        }
    }
}
