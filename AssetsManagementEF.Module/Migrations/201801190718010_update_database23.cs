namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database23 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchaseRequestAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachFile_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        PurchaseRequest_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.AttachFile_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequest_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.AttachFile_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.PurchaseRequest_ID)
                .Index(t => t.UpdateUser_ID);
            
            AddColumn("dbo.PMPatches", "CreateDate", c => c.DateTime());
            AddColumn("dbo.PMPatches", "CreateUser_ID", c => c.Int());
            CreateIndex("dbo.PMPatches", "CreateUser_ID");
            AddForeignKey("dbo.PMPatches", "CreateUser_ID", "dbo.PermissionPolicyUsers", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PMPatches", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequestAttachments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequestAttachments", "PurchaseRequest_ID", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequestAttachments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequestAttachments", "AttachFile_ID", "dbo.FileDatas");
            DropIndex("dbo.PMPatches", new[] { "CreateUser_ID" });
            DropIndex("dbo.PurchaseRequestAttachments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PurchaseRequestAttachments", new[] { "PurchaseRequest_ID" });
            DropIndex("dbo.PurchaseRequestAttachments", new[] { "CreateUser_ID" });
            DropIndex("dbo.PurchaseRequestAttachments", new[] { "AttachFile_ID" });
            DropColumn("dbo.PMPatches", "CreateUser_ID");
            DropColumn("dbo.PMPatches", "CreateDate");
            DropTable("dbo.PurchaseRequestAttachments");
        }
    }
}
