using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.ComponentModel;
using DevExpress.ExpressApp.EF.Updating;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;
using DevExpress.ExpressApp.Workflow.EF;
using DevExpress.ExpressApp.Workflow.Versioning;
using DevExpress.Workflow.EF;

namespace AssetsManagementEF.Module.BusinessObjects {
	public class AssetsManagementEFDbContext : DbContext {
		public AssetsManagementEFDbContext(String connectionString)
			: base(connectionString) {
		}
		public AssetsManagementEFDbContext(DbConnection connection)
			: base(connection, false) {
		}
		public AssetsManagementEFDbContext()
			: base("name=ConnectionString") {
		}
		public DbSet<ModuleInfo> ModulesInfo { get; set; }
	    public DbSet<PermissionPolicyRole> Roles { get; set; }
		public DbSet<PermissionPolicyTypePermissionObject> TypePermissionObjects { get; set; }
		public DbSet<PermissionPolicyUser> Users { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Resource> Resources { get; set; }
		public DbSet<FileData> FileData { get; set; }
		public DbSet<DashboardData> DashboardData { get; set; }
		public DbSet<Analysis> Analysis { get; set; }
		public DbSet<HCategory> HCategories { get; set; }
        public DbSet<KpiDefinition> KpiDefinition { get; set; }
        public DbSet<KpiInstance> KpiInstance { get; set; }
        public DbSet<KpiHistoryItem> KpiHistoryItem { get; set; }
        public DbSet<KpiScorecard> KpiScorecard { get; set; }
		public DbSet<StateMachine> StateMachines { get; set; }
		public DbSet<StateMachineState> StateMachineStates { get; set; }
		public DbSet<StateMachineTransition> StateMachineTransitions { get; set; }
		public DbSet<StateMachineAppearance> StateMachineAppearances { get; set; }
		public DbSet<ReportDataV2> ReportDataV2 { get; set; }
		public DbSet<ModelDifference> ModelDifferences { get; set; }
		public DbSet<ModelDifferenceAspect> ModelDifferenceAspects { get; set; }
		public DbSet<EFWorkflowDefinition> EFWorkflowDefinition { get; set; }
        public DbSet<EFStartWorkflowRequest> EFStartWorkflowRequest { get; set; }
        public DbSet<EFRunningWorkflowInstanceInfo> EFRunningWorkflowInstanceInfo { get; set; }
        public DbSet<EFWorkflowInstanceControlCommandRequest> EFWorkflowInstanceControlCommandRequest { get; set; }
        public DbSet<EFInstanceKey> EFInstanceKey { get; set; }
        public DbSet<EFTrackingRecord> EFTrackingRecord { get; set; }
        public DbSet<EFWorkflowInstance> EFWorkflowInstance { get; set; }
        public DbSet<EFUserActivityVersion> EFUserActivityVersion { get; set; }
        public DbSet<SystemUsers> SystemUsers { get; set; }
        public DbSet<PlannerGroups> PlannerGroups { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Contractors> Contractors { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<PurchaseQuotations> PurchaseQuotations { get; set; }
        public DbSet<PurchaseQuotationDtls> PurchaseQuotationDtls { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<EqClasses> EqClasses { get; set; }
        public DbSet<EqClassProperties> EqClassProperties { get; set; }
        public DbSet<SubLocations> SubLocations { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Equipments> Equipments { get; set; }
        public DbSet<EquipmentProperties> EquipmentProperties { get; set; }
        public DbSet<EqComponentClasses> EqComponentClasses { get; set; }
        public DbSet<EquipmentComponents> EquipmentComponents { get; set; }
        public DbSet<CheckLists> CheckLists { get; set; }
        public DbSet<PMFrequencies> PMFrequencies { get; set; }
        public DbSet<PMSchedules> PMSchedules { get; set; }
        public DbSet<Criticalities> Criticalities { get; set; }
        public DbSet<EqGroups> EqGroups { get; set; }
        public DbSet<DocTypes> WorkOrderTypes { get; set; }
        public DbSet<WorkOrderOpTypes> WorkOrderOpTypes { get; set; }
        public DbSet<Technicians> Technicians { get; set; }
        public DbSet<WorkOrders> WorkOrders { get; set; }
        public DbSet<WorkOrderEquipments> WorkOrderEquipments { get; set; }
        public DbSet<WorkOrderEquipmentOps> WorkOrderEquipmentOps { get; set; }
        public DbSet<WorkOrderEqComponents> WorkOrderEqComponents { get; set; }
        public DbSet<WorkOrderEqComponentOps> WorkOrderEqComponentOps { get; set; }
        public DbSet<ItemMasters> ItemMasters { get; set; }
        public DbSet<PurchaseRequests> PurchaseRequests { get; set; }
        public DbSet<PurchaseRequestDtls> PurchaseRequestDtls { get; set; }
        public DbSet<ContractDocs> ContractDocs { get; set; }
        public DbSet<ContractDocDtls> ContractDocDtls { get; set; }
        public DbSet<WorkRequests> WorkRequests { get; set; }
        public DbSet<WorkRequestEquipments> WorkRequestEquipments { get; set; }
        public DbSet<WorkRequestEqComponents> WorkRequestEqComponents { get; set; }
        public DbSet<CompanyDocs> CompanyDocs { get; set; }
        public DbSet<Priorities> Priorities { get; set; }
        public DbSet<WRLongDescription> WRLongDescription { get; set; }
        public DbSet<WOLongDescription> WOLongDescription { get; set; }
        public DbSet<JobStatuses> JobStatuses { get; set; }
        public DbSet<WorkOrderJobStatuses> WorkOrderJobStatuses { get; set; }
        public DbSet<EqClassDocs> EqClassDocs { get; set; }
        public DbSet<PMDepartments> PMDepartments { get; set; }
        public DbSet<WorkRequestDocStatuses> WorkRequestDocStatuses { get; set; }
        public DbSet<WorkOrderDocStatuses> WorkOrderDocStatuses { get; set; }
        public DbSet<EqComponentClassDocs> EqComponentClassDocs { get; set; }
        public DbSet<PMScheduleEquipments> PMScheduleEquipments { get; set; }
        public DbSet<PMScheduleEqComponents> PMScheduleEqComponents { get; set; }
        public DbSet<WorkOrderAttachments> WorkOrderAttachments { get; set; }
        public DbSet<WorkRequestAttachments> WorkRequestAttachments { get; set; }
        public DbSet<WorkOrderPhotos> WorkOrderPhotos { get; set; }
        public DbSet<WorkRequestPhotos> WorkRequestPhotos { get; set; }
        public DbSet<EquipmentAttachments> EquipmentAttachments { get; set; }
        public DbSet<EquipmentPhotos> EquipmentPhotos { get; set; }
        public DbSet<PMClasses> PMClasses { get; set; }
        public DbSet<PMClassDocs> PMClassDocs { get; set; }
        public DbSet<PMScheduleChecklists> PMScheduleChecklists { get; set; }
        public DbSet<WorkOrderManHours> WorkOrderManHours { get; set; }
        public DbSet<PMScheduleCalenders> PMScheduleCalenders { get; set; }
        public DbSet<PMPatches> PMPatches { get; set; }
        public DbSet<PurchaseRequestAttachments> PurchaseRequestAttachments { get; set; }
        public DbSet<vw_SAP_pr> vw_SAP_pr { get; set; }
        public DbSet<DeviationWorkOrders> DeviationWorkOrders { get; set; }
        public DbSet<DeviationWorkRequests> DeviationWorkRequests { get; set; }
        public DbSet<DeviationWOTypes> DeviationWOTypes { get; set; }
        public DbSet<DeviationWRTypes> DeviationWRTypes { get; set; }
        public DbSet<DeviationStatus> DeviationStatus { get; set; }
        public DbSet<SCECategories> SCECategories { get; set; }
        public DbSet<SCESubCategories> SCESubCategories { get; set; }
    }
}