namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkRequestDocPGs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        NewPlannerGroup_ID = c.Int(),
                        OldPlannerGroup_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.NewPlannerGroup_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.OldPlannerGroup_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.NewPlannerGroup_ID)
                .Index(t => t.OldPlannerGroup_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkRequest_ID);
            
            CreateTable(
                "dbo.WorkOrderDocPGs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        NewPlannerGroup_ID = c.Int(),
                        OldPlannerGroup_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.NewPlannerGroup_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.OldPlannerGroup_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.NewPlannerGroup_ID)
                .Index(t => t.OldPlannerGroup_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            AddColumn("dbo.PurchaseRequests", "Approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrderDocPGs", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderDocPGs", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderDocPGs", "OldPlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.WorkOrderDocPGs", "NewPlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.WorkOrderDocPGs", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestDocPGs", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequestDocPGs", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestDocPGs", "OldPlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.WorkRequestDocPGs", "NewPlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.WorkRequestDocPGs", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropIndex("dbo.WorkOrderDocPGs", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderDocPGs", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderDocPGs", new[] { "OldPlannerGroup_ID" });
            DropIndex("dbo.WorkOrderDocPGs", new[] { "NewPlannerGroup_ID" });
            DropIndex("dbo.WorkOrderDocPGs", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequestDocPGs", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkRequestDocPGs", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequestDocPGs", new[] { "OldPlannerGroup_ID" });
            DropIndex("dbo.WorkRequestDocPGs", new[] { "NewPlannerGroup_ID" });
            DropIndex("dbo.WorkRequestDocPGs", new[] { "CreateUser_ID" });
            DropColumn("dbo.PurchaseRequests", "Approved");
            DropTable("dbo.WorkOrderDocPGs");
            DropTable("dbo.WorkRequestDocPGs");
        }
    }
}
