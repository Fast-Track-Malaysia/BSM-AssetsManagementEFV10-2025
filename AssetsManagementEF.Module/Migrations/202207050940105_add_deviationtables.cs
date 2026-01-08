namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_deviationtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeviationStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeviationWorkOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowNumber = c.Int(nullable: false),
                        DeviationNo = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        DeviationStatus_ID = c.Int(),
                        DeviationType_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.DeviationStatus", t => t.DeviationStatus_ID)
                .ForeignKey("dbo.DeviationWOTypes", t => t.DeviationType_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.DeviationStatus_ID)
                .Index(t => t.DeviationType_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.DeviationWOTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeviationWorkRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowNumber = c.Int(nullable: false),
                        DeviationNo = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        DeviationStatus_ID = c.Int(),
                        DeviationType_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.DeviationStatus", t => t.DeviationStatus_ID)
                .ForeignKey("dbo.DeviationWRTypes", t => t.DeviationType_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.DeviationStatus_ID)
                .Index(t => t.DeviationType_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkRequest_ID);
            
            CreateTable(
                "dbo.DeviationWRTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeviationWorkRequests", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.DeviationWorkRequests", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationWorkRequests", "DeviationType_ID", "dbo.DeviationWRTypes");
            DropForeignKey("dbo.DeviationWorkRequests", "DeviationStatus_ID", "dbo.DeviationStatus");
            DropForeignKey("dbo.DeviationWorkRequests", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationWorkOrders", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.DeviationWorkOrders", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationWorkOrders", "DeviationType_ID", "dbo.DeviationWOTypes");
            DropForeignKey("dbo.DeviationWorkOrders", "DeviationStatus_ID", "dbo.DeviationStatus");
            DropForeignKey("dbo.DeviationWorkOrders", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropIndex("dbo.DeviationWorkRequests", new[] { "WorkRequest_ID" });
            DropIndex("dbo.DeviationWorkRequests", new[] { "UpdateUser_ID" });
            DropIndex("dbo.DeviationWorkRequests", new[] { "DeviationType_ID" });
            DropIndex("dbo.DeviationWorkRequests", new[] { "DeviationStatus_ID" });
            DropIndex("dbo.DeviationWorkRequests", new[] { "CreateUser_ID" });
            DropIndex("dbo.DeviationWorkOrders", new[] { "WorkOrder_ID" });
            DropIndex("dbo.DeviationWorkOrders", new[] { "UpdateUser_ID" });
            DropIndex("dbo.DeviationWorkOrders", new[] { "DeviationType_ID" });
            DropIndex("dbo.DeviationWorkOrders", new[] { "DeviationStatus_ID" });
            DropIndex("dbo.DeviationWorkOrders", new[] { "CreateUser_ID" });
            DropTable("dbo.DeviationWRTypes");
            DropTable("dbo.DeviationWorkRequests");
            DropTable("dbo.DeviationWOTypes");
            DropTable("dbo.DeviationWorkOrders");
            DropTable("dbo.DeviationStatus");
        }
    }
}
