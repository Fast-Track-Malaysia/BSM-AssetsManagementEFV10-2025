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
    public partial class WorkOrderLVControllers : ViewController
    {
        GenControllers genCon;
        public WorkOrderLVControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(WorkOrders);
            TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.AwaitingPlanning.Active.SetItemValue("Enabled", false);
            this.CancelWO.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            WorkOrders selectedObject = (WorkOrders)View.CurrentObject;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
            bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
            bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;
            bool IsAcknowledgePMRole = user.Roles.Where(p => p.Name == GeneralSettings.AcknowledgePMRole).Count() > 0 ? true : false;
            bool IsCancelRequest = user.Roles.Where(p => p.Name == GeneralSettings.CancelWORole).Count() > 0 ? true : false;
            bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

            if (isContractor) return;

            if (this.View is ListView && this.View.Id == "WorkOrders_ListView_PMACK")
            {
                if (IsAcknowledgePMRole)
                {
                    this.AwaitingPlanning.Active.SetItemValue("Enabled", true);
                }
            }
            else if (this.View is ListView)
            {
                this.CancelWO.Active.SetItemValue("Enabled", IsCancelRequest);
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
        }

        private void AwaitingPlanning_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new StringParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = err;
            ((StringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;

        }

        private void AwaitingPlanning_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            string remarks = p.ParamString;

            if (this.View is ListView)
            {
                if (e.SelectedObjects.Count > 0)
                {
                    IObjectSpace os = Application.CreateObjectSpace();
                    //PMSchedules obj = os.CreateObject<PMSchedules>();

                    foreach (WorkOrders selectedObject in e.SelectedObjects)
                    {
                        WorkOrders obj = os.GetObjectByKey<WorkOrders>(selectedObject.ID);
                        if (obj.JobStatus.BoCode == GeneralSettings.InitPMJobStatus)
                        {
                            obj.JobStatus = os.FindObject<JobStatuses>(new BinaryOperator("BoCode", GeneralSettings.InitCMJobStatus));

                            WorkOrderJobStatuses js = os.CreateObject<WorkOrderJobStatuses>();
                            js.JobStatus = os.FindObject<JobStatuses>(new BinaryOperator("BoCode", GeneralSettings.InitCMJobStatus));
                            js.JobRemarks = remarks;
                            obj.DetailJobStatus.Add(js);
                            //obj.OnPropertyChanged("DetailJobStatus");

                        }

                    }

                    os.CommitChanges();
                    genCon.showMsg("Successful", "Acknowledge Done.", InformationType.Success);
                    //View.Refresh();
                    //View.ObjectSpace.Refresh();
                    //RefreshController refreshController = Frame.GetController<RefreshController>();
                    //if (refreshController != null)
                    //{
                    //    refreshController.RefreshAction.DoExecute();
                    //}

                }
            }
        }

        private void CancelWO_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new StringParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = err;
            ((StringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;
        }

        private void CancelWO_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            if (this.View is ListView)
            {
                if (e.SelectedObjects.Count > 0)
                {
                    IObjectSpace os = Application.CreateObjectSpace();
                    //PMSchedules obj = os.CreateObject<PMSchedules>();

                    foreach (WorkOrders currobj in e.SelectedObjects)
                    {
                        WorkOrders selectedObject = os.GetObjectByKey<WorkOrders>(currobj.ID);
                        #region check err
                        bool err = false;
                        if (selectedObject.IsClosed && !err)
                        {
                            err = true;
                        }
                        if (selectedObject.Cancelled && !err)
                        {
                            err = true;
                        }
                        if (selectedObject.Approved && !err)
                        {
                            err = true;
                        }

                        if (selectedObject.DocPassed && !err)
                        {
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
                                err = true;
                            }
                        }
                        #endregion
                        if (err) continue;
                        if (selectedObject.DocPassed || selectedObject.Approved || selectedObject.Cancelled || selectedObject.IsClosed)
                        { }
                        else
                        {
                            selectedObject.Cancelled = true;
                            selectedObject.DocPassed = false;
                            selectedObject.Rejected = false;
                            selectedObject.Approved = false;

                            WorkOrderDocStatuses ds = os.CreateObject<WorkOrderDocStatuses>();
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

                                    PurchaseRequestDocStatuses objds = os.CreateObject<PurchaseRequestDocStatuses>();
                                    objds.DocStatus = DocumentStatus.Cancelled;
                                    objds.DocRemarks = p.ParamString;
                                    obj.DetailDocStatus.Add(objds);
                                }
                            }
                            //selectedObject.IsCancelling = true;
                        }
                    }

                    os.CommitChanges();
                    genCon.showMsg("Successful", "Cancel Done. Press Refresh button for refresh list view.", InformationType.Success);
                    //View.Refresh();
                    //View.ObjectSpace.Refresh();
                    //RefreshController refreshController = Frame.GetController<RefreshController>();
                    //if (refreshController != null)
                    //{
                    //    refreshController.RefreshAction.DoExecute();
                    //}
                }
            }


        }
    }
}
