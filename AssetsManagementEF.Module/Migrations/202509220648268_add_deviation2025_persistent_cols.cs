namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_deviation2025_persistent_cols : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deviation2025", "PlannerGroup", c => c.String());
            AddColumn("dbo.Deviation2025", "WorkOrderType", c => c.String());
            AddColumn("dbo.Deviation2025", "WorkOrderPriority", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deviation2025", "WorkOrderPriority");
            DropColumn("dbo.Deviation2025", "WorkOrderType");
            DropColumn("dbo.Deviation2025", "PlannerGroup");
        }
    }
}
