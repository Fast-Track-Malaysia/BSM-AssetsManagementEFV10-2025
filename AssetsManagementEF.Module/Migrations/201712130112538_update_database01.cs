namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PMScheduleChecklists", "CheckListLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PMScheduleChecklists", "CheckListLink");
        }
    }
}
