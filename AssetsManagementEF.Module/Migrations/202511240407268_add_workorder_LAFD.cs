namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_workorder_LAFD : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkOrders", "OriginalOLAFD", c => c.DateTime());
            AddColumn("dbo.WorkOrders", "ProposedLAFDValidDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WorkOrders", "ProposedLAFDValidDate");
            DropColumn("dbo.WorkOrders", "OriginalOLAFD");
        }
    }
}
