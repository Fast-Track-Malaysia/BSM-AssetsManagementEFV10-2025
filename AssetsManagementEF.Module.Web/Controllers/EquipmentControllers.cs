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
namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class EquipmentControllers : ViewController
    {
        GenControllers genCon;
        public EquipmentControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Equipments);
            TargetViewNesting = Nesting.Root;

        }
        protected override void OnActivated()
        {
            base.OnActivated();

            this.CreatePMSchedule.Active.SetItemValue("Enabled", false);
            this.CreateWorkRequest.Active.SetItemValue("Enabled", false);
            this.EqCopySCE.Active.SetItemValue("Enabled", false);
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            IObjectSpace ios = Application.CreateObjectSpace();
            Positions position = ios.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));

            if (user.Roles.Where(p => p.Name == GeneralSettings.RequestorRole).Count() > 0)
            {
                if (position.IsCorrectiveMaintenance && View is DetailView)
                    this.CreateWorkRequest.Active.SetItemValue("Enabled", true);
            }
            if (user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0)
            {
                if (position.IsPreventiveMaintenance)
                    this.CreatePMSchedule.Active.SetItemValue("Enabled", true);
            }


            if (View.GetType() == typeof(DetailView))
            {
                this.EqCopySCE.Active.SetItemValue("Enabled", true);

                this.CreateWorkRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.CreatePMSchedule.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.EqCopySCE.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);

                ((DetailView)View).ViewEditModeChanged += EquipmentControllers_ViewEditModeChanged;
            }
            //this.CreateWorkRequest.Active["Deactivation in code"] = true;
            //this.CreatePMSchedule.Active["Deactivation in code"] = true;
        }
        private void EquipmentControllers_ViewEditModeChanged(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                this.CreateWorkRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.CreatePMSchedule.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.EqCopySCE.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
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
            if (View.GetType() == typeof(DetailView))
            {
                ((DetailView)View).ViewEditModeChanged -= EquipmentControllers_ViewEditModeChanged;
            }
        }


        private void CreateWorkRequest_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //if (View.ObjectTypeInfo.Type == typeof(Equipments))
            //{
            //}
            try
            {
                if (this.View is ListView)
                {
                    if (e.SelectedObjects.Count > 0)
                    {
                        if (e.SelectedObjects.Count > 1)
                        {
                            genCon.showMsg("Information", "Please select only ONE Equipment.", InformationType.Warning);
                            return;
                        }
                        else
                        {
                            IObjectSpace os;
                            os = Application.CreateObjectSpace();
                            WorkRequests obj = os.CreateObject<WorkRequests>();

                            foreach (Equipments selectedObject in e.SelectedObjects)
                            {
                                if (selectedObject.IsActive && selectedObject.IsApproved)
                                {
                                    WorkRequestEquipments dtl = os.CreateObject<WorkRequestEquipments>();

                                    dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                                    foreach (EquipmentComponents selectedObjectdtl in selectedObject.EquipmentComponent)
                                    {
                                        if (selectedObjectdtl.IsActive)
                                        {
                                            WorkRequestEqComponents dtldtl = os.CreateObject<WorkRequestEqComponents>();
                                            dtldtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", selectedObjectdtl.ID));
                                            dtldtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                                            //dtl.Detail.Add(dtldtl);
                                            obj.Detail2.Add(dtldtl);
                                        }
                                    }

                                    obj.Detail.Add(dtl);
                                }

                            }

                            //os.CommitChanges();                       
                            genCon.openNewView(os, obj, ViewEditMode.Edit);
                            genCon.showMsg("Successful", "Work Request Created.", InformationType.Success);
                        }
                    }
                    else
                    {
                        genCon.showMsg("Information", "No Equipment Selected.", InformationType.Warning);
                        return;
                    }

                }
                else
                {
                    BooleanParameters p = (BooleanParameters)e.PopupWindow.View.CurrentObject;
                    if (p.IsErr) return;

                    if (((DetailView)View).ViewEditMode != ViewEditMode.View)
                    {
                        genCon.showMsg("Failed", "Edit mode cannot proceed.", InformationType.Error);
                        return;
                    }
                    Equipments selectedObject = (Equipments)e.CurrentObject;
                    if (selectedObject.IsActive && selectedObject.IsApproved)
                    { }
                    else
                    {
                        genCon.showMsg("Failed", "Please select Active Equipment.", InformationType.Error);
                        return;
                    }
                    IObjectSpace os;
                    os = Application.CreateObjectSpace();
                    WorkRequests obj = os.CreateObject<WorkRequests>();

                    ListPropertyEditor listviewDetail = null;
                    if (selectedObject.EquipmentComponent.Count > 0)
                    {
                        foreach (ViewItem item in ((DetailView)View).Items)
                        {
                            if ((item is ListPropertyEditor) && (item.Id == "EquipmentComponent"))
                                listviewDetail = item as ListPropertyEditor;
                        }
                        if (listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0)
                        {
                            foreach (EquipmentComponents selectedObjectdtl in listviewDetail.ListView.SelectedObjects)
                            {
                                if (selectedObjectdtl.IsActive)
                                {
                                    WorkRequestEqComponents dtldtl = os.CreateObject<WorkRequestEqComponents>();
                                    dtldtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", selectedObjectdtl.ID));
                                    dtldtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                                    //dtl.Detail.Add(dtldtl);
                                    obj.Detail2.Add(dtldtl);
                                }
                            }
                            if (p.ParamBoolean)
                            {
                                WorkRequestEquipments dtl = os.CreateObject<WorkRequestEquipments>();

                                dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                                obj.Detail.Add(dtl);
                            }
                        }
                        else
                        {
                            WorkRequestEquipments dtl = os.CreateObject<WorkRequestEquipments>();

                            dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                            obj.Detail.Add(dtl);

                            /*
                            foreach (EquipmentComponents selectedObjectdtl in selectedObject.EquipmentComponent)
                            {
                                if (selectedObjectdtl.IsActive)
                                {
                                    WorkRequestEqComponents dtldtl = os.CreateObject<WorkRequestEqComponents>();
                                    dtldtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", selectedObjectdtl.ID));
                                    dtldtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                                    //dtl.Detail.Add(dtldtl);
                                    obj.Detail2.Add(dtldtl);
                                }
                            }
                            */
                        }
                    }
                    else
                    {
                        WorkRequestEquipments dtl = os.CreateObject<WorkRequestEquipments>();

                        dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                        obj.Detail.Add(dtl);
                    }
                    genCon.openNewView(os, obj, ViewEditMode.Edit);
                    genCon.showMsg("Successful", "Work Request Created.", InformationType.Success);

                }


            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }

        }

        private void CreatePMSchedule_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //if (View.ObjectTypeInfo.Type == typeof(Equipments))
            //{
            //}
            try
            {

                if (this.View is ListView)
                {
                    if (e.SelectedObjects.Count > 0)
                    {

                        IObjectSpace os;
                        os = Application.CreateObjectSpace();
                        PMSchedules obj = os.CreateObject<PMSchedules>();

                        foreach (Equipments selectedObject in e.SelectedObjects)
                        {
                            if (selectedObject.IsActive && selectedObject.IsApproved)
                            {
                                PMScheduleEquipments dtl = os.CreateObject<PMScheduleEquipments>();

                                dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                                foreach (EquipmentComponents selectedObjectdtl in selectedObject.EquipmentComponent)
                                {
                                    if (selectedObjectdtl.IsActive)
                                    {
                                        PMScheduleEqComponents dtldtl = os.CreateObject<PMScheduleEqComponents>();
                                        dtldtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", selectedObjectdtl.ID));
                                        dtldtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                                        //dtl.Detail.Add(dtldtl);
                                        obj.Detail2.Add(dtldtl);
                                    }
                                }

                                obj.Detail.Add(dtl);
                            }

                        }

                        //os.CommitChanges();                       
                        genCon.openNewView(os, obj, ViewEditMode.Edit);
                        genCon.showMsg("Successful", "PM Schedule Created.", InformationType.Success);
                    }
                    else
                        genCon.showMsg("Information", "No Equipment Selected.", InformationType.Warning);

                }
                else
                {
                    BooleanParameters p = (BooleanParameters)e.PopupWindow.View.CurrentObject;
                    if (p.IsErr) return;

                    if (((DetailView)View).ViewEditMode != ViewEditMode.View)
                    {
                        genCon.showMsg("Failed", "Edit mode cannot proceed.", InformationType.Error);
                        return;
                    }
                    Equipments selectedObject = (Equipments)e.CurrentObject;
                    if (selectedObject.IsActive && selectedObject.IsApproved)
                    { }
                    else
                    {
                        genCon.showMsg("Failed", "Please select Active Equipment.", InformationType.Error);
                        return;
                    }

                    IObjectSpace os;
                    os = Application.CreateObjectSpace();
                    PMSchedules obj = os.CreateObject<PMSchedules>();

                    ListPropertyEditor listviewDetail = null;
                    if (selectedObject.EquipmentComponent.Count > 0)
                    {
                        foreach (ViewItem item in ((DetailView)View).Items)
                        {
                            if ((item is ListPropertyEditor) && (item.Id == "EquipmentComponent"))
                                listviewDetail = item as ListPropertyEditor;
                        }
                        if (listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0)
                        {
                            foreach (EquipmentComponents selectedObjectdtl in listviewDetail.ListView.SelectedObjects)
                            {
                                if (selectedObjectdtl.IsActive)
                                {
                                    PMScheduleEqComponents dtldtl = os.CreateObject<PMScheduleEqComponents>();
                                    dtldtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", selectedObjectdtl.ID));
                                    dtldtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                                    //dtl.Detail.Add(dtldtl);
                                    obj.Detail2.Add(dtldtl);
                                }
                            }
                            if (p.ParamBoolean)
                            {
                                PMScheduleEquipments dtl = os.CreateObject<PMScheduleEquipments>();

                                dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                                obj.Detail.Add(dtl);
                            }
                        }
                        else
                        {
                            PMScheduleEquipments dtl = os.CreateObject<PMScheduleEquipments>();

                            dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                            obj.Detail.Add(dtl);
                            /*
                            foreach (EquipmentComponents selectedObjectdtl in selectedObject.EquipmentComponent)
                            {
                                if (selectedObjectdtl.IsActive)
                                {
                                    PMScheduleEqComponents dtldtl = os.CreateObject<PMScheduleEqComponents>();
                                    dtldtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", selectedObjectdtl.ID));
                                    dtldtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                                    //dtl.Detail.Add(dtldtl);
                                    obj.Detail2.Add(dtldtl);
                                }
                            }
                            */
                        }
                    }
                    else
                    {
                        PMScheduleEquipments dtl = os.CreateObject<PMScheduleEquipments>();

                        dtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                        obj.Detail.Add(dtl);
                    }

                    genCon.openNewView(os, obj, ViewEditMode.Edit);
                    genCon.showMsg("Successful", "PM Schedule Created.", InformationType.Success);

                }
                return;

            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }

        }

        private void BooleanParametersCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            bool witheq = true;
            bool disable = true;
            string msg = "Press OK to continue.";

            try
            {
                if (this.View is ListView)
                {
                    err = true;
                }
                else
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        genCon.showMsg("Failed", "Edit Equipment cannot proceed.", InformationType.Error);
                        err = true;
                    }
                    Equipments selectedObject = (Equipments)View.CurrentObject;
                    if (selectedObject.IsNew && !err)
                    {
                        genCon.showMsg("Failed", "New Equipment cannot proceed.", InformationType.Error);
                        err = true;
                    }
                    else
                    {
                        SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                        Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                        //if (selectedObject.AssignPlannerGroup != position.PlannerGroup)
                        //{
                        //    genCon.showMsg("Failed", "Only Planner Group [" + selectedObject.AssignPlannerGroup.BoName + "] allowed to create Work Order.", InformationType.Error);
                        //    err = true;
                        //}

                        if (e.Action.Id == this.CreateWorkRequest.Id)
                        {
                            if (((DetailView)View).ViewEditMode != ViewEditMode.View)
                            {
                                genCon.showMsg("Failed", "Edit mode cannot proceed.", InformationType.Error);
                                err = true;
                            }
                            if (selectedObject.IsActive && selectedObject.IsApproved)
                            { }
                            else
                            {
                                genCon.showMsg("Failed", "Please select Active Equipment.", InformationType.Error);
                                err = true;
                            }
                        }
                        else if (e.Action.Id == this.CreatePMSchedule.Id)
                        {
                            if (((DetailView)View).ViewEditMode != ViewEditMode.View)
                            {
                                genCon.showMsg("Failed", "Edit mode cannot proceed.", InformationType.Error);
                                err = true;
                            }
                            if (selectedObject.IsActive && selectedObject.IsApproved)
                            { }
                            else
                            {
                                genCon.showMsg("Failed", "Please select Active Equipment.", InformationType.Error);
                                err = true;
                            }
                        }
                    }

                    ListPropertyEditor listviewDetail = null;
                    if (selectedObject.EquipmentComponent.Count > 0)
                    {
                        foreach (ViewItem item in ((DetailView)View).Items)
                        {
                            if ((item is ListPropertyEditor) && (item.Id == "EquipmentComponent"))
                                listviewDetail = item as ListPropertyEditor;
                        }
                        if (listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0)
                        {
                            witheq = false;
                            disable = false;
                            msg = "Please tick the Order with Equipment? box if you want to combine Equipment with Component in one order.";
                        }
                        else
                        {
                        }
                    }
                }
                IObjectSpace os = Application.CreateObjectSpace();
                DetailView dv = Application.CreateDetailView(os, new BooleanParameters());
                dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                ((BooleanParameters)dv.CurrentObject).ParamBoolean = witheq;
                ((BooleanParameters)dv.CurrentObject).IsErr = err;
                ((BooleanParameters)dv.CurrentObject).ActionMessage = msg;
                ((BooleanParameters)dv.CurrentObject).IsDisable = disable;


                e.View = dv;
            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }

        }

        private void EqCopySCE_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            Equipments masterobject = (Equipments)View.CurrentObject;

            int id = GeneralSettings.sceid; //masterobject.Criticality.ID;

            IObjectSpace newObjectSpace = Application.CreateObjectSpace(typeof(SCESubCategories));
            string listViewId = Application.FindLookupListViewId(typeof(SCESubCategories));
            CollectionSourceBase collectionSource = Application.CreateCollectionSource(newObjectSpace, typeof(SCESubCategories), listViewId);
            collectionSource.Criteria["Filter01"] = CriteriaOperator.Parse("SCECategory.Criticality.ID = ?", id);

            e.View = Application.CreateListView(listViewId, collectionSource, true);
        }

        private void EqCopySCE_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            try
            {
                Equipments masterobject = (Equipments)View.CurrentObject;

                if (((ListView)e.PopupWindow.View).SelectedObjects.Count != 1)
                {
                    genCon.showMsg("Cannot proceed", "Please select only 1 item.", InformationType.Error);
                    return;
                }
                int catid = 0;
                int id = 0;
                foreach (var dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                {
                    id = ((SCESubCategories)dtl).ID;
                    catid = ((SCESubCategories)dtl).SCECategory.ID;
                }
                masterobject.Criticality = View.ObjectSpace.GetObjectByKey<Criticalities>(GeneralSettings.sceid);
                masterobject.SCECategory = View.ObjectSpace.GetObjectByKey<SCECategories>(catid);
                masterobject.SCESubCategory = View.ObjectSpace.GetObjectByKey<SCESubCategories>(id);

            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);
                throw new Exception(ex.Message);
            }

        }
    }
}
