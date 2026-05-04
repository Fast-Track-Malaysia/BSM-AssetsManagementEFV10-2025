namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_costcenter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CostCentres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        Section = c.String(),
                        Department = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EquipmentComponentAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachFile_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        EquipmentComponent_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.AttachFile_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.AttachFile_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.EquipmentComponent_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.EquipmentComponentParts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RowNo = c.Int(nullable: false),
                        PartDescription = c.String(),
                        PartNumber = c.String(),
                        PartReference = c.String(),
                        Quantity = c.Int(nullable: false),
                        PartUOM = c.String(),
                        LeadTime = c.Time(nullable: false, precision: 7),
                        PartRemarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EquipmentComponent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .Index(t => t.EquipmentComponent_ID);
            
            CreateTable(
                "dbo.EquipmentComponentPhotos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachPhoto_Id = c.Int(),
                        CreateUser_ID = c.Int(),
                        EquipmentComponent_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaDataObjects", t => t.AttachPhoto_Id)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.AttachPhoto_Id)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.EquipmentComponent_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.EquipmentParts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RowNo = c.Int(nullable: false),
                        PartDescription = c.String(),
                        PartNumber = c.String(),
                        PartReference = c.String(),
                        Quantity = c.Int(nullable: false),
                        PartUOM = c.String(),
                        LeadTime = c.Time(nullable: false, precision: 7),
                        PartRemarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Equipment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .Index(t => t.Equipment_ID);
            
            AddColumn("dbo.WorkOrders", "MoCRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkOrders", "SapPONo", c => c.String());
            AddColumn("dbo.WorkOrders", "DeviationNo", c => c.String());
            AddColumn("dbo.WorkOrders", "DeviationRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkOrders", "CostCentre_ID", c => c.Int());
            AddColumn("dbo.Equipments", "Specification", c => c.String());
            AddColumn("dbo.EquipmentComponents", "Specification", c => c.String());
            AddColumn("dbo.WorkRequests", "MoCRequired", c => c.Boolean(nullable: false));
            AddColumn("dbo.WorkRequests", "CostCentre_ID", c => c.Int());
            CreateIndex("dbo.WorkOrders", "CostCentre_ID");
            CreateIndex("dbo.WorkRequests", "CostCentre_ID");
            AddForeignKey("dbo.WorkOrders", "CostCentre_ID", "dbo.CostCentres", "ID");
            AddForeignKey("dbo.WorkRequests", "CostCentre_ID", "dbo.CostCentres", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EquipmentParts", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.WorkRequests", "CostCentre_ID", "dbo.CostCentres");
            DropForeignKey("dbo.EquipmentComponentPhotos", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentComponentPhotos", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.EquipmentComponentPhotos", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentComponentPhotos", "AttachPhoto_Id", "dbo.MediaDataObjects");
            DropForeignKey("dbo.EquipmentComponentParts", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.EquipmentComponentAttachments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentComponentAttachments", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.EquipmentComponentAttachments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentComponentAttachments", "AttachFile_ID", "dbo.FileDatas");
            DropForeignKey("dbo.WorkOrders", "CostCentre_ID", "dbo.CostCentres");
            DropIndex("dbo.EquipmentParts", new[] { "Equipment_ID" });
            DropIndex("dbo.WorkRequests", new[] { "CostCentre_ID" });
            DropIndex("dbo.EquipmentComponentPhotos", new[] { "UpdateUser_ID" });
            DropIndex("dbo.EquipmentComponentPhotos", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.EquipmentComponentPhotos", new[] { "CreateUser_ID" });
            DropIndex("dbo.EquipmentComponentPhotos", new[] { "AttachPhoto_Id" });
            DropIndex("dbo.EquipmentComponentParts", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.EquipmentComponentAttachments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.EquipmentComponentAttachments", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.EquipmentComponentAttachments", new[] { "CreateUser_ID" });
            DropIndex("dbo.EquipmentComponentAttachments", new[] { "AttachFile_ID" });
            DropIndex("dbo.WorkOrders", new[] { "CostCentre_ID" });
            DropColumn("dbo.WorkRequests", "CostCentre_ID");
            DropColumn("dbo.WorkRequests", "MoCRequired");
            DropColumn("dbo.EquipmentComponents", "Specification");
            DropColumn("dbo.Equipments", "Specification");
            DropColumn("dbo.WorkOrders", "CostCentre_ID");
            DropColumn("dbo.WorkOrders", "DeviationRequired");
            DropColumn("dbo.WorkOrders", "DeviationNo");
            DropColumn("dbo.WorkOrders", "SapPONo");
            DropColumn("dbo.WorkOrders", "MoCRequired");
            DropTable("dbo.EquipmentParts");
            DropTable("dbo.EquipmentComponentPhotos");
            DropTable("dbo.EquipmentComponentParts");
            DropTable("dbo.EquipmentComponentAttachments");
            DropTable("dbo.CostCentres");
        }
    }
}
