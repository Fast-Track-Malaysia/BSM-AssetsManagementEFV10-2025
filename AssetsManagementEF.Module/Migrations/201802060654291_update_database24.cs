namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database24 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "ActualStartDate", c => c.DateTime());
            AddColumn("dbo.WorkOrders", "ActualEndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrders", "ActualEndDate");
            DropColumn("dbo.WorkOrders", "ActualStartDate");
        }
    }
}
