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
    public partial class DeviationMitigationController : ViewController
    {
        GenControllers genCon;
        public DeviationMitigationController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(DeviationMitigations);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            ActiveItem();
        }
        private void ActiveItem()
        {

            this.DeviationMitigationClose.Active.SetItemValue("Enabled", false);
            this.DeviationMitigationCancel.Active.SetItemValue("Enabled", false);
            this.DeviationMitigationReopen.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.DeviationApprover).Count() > 0 ? true : false;
            bool IsUser = user.Roles.Where(p => p.Name == GeneralSettings.DeviationUser).Count() > 0 ? true : false;
            bool IsManager = user.Roles.Where(p => p.Name == GeneralSettings.DeviationManager).Count() > 0 ? true : false;
            bool IsReopen = user.Roles.Where(p => p.Name == GeneralSettings.DeviationReopen).Count() > 0 ? true : false;

            if (View is DetailView)
            {
                ((DetailView)View).ViewEditModeChanged += DeviationMitigationController_ViewEditModeChanged;
                EnableItem();
            }
            else if (this.View is ListView && View.IsRoot == false)
            {
                this.DeviationMitigationClose.Active.SetItemValue("Enabled", IsUser || IsManager);
                this.DeviationMitigationCancel.Active.SetItemValue("Enabled", IsUser || IsManager);
                this.DeviationMitigationReopen.Active.SetItemValue("Enabled", IsReopen);
            }

        }
        private void EnableItem()
        {
            //this.DeviationMitigationClose.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            //this.DeviationMitigationCancel.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
        }
        private void DeviationMitigationController_ViewEditModeChanged(object sender, EventArgs e)
        {
            EnableItem();
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
                ((DetailView)View).ViewEditModeChanged -= DeviationMitigationController_ViewEditModeChanged;
            }
        }

        private void DeviationMitigationClose_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)((NestedFrame)Frame).ViewItem.View).ViewEditMode != ViewEditMode.Edit)
            {
                genCon.showMsg("Failed", "Please edit the deviation to proceed.", InformationType.Error);
                return;
            }

            foreach (DeviationMitigations selectedObjectdtl in View.SelectedObjects)
            {
                if (selectedObjectdtl.CloseUser == null)
                {
                    selectedObjectdtl.CloseDate = DateTime.Today;
                    selectedObjectdtl.CloseUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    selectedObjectdtl.CancelDate = null;
                    selectedObjectdtl.CancelUser = null;
                }
            }

            genCon.showMsg("Successful", "Close done, please save the deviation", InformationType.Success);
            //ObjectSpace.CommitChanges();
            //RefreshController refcon = Frame.GetController<RefreshController>();
            //if (refcon != null)
            //    refcon.RefreshAction.DoExecute();
        }

        private void DeviationMitigationCancel_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)((NestedFrame)Frame).ViewItem.View).ViewEditMode != ViewEditMode.Edit)
            {
                genCon.showMsg("Failed", "Please edit the deviation to proceed.", InformationType.Error);
                return;
            }

            foreach (DeviationMitigations selectedObjectdtl in View.SelectedObjects)
            {
                if (selectedObjectdtl.CancelUser == null)
                {
                    selectedObjectdtl.CloseDate = null;
                    selectedObjectdtl.CloseUser = null;
                    selectedObjectdtl.CancelDate = DateTime.Today;
                    selectedObjectdtl.CancelUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                }
            }

            genCon.showMsg("Successful", "Cancel done, please save the deviation", InformationType.Success);
            //ObjectSpace.CommitChanges();
            //RefreshController refcon = Frame.GetController<RefreshController>();
            //if (refcon != null)
            //    refcon.RefreshAction.DoExecute();

        }

        private void DeviationMitigationReopen_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)((NestedFrame)Frame).ViewItem.View).ViewEditMode != ViewEditMode.Edit)
            {
                genCon.showMsg("Failed", "Please edit the deviation to proceed.", InformationType.Error);
                return;
            }

            foreach (DeviationMitigations selectedObjectdtl in View.SelectedObjects)
            {
                if (selectedObjectdtl.CloseUser != null || selectedObjectdtl.CancelUser != null)
                {
                    selectedObjectdtl.CloseDate = null;
                    selectedObjectdtl.CloseUser = null;
                    selectedObjectdtl.CancelDate = null;
                    selectedObjectdtl.CancelUser = null;
                }
            }

            genCon.showMsg("Successful", "Reopen done, please save the deviation", InformationType.Success);
            //ObjectSpace.CommitChanges();
            //RefreshController refcon = Frame.GetController<RefreshController>();
            //if (refcon != null)
            //    refcon.RefreshAction.DoExecute();

        }
    }
}

