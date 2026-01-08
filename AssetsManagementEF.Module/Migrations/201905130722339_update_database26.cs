namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "RescheduleStartDate", c => c.DateTime());
            AddColumn("dbo.WorkOrders", "RescheduleEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrders", "RescheduleEndDate");
            DropColumn("dbo.WorkOrders", "RescheduleStartDate");
        }
    }
}
