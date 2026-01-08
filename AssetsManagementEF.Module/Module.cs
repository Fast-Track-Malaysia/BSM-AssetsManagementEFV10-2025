using System;
using System.Text;
using System.Linq;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Model.Core;
using DevExpress.ExpressApp.Model.DomainLogics;
using DevExpress.ExpressApp.Model.NodeGenerators;
using System.Data.Entity;
using AssetsManagementEF.Module.BusinessObjects;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.Persistent.BaseImpl.EF.Kpi;
using DevExpress.Persistent.BaseImpl.EF.StateMachine;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Notifications;
using DevExpress.Persistent.Base.General;

namespace AssetsManagementEF.Module {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppModuleBasetopic.aspx.
    public sealed partial class AssetsManagementEFModule : ModuleBase {
        static AssetsManagementEFModule() {
            DevExpress.Data.Linq.CriteriaToEFExpressionConverter.SqlFunctionsType = typeof(System.Data.Entity.SqlServer.SqlFunctions);
			DevExpress.Data.Linq.CriteriaToEFExpressionConverter.EntityFunctionsType = typeof(System.Data.Entity.DbFunctions);
			DevExpress.ExpressApp.SystemModule.ResetViewSettingsController.DefaultAllowRecreateView = false;
            // Uncomment this code to delete and recreate the database each time the data model has changed.
            // Do not use this code in a production environment to avoid data loss.
            // #if DEBUG
            // Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AssetsManagementEFDbContext>());
            // #endif 
        }
        public AssetsManagementEFModule() {
            InitializeComponent();
			DevExpress.ExpressApp.Kpi.KpiModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
			DevExpress.ExpressApp.Security.SecurityModule.UsedExportedTypes = DevExpress.Persistent.Base.UsedExportedTypes.Custom;
			AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileData));
			AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.FileAttachment));
			AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Analysis));
			AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Event));
			AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.Resource));
			AdditionalExportedTypes.Add(typeof(DevExpress.Persistent.BaseImpl.EF.HCategory));
			AdditionalExportedTypes.Add(typeof(BaseKpiObject));
			AdditionalExportedTypes.Add(typeof(KpiDefinition));
			AdditionalExportedTypes.Add(typeof(KpiHistoryItem));
			AdditionalExportedTypes.Add(typeof(KpiInstance));
			AdditionalExportedTypes.Add(typeof(KpiScorecard));
			AdditionalExportedTypes.Add(typeof(StateMachine));
			AdditionalExportedTypes.Add(typeof(StateMachineTransition));
			AdditionalExportedTypes.Add(typeof(StateMachineAppearance));
			AdditionalExportedTypes.Add(typeof(StateMachineState));
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            // Manage various aspects of the application UI and behavior at the module level.
            application.LoggedOn += new EventHandler<LogonEventArgs>(application_LoggedOn);
        }
        void application_LoggedOn(object sender, LogonEventArgs e)
        {
            NotificationsModule notificationsModule = Application.Modules.FindModule<NotificationsModule>();
            DefaultNotificationsProvider notificationsProvider = notificationsModule.DefaultNotificationsProvider;
            notificationsProvider.CustomizeNotificationCollectionCriteria += notificationsProvider_CustomizeNotificationCollectionCriteria;
        }
        //void application_LoggedOn(object sender, LogonEventArgs e)
        //{
        //    SchedulerModuleBase schedulerModule = Application.Modules.FindModule<SchedulerModuleBase>();
        //    NotificationsProvider notificationsProvider = schedulerModule.NotificationsProvider;
        //    notificationsProvider.CustomizeNotificationCollectionCriteria += notificationsProvider_CustomizeNotificationCollectionCriteria;
        //}
        void notificationsProvider_CustomizeNotificationCollectionCriteria(
            object sender, CustomizeCollectionCriteriaEventArgs e)
        {
            if (e.Type == typeof(Tasks))
            {
                e.Criteria = CriteriaOperator.Parse("AssignedTo is null || AssignedTo.Id == CurrentUserId()");
            }
        }

        public override void Setup(ApplicationModulesManager moduleManager) {
            base.Setup(moduleManager);
            StateMachineModule stateMachineModule = moduleManager.Modules.FindModule<StateMachineModule>();
            stateMachineModule.StateMachineStorageType = typeof(StateMachine);
			ReportsModuleV2 reportModule = moduleManager.Modules.FindModule<ReportsModuleV2>();
            reportModule.ReportDataType = typeof(DevExpress.Persistent.BaseImpl.EF.ReportDataV2);
        }
    }
}
