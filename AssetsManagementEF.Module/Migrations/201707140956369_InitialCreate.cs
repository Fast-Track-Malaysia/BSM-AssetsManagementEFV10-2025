namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Analyses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Criteria = c.String(),
                        ObjectTypeName = c.String(),
                        DimensionPropertiesString = c.String(),
                        PivotGridSettingsContent = c.Binary(),
                        ChartSettingsContent = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DashboardDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Title = c.String(),
                        SynchronizeTitle = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EFInstanceKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KeyId = c.Guid(nullable: false),
                        InstanceId = c.Guid(nullable: false),
                        Properties = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EFRunningWorkflowInstanceInfoes",
                c => new
                    {
                        Oid = c.Guid(nullable: false),
                        Id = c.Int(nullable: false),
                        WorkflowName = c.String(),
                        WorkflowUniqueId = c.String(),
                        TargetObjectHandle = c.String(),
                        ActivityInstanceId = c.Guid(nullable: false),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.Oid);
            
            CreateTable(
                "dbo.EFStartWorkflowRequests",
                c => new
                    {
                        Oid = c.Guid(nullable: false),
                        Id = c.Int(nullable: false),
                        TargetWorkflowUniqueId = c.String(),
                        TargetObjectKeyStorage = c.String(),
                        TypeStorage = c.String(),
                    })
                .PrimaryKey(t => t.Oid);
            
            CreateTable(
                "dbo.EFTrackingRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InstanceId = c.Guid(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Data = c.String(),
                        ActivityId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EFUserActivityVersions",
                c => new
                    {
                        Oid = c.Guid(nullable: false),
                        Id = c.Int(nullable: false),
                        WorkflowUniqueId = c.String(),
                        Xaml = c.String(),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Oid);
            
            CreateTable(
                "dbo.EFWorkflowDefinitions",
                c => new
                    {
                        Oid = c.Guid(nullable: false),
                        TypeStorage = c.String(),
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Xaml = c.String(),
                        Criteria = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        AutoStartWhenObjectIsCreated = c.Boolean(nullable: false),
                        AutoStartWhenObjectFitsCriteria = c.Boolean(nullable: false),
                        AllowMultipleRuns = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Oid);
            
            CreateTable(
                "dbo.EFWorkflowInstances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Owner = c.String(),
                        InstanceId = c.Guid(nullable: false),
                        Status = c.Int(nullable: false),
                        Content = c.String(),
                        Metadata = c.String(),
                        ExpirationDateTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EFWorkflowInstanceControlCommandRequests",
                c => new
                    {
                        Oid = c.Guid(nullable: false),
                        Id = c.Int(nullable: false),
                        TargetWorkflowUniqueId = c.String(),
                        TargetActivityInstanceId = c.Guid(nullable: false),
                        Command = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Oid);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        Description = c.String(),
                        StartOn = c.DateTime(),
                        EndOn = c.DateTime(),
                        AllDay = c.Boolean(nullable: false),
                        Location = c.String(),
                        Label = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        RecurrenceInfoXml = c.String(maxLength: 300),
                        ReminderInfoXml = c.String(maxLength: 200),
                        RemindInSeconds = c.Int(nullable: false),
                        AlarmTime = c.DateTime(),
                        IsPostponed = c.Boolean(nullable: false),
                        RecurrencePattern_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Events", t => t.RecurrencePattern_ID)
                .Index(t => t.RecurrencePattern_ID);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Color_Int = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.FileDatas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                        FileName = c.String(),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.HCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.HCategories", t => t.Parent_ID)
                .Index(t => t.Parent_ID);
            
            CreateTable(
                "dbo.KpiDefinitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        TargetObjectTypeFullName = c.String(),
                        Criteria = c.String(),
                        Expression = c.String(),
                        GreenZone = c.Single(nullable: false),
                        RedZone = c.Single(nullable: false),
                        Compare = c.Boolean(nullable: false),
                        RangeName = c.String(),
                        RangeToCompareName = c.String(),
                        MeasurementFrequency = c.Int(nullable: false),
                        MeasurementMode = c.Int(nullable: false),
                        Direction = c.Int(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        SuppressedSeries = c.String(),
                        EnableCustomizeRepresentation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.KpiInstances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ForceMeasurementDateTime = c.DateTime(),
                        Settings = c.String(),
                        KpiDefinition_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KpiDefinitions", t => t.KpiDefinition_ID, cascadeDelete: true)
                .Index(t => t.KpiDefinition_ID);
            
            CreateTable(
                "dbo.KpiHistoryItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RangeStart = c.DateTime(nullable: false),
                        RangeEnd = c.DateTime(nullable: false),
                        Value = c.Single(nullable: false),
                        KpiInstance_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.KpiInstances", t => t.KpiInstance_ID, cascadeDelete: true)
                .Index(t => t.KpiInstance_ID);
            
            CreateTable(
                "dbo.KpiScorecards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModelDifferenceAspects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Xml = c.String(),
                        Owner_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ModelDifferences", t => t.Owner_ID)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.ModelDifferences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ContextId = c.String(),
                        Version = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModuleInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Version = c.String(),
                        AssemblyFileName = c.String(),
                        IsMain = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReportDataV2",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataTypeName = c.String(),
                        IsInplaceReport = c.Boolean(nullable: false),
                        PredefinedReportTypeName = c.String(),
                        Content = c.Binary(),
                        DisplayName = c.String(),
                        ParametersObjectTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyRoleBases",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsAdministrative = c.Boolean(nullable: false),
                        CanEditModel = c.Boolean(nullable: false),
                        PermissionPolicy = c.Int(nullable: false),
                        IsAllowPermissionPriority = c.Boolean(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PermissionPolicyNavigationPermissionObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemPath = c.String(),
                        TargetTypeFullName = c.String(),
                        NavigateState = c.Int(),
                        Role_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyRoleBases", t => t.Role_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.PermissionPolicyTypePermissionObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TargetTypeFullName = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        CreateState = c.Int(),
                        DeleteState = c.Int(),
                        NavigateState = c.Int(),
                        Role_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyRoleBases", t => t.Role_ID)
                .Index(t => t.Role_ID);
            
            CreateTable(
                "dbo.PermissionPolicyMemberPermissionsObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Members = c.String(),
                        Criteria = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        TypePermissionObject_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyTypePermissionObjects", t => t.TypePermissionObject_ID)
                .Index(t => t.TypePermissionObject_ID);
            
            CreateTable(
                "dbo.PermissionPolicyObjectPermissionsObjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Criteria = c.String(),
                        ReadState = c.Int(),
                        WriteState = c.Int(),
                        DeleteState = c.Int(),
                        NavigateState = c.Int(),
                        TypePermissionObject_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PermissionPolicyTypePermissionObjects", t => t.TypePermissionObject_ID)
                .Index(t => t.TypePermissionObject_ID);
            
            CreateTable(
                "dbo.PermissionPolicyUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ChangePasswordOnFirstLogon = c.Boolean(nullable: false),
                        StoredPassword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StateMachineAppearances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TargetItems = c.String(),
                        AppearanceItemType = c.String(),
                        Criteria = c.String(),
                        Context = c.String(),
                        Priority = c.Int(nullable: false),
                        FontStyle = c.Int(),
                        FontColorInt = c.Int(nullable: false),
                        BackColorInt = c.Int(nullable: false),
                        Visibility = c.Int(),
                        Enabled = c.Boolean(),
                        Method = c.String(),
                        State_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StateMachineStates", t => t.State_ID)
                .Index(t => t.State_ID);
            
            CreateTable(
                "dbo.StateMachineStates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        MarkerValue = c.String(),
                        TargetObjectCriteria = c.String(),
                        StateMachine_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StateMachines", t => t.StateMachine_ID)
                .Index(t => t.StateMachine_ID);
            
            CreateTable(
                "dbo.StateMachines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Active = c.Boolean(nullable: false),
                        TargetObjectTypeName = c.String(),
                        StatePropertyNameBase = c.String(),
                        ExpandActionsInDetailView = c.Boolean(nullable: false),
                        StartState_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StateMachineStates", t => t.StartState_ID)
                .Index(t => t.StartState_ID);
            
            CreateTable(
                "dbo.StateMachineTransitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        Index = c.Int(nullable: false),
                        SaveAndCloseView = c.Boolean(nullable: false),
                        TargetState_ID = c.Int(),
                        SourceState_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StateMachineStates", t => t.TargetState_ID)
                .ForeignKey("dbo.StateMachineStates", t => t.SourceState_ID)
                .Index(t => t.TargetState_ID)
                .Index(t => t.SourceState_ID);
            
            CreateTable(
                "dbo.ResourceEvents",
                c => new
                    {
                        Resource_Key = c.Int(nullable: false),
                        Event_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Key, t.Event_ID })
                .ForeignKey("dbo.Resources", t => t.Resource_Key, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_ID, cascadeDelete: true)
                .Index(t => t.Resource_Key)
                .Index(t => t.Event_ID);
            
            CreateTable(
                "dbo.KpiInstanceKpiScorecards",
                c => new
                    {
                        KpiInstance_ID = c.Int(nullable: false),
                        KpiScorecard_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.KpiInstance_ID, t.KpiScorecard_ID })
                .ForeignKey("dbo.KpiInstances", t => t.KpiInstance_ID, cascadeDelete: true)
                .ForeignKey("dbo.KpiScorecards", t => t.KpiScorecard_ID, cascadeDelete: true)
                .Index(t => t.KpiInstance_ID)
                .Index(t => t.KpiScorecard_ID);
            
            CreateTable(
                "dbo.PermissionPolicyUserPermissionPolicyRoles",
                c => new
                    {
                        PermissionPolicyUser_ID = c.Int(nullable: false),
                        PermissionPolicyRole_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionPolicyUser_ID, t.PermissionPolicyRole_ID })
                .ForeignKey("dbo.PermissionPolicyUsers", t => t.PermissionPolicyUser_ID, cascadeDelete: true)
                .ForeignKey("dbo.PermissionPolicyRoleBases", t => t.PermissionPolicyRole_ID, cascadeDelete: true)
                .Index(t => t.PermissionPolicyUser_ID)
                .Index(t => t.PermissionPolicyRole_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StateMachineTransitions", "SourceState_ID", "dbo.StateMachineStates");
            DropForeignKey("dbo.StateMachineTransitions", "TargetState_ID", "dbo.StateMachineStates");
            DropForeignKey("dbo.StateMachineStates", "StateMachine_ID", "dbo.StateMachines");
            DropForeignKey("dbo.StateMachines", "StartState_ID", "dbo.StateMachineStates");
            DropForeignKey("dbo.StateMachineAppearances", "State_ID", "dbo.StateMachineStates");
            DropForeignKey("dbo.PermissionPolicyUserPermissionPolicyRoles", "PermissionPolicyRole_ID", "dbo.PermissionPolicyRoleBases");
            DropForeignKey("dbo.PermissionPolicyUserPermissionPolicyRoles", "PermissionPolicyUser_ID", "dbo.PermissionPolicyUsers");
            DropForeignKey("dbo.PermissionPolicyTypePermissionObjects", "Role_ID", "dbo.PermissionPolicyRoleBases");
            DropForeignKey("dbo.PermissionPolicyObjectPermissionsObjects", "TypePermissionObject_ID", "dbo.PermissionPolicyTypePermissionObjects");
            DropForeignKey("dbo.PermissionPolicyMemberPermissionsObjects", "TypePermissionObject_ID", "dbo.PermissionPolicyTypePermissionObjects");
            DropForeignKey("dbo.PermissionPolicyNavigationPermissionObjects", "Role_ID", "dbo.PermissionPolicyRoleBases");
            DropForeignKey("dbo.ModelDifferenceAspects", "Owner_ID", "dbo.ModelDifferences");
            DropForeignKey("dbo.KpiInstances", "KpiDefinition_ID", "dbo.KpiDefinitions");
            DropForeignKey("dbo.KpiInstanceKpiScorecards", "KpiScorecard_ID", "dbo.KpiScorecards");
            DropForeignKey("dbo.KpiInstanceKpiScorecards", "KpiInstance_ID", "dbo.KpiInstances");
            DropForeignKey("dbo.KpiHistoryItems", "KpiInstance_ID", "dbo.KpiInstances");
            DropForeignKey("dbo.HCategories", "Parent_ID", "dbo.HCategories");
            DropForeignKey("dbo.ResourceEvents", "Event_ID", "dbo.Events");
            DropForeignKey("dbo.ResourceEvents", "Resource_Key", "dbo.Resources");
            DropForeignKey("dbo.Events", "RecurrencePattern_ID", "dbo.Events");
            DropIndex("dbo.PermissionPolicyUserPermissionPolicyRoles", new[] { "PermissionPolicyRole_ID" });
            DropIndex("dbo.PermissionPolicyUserPermissionPolicyRoles", new[] { "PermissionPolicyUser_ID" });
            DropIndex("dbo.KpiInstanceKpiScorecards", new[] { "KpiScorecard_ID" });
            DropIndex("dbo.KpiInstanceKpiScorecards", new[] { "KpiInstance_ID" });
            DropIndex("dbo.ResourceEvents", new[] { "Event_ID" });
            DropIndex("dbo.ResourceEvents", new[] { "Resource_Key" });
            DropIndex("dbo.StateMachineTransitions", new[] { "SourceState_ID" });
            DropIndex("dbo.StateMachineTransitions", new[] { "TargetState_ID" });
            DropIndex("dbo.StateMachines", new[] { "StartState_ID" });
            DropIndex("dbo.StateMachineStates", new[] { "StateMachine_ID" });
            DropIndex("dbo.StateMachineAppearances", new[] { "State_ID" });
            DropIndex("dbo.PermissionPolicyObjectPermissionsObjects", new[] { "TypePermissionObject_ID" });
            DropIndex("dbo.PermissionPolicyMemberPermissionsObjects", new[] { "TypePermissionObject_ID" });
            DropIndex("dbo.PermissionPolicyTypePermissionObjects", new[] { "Role_ID" });
            DropIndex("dbo.PermissionPolicyNavigationPermissionObjects", new[] { "Role_ID" });
            DropIndex("dbo.ModelDifferenceAspects", new[] { "Owner_ID" });
            DropIndex("dbo.KpiHistoryItems", new[] { "KpiInstance_ID" });
            DropIndex("dbo.KpiInstances", new[] { "KpiDefinition_ID" });
            DropIndex("dbo.HCategories", new[] { "Parent_ID" });
            DropIndex("dbo.Events", new[] { "RecurrencePattern_ID" });
            DropTable("dbo.PermissionPolicyUserPermissionPolicyRoles");
            DropTable("dbo.KpiInstanceKpiScorecards");
            DropTable("dbo.ResourceEvents");
            DropTable("dbo.StateMachineTransitions");
            DropTable("dbo.StateMachines");
            DropTable("dbo.StateMachineStates");
            DropTable("dbo.StateMachineAppearances");
            DropTable("dbo.PermissionPolicyUsers");
            DropTable("dbo.PermissionPolicyObjectPermissionsObjects");
            DropTable("dbo.PermissionPolicyMemberPermissionsObjects");
            DropTable("dbo.PermissionPolicyTypePermissionObjects");
            DropTable("dbo.PermissionPolicyNavigationPermissionObjects");
            DropTable("dbo.PermissionPolicyRoleBases");
            DropTable("dbo.ReportDataV2");
            DropTable("dbo.ModuleInfoes");
            DropTable("dbo.ModelDifferences");
            DropTable("dbo.ModelDifferenceAspects");
            DropTable("dbo.KpiScorecards");
            DropTable("dbo.KpiHistoryItems");
            DropTable("dbo.KpiInstances");
            DropTable("dbo.KpiDefinitions");
            DropTable("dbo.HCategories");
            DropTable("dbo.FileDatas");
            DropTable("dbo.Resources");
            DropTable("dbo.Events");
            DropTable("dbo.EFWorkflowInstanceControlCommandRequests");
            DropTable("dbo.EFWorkflowInstances");
            DropTable("dbo.EFWorkflowDefinitions");
            DropTable("dbo.EFUserActivityVersions");
            DropTable("dbo.EFTrackingRecords");
            DropTable("dbo.EFStartWorkflowRequests");
            DropTable("dbo.EFRunningWorkflowInstanceInfoes");
            DropTable("dbo.EFInstanceKeys");
            DropTable("dbo.DashboardDatas");
            DropTable("dbo.Analyses");
        }
    }
}
