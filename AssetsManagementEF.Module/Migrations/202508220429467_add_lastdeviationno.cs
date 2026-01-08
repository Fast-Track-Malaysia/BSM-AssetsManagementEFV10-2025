namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_lastdeviationno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deviation2025", "LastDeviationNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deviation2025", "LastDeviationNo");
        }
    }
}
