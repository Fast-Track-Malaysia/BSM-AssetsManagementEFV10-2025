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
    public partial class DetailViewNestedController : ViewController
    {
        GenControllers genCon;
        public DetailViewNestedController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.DetailView;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.DeviationCloseold.Active.SetItemValue("Enabled", false);
            this.DeviationCancelold.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
            bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
            bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;
            bool IsCancelRequest = user.Roles.Where(p => p.Name == GeneralSettings.CancelWRRole).Count() > 0 ? true : false;

            if (this.View is DetailView)
            {
                if (View.ObjectTypeInfo.Type == typeof(DeviationWorkOrders))
                {
                    DeviationWorkOrders selectedobject = (DeviationWorkOrders)View.CurrentObject;
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusOpen)
                    {
                        this.DeviationCloseold.Active.SetItemValue("Enabled", true);
                        this.DeviationCancelold.Active.SetItemValue("Enabled", true);
                    }
                    else if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusClose)
                    {
                        this.DeviationCancelold.Active.SetItemValue("Enabled", true);
                    }
                }
                if (View.ObjectTypeInfo.Type == typeof(DeviationWorkRequests))
                {
                    DeviationWorkRequests selectedobject = (DeviationWorkRequests)View.CurrentObject;
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusOpen)
                    {
                        this.DeviationCloseold.Active.SetItemValue("Enabled", true);
                        this.DeviationCancelold.Active.SetItemValue("Enabled", true);
                    }
                    else if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusClose)
                    {
                        this.DeviationCancelold.Active.SetItemValue("Enabled", true);
                    }
                }
                this.DeviationCloseold.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.DeviationCancelold.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                ((DetailView)View).ViewEditModeChanged += DetailViewNestedController_ViewEditModeChanged;
            }
        }

        private void DetailViewNestedController_ViewEditModeChanged(object sender, EventArgs e)
        {
            this.DeviationCloseold.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.DeviationCancelold.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
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
                ((DetailView)View).ViewEditModeChanged -= DetailViewNestedController_ViewEditModeChanged;
            }
        }

        private void DeviationClose_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View.ObjectTypeInfo.Type == typeof(DeviationWorkOrders))
            {
                DeviationWorkOrders selectedobject = (DeviationWorkOrders)View.CurrentObject;
                if (selectedobject.DeviationStatus.BoCode != GeneralSettings.DeviationStatusOpen) return;
                selectedobject.DeviationStatus = ObjectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusClose));
                selectedobject.CloseUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                selectedobject.CloseDate = DateTime.Now;
            }
            if (View.ObjectTypeInfo.Type == typeof(DeviationWorkRequests))
            {
                DeviationWorkRequests selectedobject = (DeviationWorkRequests)View.CurrentObject;
                if (selectedobject.DeviationStatus.BoCode != GeneralSettings.DeviationStatusOpen) return;
                selectedobject.DeviationStatus = ObjectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusClose));
                selectedobject.CloseUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                selectedobject.CloseDate = DateTime.Now;
            }
            genCon.showMsg("Done", "Close done. Press OK to confirm", InformationType.Warning);
        }

        private void DeviationCancel_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View.ObjectTypeInfo.Type == typeof(DeviationWorkOrders))
            {
                DeviationWorkOrders selectedobject = (DeviationWorkOrders)View.CurrentObject;
                if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusCancel) return;
                selectedobject.DeviationStatus = ObjectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusCancel));
                selectedobject.CancelUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                selectedobject.CancelDate = DateTime.Now;
            }
            if (View.ObjectTypeInfo.Type == typeof(DeviationWorkRequests))
            {
                DeviationWorkRequests selectedobject = (DeviationWorkRequests)View.CurrentObject;
                if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusCancel) return;
                selectedobject.DeviationStatus = ObjectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusCancel));
                selectedobject.CancelUser = ObjectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                selectedobject.CancelDate = DateTime.Now;
            }
            genCon.showMsg("Done", "Cancel done. Press OK to confirm", InformationType.Warning);
        }
    }
}

