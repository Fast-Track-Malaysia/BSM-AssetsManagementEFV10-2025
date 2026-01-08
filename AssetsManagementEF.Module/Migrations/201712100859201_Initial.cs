namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CheckLists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        File_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.File_ID)
                .Index(t => t.File_ID);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        PrefixQuot = c.String(),
                        NextQuotNo = c.Long(nullable: false),
                        NextAssetsNo = c.Long(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompanyDocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NextDocNo = c.Long(nullable: false),
                        Company_ID = c.Int(),
                        DocType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.DocTypes", t => t.DocType_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.DocType_ID);
            
            CreateTable(
                "dbo.DocTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        NextBoNo = c.Long(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContractDocDtls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ItemDesc = c.String(),
                        QTY = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        ContractDoc_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        ItemMaster_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ContractDocs", t => t.ContractDoc_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.ItemMasters", t => t.ItemMaster_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.ContractDoc_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.ItemMaster_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.ContractDocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocNumSeq = c.Long(nullable: false),
                        DocNum = c.String(),
                        DocDate = c.DateTime(nullable: false),
                        RefNo = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Contractor_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Contractors", t => t.Contractor_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.Contractor_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        DueDate = c.DateTime(nullable: false),
                        AlarmTime = c.DateTime(),
                        IsPostponed = c.Boolean(nullable: false),
                        RemindIn = c.Time(precision: 7),
                        AssignedTo_ID = c.Int(),
                        PurchaseRequest_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.AssignedTo_ID)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequest_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.AssignedTo_ID)
                .Index(t => t.PurchaseRequest_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.PurchaseRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocNumSeq = c.Long(nullable: false),
                        DocNum = c.String(),
                        DocDate = c.DateTime(nullable: false),
                        RefNo = c.String(),
                        DocPassed = c.Boolean(nullable: false),
                        Cancelled = c.Boolean(nullable: false),
                        Rejected = c.Boolean(nullable: false),
                        DocPosted = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Company_ID = c.Int(),
                        ContractDoc_ID = c.Int(),
                        Contractor_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        DocType_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.ContractDocs", t => t.ContractDoc_ID)
                .ForeignKey("dbo.Contractors", t => t.Contractor_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.DocTypes", t => t.DocType_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.ContractDoc_ID)
                .Index(t => t.Contractor_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.DocType_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.PurchaseRequestDtls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ItemDesc = c.String(),
                        QTY = c.Double(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        ItemMaster_ID = c.Int(),
                        PurchaseRequest_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.ItemMasters", t => t.ItemMaster_ID)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequest_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.ItemMaster_ID)
                .Index(t => t.PurchaseRequest_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.ItemMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PurchaseRequestDocStatuses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocStatus = c.Int(nullable: false),
                        DocRemarks = c.String(),
                        IsReverse = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        PurchaseRequest_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PurchaseRequests", t => t.PurchaseRequest_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.PurchaseRequest_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocNumSeq = c.Long(nullable: false),
                        DocNum = c.String(),
                        DocDate = c.DateTime(nullable: false),
                        PMDate = c.DateTime(),
                        RefNo = c.String(),
                        Remarks = c.String(),
                        PlanManCount = c.Int(nullable: false),
                        PlanManHour = c.Time(precision: 7),
                        PlanStartDate = c.DateTime(),
                        PlanEndDate = c.DateTime(),
                        ScheduleStartDate = c.DateTime(),
                        ScheduleEndDate = c.DateTime(),
                        CheckListName = c.String(),
                        CheckListLink = c.String(),
                        WorkInstruction = c.String(),
                        DocPassed = c.Boolean(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Cancelled = c.Boolean(nullable: false),
                        Rejected = c.Boolean(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        IsPreventiveMaintenance = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AssignPlannerGroup_ID = c.Int(),
                        CheckList_ID = c.Int(),
                        Company_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                        DocType_ID = c.Int(),
                        JobStatus_ID = c.Int(),
                        PMClass_ID = c.Int(),
                        PMSchedule_ID = c.Int(),
                        Priority_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkDescription_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PlannerGroups", t => t.AssignPlannerGroup_ID)
                .ForeignKey("dbo.CheckLists", t => t.CheckList_ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .ForeignKey("dbo.DocTypes", t => t.DocType_ID)
                .ForeignKey("dbo.JobStatuses", t => t.JobStatus_ID)
                .ForeignKey("dbo.PMClasses", t => t.PMClass_ID)
                .ForeignKey("dbo.PMSchedules", t => t.PMSchedule_ID)
                .ForeignKey("dbo.Priorities", t => t.Priority_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WOLongDescriptions", t => t.WorkDescription_ID)
                .Index(t => t.AssignPlannerGroup_ID)
                .Index(t => t.CheckList_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.WorkRequest_ID)
                .Index(t => t.DocType_ID)
                .Index(t => t.JobStatus_ID)
                .Index(t => t.PMClass_ID)
                .Index(t => t.PMSchedule_ID)
                .Index(t => t.Priority_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkDescription_ID);
            
            CreateTable(
                "dbo.PlannerGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMClasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        IsPlanner = c.Boolean(nullable: false),
                        IsApprover = c.Boolean(nullable: false),
                        IsWPS = c.Boolean(nullable: false),
                        IsPreventiveMaintenance = c.Boolean(nullable: false),
                        IsCorrectiveMaintenance = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CurrentUser_ID = c.Int(),
                        PlannerGroup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CurrentUser_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.PlannerGroup_ID)
                .Index(t => t.CurrentUser_ID)
                .Index(t => t.PlannerGroup_ID);
            
            CreateTable(
                "dbo.WorkOrderEquipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.WorkOrderEqComponents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        EquipmentComponent_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                        WorkOrderEquipment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .ForeignKey("dbo.WorkOrderEquipments", t => t.WorkOrderEquipment_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.EquipmentComponent_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID)
                .Index(t => t.WorkOrderEquipment_ID);
            
            CreateTable(
                "dbo.WorkOrderEqComponentOps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        FDate = c.DateTime(),
                        TDate = c.DateTime(),
                        ManHours = c.Long(nullable: false),
                        Operation = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        IsCancel = c.Boolean(nullable: false),
                        CpDownTime = c.DateTime(nullable: false),
                        CpUpTime = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Technician_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrderEqComponent_ID = c.Int(),
                        WorkOrderOpType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Technicians", t => t.Technician_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrderEqComponents", t => t.WorkOrderEqComponent_ID)
                .ForeignKey("dbo.WorkOrderOpTypes", t => t.WorkOrderOpType_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Technician_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrderEqComponent_ID)
                .Index(t => t.WorkOrderOpType_ID);
            
            CreateTable(
                "dbo.Technicians",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WorkOrderOpTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        IsDown = c.Boolean(nullable: false),
                        IsUp = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        BoInt = c.Int(nullable: false),
                        BoCode = c.String(),
                        BoName = c.String(),
                        Legacy = c.String(),
                        BoFullCode = c.String(),
                        Model = c.String(),
                        SerialNo = c.String(),
                        Make = c.String(),
                        LifeSpan = c.String(),
                        Manufacture = c.String(),
                        Remarks = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Area_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        Criticality_ID = c.Int(),
                        EqClass_ID = c.Int(),
                        EqGroup_ID = c.Int(),
                        Location_ID = c.Int(),
                        SubLocation_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Criticalities", t => t.Criticality_ID)
                .ForeignKey("dbo.EqClasses", t => t.EqClass_ID)
                .ForeignKey("dbo.EqGroups", t => t.EqGroup_ID)
                .ForeignKey("dbo.Locations", t => t.Location_ID)
                .ForeignKey("dbo.SubLocations", t => t.SubLocation_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.Area_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Criticality_ID)
                .Index(t => t.EqClass_ID)
                .Index(t => t.EqGroup_ID)
                .Index(t => t.Location_ID)
                .Index(t => t.SubLocation_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.Criticalities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        LevelOfCriticality = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EqClasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        NextBoNo = c.Long(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EqGroup_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EqGroups", t => t.EqGroup_ID)
                .Index(t => t.EqGroup_ID);
            
            CreateTable(
                "dbo.EqClassProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EqGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EquipmentAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachFile_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.AttachFile_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.AttachFile_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.EquipmentComponents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        BoInt = c.Int(nullable: false),
                        BoCode = c.String(),
                        BoName = c.String(),
                        Legacy = c.String(),
                        Model = c.String(),
                        SerialNo = c.String(),
                        Make = c.String(),
                        LifeSpan = c.String(),
                        Manufacture = c.String(),
                        Remarks = c.String(),
                        BoFullCode = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Criticality_ID = c.Int(),
                        EqComponentClass_ID = c.Int(),
                        EqComponentGroup_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Criticalities", t => t.Criticality_ID)
                .ForeignKey("dbo.EqComponentClasses", t => t.EqComponentClass_ID)
                .ForeignKey("dbo.EqComponentGroups", t => t.EqComponentGroup_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Criticality_ID)
                .Index(t => t.EqComponentClass_ID)
                .Index(t => t.EqComponentGroup_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.EqComponentClasses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        NextBoNo = c.Long(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EqComponentGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EquipmentPhotos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachPhoto_Id = c.Int(),
                        CreateUser_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaDataObjects", t => t.AttachPhoto_Id)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.AttachPhoto_Id)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.MediaDataObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaDataKey = c.String(),
                        MediaResource_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MediaResourceObjects", t => t.MediaResource_Id)
                .Index(t => t.MediaResource_Id);
            
            CreateTable(
                "dbo.MediaResourceObjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MediaData = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EquipmentProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        EqClassProperty_ID = c.Int(),
                        Equipment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EqClassProperties", t => t.EqClassProperty_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .Index(t => t.EqClassProperty_ID)
                .Index(t => t.Equipment_ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Area_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .Index(t => t.Area_ID);
            
            CreateTable(
                "dbo.PMScheduleEquipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        PMSchedule_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PMSchedules", t => t.PMSchedule_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.PMSchedule_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.PMScheduleEqComponents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        EquipmentComponent_ID = c.Int(),
                        PMSchedule_ID = c.Int(),
                        PMScheduleEquipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .ForeignKey("dbo.PMSchedules", t => t.PMSchedule_ID)
                .ForeignKey("dbo.PMScheduleEquipments", t => t.PMScheduleEquipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.EquipmentComponent_ID)
                .Index(t => t.PMSchedule_ID)
                .Index(t => t.PMScheduleEquipment_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.PMSchedules",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoInt = c.Int(nullable: false),
                        BoFullCode = c.String(),
                        BoCode = c.String(),
                        BoName = c.String(),
                        PMDescription = c.String(),
                        FromDate = c.DateTime(),
                        CheckListName = c.String(),
                        CheckListLink = c.String(),
                        BufferMonth = c.Int(nullable: false),
                        ReleaseWindow = c.Double(nullable: false),
                        WorkInstruction = c.String(),
                        PlanManCount = c.Int(nullable: false),
                        PlanManHour = c.Time(precision: 7),
                        IsNested = c.Boolean(nullable: false),
                        IsConsistent = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CheckList_ID = c.Int(),
                        PlannerGroup_ID = c.Int(),
                        PMClass_ID = c.Int(),
                        PMDepartment_ID = c.Int(),
                        PMFrequency_ID = c.Int(),
                        Priority_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CheckLists", t => t.CheckList_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.PlannerGroup_ID)
                .ForeignKey("dbo.PMClasses", t => t.PMClass_ID)
                .ForeignKey("dbo.PMDepartments", t => t.PMDepartment_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMFrequency_ID)
                .ForeignKey("dbo.Priorities", t => t.Priority_ID)
                .Index(t => t.CheckList_ID)
                .Index(t => t.PlannerGroup_ID)
                .Index(t => t.PMClass_ID)
                .Index(t => t.PMDepartment_ID)
                .Index(t => t.PMFrequency_ID)
                .Index(t => t.Priority_ID);
            
            CreateTable(
                "dbo.PMDepartments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PMFrequencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        Frequency = c.Int(nullable: false),
                        CycleCount = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Priorities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoDescription = c.String(),
                        AllowedDayFrom = c.Int(nullable: false),
                        AllowedDayTo = c.Int(nullable: false),
                        RiskSourceFrom = c.Int(nullable: false),
                        RiskSourceTo = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SubLocations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        BoShortName = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WorkRequestEquipments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.WorkRequest_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.WorkRequestEqComponents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Equipment_ID = c.Int(),
                        EquipmentComponent_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                        WorkRequestEquipment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .ForeignKey("dbo.WorkRequestEquipments", t => t.WorkRequestEquipment_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.EquipmentComponent_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkRequest_ID)
                .Index(t => t.WorkRequestEquipment_ID);
            
            CreateTable(
                "dbo.WorkRequests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocNumSeq = c.Long(nullable: false),
                        DocNum = c.String(),
                        DocDate = c.DateTime(nullable: false),
                        RefNo = c.String(),
                        WRTargetDate = c.DateTime(),
                        Remarks = c.String(),
                        DocPassed = c.Boolean(nullable: false),
                        Approved = c.Boolean(nullable: false),
                        Cancelled = c.Boolean(nullable: false),
                        Rejected = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Company_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        DetailDescription_ID = c.Int(),
                        DocType_ID = c.Int(),
                        PlannerGroup_ID = c.Int(),
                        Priority_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.WRLongDescriptions", t => t.DetailDescription_ID)
                .ForeignKey("dbo.DocTypes", t => t.DocType_ID)
                .ForeignKey("dbo.PlannerGroups", t => t.PlannerGroup_ID)
                .ForeignKey("dbo.Priorities", t => t.Priority_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.DetailDescription_ID)
                .Index(t => t.DocType_ID)
                .Index(t => t.PlannerGroup_ID)
                .Index(t => t.Priority_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.WorkRequestAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachFile_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.AttachFile_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .Index(t => t.AttachFile_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkRequest_ID);
            
            CreateTable(
                "dbo.WRLongDescriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        LongDescription = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.WorkRequestDocStatuses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocStatus = c.Int(nullable: false),
                        DocRemarks = c.String(),
                        IsReverse = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkRequest_ID);
            
            CreateTable(
                "dbo.WorkRequestPhotos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachPhoto_Id = c.Int(),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkRequest_ID = c.Int(),
                        WorkOrders_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaDataObjects", t => t.AttachPhoto_Id)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkRequests", t => t.WorkRequest_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrders_ID)
                .Index(t => t.AttachPhoto_Id)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkRequest_ID)
                .Index(t => t.WorkOrders_ID);
            
            CreateTable(
                "dbo.WorkOrderEquipmentOps",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        FDate = c.DateTime(),
                        TDate = c.DateTime(),
                        ManHours = c.Long(nullable: false),
                        Operation = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        IsCancel = c.Boolean(nullable: false),
                        EqDownTime = c.DateTime(nullable: false),
                        EqUpTime = c.DateTime(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        Technician_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrderEquipment_ID = c.Int(),
                        WorkOrderOpType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.Technicians", t => t.Technician_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrderEquipments", t => t.WorkOrderEquipment_ID)
                .ForeignKey("dbo.WorkOrderOpTypes", t => t.WorkOrderOpType_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.Technician_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrderEquipment_ID)
                .Index(t => t.WorkOrderOpType_ID);
            
            CreateTable(
                "dbo.WorkOrderAttachments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachFile_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.FileDatas", t => t.AttachFile_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.AttachFile_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.WorkOrderDocStatuses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        DocStatus = c.Int(nullable: false),
                        DocRemarks = c.String(),
                        IsReverse = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.WorkOrderJobStatuses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        JobRemarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        JobStatus_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.JobStatuses", t => t.JobStatus_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.JobStatus_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.JobStatuses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BoCode = c.String(),
                        BoName = c.String(),
                        IsPlanning = c.Boolean(nullable: false),
                        IsPreExecution = c.Boolean(nullable: false),
                        IsExecution = c.Boolean(nullable: false),
                        IsPostExecution = c.Boolean(nullable: false),
                        IsClosure = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.WorkOrderManHours",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ManCount = c.Int(nullable: false),
                        TimeSpend = c.Time(precision: 7),
                        ManHours = c.Long(nullable: false),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                        WorkOrderOpType_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .ForeignKey("dbo.WorkOrderOpTypes", t => t.WorkOrderOpType_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID)
                .Index(t => t.WorkOrderOpType_ID);
            
            CreateTable(
                "dbo.WOLongDescriptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        LongDescription = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.EqClassDocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NextDocNo = c.Long(nullable: false),
                        EqClass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EqClasses", t => t.EqClass_ID)
                .Index(t => t.EqClass_ID);
            
            CreateTable(
                "dbo.EqComponentClassDocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NextDocNo = c.Long(nullable: false),
                        EqComponentClass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EqComponentClasses", t => t.EqComponentClass_ID)
                .Index(t => t.EqComponentClass_ID);
            
            CreateTable(
                "dbo.PMClassDocs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NextDocNo = c.Long(nullable: false),
                        PMClass_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PMClasses", t => t.PMClass_ID)
                .Index(t => t.PMClass_ID);
            
            CreateTable(
                "dbo.PMScheduleChecklists",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        ChecklistName = c.String(),
                        Remarks = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CheckList_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                        PMSchedule_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CheckLists", t => t.CheckList_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PMSchedules", t => t.PMSchedule_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .Index(t => t.CheckList_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.PMSchedule_ID)
                .Index(t => t.UpdateUser_ID);
            
            CreateTable(
                "dbo.PurchaseQuotationDtls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        CreateUser_ID = c.Int(),
                        PurchaseQuotation_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PurchaseQuotations", t => t.PurchaseQuotation_ID)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.PurchaseQuotation_ID);
            
            CreateTable(
                "dbo.PurchaseQuotations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DocNumSeq = c.Long(nullable: false),
                        DocNum = c.String(),
                        DocDate = c.DateTime(nullable: false),
                        RefNo = c.String(),
                        Approved = c.Boolean(nullable: false),
                        Cancelled = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Company_ID = c.Int(),
                        Contractor_ID = c.Int(),
                        CreateUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.Company_ID)
                .ForeignKey("dbo.Contractors", t => t.Contractor_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .Index(t => t.Company_ID)
                .Index(t => t.Contractor_ID)
                .Index(t => t.CreateUser_ID);
            
            CreateTable(
                "dbo.WorkOrderPhotos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(),
                        UpdateDate = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        AttachPhoto_Id = c.Int(),
                        CreateUser_ID = c.Int(),
                        UpdateUser_ID = c.Int(),
                        WorkOrder_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaDataObjects", t => t.AttachPhoto_Id)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.CreateUser_ID)
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.UpdateUser_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_ID)
                .Index(t => t.AttachPhoto_Id)
                .Index(t => t.CreateUser_ID)
                .Index(t => t.UpdateUser_ID)
                .Index(t => t.WorkOrder_ID);
            
            CreateTable(
                "dbo.SystemUsersContractors",
                c => new
                    {
                        SystemUsers_ID = c.Int(nullable: false),
                        Contractors_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SystemUsers_ID, t.Contractors_ID })
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.SystemUsers_ID, cascadeDelete: true)
                .ForeignKey("dbo.Contractors", t => t.Contractors_ID, cascadeDelete: true)
                .Index(t => t.SystemUsers_ID)
                .Index(t => t.Contractors_ID);
            
            CreateTable(
                "dbo.PMClassesPlannerGroups",
                c => new
                    {
                        PMClasses_ID = c.Int(nullable: false),
                        PlannerGroups_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PMClasses_ID, t.PlannerGroups_ID })
                .ForeignKey("dbo.PMClasses", t => t.PMClasses_ID, cascadeDelete: true)
                .ForeignKey("dbo.PlannerGroups", t => t.PlannerGroups_ID, cascadeDelete: true)
                .Index(t => t.PMClasses_ID)
                .Index(t => t.PlannerGroups_ID);
            
            CreateTable(
                "dbo.EqClassPropertiesEqClasses",
                c => new
                    {
                        EqClassProperties_ID = c.Int(nullable: false),
                        EqClasses_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EqClassProperties_ID, t.EqClasses_ID })
                .ForeignKey("dbo.EqClassProperties", t => t.EqClassProperties_ID, cascadeDelete: true)
                .ForeignKey("dbo.EqClasses", t => t.EqClasses_ID, cascadeDelete: true)
                .Index(t => t.EqClassProperties_ID)
                .Index(t => t.EqClasses_ID);
            
            AddColumn("dbo.PermissionPolicyUsers", "FullName", c => c.String());
            AddColumn("dbo.PermissionPolicyUsers", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.PermissionPolicyUsers", "Company_ID", c => c.Int());
            CreateIndex("dbo.PermissionPolicyUsers", "Company_ID");
            AddForeignKey("dbo.PermissionPolicyUsers", "Company_ID", "dbo.Companies", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkOrderPhotos", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderPhotos", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderPhotos", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderPhotos", "AttachPhoto_Id", "dbo.MediaDataObjects");
            DropForeignKey("dbo.PurchaseQuotationDtls", "PurchaseQuotation_ID", "dbo.PurchaseQuotations");
            DropForeignKey("dbo.PurchaseQuotations", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseQuotations", "Contractor_ID", "dbo.Contractors");
            DropForeignKey("dbo.PurchaseQuotations", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.PurchaseQuotationDtls", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMScheduleChecklists", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMScheduleChecklists", "PMSchedule_ID", "dbo.PMSchedules");
            DropForeignKey("dbo.PMScheduleChecklists", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMScheduleChecklists", "CheckList_ID", "dbo.CheckLists");
            DropForeignKey("dbo.PMClassDocs", "PMClass_ID", "dbo.PMClasses");
            DropForeignKey("dbo.EqComponentClassDocs", "EqComponentClass_ID", "dbo.EqComponentClasses");
            DropForeignKey("dbo.EqClassDocs", "EqClass_ID", "dbo.EqClasses");
            DropForeignKey("dbo.ContractDocDtls", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.ContractDocDtls", "ItemMaster_ID", "dbo.ItemMasters");
            DropForeignKey("dbo.ContractDocDtls", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.ContractDocs", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.ContractDocDtls", "ContractDoc_ID", "dbo.ContractDocs");
            DropForeignKey("dbo.ContractDocs", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.ContractDocs", "Contractor_ID", "dbo.Contractors");
            DropForeignKey("dbo.Tasks", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.Tasks", "PurchaseRequest_ID", "dbo.PurchaseRequests");
            DropForeignKey("dbo.WorkOrders", "WorkDescription_ID", "dbo.WOLongDescriptions");
            DropForeignKey("dbo.WOLongDescriptions", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WOLongDescriptions", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrders", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrders", "Priority_ID", "dbo.Priorities");
            DropForeignKey("dbo.WorkOrders", "PMSchedule_ID", "dbo.PMSchedules");
            DropForeignKey("dbo.WorkOrders", "PMClass_ID", "dbo.PMClasses");
            DropForeignKey("dbo.WorkOrders", "JobStatus_ID", "dbo.JobStatuses");
            DropForeignKey("dbo.WorkOrders", "DocType_ID", "dbo.DocTypes");
            DropForeignKey("dbo.PurchaseRequests", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkRequestPhotos", "WorkOrders_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderManHours", "WorkOrderOpType_ID", "dbo.WorkOrderOpTypes");
            DropForeignKey("dbo.WorkOrderManHours", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderManHours", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderManHours", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderJobStatuses", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderJobStatuses", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderJobStatuses", "JobStatus_ID", "dbo.JobStatuses");
            DropForeignKey("dbo.WorkOrderJobStatuses", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderDocStatuses", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderDocStatuses", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderDocStatuses", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderAttachments", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderAttachments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderAttachments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderAttachments", "AttachFile_ID", "dbo.FileDatas");
            DropForeignKey("dbo.WorkOrderEquipments", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderEquipments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEquipmentOps", "WorkOrderOpType_ID", "dbo.WorkOrderOpTypes");
            DropForeignKey("dbo.WorkOrderEquipmentOps", "WorkOrderEquipment_ID", "dbo.WorkOrderEquipments");
            DropForeignKey("dbo.WorkOrderEquipmentOps", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEquipmentOps", "Technician_ID", "dbo.Technicians");
            DropForeignKey("dbo.WorkOrderEquipmentOps", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEqComponents", "WorkOrderEquipment_ID", "dbo.WorkOrderEquipments");
            DropForeignKey("dbo.WorkOrderEqComponents", "WorkOrder_ID", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrderEqComponents", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEqComponents", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.WorkOrderEqComponents", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.WorkRequestEquipments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestEquipments", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.WorkRequestEqComponents", "WorkRequestEquipment_ID", "dbo.WorkRequestEquipments");
            DropForeignKey("dbo.WorkRequests", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequests", "Priority_ID", "dbo.Priorities");
            DropForeignKey("dbo.WorkRequests", "PlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.WorkRequests", "DocType_ID", "dbo.DocTypes");
            DropForeignKey("dbo.WorkRequestPhotos", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequestPhotos", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestPhotos", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestPhotos", "AttachPhoto_Id", "dbo.MediaDataObjects");
            DropForeignKey("dbo.WorkRequestDocStatuses", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequestDocStatuses", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestDocStatuses", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequests", "DetailDescription_ID", "dbo.WRLongDescriptions");
            DropForeignKey("dbo.WRLongDescriptions", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WRLongDescriptions", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestAttachments", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequestAttachments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestAttachments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestAttachments", "AttachFile_ID", "dbo.FileDatas");
            DropForeignKey("dbo.WorkOrders", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequestEqComponents", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequestEquipments", "WorkRequest_ID", "dbo.WorkRequests");
            DropForeignKey("dbo.WorkRequests", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequests", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.WorkRequestEqComponents", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestEqComponents", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.WorkRequestEqComponents", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.WorkRequestEqComponents", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkRequestEquipments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEquipments", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.Equipments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.Equipments", "SubLocation_ID", "dbo.SubLocations");
            DropForeignKey("dbo.PMScheduleEquipments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMScheduleEquipments", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.PMScheduleEqComponents", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMScheduleEqComponents", "PMScheduleEquipment_ID", "dbo.PMScheduleEquipments");
            DropForeignKey("dbo.PMSchedules", "Priority_ID", "dbo.Priorities");
            DropForeignKey("dbo.PMSchedules", "PMFrequency_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMSchedules", "PMDepartment_ID", "dbo.PMDepartments");
            DropForeignKey("dbo.PMSchedules", "PMClass_ID", "dbo.PMClasses");
            DropForeignKey("dbo.PMSchedules", "PlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.PMScheduleEqComponents", "PMSchedule_ID", "dbo.PMSchedules");
            DropForeignKey("dbo.PMScheduleEquipments", "PMSchedule_ID", "dbo.PMSchedules");
            DropForeignKey("dbo.PMSchedules", "CheckList_ID", "dbo.CheckLists");
            DropForeignKey("dbo.PMScheduleEqComponents", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.PMScheduleEqComponents", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.PMScheduleEqComponents", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMScheduleEquipments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.Equipments", "Location_ID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.EquipmentProperties", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentProperties", "EqClassProperty_ID", "dbo.EqClassProperties");
            DropForeignKey("dbo.EquipmentPhotos", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentPhotos", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentPhotos", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentPhotos", "AttachPhoto_Id", "dbo.MediaDataObjects");
            DropForeignKey("dbo.MediaDataObjects", "MediaResource_Id", "dbo.MediaResourceObjects");
            DropForeignKey("dbo.EquipmentComponents", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentComponents", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentComponents", "EqComponentGroup_ID", "dbo.EqComponentGroups");
            DropForeignKey("dbo.EquipmentComponents", "EqComponentClass_ID", "dbo.EqComponentClasses");
            DropForeignKey("dbo.EquipmentComponents", "Criticality_ID", "dbo.Criticalities");
            DropForeignKey("dbo.EquipmentComponents", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentAttachments", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentAttachments", "Equipment_ID", "dbo.Equipments");
            DropForeignKey("dbo.EquipmentAttachments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.EquipmentAttachments", "AttachFile_ID", "dbo.FileDatas");
            DropForeignKey("dbo.Equipments", "EqGroup_ID", "dbo.EqGroups");
            DropForeignKey("dbo.Equipments", "EqClass_ID", "dbo.EqClasses");
            DropForeignKey("dbo.EqClasses", "EqGroup_ID", "dbo.EqGroups");
            DropForeignKey("dbo.EqClassPropertiesEqClasses", "EqClasses_ID", "dbo.EqClasses");
            DropForeignKey("dbo.EqClassPropertiesEqClasses", "EqClassProperties_ID", "dbo.EqClassProperties");
            DropForeignKey("dbo.Equipments", "Criticality_ID", "dbo.Criticalities");
            DropForeignKey("dbo.Equipments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.Equipments", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.WorkOrderEqComponentOps", "WorkOrderOpType_ID", "dbo.WorkOrderOpTypes");
            DropForeignKey("dbo.WorkOrderEqComponentOps", "WorkOrderEqComponent_ID", "dbo.WorkOrderEqComponents");
            DropForeignKey("dbo.WorkOrderEqComponentOps", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEqComponentOps", "Technician_ID", "dbo.Technicians");
            DropForeignKey("dbo.WorkOrderEqComponentOps", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEqComponents", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrderEquipments", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrders", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.WorkOrders", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.WorkOrders", "CheckList_ID", "dbo.CheckLists");
            DropForeignKey("dbo.WorkOrders", "AssignPlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.Positions", "PlannerGroup_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.Positions", "CurrentUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PMClassesPlannerGroups", "PlannerGroups_ID", "dbo.PlannerGroups");
            DropForeignKey("dbo.PMClassesPlannerGroups", "PMClasses_ID", "dbo.PMClasses");
            DropForeignKey("dbo.PurchaseRequests", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequests", "DocType_ID", "dbo.DocTypes");
            DropForeignKey("dbo.PurchaseRequestDocStatuses", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequestDocStatuses", "PurchaseRequest_ID", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequestDocStatuses", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequestDtls", "UpdateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequestDtls", "PurchaseRequest_ID", "dbo.PurchaseRequests");
            DropForeignKey("dbo.PurchaseRequestDtls", "ItemMaster_ID", "dbo.ItemMasters");
            DropForeignKey("dbo.PurchaseRequestDtls", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequests", "CreateUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PurchaseRequests", "Contractor_ID", "dbo.Contractors");
            DropForeignKey("dbo.PurchaseRequests", "ContractDoc_ID", "dbo.ContractDocs");
            DropForeignKey("dbo.PurchaseRequests", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.Tasks", "AssignedTo_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.SystemUsersContractors", "Contractors_ID", "dbo.Contractors");
            DropForeignKey("dbo.SystemUsersContractors", "SystemUsers_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PermissionPolicyUsers", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.CompanyDocs", "DocType_ID", "dbo.DocTypes");
            DropForeignKey("dbo.CompanyDocs", "Company_ID", "dbo.Companies");
            DropForeignKey("dbo.CheckLists", "File_ID", "dbo.FileDatas");
            DropIndex("dbo.EqClassPropertiesEqClasses", new[] { "EqClasses_ID" });
            DropIndex("dbo.EqClassPropertiesEqClasses", new[] { "EqClassProperties_ID" });
            DropIndex("dbo.PMClassesPlannerGroups", new[] { "PlannerGroups_ID" });
            DropIndex("dbo.PMClassesPlannerGroups", new[] { "PMClasses_ID" });
            DropIndex("dbo.SystemUsersContractors", new[] { "Contractors_ID" });
            DropIndex("dbo.SystemUsersContractors", new[] { "SystemUsers_ID" });
            DropIndex("dbo.WorkOrderPhotos", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderPhotos", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderPhotos", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderPhotos", new[] { "AttachPhoto_Id" });
            DropIndex("dbo.PurchaseQuotations", new[] { "CreateUser_ID" });
            DropIndex("dbo.PurchaseQuotations", new[] { "Contractor_ID" });
            DropIndex("dbo.PurchaseQuotations", new[] { "Company_ID" });
            DropIndex("dbo.PurchaseQuotationDtls", new[] { "PurchaseQuotation_ID" });
            DropIndex("dbo.PurchaseQuotationDtls", new[] { "CreateUser_ID" });
            DropIndex("dbo.PMScheduleChecklists", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PMScheduleChecklists", new[] { "PMSchedule_ID" });
            DropIndex("dbo.PMScheduleChecklists", new[] { "CreateUser_ID" });
            DropIndex("dbo.PMScheduleChecklists", new[] { "CheckList_ID" });
            DropIndex("dbo.PMClassDocs", new[] { "PMClass_ID" });
            DropIndex("dbo.EqComponentClassDocs", new[] { "EqComponentClass_ID" });
            DropIndex("dbo.EqClassDocs", new[] { "EqClass_ID" });
            DropIndex("dbo.WOLongDescriptions", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WOLongDescriptions", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderManHours", new[] { "WorkOrderOpType_ID" });
            DropIndex("dbo.WorkOrderManHours", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderManHours", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderManHours", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderJobStatuses", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderJobStatuses", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderJobStatuses", new[] { "JobStatus_ID" });
            DropIndex("dbo.WorkOrderJobStatuses", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderDocStatuses", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderDocStatuses", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderDocStatuses", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderAttachments", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderAttachments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderAttachments", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderAttachments", new[] { "AttachFile_ID" });
            DropIndex("dbo.WorkOrderEquipmentOps", new[] { "WorkOrderOpType_ID" });
            DropIndex("dbo.WorkOrderEquipmentOps", new[] { "WorkOrderEquipment_ID" });
            DropIndex("dbo.WorkOrderEquipmentOps", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderEquipmentOps", new[] { "Technician_ID" });
            DropIndex("dbo.WorkOrderEquipmentOps", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequestPhotos", new[] { "WorkOrders_ID" });
            DropIndex("dbo.WorkRequestPhotos", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkRequestPhotos", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequestPhotos", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequestPhotos", new[] { "AttachPhoto_Id" });
            DropIndex("dbo.WorkRequestDocStatuses", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkRequestDocStatuses", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequestDocStatuses", new[] { "CreateUser_ID" });
            DropIndex("dbo.WRLongDescriptions", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WRLongDescriptions", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequestAttachments", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkRequestAttachments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequestAttachments", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequestAttachments", new[] { "AttachFile_ID" });
            DropIndex("dbo.WorkRequests", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequests", new[] { "Priority_ID" });
            DropIndex("dbo.WorkRequests", new[] { "PlannerGroup_ID" });
            DropIndex("dbo.WorkRequests", new[] { "DocType_ID" });
            DropIndex("dbo.WorkRequests", new[] { "DetailDescription_ID" });
            DropIndex("dbo.WorkRequests", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequests", new[] { "Company_ID" });
            DropIndex("dbo.WorkRequestEqComponents", new[] { "WorkRequestEquipment_ID" });
            DropIndex("dbo.WorkRequestEqComponents", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkRequestEqComponents", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequestEqComponents", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.WorkRequestEqComponents", new[] { "Equipment_ID" });
            DropIndex("dbo.WorkRequestEqComponents", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkRequestEquipments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkRequestEquipments", new[] { "Equipment_ID" });
            DropIndex("dbo.WorkRequestEquipments", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkRequestEquipments", new[] { "CreateUser_ID" });
            DropIndex("dbo.PMSchedules", new[] { "Priority_ID" });
            DropIndex("dbo.PMSchedules", new[] { "PMFrequency_ID" });
            DropIndex("dbo.PMSchedules", new[] { "PMDepartment_ID" });
            DropIndex("dbo.PMSchedules", new[] { "PMClass_ID" });
            DropIndex("dbo.PMSchedules", new[] { "PlannerGroup_ID" });
            DropIndex("dbo.PMSchedules", new[] { "CheckList_ID" });
            DropIndex("dbo.PMScheduleEqComponents", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PMScheduleEqComponents", new[] { "PMScheduleEquipment_ID" });
            DropIndex("dbo.PMScheduleEqComponents", new[] { "PMSchedule_ID" });
            DropIndex("dbo.PMScheduleEqComponents", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.PMScheduleEqComponents", new[] { "Equipment_ID" });
            DropIndex("dbo.PMScheduleEqComponents", new[] { "CreateUser_ID" });
            DropIndex("dbo.PMScheduleEquipments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PMScheduleEquipments", new[] { "Equipment_ID" });
            DropIndex("dbo.PMScheduleEquipments", new[] { "PMSchedule_ID" });
            DropIndex("dbo.PMScheduleEquipments", new[] { "CreateUser_ID" });
            DropIndex("dbo.Locations", new[] { "Area_ID" });
            DropIndex("dbo.EquipmentProperties", new[] { "Equipment_ID" });
            DropIndex("dbo.EquipmentProperties", new[] { "EqClassProperty_ID" });
            DropIndex("dbo.MediaDataObjects", new[] { "MediaResource_Id" });
            DropIndex("dbo.EquipmentPhotos", new[] { "UpdateUser_ID" });
            DropIndex("dbo.EquipmentPhotos", new[] { "Equipment_ID" });
            DropIndex("dbo.EquipmentPhotos", new[] { "CreateUser_ID" });
            DropIndex("dbo.EquipmentPhotos", new[] { "AttachPhoto_Id" });
            DropIndex("dbo.EquipmentComponents", new[] { "UpdateUser_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "Equipment_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "EqComponentGroup_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "EqComponentClass_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "Criticality_ID" });
            DropIndex("dbo.EquipmentComponents", new[] { "CreateUser_ID" });
            DropIndex("dbo.EquipmentAttachments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.EquipmentAttachments", new[] { "Equipment_ID" });
            DropIndex("dbo.EquipmentAttachments", new[] { "CreateUser_ID" });
            DropIndex("dbo.EquipmentAttachments", new[] { "AttachFile_ID" });
            DropIndex("dbo.EqClasses", new[] { "EqGroup_ID" });
            DropIndex("dbo.Equipments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.Equipments", new[] { "SubLocation_ID" });
            DropIndex("dbo.Equipments", new[] { "Location_ID" });
            DropIndex("dbo.Equipments", new[] { "EqGroup_ID" });
            DropIndex("dbo.Equipments", new[] { "EqClass_ID" });
            DropIndex("dbo.Equipments", new[] { "Criticality_ID" });
            DropIndex("dbo.Equipments", new[] { "CreateUser_ID" });
            DropIndex("dbo.Equipments", new[] { "Area_ID" });
            DropIndex("dbo.WorkOrderEqComponentOps", new[] { "WorkOrderOpType_ID" });
            DropIndex("dbo.WorkOrderEqComponentOps", new[] { "WorkOrderEqComponent_ID" });
            DropIndex("dbo.WorkOrderEqComponentOps", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderEqComponentOps", new[] { "Technician_ID" });
            DropIndex("dbo.WorkOrderEqComponentOps", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderEqComponents", new[] { "WorkOrderEquipment_ID" });
            DropIndex("dbo.WorkOrderEqComponents", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderEqComponents", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderEqComponents", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.WorkOrderEqComponents", new[] { "Equipment_ID" });
            DropIndex("dbo.WorkOrderEqComponents", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrderEquipments", new[] { "WorkOrder_ID" });
            DropIndex("dbo.WorkOrderEquipments", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrderEquipments", new[] { "Equipment_ID" });
            DropIndex("dbo.WorkOrderEquipments", new[] { "CreateUser_ID" });
            DropIndex("dbo.Positions", new[] { "PlannerGroup_ID" });
            DropIndex("dbo.Positions", new[] { "CurrentUser_ID" });
            DropIndex("dbo.WorkOrders", new[] { "WorkDescription_ID" });
            DropIndex("dbo.WorkOrders", new[] { "UpdateUser_ID" });
            DropIndex("dbo.WorkOrders", new[] { "Priority_ID" });
            DropIndex("dbo.WorkOrders", new[] { "PMSchedule_ID" });
            DropIndex("dbo.WorkOrders", new[] { "PMClass_ID" });
            DropIndex("dbo.WorkOrders", new[] { "JobStatus_ID" });
            DropIndex("dbo.WorkOrders", new[] { "DocType_ID" });
            DropIndex("dbo.WorkOrders", new[] { "WorkRequest_ID" });
            DropIndex("dbo.WorkOrders", new[] { "CreateUser_ID" });
            DropIndex("dbo.WorkOrders", new[] { "Company_ID" });
            DropIndex("dbo.WorkOrders", new[] { "CheckList_ID" });
            DropIndex("dbo.WorkOrders", new[] { "AssignPlannerGroup_ID" });
            DropIndex("dbo.PurchaseRequestDocStatuses", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PurchaseRequestDocStatuses", new[] { "PurchaseRequest_ID" });
            DropIndex("dbo.PurchaseRequestDocStatuses", new[] { "CreateUser_ID" });
            DropIndex("dbo.PurchaseRequestDtls", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PurchaseRequestDtls", new[] { "PurchaseRequest_ID" });
            DropIndex("dbo.PurchaseRequestDtls", new[] { "ItemMaster_ID" });
            DropIndex("dbo.PurchaseRequestDtls", new[] { "CreateUser_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "WorkOrder_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "UpdateUser_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "DocType_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "CreateUser_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "Contractor_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "ContractDoc_ID" });
            DropIndex("dbo.PurchaseRequests", new[] { "Company_ID" });
            DropIndex("dbo.Tasks", new[] { "WorkOrder_ID" });
            DropIndex("dbo.Tasks", new[] { "PurchaseRequest_ID" });
            DropIndex("dbo.Tasks", new[] { "AssignedTo_ID" });
            DropIndex("dbo.PermissionPolicyUsers", new[] { "Company_ID" });
            DropIndex("dbo.ContractDocs", new[] { "UpdateUser_ID" });
            DropIndex("dbo.ContractDocs", new[] { "CreateUser_ID" });
            DropIndex("dbo.ContractDocs", new[] { "Contractor_ID" });
            DropIndex("dbo.ContractDocDtls", new[] { "UpdateUser_ID" });
            DropIndex("dbo.ContractDocDtls", new[] { "ItemMaster_ID" });
            DropIndex("dbo.ContractDocDtls", new[] { "CreateUser_ID" });
            DropIndex("dbo.ContractDocDtls", new[] { "ContractDoc_ID" });
            DropIndex("dbo.CompanyDocs", new[] { "DocType_ID" });
            DropIndex("dbo.CompanyDocs", new[] { "Company_ID" });
            DropIndex("dbo.CheckLists", new[] { "File_ID" });
            DropColumn("dbo.PermissionPolicyUsers", "Company_ID");
            DropColumn("dbo.PermissionPolicyUsers", "Discriminator");
            DropColumn("dbo.PermissionPolicyUsers", "FullName");
            DropTable("dbo.EqClassPropertiesEqClasses");
            DropTable("dbo.PMClassesPlannerGroups");
            DropTable("dbo.SystemUsersContractors");
            DropTable("dbo.WorkOrderPhotos");
            DropTable("dbo.PurchaseQuotations");
            DropTable("dbo.PurchaseQuotationDtls");
            DropTable("dbo.PMScheduleChecklists");
            DropTable("dbo.PMClassDocs");
            DropTable("dbo.EqComponentClassDocs");
            DropTable("dbo.EqClassDocs");
            DropTable("dbo.WOLongDescriptions");
            DropTable("dbo.WorkOrderManHours");
            DropTable("dbo.JobStatuses");
            DropTable("dbo.WorkOrderJobStatuses");
            DropTable("dbo.WorkOrderDocStatuses");
            DropTable("dbo.WorkOrderAttachments");
            DropTable("dbo.WorkOrderEquipmentOps");
            DropTable("dbo.WorkRequestPhotos");
            DropTable("dbo.WorkRequestDocStatuses");
            DropTable("dbo.WRLongDescriptions");
            DropTable("dbo.WorkRequestAttachments");
            DropTable("dbo.WorkRequests");
            DropTable("dbo.WorkRequestEqComponents");
            DropTable("dbo.WorkRequestEquipments");
            DropTable("dbo.SubLocations");
            DropTable("dbo.Priorities");
            DropTable("dbo.PMFrequencies");
            DropTable("dbo.PMDepartments");
            DropTable("dbo.PMSchedules");
            DropTable("dbo.PMScheduleEqComponents");
            DropTable("dbo.PMScheduleEquipments");
            DropTable("dbo.Locations");
            DropTable("dbo.EquipmentProperties");
            DropTable("dbo.MediaResourceObjects");
            DropTable("dbo.MediaDataObjects");
            DropTable("dbo.EquipmentPhotos");
            DropTable("dbo.EqComponentGroups");
            DropTable("dbo.EqComponentClasses");
            DropTable("dbo.EquipmentComponents");
            DropTable("dbo.EquipmentAttachments");
            DropTable("dbo.EqGroups");
            DropTable("dbo.EqClassProperties");
            DropTable("dbo.EqClasses");
            DropTable("dbo.Criticalities");
            DropTable("dbo.Equipments");
            DropTable("dbo.WorkOrderOpTypes");
            DropTable("dbo.Technicians");
            DropTable("dbo.WorkOrderEqComponentOps");
            DropTable("dbo.WorkOrderEqComponents");
            DropTable("dbo.WorkOrderEquipments");
            DropTable("dbo.Positions");
            DropTable("dbo.PMClasses");
            DropTable("dbo.PlannerGroups");
            DropTable("dbo.WorkOrders");
            DropTable("dbo.PurchaseRequestDocStatuses");
            DropTable("dbo.ItemMasters");
            DropTable("dbo.PurchaseRequestDtls");
            DropTable("dbo.PurchaseRequests");
            DropTable("dbo.Tasks");
            DropTable("dbo.Contractors");
            DropTable("dbo.ContractDocs");
            DropTable("dbo.ContractDocDtls");
            DropTable("dbo.DocTypes");
            DropTable("dbo.CompanyDocs");
            DropTable("dbo.Companies");
            DropTable("dbo.CheckLists");
            DropTable("dbo.Areas");
        }
    }
}
