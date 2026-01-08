namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMScheduleEquipments", "IsCombine", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMScheduleEquipments", "IsCombine");
        }
    }
}
