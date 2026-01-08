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
    public partial class WorkRequestController : ViewController
    {
        GenControllers genCon;
        public WorkRequestController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(WorkRequests);
            TargetViewType = ViewType.DetailView;
            TargetViewNesting = Nesting.Root;
            //PopupWindowShowAction action = new PopupWindowShowAction(this, "CancelRequest", DevExpress.Persistent.Base.PredefinedCategory.Edit);
            //action.CustomizePopupWindowParams += new CustomizePopupWindowParamsEventHandler(action_CustomizePopupWindowParams);
            //action.Execute += new PopupWindowShowActionExecuteEventHandler(action_Execute);
        }
        //void action_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        //{
        //    IObjectSpace os = Application.CreateObjectSpace();
        //    DetailView dv = Application.CreateDetailView(os, new StringParameters());
        //    dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
        //    e.View = dv;
        //}
        //void action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        //{
        //    StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;

        //}

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.CreateWorkOrder.Active.SetItemValue("Enabled", false);
            this.CancelRequest.Active.SetItemValue("Enabled", false);
            this.PassRequest.Active.SetItemValue("Enabled", false);
            this.RejectRequest.Active.SetItemValue("Enabled", false);

            WorkRequests selectedObject = (WorkRequests)View.CurrentObject;
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
            bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
            bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;
            bool IsSupervisor = user.Roles.Where(p => p.Name == GeneralSettings.WRSupervisorRole).Count() > 0 ? true : false;
            bool IsCancelRequest = user.Roles.Where(p => p.Name == GeneralSettings.CancelWRRole).Count() > 0 ? true : false;
            bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

            if (isContractor) return;

            if (!selectedObject.IsNew)
            {
                if (user.ID == selectedObject.CreateUser.ID || IsSupervisor)
                {
                    if (selectedObject.DocPassed || selectedObject.Cancelled || selectedObject.Approved)
                    { }
                    else
                    {
                        this.PassRequest.Active.SetItemValue("Enabled", true);
                    }
                }
                if (user.ID == selectedObject.CreateUser.ID || IsSupervisor || IsCancelRequest)
                {
                    if (selectedObject.DocPassed || selectedObject.Cancelled || selectedObject.Approved)
                    { }
                    else
                    {
                        this.CancelRequest.Active.SetItemValue("Enabled", true);
                    }
                }
                if (IsPlanner && selectedObject.DocPassed)
                {
                    this.RejectRequest.Active.SetItemValue("Enabled", true);
                    this.CreateWorkOrder.Active.SetItemValue("Enabled", true);
                }
            }

            this.CreateWorkOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.RejectRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.CancelRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.PassRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);

            if (View.GetType() == typeof(DetailView))
                ((DetailView)View).ViewEditModeChanged += WorkRequestController_ViewEditModeChanged;

        }

        private void WorkRequestController_ViewEditModeChanged(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                this.CreateWorkOrder.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.RejectRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.CancelRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.PassRequest.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
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
                ((DetailView)View).ViewEditModeChanged -= WorkRequestController_ViewEditModeChanged;
            }
        }

        private void CreateWorkOrder_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    genCon.showMsg("Failed", "Editing Work Request cannot proceed.", InformationType.Error);
                    return;
                }
                WorkRequests selectedObject = (WorkRequests)e.CurrentObject;
                if (selectedObject.IsNew)
                {
                    genCon.showMsg("Failed", "New Work Request cannot proceed.", InformationType.Error);
                    return;
                }
                if (selectedObject.IsPlannerChecking || selectedObject.IsApproverChecking)
                { }
                else
                {
                    genCon.showMsg("Failed", "This is not a Planner nor Approver proceed.", InformationType.Error);
                    return;
                }
                if (selectedObject.Approved)
                {
                    genCon.showMsg("Failed", "Approved Work Request cannot proceed.", InformationType.Error);
                    return;
                }
                if (selectedObject.Cancelled)
                {
                    genCon.showMsg("Failed", "Cancelled Work Request cannot proceed.", InformationType.Error);
                    return;
                }
                if (selectedObject.Rejected)
                {
                    genCon.showMsg("Failed", "Rejected Work Request cannot proceed.", InformationType.Error);
                    return;
                }
                if (!selectedObject.DocPassed)
                {
                    genCon.showMsg("Failed", "Pending Work Request cannot proceed.", InformationType.Error);
                    return;
                }

                if (selectedObject.IsPlannerChecking || selectedObject.IsApproverChecking)
                {
                    SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                    Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                    //if (selectedObject.PlannerGroup != position.PlannerGroup)
                    //{
                    //    genCon.showMsg("Failed", "Only Planner Group [" + selectedObject.PlannerGroup.BoName + "] allowed to create Work Order.", InformationType.Error);
                    //    return;
                    //}
                }
                else
                {
                    genCon.showMsg("Failed", "Only Planner allowed to create Work Order.", InformationType.Error);
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

                obj.Remarks = selectedObject.Remarks;

                if (selectedObject.Priority != null)
                    obj.Priority = os.FindObject<Priorities>(CriteriaOperator.Parse("ID=?", selectedObject.Priority.ID));

                if (selectedObject.PlannerGroup != null)
                    obj.AssignPlannerGroup = os.FindObject<PlannerGroups>(new BinaryOperator("ID", selectedObject.PlannerGroup.ID));

                obj.WorkRequest = os.FindObject<WorkRequests>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                obj.DocType = os.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoBreakdown));
                obj.PMClass = os.FindObject<PMClasses>(new BinaryOperator("BoCode", GeneralSettings.defaultcode));

                obj.IsPreventiveMaintenance = false;

                foreach (WorkRequestEquipments dtl in selectedObject.Detail)
                {
                    WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                    objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl.Equipment.ID));

                    obj.Detail.Add(objdtl);
                }

                foreach (WorkRequestEqComponents dtl in selectedObject.Detail2)
                {
                    WorkOrderEqComponents objdtl = os.CreateObject<WorkOrderEqComponents>();

                    objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl.Equipment.ID));
                    objdtl.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", dtl.EquipmentComponent.ID));

                    obj.Detail2.Add(objdtl);
                }

                genCon.openNewView(os, obj, ViewEditMode.Edit);
                genCon.showMsg("Successful", "Work Order Created.", InformationType.Success);
            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }

        }


        private void StringParametersCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            if (((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Viewing Work Request cannot proceed.", InformationType.Error);
                err = true;
            }
            else
            {
                WorkRequests selectedObject = (WorkRequests)View.CurrentObject;
                if (selectedObject.IsNew && !err)
                {
                    genCon.showMsg("Failed", "New Work Request cannot proceed.", InformationType.Error);
                    err = true;
                }
                else
                {
                    if (selectedObject.Approved && !err)
                    {
                        genCon.showMsg("Failed", "Approved Work Request cannot proceed.", InformationType.Error);
                        err = true;
                    }
                    else
                    {
                        if (e.Action.Id == this.CancelRequest.Id)
                        {
                            if (selectedObject.Cancelled && !err)
                            {
                                genCon.showMsg("Failed", "Cancelled Work Request cannot proceed.", InformationType.Error);
                                err = true;
                            }
                            if (selectedObject.DocPassed && !err)
                            {
                                genCon.showMsg("Failed", "Passed Work Request cannot proceed.", InformationType.Error);
                                err = true;
                            }

                            if (!err)
                            {
                                if (selectedObject.IsSupervisorChecking || selectedObject.IsRequestorChecking)
                                { }
                                else if (selectedObject.IsCancelUserChecking)
                                { }
                                else
                                {
                                    genCon.showMsg("Failed", "Only WR Supervisor belong to the Planner Group / Requestor / Cancel Role allowed.", InformationType.Error);
                                    err = true;
                                }
                            }

                        }
                        else if (e.Action.Id == this.PassRequest.Id)
                        {
                            if (selectedObject.DocPassed && !err)
                            {
                                genCon.showMsg("Failed", "Passed Work Request cannot proceed.", InformationType.Error);
                                err = true;
                            }

                            if (!selectedObject.IsRequestorChecking && !err)
                            {
                                genCon.showMsg("Failed", "This is not a Requestor process.", InformationType.Error);
                                err = true;
                            }

                            if (selectedObject.Cancelled && !err)
                            {
                                genCon.showMsg("Failed", "Cancelled Work Request cannot proceed.", InformationType.Error);
                                err = true;
                            }

                        }
                        else if (e.Action.Id == this.RejectRequest.Id)
                        {
                            if (selectedObject.IsPlannerChecking || selectedObject.IsApproverChecking)
                            {
                                //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                                //Positions position = ObjectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                                //if (selectedObject.PlannerGroup != position.PlannerGroup)
                                //{
                                //    genCon.showMsg("Failed", "Only Planner Group [" + selectedObject.PlannerGroup.BoName + "] allowed to proceed.", InformationType.Error);
                                //    return;
                                //}
                            }
                            else
                            {
                                genCon.showMsg("Failed", "This is not a Planner process.", InformationType.Error);
                                err = true;
                            }
                            if (selectedObject.Rejected && !err)
                            {
                                genCon.showMsg("Failed", "Rejected Work Request cannot proceed.", InformationType.Error);
                                err = true;
                            }
                            if (!selectedObject.DocPassed && !err)
                            {
                                genCon.showMsg("Failed", "Pending Work Request cannot proceed.", InformationType.Error);
                                err = true;
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

        private void CancelRequest_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkRequests selectedObject = (WorkRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            if (selectedObject.Cancelled || selectedObject.Approved || selectedObject.DocPassed)
            {
                //selectedObject.Cancelled = false;

                //WorkRequestDocStatuses ds = ObjectSpace.CreateObject<WorkRequestDocStatuses>();
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

                WorkRequestDocStatuses ds = ObjectSpace.CreateObject<WorkRequestDocStatuses>();
                ds.DocStatus = DocumentStatus.Cancelled;
                ds.DocRemarks = p.ParamString;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");
                selectedObject.IsCancelling = true;
                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Cancel Done.", InformationType.Success);
            }
        }

        private void RejectRequest_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            WorkRequests selectedObject = (WorkRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (!selectedObject.DocPassed)
            {
                return;
            }
            else
            {
                if (selectedObject.Rejected)
                {
                    //selectedObject.Rejected = false;

                    //WorkRequestDocStatuses ds = ObjectSpace.CreateObject<WorkRequestDocStatuses>();
                    //ds.DocStatus = DocumentStatus.Rejected;
                    //ds.DocRemarks = p.ParamString;
                    //ds.IsReverse = true;
                    //selectedObject.DetailDocStatus.Add(ds);
                    //selectedObject.OnPropertyChanged("DetailDocStatus");

                    //genCon.showMsg("Successful", "Undo Reject Done. Please save changes afterwards.", InformationType.Success);
                }
                else
                {
                    selectedObject.Rejected = true;
                    selectedObject.DocPassed = false;
                    selectedObject.Cancelled = false;

                    WorkRequestDocStatuses ds = ObjectSpace.CreateObject<WorkRequestDocStatuses>();
                    ds.DocStatus = DocumentStatus.Rejected;
                    ds.DocRemarks = p.ParamString;
                    selectedObject.DetailDocStatus.Add(ds);
                    selectedObject.OnPropertyChanged("DetailDocStatus");

                    ModificationsController controller = Frame.GetController<ModificationsController>();
                    if (controller != null)
                    {
                        controller.SaveAction.DoExecute();
                    }

                    genCon.showMsg("Successful", "Reject Done.", InformationType.Success);
                }
            }

        }

        private void PassRequest_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

            WorkRequests selectedObject = (WorkRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.Cancelled)
            {
                return;
            }
            else
            {
                if (selectedObject.DocPassed)
                {
                    //selectedObject.DocPassed = false;

                    //WorkRequestDocStatuses ds = ObjectSpace.CreateObject<WorkRequestDocStatuses>();
                    //ds.DocStatus = DocumentStatus.DocPassed;
                    //ds.DocRemarks = p.ParamString;
                    //ds.IsReverse = true;
                    //selectedObject.DetailDocStatus.Add(ds);
                    //selectedObject.OnPropertyChanged("DetailDocStatus");

                    //ModificationsController controller = Frame.GetController<ModificationsController>();
                    //if (controller != null)
                    //{
                    //    controller.SaveAction.DoExecute();
                    //}

                    //genCon.showMsg("Successful", "Undo Passing Done.", InformationType.Success);
                }
                else
                {
                    selectedObject.DocPassed = true;
                    selectedObject.Rejected = false;
                    selectedObject.Cancelled = false;

                    WorkRequestDocStatuses ds = ObjectSpace.CreateObject<WorkRequestDocStatuses>();
                    ds.DocStatus = DocumentStatus.DocPassed;
                    ds.DocRemarks = p.ParamString;
                    selectedObject.DetailDocStatus.Add(ds);
                    selectedObject.OnPropertyChanged("DetailDocStatus");

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
                            IList<Positions> positionlist = ObjectSpace.GetObjects<Positions>(new ContainsOperator("DetailPlannerGroup", new BinaryOperator("ID", selectedObject.PlannerGroup.ID, BinaryOperatorType.Equal)));
                            List<string> ToEmails = new List<string>();

                            foreach (Positions position in positionlist)
                            {
                                if (position.IsCorrectiveMaintenance && position.CurrentUser.UserEmail.Trim() != "")
                                    if (position.CurrentUser.Roles.Where(po => po.Name == GeneralSettings.PlannerRole).Count() > 0)
                                    {
                                        ToEmails.Add(position.CurrentUser.UserEmail.Trim());
                                    }

                            }
                            if (ToEmails.Count > 0)
                                genCon.SendEmail("Work Request Pending For Approval.", "Planner Group = " + selectedObject.PlannerGroup.BoCode + System.Environment.NewLine + "Document No = " + selectedObject.DocNum, ToEmails);
                        }
                        catch (Exception ex)
                        {
                            genCon.showMsg("Error", ex.Message, InformationType.Error);
                        }
                    }

                }
            }
        }


    }
}

