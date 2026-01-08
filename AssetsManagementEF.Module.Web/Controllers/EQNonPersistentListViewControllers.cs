using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using AssetsManagementEF.Module.BusinessObjects;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Collections;
using System.Data.SqlClient;
using DevExpress.ExpressApp.EF;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl.EF;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class EQNonPersistentListViewControllers : ViewController
    {
        GenControllers genCon;

        int myyear = DateTime.Today.Year;
        int mymonth = DateTime.Today.Month;
        bool buttonpress = false;
        SingleChoiceAction customSingleChoiceItem;
        ChoiceActionItem customChoiceActionItem;
        const string refreshPMId = "PMSCH";

        SingleChoiceAction customSingleChoiceItemPM;
        ChoiceActionItem customChoiceActionItemPM;
        const string genPMWOId = "GenPMWO";

        DateTime? DateFrom;
        DateTime? DateTo;

        //IList<Equipments> equipment;
        //IList<EquipmentComponents> equipmentcomponent;
        public EQNonPersistentListViewControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //TargetObjectType = typeof(PMScheduleDC);
            //TargetViewType = ViewType.ListView;
            //TargetViewNesting = Nesting.Root;


            customSingleChoiceItem =
                new SingleChoiceAction(this, refreshPMId, PredefinedCategory.View);
            customSingleChoiceItem.Caption = "Refresh Calendar";
            customSingleChoiceItem.ItemType = SingleChoiceActionItemType.ItemIsOperation;
            customSingleChoiceItem.Execute += customSingleChoiceItem_Execute;

            //customChoiceActionItem =
            //new ChoiceActionItem(CaptionHelper.GetMemberCaption(typeof(Locations), "BoName"), null);
            //customSingleChoiceItem.Items.Add(customChoiceActionItem);
            customChoiceActionItem =
            new ChoiceActionItem("Years", null);
            customSingleChoiceItem.Items.Add(customChoiceActionItem);


            customSingleChoiceItemPM =
                new SingleChoiceAction(this, genPMWOId, PredefinedCategory.View);
            customSingleChoiceItemPM.Caption = "Generate PM WO";
            customSingleChoiceItemPM.ItemType = SingleChoiceActionItemType.ItemIsOperation;
            customSingleChoiceItemPM.Execute += customSingleChoiceItemPM_Execute;

            customChoiceActionItemPM =
            new ChoiceActionItem("YYYYMM", null);
            customSingleChoiceItemPM.Items.Add(customChoiceActionItemPM);
        }
        void GeneratePM(int year, int month, int userid)
        {
            string temp = year.ToString() + month.ToString().PadLeft(2, '0');
            SqlParameter param = new SqlParameter("@myyear", year);
            SqlParameter param1 = new SqlParameter("@mymonth", month);
            SqlParameter param2 = new SqlParameter("@userid", userid);

            EFObjectSpace persistentObjectSpace = null;
            bool disposePersistentObjectSpace = false;
            //IObjectSpace ios = Application.CreateObjectSpace();
            if (this.ObjectSpace is EFObjectSpace)
            {
                persistentObjectSpace = (EFObjectSpace)ObjectSpace;
            }
            else
            {
                persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(PMScheduleCalenders));
                disposePersistentObjectSpace = true;
            }

            IList<int> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<int>("GenPMCalenderMonthly @myyear, @mymonth, @userid", param, param1, param2).ToList();

            foreach (int dtl in lists)
            {
                if (dtl > 0)
                {
                    genCon.showMsg("Completed", "PM for " + temp + " generated.", InformationType.Success);

                    string reportname = "All PM Patch Documents";
                    try
                    {
                        if (reportname != "")
                        {
                            IObjectSpace objectSpace = ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(ReportDataV2));
                            IReportDataV2 reportData = objectSpace.FindObject<ReportDataV2>(CriteriaOperator.Parse("[DisplayName] = '" + reportname + "'"));
                            string handle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                            Frame.GetController<ReportServiceController>().ShowPreview(handle, CriteriaOperator.Parse("ID=?", dtl));
                        }
                    }
                    catch (Exception ex)
                    {
                        genCon.showMsg("Report error", reportname + " " + ex.Message, InformationType.Success);
                    }

                }
                else
                {
                    genCon.showMsg("No record found", "PM " + temp + " generated or cannot found.", InformationType.Warning);
                }
                //WorkOrders wo = ios.CreateObject<WorkOrders>();
                //wo.DocDate = dtl.DocDate;
                //wo.PMDate = dtl.PMDate;
                //wo.PMClass = ios.GetObjectByKey<PMClasses>(dtl.PMClassID);
                //wo.DocType = ios.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoPlantMaintenance));
                //wo.IsPreventiveMaintenance = true;
                //wo.Priority = ios.GetObjectByKey<Priorities>(dtl.PriorityID);
                //wo.Remarks = dtl.Remarks;
                //wo.JobStatus = ios.FindObject<JobStatuses>(new BinaryOperator("BoCode", GeneralSettings.InitPMJobStatus));

                //ios.CommitChanges();
            }

            if (disposePersistentObjectSpace)
            {
                persistentObjectSpace.Dispose();
            }

        }
        void customSingleChoiceItemPM_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.SelectedChoiceActionItem.ParentItem == null)
            {
                genCon.showMsg("Cannot Proceed", "Please choose valid item.", InformationType.Warning);
            }
            else if (e.SelectedChoiceActionItem.ParentItem == customChoiceActionItemPM)
            {
                if (e.SelectedChoiceActionItem.Data == null)
                {
                    genCon.showMsg("Cannot Proceed", "Please choose valid item.", InformationType.Warning);
                }
                else
                {
                    string temp = e.SelectedChoiceActionItem.Data.ToString();
                    SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;

                    GeneratePM(int.Parse(temp.Substring(0, 4)), int.Parse(temp.Substring(4, 2)), user.ID);


                }
            }
        }
        void customSingleChoiceItem_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //if (GeneralSettings.oCompany != null)
            //{
            //    if (GeneralSettings.oCompany.Connected)
            //        GeneralSettings.oCompany.Disconnect();
            //}

            buttonpress = true;
            myyear = DateTime.Today.Year;
            if (e.SelectedChoiceActionItem.ParentItem == customChoiceActionItem)
            {
                if (e.SelectedChoiceActionItem.Data != null)
                {
                    myyear = (int)e.SelectedChoiceActionItem.Data;
                }
            }
            RefreshController refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.DoExecute();
            }

            //IObjectSpace os = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            //ArrayList objectsToProcess = new ArrayList(e.SelectedObjects);

            //if (e.SelectedChoiceActionItem.ParentItem == customChoiceActionItem)
            //{
            //    Locations loc = os.GetObject(e.SelectedChoiceActionItem.Data as Locations);
            //    if (View.ObjectTypeInfo.Type == typeof(PMScheduleDC))
            //    {
            //        if (loc == null)
            //        {
            //            BindingList<PMScheduleDC> objects = new BindingList<PMScheduleDC>();
            //            equipment = os.GetObjects<Equipments>(CriteriaOperator.Parse("[IsApproved] and [IsActive]"));

            //        }
            //        else
            //        {
            //            BindingList<PMScheduleDC> objects = new BindingList<PMScheduleDC>();
            //            equipment = os.GetObjects<Equipments>(CriteriaOperator.Parse("[IsApproved] and [IsActive] and [Location.ID]=?", loc.ID));

            //        }

            //        RefreshController refreshController = Frame.GetController<RefreshController>();
            //        if (refreshController != null)
            //        {
            //            refreshController.RefreshAction.DoExecute();
            //        }

            //    }
            //    else if (View.ObjectTypeInfo.Type == typeof(PMScheduleCOMDC))
            //    {
            //        if (loc == null)
            //        {
            //            BindingList<PMScheduleCOMDC> objects = new BindingList<PMScheduleCOMDC>();
            //            equipmentcomponent = os.GetObjects<EquipmentComponents>(CriteriaOperator.Parse("[Equipment.IsApproved] and [Equipment.IsActive] and [IsActive]"));

            //            //((ListView)View).CurrentObject = objects;
            //        }
            //        else
            //        {
            //            BindingList<PMScheduleCOMDC> objects = new BindingList<PMScheduleCOMDC>();
            //            equipmentcomponent = os.GetObjects<EquipmentComponents>(CriteriaOperator.Parse("[Equipment.IsApproved] and [Equipment.IsActive] and [Equipment.Location.ID]=? and [IsActive]", loc.ID));

            //            //((ListView)View).CurrentObject = objects;
            //        }

            //        RefreshController refreshController = Frame.GetController<RefreshController>();
            //        if (refreshController != null)
            //        {
            //            refreshController.RefreshAction.DoExecute();
            //        }

            //    }
            //}

            //else
            //    if (e.SelectedChoiceActionItem.ParentItem == setStatusItem)
            //{
            //    foreach (Object obj in objectsToProcess)
            //    {
            //        DemoTask objInNewObjectSpace = (DemoTask)objectSpace.GetObject(obj);
            //        objInNewObjectSpace.Status = (TaskStatus)e.SelectedChoiceActionItem.Data;
            //    }
            //}

        }
        void PopulateItemsCollection()
        {
            //List<SortProperty> sortProperties = new List<SortProperty>();
            //sortProperties.Add(new SortProperty("BoCode", SortingDirection.Ascending));
            //foreach (Locations loc in
            //    View.ObjectSpace.CreateCollection(typeof(Locations), null, sortProperties))
            //{
            //    string itemCaption =
            //        CaptionHelper.GetMemberCaption(
            //        typeof(Locations), "BoName") + " :" + loc.BoName;
            //    customersAction.Items.Add(new ChoiceActionItem(loc.ID.ToString(), itemCaption, loc));
            //}
            //#region get locations
            //customChoiceActionItem.Items.Clear();
            //customChoiceActionItem.Items.Add(new ChoiceActionItem(refreshPMId, "Refresh By Location", null));
            //IObjectSpace os = Application.CreateObjectSpace();
            //IList<Locations> location = os.GetObjects<Locations>();
            //IEnumerable<Locations> sortedEnum = location.OrderBy(t => t.BoCode);
            //IList<Locations> sortedList = sortedEnum.ToList();

            //foreach (Locations loc in sortedList)
            //{
            //    //string itemCaption =
            //    //    CaptionHelper.GetMemberCaption(
            //    //    typeof(Locations), "BoName") + " :" + loc.BoName;
            //    string itemCaption = loc.BoCode + " :" + loc.BoName;
            //    customChoiceActionItem.Items.Add(new ChoiceActionItem(loc.ID.ToString(), itemCaption, loc));
            //}
            //#endregion
            customChoiceActionItem.Items.Clear();
            customChoiceActionItem.Items.Add(new ChoiceActionItem(refreshPMId, "Current Year", null));

            int temp = GeneralSettings.pmstartdate.Year;
            int end = DateTime.Today.Year + 7;

            for (int x = temp; x <= end; x++)
            {
                customChoiceActionItem.Items.Add(new ChoiceActionItem(x.ToString(), x.ToString(), x));
            }


            customChoiceActionItemPM.Items.Clear();
            customChoiceActionItemPM.Items.Add(new ChoiceActionItem(genPMWOId, "Current Month", null));

            temp = DateTime.Today.Year;
            for (int y = temp; y <= temp + 1; y++)
            {
                for (int x = 1; x <= 12; x++)
                {
                    customChoiceActionItemPM.Items.Add(new ChoiceActionItem(y.ToString() + x.ToString().PadLeft(2, '0'), y.ToString() + x.ToString().PadLeft(2, '0'), y.ToString() + x.ToString().PadLeft(2, '0')));
                }
            }

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            Application.ListViewCreating += Application_ListViewCreating;

            this.GenAMSMeasure.Active.SetItemValue("Enabled", false);
            this.GenFullList.Active.SetItemValue("Enabled", false);
            this.customSingleChoiceItem.Active.SetItemValue("Enabled", false);
            this.customSingleChoiceItemPM.Active.SetItemValue("Enabled", false);
            this.GetYearMonth.Active.SetItemValue("Enabled", false);
            this.GetDateRange.Active.SetItemValue("Enabled", false);

            if (View.ObjectTypeInfo.Type == typeof(WeeklyTodayAMSMeasure))
            {
                this.GenAMSMeasure.Active.SetItemValue("Enabled", true);
            }
            else if (View.ObjectTypeInfo.Type == typeof(WRWOFullStatus))
            {
                this.GenFullList.Active.SetItemValue("Enabled", true);
            }
            else if (View.ObjectTypeInfo.Type == typeof(PMCalenderTemp))
            {
                this.customSingleChoiceItem.Active.SetItemValue("Enabled", true);

                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                bool IsGeneratePMRold = user.Roles.Where(p => p.Name == GeneralSettings.GeneratePMRole).Count() > 0 ? true : false;
                if (IsGeneratePMRold)
                {
                    //this.customSingleChoiceItemPM.Active.SetItemValue("Enabled", true); // hide old pm wo gen button
                    //this.GetYearMonth.Active.SetItemValue("Enabled", true); // hide manual PM generation
                }
            }
            else if (View.ObjectTypeInfo.Type == typeof(BadActorDetails))
            {
                this.GetDateRange.Active.SetItemValue("Enabled", true);
            }
            else if (View.ObjectTypeInfo.Type == typeof(MEMonthlyKPIMeasuresDetails))
            {
                this.GetDateRange.Active.SetItemValue("Enabled", true);
            }
            else if (View.ObjectTypeInfo.Type == typeof(IAMonthlyKPIMeasuresDetails))
            {
                this.GetDateRange.Active.SetItemValue("Enabled", true);
            }
            else if (View.ObjectTypeInfo.Type == typeof(WODeviationList))
            {
                this.GetDateRange.Active.SetItemValue("Enabled", true);
            }
            else if (View.ObjectTypeInfo.Type == typeof(WRDeviationList))
            {
                this.GetDateRange.Active.SetItemValue("Enabled", true);
            }
        }
        private void Application_ListViewCreating(Object sender, ListViewCreatingEventArgs e)
        {
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(PMCalenderTemp))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(WRWOFullStatus))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(WeeklyTodayAMSMeasure))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(BadActorDetails))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(MEMonthlyKPIMeasuresDetails))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(IAMonthlyKPIMeasuresDetails))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(WODeviationList))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(WRDeviationList))
                if ((e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
                {
                    ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                    //PopulateItemsCollection();
                }
        }
        private void ObjectSpace_ObjectsGetting(Object sender, ObjectsGettingEventArgs e)
        {
            EFObjectSpace persistentObjectSpace = null;
            bool disposePersistentObjectSpace = false;
            try
            {
                if (buttonpress)
                {
                    if (View.ObjectTypeInfo.Type == typeof(WeeklyTodayAMSMeasure))
                    {
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(WeeklyTodayAMSMeasure))
                        {
                            IList<WeeklyTodayAMSMeasure> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<WeeklyTodayAMSMeasure>("GetWeeklyTodayAMSMeasure").ToList();
                            BindingList<WeeklyTodayAMSMeasure> objects = new BindingList<WeeklyTodayAMSMeasure>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }

                    }

                    else if (View.ObjectTypeInfo.Type == typeof(WRWOFullStatus))
                    {

                        SqlParameter param = new SqlParameter("@DocDataFrom", DateFrom);
                        SqlParameter param2 = new SqlParameter("@DocDataTo", DateTo);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(WRWOFullStatus))
                        {
                            IList<WRWOFullStatus> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<WRWOFullStatus>("GetWRWOFullStatus @DocDataFrom, @DocDataTo", param, param2).ToList();
                            BindingList<WRWOFullStatus> objects = new BindingList<WRWOFullStatus>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }

                    }
                    else if (View.ObjectTypeInfo.Type == typeof(PMCalenderTemp))
                    {
                        //if (equipmentcomponent != null)
                        //{
                        //    BindingList<PMScheduleCOMDC> objects = new BindingList<PMScheduleCOMDC>();
                        //    foreach (EquipmentComponents eq in equipmentcomponent)
                        //    {
                        //        foreach (PMScheduleCOMDC pm in eq.PMSchedule)
                        //        {
                        //            objects.Add(pm);
                        //        }
                        //    }
                        //    e.Objects = objects;
                        //}

                        SqlParameter param = new SqlParameter("@myyear", myyear);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(PMScheduleCalenders));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(PMCalenderTemp))
                        {
                            IList<PMCalenderTemp> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMCalenderTemp>("GetEQComCalenderNew @myyear", param).ToList();
                            BindingList<PMCalenderTemp> objects = new BindingList<PMCalenderTemp>(lists);

                            //IList<PMCalenderTemp> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMCalenderTemp>("GetEQComCalenderNew @myyear", param).ToList();
                            //BindingList<PMScheduleCOMDC> objects = new BindingList<PMScheduleCOMDC>();

                            //foreach (PMCalenderTemp obj in lists)
                            //{
                            //    objects.Add(new PMScheduleCOMDC()
                            //    {
                            //        Equipment = ios.FindObject<Equipments>(new BinaryOperator("ID", obj.Equipment, BinaryOperatorType.Equal)),
                            //        EquipmentComponent = obj.EquipmentComponent > 0? ios.FindObject<EquipmentComponents>(new BinaryOperator("ID", obj.EquipmentComponent, BinaryOperatorType.Equal)): null,
                            //        PMSchedule = ios.FindObject<PMSchedules>(new BinaryOperator("ID", obj.PMSchedule, BinaryOperatorType.Equal)),
                            //        IsNested = obj.IsNested == 1 ? true : false,
                            //        PMYear = obj.PMYear,
                            //        PMSchedule101 = obj.PMSchedule101,
                            //        PMSchedule102 = obj.PMSchedule102,
                            //        PMSchedule103 = obj.PMSchedule103,
                            //        PMSchedule104 = obj.PMSchedule104,
                            //        PMSchedule105 = obj.PMSchedule105,
                            //        PMSchedule106 = obj.PMSchedule106,
                            //        PMSchedule107 = obj.PMSchedule107,
                            //        PMSchedule108 = obj.PMSchedule108,
                            //        PMSchedule109 = obj.PMSchedule109,
                            //        PMSchedule110 = obj.PMSchedule110,
                            //        PMSchedule111 = obj.PMSchedule111,
                            //        PMSchedule112 = obj.PMSchedule112,
                            //        PMSchedule201 = obj.PMSchedule201,
                            //        PMSchedule202 = obj.PMSchedule202,
                            //        PMSchedule203 = obj.PMSchedule203,
                            //        PMSchedule204 = obj.PMSchedule204,
                            //        PMSchedule205 = obj.PMSchedule205,
                            //        PMSchedule206 = obj.PMSchedule206,
                            //        PMSchedule207 = obj.PMSchedule207,
                            //        PMSchedule208 = obj.PMSchedule208,
                            //        PMSchedule209 = obj.PMSchedule209,
                            //        PMSchedule210 = obj.PMSchedule210,
                            //        PMSchedule211 = obj.PMSchedule211,
                            //        PMSchedule212 = obj.PMSchedule212
                            //    });
                            //}

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }
                    }
                    else if (View.ObjectTypeInfo.Type == typeof(BadActorDetails))
                    {

                        SqlParameter param = new SqlParameter("@WODateFrom", DateFrom);
                        SqlParameter param1 = new SqlParameter("@WODateTo", DateTo);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(BadActorDetails))
                        {
                            IList<BadActorDetails> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<BadActorDetails>("GetListWOPRCntByEquipmentGroup @WODateFrom, @WODateTo", param, param1).ToList();
                            BindingList<BadActorDetails> objects = new BindingList<BadActorDetails>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }
                    }
                    else if (View.ObjectTypeInfo.Type == typeof(MEMonthlyKPIMeasuresDetails))
                    {

                        SqlParameter param = new SqlParameter("@WODateFrom", DateFrom);
                        SqlParameter param1 = new SqlParameter("@WODateTo", DateTo);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(MEMonthlyKPIMeasuresDetails))
                        {
                            IList<MEMonthlyKPIMeasuresDetails> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<MEMonthlyKPIMeasuresDetails>("GetListWOMonthlyMeasure @WODateFrom, @WODateTo", param, param1).ToList();
                            BindingList<MEMonthlyKPIMeasuresDetails> objects = new BindingList<MEMonthlyKPIMeasuresDetails>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }
                    }
                    else if (View.ObjectTypeInfo.Type == typeof(IAMonthlyKPIMeasuresDetails))
                    {

                        SqlParameter param = new SqlParameter("@WODateFrom", DateFrom);
                        SqlParameter param1 = new SqlParameter("@WODateTo", DateTo);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(IAMonthlyKPIMeasuresDetails))
                        {
                            IList<IAMonthlyKPIMeasuresDetails> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<IAMonthlyKPIMeasuresDetails>("GetListWOMonthlySCEMeasure @WODateFrom, @WODateTo", param, param1).ToList();
                            BindingList<IAMonthlyKPIMeasuresDetails> objects = new BindingList<IAMonthlyKPIMeasuresDetails>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }
                    }
                    else if (View.ObjectTypeInfo.Type == typeof(WODeviationList))
                    {

                        SqlParameter param = new SqlParameter("@WODateFrom", DateFrom);
                        SqlParameter param1 = new SqlParameter("@WODateTo", DateTo);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(WODeviationList))
                        {
                            IList<WODeviationList> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<WODeviationList>("GetListWODeviation @WODateFrom, @WODateTo", param, param1).ToList();
                            BindingList<WODeviationList> objects = new BindingList<WODeviationList>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }
                    }
                    else if (View.ObjectTypeInfo.Type == typeof(WRDeviationList))
                    {

                        SqlParameter param = new SqlParameter("@WODateFrom", DateFrom);
                        SqlParameter param1 = new SqlParameter("@WODateTo", DateTo);
                        //IObjectSpace ios = Application.CreateObjectSpace();
                        if (this.ObjectSpace is EFObjectSpace)
                        {
                            persistentObjectSpace = (EFObjectSpace)ObjectSpace;
                        }
                        else
                        {
                            persistentObjectSpace = (EFObjectSpace)Application.CreateObjectSpace(typeof(Companies));
                            disposePersistentObjectSpace = true;
                        }
                        if (View.ObjectTypeInfo.Type == typeof(WRDeviationList))
                        {
                            IList<WRDeviationList> lists = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<WRDeviationList>("GetListWRDeviation @WODateFrom, @WODateTo", param, param1).ToList();
                            BindingList<WRDeviationList> objects = new BindingList<WRDeviationList>(lists);

                            e.Objects = objects;
                        }
                        else
                        {
                            //e.Objects = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<PMScheduleCOMDC>("GetEQComCalender @myyear", param).ToList();
                        }

                        if (disposePersistentObjectSpace)
                        {
                            persistentObjectSpace.Dispose();
                        }
                    }
                }
                buttonpress = false;
            }
            catch (Exception ex)
            {
                if (disposePersistentObjectSpace)
                {
                    persistentObjectSpace.Dispose();
                }
                throw new Exception(ex.Message);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            genCon = Frame.GetController<GenControllers>();
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            Application.ListViewCreating -= Application_ListViewCreating;
            //equipment = new BindingList<Equipments>();
            //equipmentcomponent = new BindingList<EquipmentComponents>();
        }

        private void GenFullList_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new DateRangeFilterParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((DateRangeFilterParameters)dv.CurrentObject).From = DateTime.Today;
            ((DateRangeFilterParameters)dv.CurrentObject).To = DateTime.Today;

            e.View = dv;

        }

        private void GenFullList_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DateRangeFilterParameters p = (DateRangeFilterParameters)e.PopupWindow.View.CurrentObject;

            DateFrom = p.From;
            DateTo = p.To;

            if (DateFrom == null || DateTo == null)
            {
                genCon.showMsg("Date is invalid", "Cannot Proceed. Date Range is invalid.", InformationType.Error);
                return;
            }

            buttonpress = true;

            RefreshController refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.DoExecute();
            }
        }

        private void GenAMSMeasure_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            buttonpress = true;

            RefreshController refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.DoExecute();
            }
        }

        private void GetYearMonth_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new YearMonthParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((YearMonthParameters)dv.CurrentObject).MyYear = myyear;
            ((YearMonthParameters)dv.CurrentObject).MyMonth = mymonth;

            e.View = dv;
        }

        private void GetYearMonth_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            YearMonthParameters p = (YearMonthParameters)e.PopupWindow.View.CurrentObject;
            myyear = p.MyYear;
            mymonth = p.MyMonth;

            buttonpress = true;

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));

            GeneratePM(myyear, mymonth, user.ID);

            RefreshController refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.DoExecute();
            }

        }

        private void GetDateRange_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new DateRangeFilterParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((DateRangeFilterParameters)dv.CurrentObject).From = DateTime.Today;
            ((DateRangeFilterParameters)dv.CurrentObject).To = DateTime.Today;

            e.View = dv;
        }

        private void GetDateRange_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DateRangeFilterParameters p = (DateRangeFilterParameters)e.PopupWindow.View.CurrentObject;
            DateFrom = p.From;
            DateTo = p.To;

            buttonpress = true;

            RefreshController refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.DoExecute();
            }
        }
    }
}
