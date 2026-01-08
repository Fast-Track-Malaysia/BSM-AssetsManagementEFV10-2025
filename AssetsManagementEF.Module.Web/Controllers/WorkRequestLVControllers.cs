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
    public partial class WorkRequestLVControllers : ViewController
    {
        GenControllers genCon;
        public WorkRequestLVControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(WorkRequests);
            TargetViewType = ViewType.ListView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.CancelWR.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            WorkRequests selectedObject = (WorkRequests)View.CurrentObject;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
            bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
            bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;
            bool IsCancelRequest = user.Roles.Where(p => p.Name == GeneralSettings.CancelWRRole).Count() > 0 ? true : false;
            bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

            if (isContractor) return;

            if (this.View is ListView)
            {
                this.CancelWR.Active.SetItemValue("Enabled", IsCancelRequest);
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

        private void CancelWR_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new StringParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = err;
            ((StringParameters)dv.CurrentObject).ActionMessage = "Press OK to CONFIRM the action and SAVE, else press Cancel.";

            e.View = dv;

        }

        private void CancelWR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            if (this.View is ListView)
            {
                if (e.SelectedObjects.Count > 0)
                {
                    IObjectSpace os = Application.CreateObjectSpace();
                    //PMSchedules obj = os.CreateObject<PMSchedules>();

                    foreach (WorkRequests currobj in e.SelectedObjects)
                    {
                        WorkRequests selectedObject = os.GetObjectByKey<WorkRequests>(currobj.ID);
                        #region check err
                        bool err = false;
                        if (selectedObject.Cancelled && !err)
                        {
                            err = true;
                        }
                        if (selectedObject.DocPassed && !err)
                        {
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
                                err = true;
                            }
                        }
                        #endregion
                        if (err) continue;
                        if (selectedObject.Cancelled || selectedObject.Approved || selectedObject.DocPassed)
                        { }
                        else
                        {
                            selectedObject.Cancelled = true;
                            selectedObject.DocPassed = false;
                            selectedObject.Rejected = false;

                            WorkRequestDocStatuses ds = os.CreateObject<WorkRequestDocStatuses>();
                            ds.DocStatus = DocumentStatus.Cancelled;
                            ds.DocRemarks = p.ParamString;
                            selectedObject.DetailDocStatus.Add(ds);
                            selectedObject.OnPropertyChanged("DetailDocStatus");
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
