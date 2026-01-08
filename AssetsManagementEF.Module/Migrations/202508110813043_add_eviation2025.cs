namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_eviation2025 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deviation2025",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UpdateDate = c.DateTime(),
                        DocNumSeq = c.Long(nullable: false),
                        DocNum = c.String(),
                        DeviationRank = c.Int(nullable: false),
                        LastDeviationRank = c.Int(nullable: false),
                        ApprovedValidDate = c.DateTime(),
                        DeviationTitle = c.String(),
                        OriginalOLAFD = c.DateTime(),
                        ProposedLAFDValidDate = c.DateTime(),
                        CreateDate = c.DateTime(),
                        Dscription = c.String(),
                        RiskAssessment = c.String(),
                        RiskComment = c.String(),
                        RiskIsolationID = c.String(),
                        RiskActionPlan = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Assets_ID = c.Int(),
                        Community_ID = c.Int(),
                        Company_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        Criticality_ID = c.Int(),
                        DeviationDiscipline_ID = c.Int(),
                        DeviationStatus_ID = c.Int(),
                        DeviationType_ID = c.Int(),
                        DocType_ID = c.Int(),
                        Location_ID = c.Int(),
                        OverallRisk_ID = c.Int(),
                        People1_ID = c.Int(),
                        People2_ID = c.Int(),
                        SCECategory_ID = c.Int(),
                        SCESubCategory_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RiskAssets", t => t.Assets_ID)
                .ForeignKey("dbo.RiskCommunities", t => t.Community_ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Criticalities", t => t.Criticality_ID)
                .ForeignKey("dbo.DeviationDisciplines", t => t.DeviationDiscipline_ID)
                .ForeignKey("dbo.DeviationStatus", t => t.DeviationStatus_ID)
                .ForeignKey("dbo.DeviationTypes", t => t.DeviationType_ID)
                .ForeignKey("dbo.DocTypes", t => t.DocType_ID)
                .ForeignKey("dbo.DeviationLocations", t => t.Location_ID)
                .ForeignKey("dbo.Risks", t => t.OverallRisk_ID)
                .ForeignKey("dbo.RiskPeoples", t => t.People1_ID)
                .ForeignKey("dbo.RiskPeoples", t => t.People2_ID)
                .ForeignKey("dbo.SCECategories", t => t.SCECategory_ID)
                .ForeignKey("dbo.SCESubCategories", t => t.SCESubCategory_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.Assets_ID)
                .Index(t => t.Community_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Criticality_ID)
                .Index(t => t.DeviationDiscipline_ID)
                .Index(t => t.DeviationStatus_ID)
                .Index(t => t.DeviationType_ID)
                .Index(t => t.DocType_ID)
                .Index(t => t.Location_ID)
                .Index(t => t.OverallRisk_ID)
                .Index(t => t.People1_ID)
                .Index(t => t.People2_ID)
                .Index(t => t.SCECategory_ID)
                .Index(t => t.SCESubCategory_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.RiskAssets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RiskCommunities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeviationMitigations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowNumber = c.Int(nullable: false),
                        Dscription = c.String(),
                        Reason = c.String(),
                        AcceptanceCriteria = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        CloseDate = c.DateTime(),
                        CancelDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CancelUser_ID = c.Int(),
                        CloseUser_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        Deviation_ID = c.Int(),
                        Frequency_ID = c.Int(),
                        Position_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CancelUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CloseUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Deviation2025", t => t.Deviation_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.Frequency_ID)
                .ForeignKey("dbo.Positions", t => t.Position_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CancelUser_ID)
                .Index(t => t.CloseUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Deviation_ID)
                .Index(t => t.Frequency_ID)
                .Index(t => t.Position_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.DeviationReviewers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowNumber = c.Int(nullable: false),
                        PositionRole = c.String(),
                        KeyReviewer = c.Boolean(nullable: false),
                        ActReviewer = c.Boolean(nullable: false),
                        ReviewDate = c.DateTime(nullable: false),
                        Comments = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Deviation_ID = c.Int(),
                        Position_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Deviation2025", t => t.Deviation_ID)
                .ForeignKey("dbo.Positions", t => t.Position_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Deviation_ID)
                .Index(t => t.Position_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.DeviationAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Dscription = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachFile_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        Deviation_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.AttachFile_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Deviation2025", t => t.Deviation_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.AttachFile_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Deviation_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.DeviationDocStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocRemarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Deviation_ID = c.Int(),
                        DocStatus_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Deviation2025", t => t.Deviation_ID)
                .ForeignKey("dbo.DeviationStatus", t => t.DocStatus_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Deviation_ID)
                .Index(t => t.DocStatus_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.DeviationDisciplines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeviationTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DeviationLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Risks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RiskPeoples",
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
            DropForeignKey("dbo.Deviation2025", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.Deviation2025", "SCESubCategory_ID", "dbo.SCESubCategories");
            DropForeignKey("dbo.Deviation2025", "SCECategory_ID", "dbo.SCECategories");
            DropForeignKey("dbo.Deviation2025", "People2_ID", "dbo.RiskPeoples");
            DropForeignKey("dbo.Deviation2025", "People1_ID", "dbo.RiskPeoples");
            DropForeignKey("dbo.Deviation2025", "OverallRisk_ID", "dbo.Risks");
            DropForeignKey("dbo.Deviation2025", "Location_ID", "dbo.DeviationLocations");
            DropForeignKey("dbo.Deviation2025", "DocType_ID", "dbo.DocTypes");
            DropForeignKey("dbo.Deviation2025", "DeviationType_ID", "dbo.DeviationTypes");
            DropForeignKey("dbo.Deviation2025", "DeviationStatus_ID", "dbo.DeviationStatus");
            DropForeignKey("dbo.Deviation2025", "DeviationDiscipline_ID", "dbo.DeviationDisciplines");
            DropForeignKey("dbo.DeviationDocStatus", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationDocStatus", "DocStatus_ID", "dbo.DeviationStatus");
            DropForeignKey("dbo.DeviationDocStatus", "Deviation_ID", "dbo.Deviation2025");
            DropForeignKey("dbo.DeviationDocStatus", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationAttachments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationAttachments", "Deviation_ID", "dbo.Deviation2025");
            DropForeignKey("dbo.DeviationAttachments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationAttachments", "AttachFile_ID", "dbo.FileDatas");
            DropForeignKey("dbo.DeviationReviewers", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationReviewers", "Position_ID", "dbo.Positions");
            DropForeignKey("dbo.DeviationReviewers", "Deviation_ID", "dbo.Deviation2025");
            DropForeignKey("dbo.DeviationReviewers", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationMitigations", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationMitigations", "Position_ID", "dbo.Positions");
            DropForeignKey("dbo.DeviationMitigations", "Frequency_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.DeviationMitigations", "Deviation_ID", "dbo.Deviation2025");
            DropForeignKey("dbo.DeviationMitigations", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationMitigations", "CloseUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.DeviationMitigations", "CancelUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.Deviation2025", "Criticality_ID", "dbo.Criticalities");
            DropForeignKey("dbo.Deviation2025", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.Deviation2025", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.Deviation2025", "Community_ID", "dbo.RiskCommunities");
            DropForeignKey("dbo.Deviation2025", "Assets_ID", "dbo.RiskAssets");
            DropIndex("dbo.DeviationDocStatus", new[] { "UpdateUser_ID" });
            DropIndex("dbo.DeviationDocStatus", new[] { "DocStatus_ID" });
            DropIndex("dbo.DeviationDocStatus", new[] { "Deviation_ID" });
            DropIndex("dbo.DeviationDocStatus", new[] { "CreateUser_ID" });
            DropIndex("dbo.DeviationAttachments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.DeviationAttachments", new[] { "Deviation_ID" });
            DropIndex("dbo.DeviationAttachments", new[] { "CreateUser_ID" });
            DropIndex("dbo.DeviationAttachments", new[] { "AttachFile_ID" });
            DropIndex("dbo.DeviationReviewers", new[] { "UpdateUser_ID" });
            DropIndex("dbo.DeviationReviewers", new[] { "Position_ID" });
            DropIndex("dbo.DeviationReviewers", new[] { "Deviation_ID" });
            DropIndex("dbo.DeviationReviewers", new[] { "CreateUser_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "UpdateUser_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "Position_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "Frequency_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "Deviation_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "CreateUser_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "CloseUser_ID" });
            DropIndex("dbo.DeviationMitigations", new[] { "CancelUser_ID" });
            DropIndex("dbo.Deviation2025", new[] { "WorkOrder_ID" });
            DropIndex("dbo.Deviation2025", new[] { "UpdateUser_ID" });
            DropIndex("dbo.Deviation2025", new[] { "SCESubCategory_ID" });
            DropIndex("dbo.Deviation2025", new[] { "SCECategory_ID" });
            DropIndex("dbo.Deviation2025", new[] { "People2_ID" });
            DropIndex("dbo.Deviation2025", new[] { "People1_ID" });
            DropIndex("dbo.Deviation2025", new[] { "OverallRisk_ID" });
            DropIndex("dbo.Deviation2025", new[] { "Location_ID" });
            DropIndex("dbo.Deviation2025", new[] { "DocType_ID" });
            DropIndex("dbo.Deviation2025", new[] { "DeviationType_ID" });
            DropIndex("dbo.Deviation2025", new[] { "DeviationStatus_ID" });
            DropIndex("dbo.Deviation2025", new[] { "DeviationDiscipline_ID" });
            DropIndex("dbo.Deviation2025", new[] { "Criticality_ID" });
            DropIndex("dbo.Deviation2025", new[] { "CreateUser_ID" });
            DropIndex("dbo.Deviation2025", new[] { "Company_ID" });
            DropIndex("dbo.Deviation2025", new[] { "Community_ID" });
            DropIndex("dbo.Deviation2025", new[] { "Assets_ID" });
            DropTable("dbo.RiskPeoples");
            DropTable("dbo.Risks");
            DropTable("dbo.DeviationLocations");
            DropTable("dbo.DeviationTypes");
            DropTable("dbo.DeviationDisciplines");
            DropTable("dbo.DeviationDocStatus");
            DropTable("dbo.DeviationAttachments");
            DropTable("dbo.DeviationReviewers");
            DropTable("dbo.DeviationMitigations");
            DropTable("dbo.RiskCommunities");
            DropTable("dbo.RiskAssets");
            DropTable("dbo.Deviation2025");
        }
    }
}
