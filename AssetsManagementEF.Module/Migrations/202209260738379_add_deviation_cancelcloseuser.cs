namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_deviation_cancelcloseuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviationWorkRequests", "CloseDate", c => c.DateTime());
            AddColumn("dbo.DeviationWorkRequests", "CancelDate", c => c.DateTime());
            AddColumn("dbo.DeviationWorkRequests", "CancelUser_ID", c => c.Int());
            AddColumn("dbo.DeviationWorkRequests", "CloseUser_ID", c => c.Int());
            AddColumn("dbo.DeviationWorkOrders", "CloseDate", c => c.DateTime());
            AddColumn("dbo.DeviationWorkOrders", "CancelDate", c => c.DateTime());
            AddColumn("dbo.DeviationWorkOrders", "CancelUser_ID", c => c.Int());
            AddColumn("dbo.DeviationWorkOrders", "CloseUser_ID", c => c.Int());
            CreateIndex("dbo.DeviationWorkRequests", "CancelUser_ID");
            CreateIndex("dbo.DeviationWorkRequests", "CloseUser_ID");
            CreateIndex("dbo.DeviationWorkOrders", "CancelUser_ID");
            CreateIndex("dbo.DeviationWorkOrders", "CloseUser_ID");
            AddForeignKey("dbo.DeviationWorkRequests", "CancelUser_ID", "dbo.PermissionPolicyUsers", "ID");
            AddForeignKey("dbo.DeviationWorkRequests", "CloseUser_ID", "dbo.PermissionPolicyUsers", "ID");
            AddForeignKey("dbo.DeviationWorkOrders", "CancelUser_ID", "dbo.PermissionPolicyUsers", "ID");
            AddForeignKey("dbo.DeviationWorkOrders", "CloseUser_ID", "dbo.PermissionPolicyUsers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviationWorkOrders", "CloseUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationWorkOrders", "CancelUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationWorkRequests", "CloseUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationWorkRequests", "CancelUser_ID", "dbo.PermissionPolicyUsers");
            DropIndex("dbo.DeviationWorkOrders", new[] { "CloseUser_ID" });
            DropIndex("dbo.DeviationWorkOrders", new[] { "CancelUser_ID" });
            DropIndex("dbo.DeviationWorkRequests", new[] { "CloseUser_ID" });
            DropIndex("dbo.DeviationWorkRequests", new[] { "CancelUser_ID" });
            DropColumn("dbo.DeviationWorkOrders", "CloseUser_ID");
            DropColumn("dbo.DeviationWorkOrders", "CancelUser_ID");
            DropColumn("dbo.DeviationWorkOrders", "CancelDate");
            DropColumn("dbo.DeviationWorkOrders", "CloseDate");
            DropColumn("dbo.DeviationWorkRequests", "CloseUser_ID");
            DropColumn("dbo.DeviationWorkRequests", "CancelUser_ID");
            DropColumn("dbo.DeviationWorkRequests", "CancelDate");
            DropColumn("dbo.DeviationWorkRequests", "CloseDate");
        }
    }
}
