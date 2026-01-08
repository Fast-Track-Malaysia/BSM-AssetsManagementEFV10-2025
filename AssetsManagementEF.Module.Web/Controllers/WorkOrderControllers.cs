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
using System.Web.UI;
using DevExpress.ExpressApp.Web;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class WorkOrderControllers : ViewController
    {
        GenControllers genCon;
        public WorkOrderControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(WorkOrders);
            TargetViewType = ViewType.DetailView;
            TargetViewNesting = Nesting.Root;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.CreatePR.Active.SetItemValue("Enabled", false);
            this.CancelOrder.Active.SetItemValue("Enabled", false);
            this.PassOrder.Active.SetItemValue("Enabled", false);
            this.RejectOrder.Active.SetItemValue("Enabled", false);
            this.ApproveOrder.Active.SetItemValue("Enabled", false);
            this.ChangeJobStatus.Active.SetItemValue("Enabled", false);
            this.TechnicalClosure.Active.SetItemValue("Enabled", false);
            this.DuplicateWO.Active.SetItemValue("Enabled", false);
            this.GoToPR.Active.SetItemValue("Enabled", false);
            this.PrintCheckList.Active.SetItemValue("Enabled", false);
            this.BackToPMPatch.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            WorkOrders selectedObject = (WorkOrders)View.CurrentObject;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
            bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
            bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;
            bool IsCancelRequest = user.Roles.Where(p => p.Name == GeneralSettings.CancelWORole).Count() > 0 ? true : false;
            bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

            if (isContractor)
            {
                if (!selectedObject.IsNew)
                {
                    if (selectedObject.DocPassed || selectedObject.Cancelled || selectedObject.IsClosed)
                    { }
                    else
                    {
                        this.ChangeJobStatus.Active.SetItemValue("Enabled", true);
                    }
                    if (selectedObject.DocPassed || selectedObject.Approved || selectedObject.Cancelled || selectedObject.IsClosed)
                    { }
                    else
                    {
                        this.PassOrder.Active.SetItemValue("Enabled", true);
                    }
                }
            }
            else
            {
                if (selectedObject.JobStatus != null && selectedObject.JobStatus.BoCode == GeneralSettings.InitPMJobStatus)
                {
                    this.BackToPMPatch.Active.SetItemValue("Enabled", true);
                }
                else
                {
                    if (!selectedObject.IsNew)
                    {
                        if (IsPlanner || IsWPS)
                        {
                            this.DuplicateWO.Active.SetItemValue("Enabled", true);
                            if (selectedObject.DocPassed || selectedObject.Cancelled || selectedObject.IsClosed)
                            { }
                            else
                            {
                                this.ChangeJobStatus.Active.SetItemValue("Enabled", true);
                                this.CreatePR.Active.SetItemValue("Enabled", true);
                            }
                        }
                        if (IsPlanner)
                        {
                            if (selectedObject.DocPassed || selectedObject.Approved || selectedObject.Cancelled || selectedObject.IsClosed)
                            { }
                            else
                            {
                                this.PassOrder.Active.SetItemValue("Enabled", true);
                            }
                        }
                        if (IsPlanner || IsCancelRequest)
                        {
                            if (selectedObject.DocPassed || selectedObject.Approved || selectedObject.Cancelled || selectedObject.IsClosed)
                            { }
                            else
                            {
                                this.CancelOrder.Active.SetItemValue("Enabled", true);
                            }
                        }
                        if (IsWPS)
                        {
                            if (selectedObject.Approved && !selectedObject.IsClosed)
                            {
                                this.TechnicalClosure.Active.SetItemValue("Enabled", true);
                            }
                        }
                        if (IsApprover)
                        {
                            if (selectedObject.DocPassed)
                            {
                                this.RejectOrder.Active.SetItemValue("Enabled", true);
                                this.ApproveOrder.Active.SetItemValue("Enabled", true);
                            }
                            if (selectedObject.Approved && !selectedObject.IsClosed)
                            {
                                this.ApproveOrder.Active.SetItemValue("Enabled", true);
                            }
                        }
                        if (selectedObject.DetailRequest.Count > 0)
                        {
                            this.GoToPR.Active.SetItemValue("Enabled", true);
                        }
                        if (selectedObject.IsPreventiveMaintenance)
                            this.BackToPMPatch.Active.SetItemValue("Enabled", true);
                    }
                }
            }
            this.CreatePR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.CancelOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.PassOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.RejectOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.ApproveOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.ChangeJobStatus.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.TechnicalClosure.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.DuplicateWO.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.GoToPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.BackToPMPatch.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);

            if (View.GetType() == typeof(DetailView))
                ((DetailView)View).ViewEditModeChanged += WorkOrderControllers_ViewEditModeChanged;
        }

        private void WorkOrderControllers_ViewEditModeChanged(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                this.CreatePR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.CancelOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.PassOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.RejectOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.ApproveOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.ChangeJobStatus.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.TechnicalClosure.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.DuplicateWO.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.GoToPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.BackToPMPatch.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
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
                ((DetailView)View).ViewEditModeChanged -= WorkOrderControllers_ViewEditModeChanged;
            }

        }

        private void CreatePR_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                if (((DetailView)View).ViewEditMode != ViewEditMode.View)
                {
                    genCon.showMsg("Failed", "Editing Work Order cannot proceed.", InformationType.Error);
                    return;
                }
                WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
                if (selectedObject.IsNew)
                {
                    genCon.showMsg("Failed", "New Work Order cannot proceed.", InformationType.Error);
                    return;
                }
                if (selectedObject.IsClosed)
                {
                    genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                    return;
                }
                if (selectedObject.IsPlannerChecking || selectedObject.IsWPSChecking)
                {
                    //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                    //Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                    //if (selectedObject.AssignPlannerGroup != position.PlannerGroup)
                    //{
                    //    genCon.showMsg("Failed", "Only Planner Group [" + selectedObject.AssignPlannerGroup.BoName + "] allowed to create Work Order.", InformationType.Error);
                    //    return;
                    //}

                }
                else
                {
                    genCon.showMsg("Failed", "This is not a Planner not WPS process.", InformationType.Error);
                    return;
                }
                IObjectSpace os;
                os = Application.CreateObjectSpace();
                PurchaseRequests obj = os.CreateObject<PurchaseRequests>();

                obj.WorkOrder = os.FindObject<WorkOrders>(CriteriaOperator.Parse("ID=?", selectedObject.ID));
                obj.PlannerGroup = os.FindObject<PlannerGroups>(new BinaryOperator("ID", selectedObject.AssignPlannerGroup.ID));
                obj.IsPreventiveMaintenance = selectedObject.IsPreventiveMaintenance;

                genCon.openNewView(os, obj, ViewEditMode.Edit);
                genCon.showMsg("Successful", "Purchase Request Created.", InformationType.Success);

            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }


        }

        private void PrintCheckList_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Page page = null;
            //if (Frame.Template is Page)
            //{
            //    page = (Page)Frame.Template;
            //}
            //else
            //{
            //    page = ((Control)Frame.Template).Page;
            //}
            //string script = @"<script>
            //            var openedWindow = window.open('{0}', 'PDF');
            //        </script>";
            //script = string.Format(script, ActualPDFFileName);
            //page.ClientScript.RegisterStartupScript(GetType(), "clientScriptForPDFPopupWindow", script);
            string ActualPDFFileName = @"https://www.devexpress.com/Products/NET/Application_Framework/data/pdf/eXpressAppFrameworkOverview.pdf";
            string script = string.Format(@"var openedWindow = window.open('{0}', 'PDF');", ActualPDFFileName);
            WebWindow.CurrentRequestWindow.RegisterStartupScript("clientScriptForPDFPopupWindow", script);

        }


        private void StringParametersCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            if (((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Viewing Work Order cannot proceed.", InformationType.Error);
                err = true;
            }
            else
            {
                WorkOrders selectedObject = (WorkOrders)View.CurrentObject;
                if (selectedObject.IsNew && !err)
                {
                    genCon.showMsg("Failed", "New Work Order cannot proceed.", InformationType.Error);
                    err = true;
                }
                else
                {
                    SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                    //bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;
                    //Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                    //if (selectedObject.AssignPlannerGroup != position.PlannerGroup)
                    //{
                    //    genCon.showMsg("Failed", "Only Planner Group [" + selectedObject.AssignPlannerGroup.BoName + "] allowed to create Work Order.", InformationType.Error);
                    //    err = true;
                    //}

                    if (e.Action.Id == this.TechnicalClosure.Id)
                    {
                        if (selectedObject.IsClosed && !err)
                        {
                            genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (!selectedObject.Approved)
                        {
                            genCon.showMsg("Failed", "Only Approved Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }

                        foreach (PurchaseRequests dtl in selectedObject.DetailRequest)
                        {
                            if (dtl.DocPassed || dtl.Cancelled || dtl.DocPosted)
                            { }
                            else
                            {
                                genCon.showMsg("Failed", "There are still Pending Purchase Request to be passed.", InformationType.Error);
                                err = true;
                                break;
                            }
                        }
                    }
                    else if (e.Action.Id == this.CancelOrder.Id)
                    {
                        if (selectedObject.IsClosed && !err)
                        {
                            genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.Cancelled && !err)
                        {
                            genCon.showMsg("Failed", "Cancelled Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.Approved && !err)
                        {
                            genCon.showMsg("Failed", "Approved Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }

                        if (selectedObject.DocPassed && !err)
                        {
                            genCon.showMsg("Failed", "Passed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }

                        if (!err)
                        {
                            if (selectedObject.IsPlannerChecking)
                            { }
                            else if (selectedObject.IsCancelUserChecking)
                            { }
                            else
                            {
                                genCon.showMsg("Failed", "Only Planner belong to the Planner Group / Cancel Role allowed.", InformationType.Error);
                                err = true;
                            }
                        }
                    }
                    else if (e.Action.Id == this.PassOrder.Id && !err)
                    {
                        if (selectedObject.DocPassed && !err)
                        {
                            genCon.showMsg("Failed", "Passed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.IsClosed && !err)
                        {
                            genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.Approved && !err)
                        {
                            genCon.showMsg("Failed", "Approved Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }

                        if (!selectedObject.IsPlannerChecking && !err)
                        {
                            genCon.showMsg("Failed", "This is not a Planner process.", InformationType.Error);
                            err = true;
                        }

                        if (selectedObject.Cancelled && !err)
                        {
                            genCon.showMsg("Failed", "Cancelled Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }

                        if (!selectedObject.IsPreventiveMaintenance && !err)
                        {
                            if (selectedObject.ScheduleStartDate == null || selectedObject.ScheduleEndDate == null)
                            {
                                genCon.showMsg("Failed", "Non-scheduled CM Work Order cannot proceed.", InformationType.Error);
                                err = true;
                            }
                            else if (selectedObject.ScheduleStartDate > selectedObject.ScheduleEndDate)
                            {
                                genCon.showMsg("Failed", "Schedule Date is invalid.", InformationType.Error);
                                err = true;
                            }
                        }
                    }
                    else if (e.Action.Id == this.RejectOrder.Id)
                    {
                        if (selectedObject.Rejected && !err)
                        {
                            genCon.showMsg("Failed", "Rejected Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.IsClosed && !err)
                        {
                            genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.Approved && !err)
                        {
                            genCon.showMsg("Failed", "Approved Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }

                        if (!selectedObject.IsApproverChecking && !err)
                        {
                            genCon.showMsg("Failed", "This is not a Approver process.", InformationType.Error);
                            err = true;
                        }

                        if (!selectedObject.DocPassed && !err)
                        {
                            genCon.showMsg("Failed", "Pending Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                    }
                    else if (e.Action.Id == this.ApproveOrder.Id)
                    {
                        if (selectedObject.Cancelled && !err)
                        {
                            genCon.showMsg("Failed", "Cancelled Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (selectedObject.IsClosed && !err)
                        {
                            genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                            err = true;
                        }
                        if (!selectedObject.IsApproverChecking && !err)
                        {
                            genCon.showMsg("Failed", "This is not a Approver process.", InformationType.Error);
                            err = true;
                        }
                        if (!selectedObject.Approved)
                        {
                            if (!selectedObject.DocPassed && !err)
                            {
                                genCon.showMsg("Failed", "Pending Work Order cannot proceed.", InformationType.Error);
                                err = true;
                            }
                            if (!err)
                            {
                                if (selectedObject.PlanStartDate == null || selectedObject.PlanEndDate == null)
                                {
                                    genCon.showMsg("Failed", "Planning Date is not assigned.", InformationType.Error);
                                    err = true;
                                }
                            }
                        }
                        else
                        {
                            if (selectedObject.Approved && !err)
                            {
                                genCon.showMsg("Information", "Action will undo the Approval.", InformationType.Info);
                                //err = true;
                            }
                        }
                    }
                }
            }

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new StringParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = err;
            ((StringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;
        }

        private void ChangeJobStatus_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            bool isPlanning = false;
            bool isPreExecution = false;
            bool isExecution = false;
            bool isPostExecution = false;

            WorkOrders selectedObject = (WorkOrders)View.CurrentObject;
            if (((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Viewing Work Order proceed.", InformationType.Error);
                err = true;
            }
            else
            {
                if (selectedObject.IsNew)
                {
                    genCon.showMsg("Failed", "New Work Order cannot proceed.", InformationType.Error);
                    err = true;
                }
                else if (selectedObject.IsClosed)
                {
                    genCon.showMsg("Failed", "Closed Work Order cannot proceed.", InformationType.Error);
                    err = true;
                }
                else if (selectedObject.Cancelled)
                {
                    genCon.showMsg("Failed", "Cancelled Work Order cannot proceed.", InformationType.Error);
                    err = true;
                }
                else if (selectedObject.DocPassed)
                {
                    genCon.showMsg("Failed", "Passed Work Order cannot proceed.", InformationType.Error);
                    err = true;
                }


            }

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new JobStringParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((JobStringParameters)dv.CurrentObject).IsErr = err;
            if (selectedObject.JobStatus != null)
                ((JobStringParameters)dv.CurrentObject).OldJobStatus = os.GetObjectByKey<JobStatuses>(selectedObject.JobStatus.ID);
            ((JobStringParameters)dv.CurrentObject).IsPlanning = isPlanning;
            ((JobStringParameters)dv.CurrentObject).IsPreExecution = isPreExecution;
            ((JobStringParameters)dv.CurrentObject).IsExecution = isExecution;
            ((JobStringParameters)dv.CurrentObject).IsPostExecution = isPostExecution;
            ((JobStringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;

        }

        private void ChangeJobStatus_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
            JobStringParameters p = (JobStringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            if (p.JobStatus == null)
            {
                genCon.showMsg("Failed", "Please select New Job Status.", InformationType.Error);
                return;
            }
            if (p.JobStatus == p.OldJobStatus)
            {
                genCon.showMsg("Failed", "Please change to New Job Status.", InformationType.Error);
                return;
            }

            selectedObject.JobStatus = ObjectSpace.GetObjectByKey<JobStatuses>(p.JobStatus.ID);
            selectedObject.OnPropertyChanged("JobStatus");

            WorkOrderJobStatuses js = ObjectSpace.CreateObject<WorkOrderJobStatuses>();
            js.JobStatus = ObjectSpace.GetObjectByKey<JobStatuses>(p.JobStatus.ID);
            js.JobRemarks = p.ParamString;
            selectedObject.DetailJobStatus.Add(js);
            selectedObject.OnPropertyChanged("DetailJobStatus");

            ModificationsController controller = Frame.GetController<ModificationsController>();
            if (controller != null)
            {
                controller.SaveAction.DoExecute();
            }

            genCon.showMsg("Successful", "Change Job Status Done.", InformationType.Success);
        }

        private void CancelOrder_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.DocPassed || selectedObject.Approved || selectedObject.Cancelled || selectedObject.IsClosed)
            {
                //selectedObject.Cancelled = false;

                //WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                //ds.DocStatus = DocumentStatus.Cancelled;
                //ds.DocRemarks = p.ParamString;
                //ds.IsReverse = true;
                //selectedObject.DetailDocStatus.Add(ds);
                //selectedObject.OnPropertyChanged("DetailDocStatus");

                //genCon.showMsg("Successful", "Undo Cancel Done. Please save changes afterwards.", InformationType.Success);
            }
            else
            {
                selectedObject.Cancelled = true;
                selectedObject.DocPassed = false;
                selectedObject.Rejected = false;
                selectedObject.Approved = false;

                WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.Cancelled;
                ds.DocRemarks = p.ParamString;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");

                foreach (PurchaseRequests obj in selectedObject.DetailRequest)
                {
                    if (obj.DocPosted || obj.Cancelled)
                    { }
                    else
                    {
                        obj.DocPassed = false;
                        obj.Cancelled = true;

                        PurchaseRequestDocStatuses objds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                        objds.DocStatus = DocumentStatus.Cancelled;
                        objds.DocRemarks = p.ParamString;
                        obj.DetailDocStatus.Add(objds);
                    }
                }
                selectedObject.IsCancelling = true;
                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Cancel Done.", InformationType.Success);
            }

        }

        private void PassOrder_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.DocPassed)
            {
                //selectedObject.DocPassed = false;

                //WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                //ds.DocStatus = DocumentStatus.DocPassed;
                //ds.DocRemarks = p.ParamString;
                //ds.IsReverse = true;
                //selectedObject.DetailDocStatus.Add(ds);
                //selectedObject.OnPropertyChanged("DetailDocStatus");

                //genCon.showMsg("Successful", "Undo Passing Done. Please save changes afterwards.", InformationType.Success);
            }
            else
            {
                selectedObject.DocPassed = true;
                selectedObject.Rejected = false;
                selectedObject.Approved = false;
                selectedObject.Cancelled = false;

                WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.DocPassed;
                ds.DocRemarks = p.ParamString;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");

                foreach (PurchaseRequests obj in selectedObject.DetailRequest)
                {
                    if (obj.Cancelled || obj.DocPosted)
                    { }
                    else
                    {
                        obj.DocPassed = true;
                        obj.Rejected = false;

                        PurchaseRequestDocStatuses objds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                        objds.DocStatus = DocumentStatus.DocPassed;
                        objds.DocRemarks = p.ParamString;
                        obj.DetailDocStatus.Add(objds);
                    }
                }

                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Passing Done.", InformationType.Success);

                if (GeneralSettings.EmailSend)
                {
                    try
                    {
                        IList<Positions> positionlist = ObjectSpace.GetObjects<Positions>(new ContainsOperator("DetailPlannerGroup", new BinaryOperator("ID", selectedObject.AssignPlannerGroup.ID, BinaryOperatorType.Equal)));
                        List<string> ToEmails = new List<string>();

                        foreach (Positions position in positionlist)
                        {
                            if (selectedObject.IsPreventiveMaintenance)
                            {
                                if (position.IsPreventiveMaintenance && position.CurrentUser.UserEmail.Trim() != "")
                                    if (position.CurrentUser.Roles.Where(po => po.Name == GeneralSettings.ApproverRole).Count() > 0)
                                    {
                                        ToEmails.Add(position.CurrentUser.UserEmail.Trim());
                                    }
                            }
                            else
                            {
                                if (position.IsCorrectiveMaintenance && position.CurrentUser.UserEmail.Trim() != "")
                                    if (position.CurrentUser.Roles.Where(po => po.Name == GeneralSettings.PlannerRole).Count() > 0)
                                    {
                                        ToEmails.Add(position.CurrentUser.UserEmail.Trim());
                                    }
                            }

                        }
                        if (ToEmails.Count > 0)
                            genCon.SendEmail("Work Order Pending For Approval.", "Planner Group = " + selectedObject.AssignPlannerGroup.BoCode + System.Environment.NewLine + "Document No = " + selectedObject.DocNum, ToEmails);
                    }
                    catch (Exception ex)
                    {
                        genCon.showMsg("Error", ex.Message, InformationType.Error);
                    }
                }

            }

        }

        private void ApproveOrder_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.Approved)
            {
                selectedObject.Approved = false;
                selectedObject.DocPassed = true;

                WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.Approved;
                ds.DocRemarks = p.ParamString;
                ds.IsReverse = true;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");

                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Undo Approval Done. Please save changes afterwards.", InformationType.Success);
            }
            else
            {
                selectedObject.Approved = true;
                selectedObject.DocPassed = false;
                selectedObject.Rejected = false;
                selectedObject.Cancelled = false;

                WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.Approved;
                ds.DocRemarks = p.ParamString;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");

                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Approval Done.", InformationType.Success);
            }

        }

        private void RejectOrder_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.Rejected)
            {
                //selectedObject.Rejected = false;

                //WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                //ds.DocStatus = DocumentStatus.Rejected;
                //ds.DocRemarks = p.ParamString;
                //ds.IsReverse = true;
                //selectedObject.DetailDocStatus.Add(ds);
                //selectedObject.OnPropertyChanged("DetailDocStatus");

                //foreach (PurchaseRequests obj in selectedObject.DetailRequest)
                //{
                //    if (obj.Rejected)
                //    {
                //        obj.Rejected = false;

                //        PurchaseRequestDocStatuses objds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                //        objds.DocStatus = DocumentStatus.Rejected;
                //        objds.DocRemarks = p.ParamString;
                //        objds.IsReverse = true;
                //        obj.DetailDocStatus.Add(objds);
                //    }
                //}

                //genCon.showMsg("Successful", "Undo Reject Done. Please save changes afterwards.", InformationType.Success);
            }
            else
            {
                selectedObject.Rejected = true;
                selectedObject.Approved = false;
                selectedObject.DocPassed = false;
                selectedObject.Cancelled = false;

                WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.Rejected;
                ds.DocRemarks = p.ParamString;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");

                foreach (PurchaseRequests obj in selectedObject.DetailRequest)
                {
                    if (obj.DocPassed)
                    {
                        obj.Rejected = true;
                        obj.DocPassed = false;

                        PurchaseRequestDocStatuses objds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                        objds.DocStatus = DocumentStatus.Rejected;
                        objds.DocRemarks = p.ParamString;
                        obj.DetailDocStatus.Add(objds);
                    }
                }

                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Reject Done.", InformationType.Success);
            }

        }

        private void TechnicalClosure_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.Approved)
            {
                selectedObject.JobStatus = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", GeneralSettings.ClosureJobStatus));
                selectedObject.OnPropertyChanged("JobStatus");
                selectedObject.IsClosed = true;

                WorkOrderJobStatuses js = ObjectSpace.CreateObject<WorkOrderJobStatuses>();
                js.JobStatus = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", GeneralSettings.ClosureJobStatus));
                js.JobRemarks = p.ParamString;
                selectedObject.DetailJobStatus.Add(js);
                selectedObject.OnPropertyChanged("DetailJobStatus");

                WorkOrderDocStatuses ds = ObjectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.Closed;
                ds.DocRemarks = p.ParamString;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");
                selectedObject.IsCancelling = true;
                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Technical Closure Done.", InformationType.Success);

            }

        }

        private void DuplicateWO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    genCon.showMsg("Failed", "Editing Work Order cannot create Work Order.", InformationType.Error);
                    return;
                }
                WorkOrders selectedObject = (WorkOrders)e.CurrentObject;
                if (selectedObject.IsNew)
                {
                    genCon.showMsg("Failed", "New PM Schedule cannot proceed.", InformationType.Error);
                    return;
                }

                if (selectedObject.IsWPSChecking)
                {
                    //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                    //Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                    //if (selectedObject.AssignPlannerGroup != position.PlannerGroup)
                    //{
                    //    genCon.showMsg("Failed", "Only Planner Group [" + selectedObject.AssignPlannerGroup.BoName + "] allowed to create Work Order.", InformationType.Error);
                    //    return;
                    //}
                }
                else
                {
                    genCon.showMsg("Failed", "Only WPS allowed to create Work Order.", InformationType.Error);
                    return;
                }

                IObjectSpace os;
                os = Application.CreateObjectSpace();
                WorkOrders obj = os.CreateObject<WorkOrders>();

                //if (selectedObject.DetailDescription != null)
                //{
                //    WRLongDescription desc = os.CreateObject<WRLongDescription>();
                //    desc.LongDescription = selectedObject.DetailDescription.LongDescription;
                //    obj.WRDetailDescription = desc;
                //}

                if (selectedObject.AssignPlannerGroup != null)
                    obj.AssignPlannerGroup = os.FindObject<PlannerGroups>(CriteriaOperator.Parse("ID=?", selectedObject.AssignPlannerGroup.ID));

                if (selectedObject.Priority != null)
                    obj.Priority = os.FindObject<Priorities>(CriteriaOperator.Parse("ID=?", selectedObject.Priority.ID));

                if (selectedObject.PMSchedule != null)
                    obj.PMSchedule = os.FindObject<PMSchedules>(CriteriaOperator.Parse("ID=?", selectedObject.PMSchedule.ID));

                if (selectedObject.WorkRequest != null)
                    obj.WorkRequest = os.FindObject<WorkRequests>(CriteriaOperator.Parse("ID=?", selectedObject.WorkRequest.ID));

                if (selectedObject.PMDate != null)
                    obj.PMDate = selectedObject.PMDate;

                obj.WorkInstruction = selectedObject.WorkInstruction;
                obj.CheckListName = selectedObject.CheckListName;
                obj.CheckListLink = selectedObject.CheckListLink;
                if (selectedObject.CheckList != null)
                    obj.CheckList = os.FindObject<CheckLists>(CriteriaOperator.Parse("ID=?", selectedObject.CheckList.ID));

                obj.PMClass = os.FindObject<PMClasses>(new BinaryOperator("ID", selectedObject.PMClass.ID));
                obj.DocType = os.FindObject<DocTypes>(new BinaryOperator("ID", selectedObject.DocType.ID));
                obj.IsPreventiveMaintenance = selectedObject.IsPreventiveMaintenance;
                obj.Remarks = selectedObject.Remarks;

                ListPropertyEditor listviewDetail = null;
                ListPropertyEditor listviewDetail2 = null;
                if (((WorkOrders)View.CurrentObject).Detail.Count > 0 || ((WorkOrders)View.CurrentObject).Detail2.Count > 0)
                {
                    foreach (ViewItem item in ((DetailView)View).Items)
                    {
                        if ((item is ListPropertyEditor) && (item.Id == "Detail"))
                            listviewDetail = item as ListPropertyEditor;
                        if ((item is ListPropertyEditor) && (item.Id == "Detail2"))
                            listviewDetail2 = item as ListPropertyEditor;
                    }
                    if ((listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0) || (listviewDetail2 != null && listviewDetail2.ListView != null && listviewDetail2.ListView.SelectedObjects.Count > 0))
                    {
                        if (((WorkOrders)View.CurrentObject).Detail.Count > 0 && listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0)
                        {
                            foreach (WorkOrderEquipments dtl in listviewDetail.ListView.SelectedObjects)
                            {
                                if (dtl.Equipment.IsApproved && dtl.Equipment.IsActive)
                                {
                                    WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                    objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl.Equipment.ID));

                                    obj.Detail.Add(objdtl);

                                }
                            }

                        }
                        if (((WorkOrders)View.CurrentObject).Detail2.Count > 0 && listviewDetail2 != null && listviewDetail2.ListView != null && listviewDetail2.ListView.SelectedObjects.Count > 0)
                        {
                            foreach (WorkOrderEqComponents dtl2 in listviewDetail2.ListView.SelectedObjects)
                            {
                                if (dtl2.Equipment.IsApproved && dtl2.Equipment.IsActive)
                                {
                                    if (obj.Detail.Count(p => p.Equipment.ID == dtl2.Equipment.ID) <= 0)
                                    {
                                        WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                        objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));

                                        obj.Detail.Add(objdtl);

                                    }
                                    WorkOrderEqComponents objdtl2 = os.CreateObject<WorkOrderEqComponents>();

                                    objdtl2.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));
                                    objdtl2.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", dtl2.EquipmentComponent.ID));

                                    obj.Detail2.Add(objdtl2);
                                }
                            }
                        }

                        //if (listviewDetail != null && listviewDetail.ListView.SelectedObjects.Count == 0)
                        //    throw new UserFriendlyException("WARNING  -  You must select only ONE charge split to add a credit split !");
                    }
                    else
                    {
                        throw new UserFriendlyException("WARNING  -  You must select at least ONE Equipment or Component to Create New Work Order.");
                        /*
                         foreach (WorkOrderEquipments dtl in selectedObject.Detail)
                         {
                             if (dtl.Equipment.IsApproved && dtl.Equipment.IsActive)
                             {
                                 WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                 objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl.Equipment.ID));

                                 obj.Detail.Add(objdtl);

                             }
                         }
                         foreach (WorkOrderEqComponents dtl2 in selectedObject.Detail2)
                         {
                             if (dtl2.Equipment.IsApproved && dtl2.Equipment.IsActive)
                             {
                                 if (obj.Detail.Count(p => p.Equipment.ID == dtl2.Equipment.ID) <= 0)
                                 {
                                     WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                     objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));

                                     obj.Detail.Add(objdtl);

                                 }
                                 WorkOrderEqComponents objdtl2 = os.CreateObject<WorkOrderEqComponents>();

                                 objdtl2.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));
                                 objdtl2.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", dtl2.EquipmentComponent.ID));

                                 obj.Detail2.Add(objdtl2);
                             }
                         }
                         */
                    }


                }

                else
                {
                    throw new UserFriendlyException("WARNING  -  You must select at least ONE Equipment or Component to Create New PM WO.");
                }

                genCon.openNewView(os, obj, ViewEditMode.Edit);
                genCon.showMsg("Successful", "Work Order Created.", InformationType.Success);
            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }

            GC.Collect();

        }

        private void GoToPR_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)View).ViewEditMode != ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Editing Work Order cannot proceed.", InformationType.Error);
                return;
            }
            WorkOrders selectedObject = (WorkOrders)e.CurrentObject;

            ListPropertyEditor listviewDetail = null;
            if (selectedObject.DetailRequest.Count > 0)
            {
                foreach (ViewItem item in ((DetailView)View).Items)
                {
                    if ((item is ListPropertyEditor) && (item.Id == "DetailRequest"))
                        listviewDetail = item as ListPropertyEditor;
                }
                if (listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count == 1)
                {
                    int id = 0;
                    foreach (PurchaseRequests selectedObjectdtl in listviewDetail.ListView.SelectedObjects)
                    {
                        id = selectedObjectdtl.ID;
                    }
                    IObjectSpace os = Application.CreateObjectSpace();
                    PurchaseRequests obj = os.FindObject<PurchaseRequests>(CriteriaOperator.Parse("ID=?", id));

                    genCon.openNewView(os, obj, ViewEditMode.View);
                    genCon.showMsg("Successful", "Back to Purchase Order.", InformationType.Success);

                }
                else
                {
                    genCon.showMsg("Failed", "Please select ONE Purchase Request.", InformationType.Error);
                    return;

                }
            }

        }

        private void BackToPMPatch_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            WorkOrders selectedObject = (WorkOrders)View.CurrentObject;
            if (selectedObject.PMPatch != null)
            {
                IObjectSpace os = Application.CreateObjectSpace();
                PMPatches obj = os.FindObject<PMPatches>(CriteriaOperator.Parse("ID=?", selectedObject.PMPatch.ID));

                genCon.openNewView(os, obj, ViewEditMode.View);
                genCon.showMsg("Successful", "Back to PM Patch.", InformationType.Success);
            }
            else
                genCon.showMsg("Failed", "There is no PM Patch.", InformationType.Error);

        }
    }
}
