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
using DevExpress.ExpressApp.EF;
using System.Data.SqlClient;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DeviationController : ViewController
    {
        GenControllers genCon;
        public DeviationController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(Deviation2025);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            ActiveItem();
        }
        private void ActiveItem()
        {
            this.DeviationUnderReview.Active.SetItemValue("Enabled", false);
            this.DeviationSubmitAck.Active.SetItemValue("Enabled", false);
            this.DeviationApproval.Active.SetItemValue("Enabled", false);
            this.DeviationApprovedExtend.Active.SetItemValue("Enabled", false);
            this.DeviationWithdraw.Active.SetItemValue("Enabled", false);
            this.DeviationClose.Active.SetItemValue("Enabled", false);
            this.DeviationCancel.Active.SetItemValue("Enabled", false);
            this.DeviationNew.Active.SetItemValue("Enabled", false);
            this.DeviationEdit.Active.SetItemValue("Enabled", false);
            this.DeviationDuplicate.Active.SetItemValue("Enabled", false);
            this.DeviationToWO.Active.SetItemValue("Enabled", false);
            this.DeviationDraftExtend.Active.SetItemValue("Enabled", false);
            this.DeviationSubmitApp.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.DeviationApprover).Count() > 0 ? true : false;
            bool IsUser = user.Roles.Where(p => p.Name == GeneralSettings.DeviationUser).Count() > 0 ? true : false;
            bool IsManager = user.Roles.Where(p => p.Name == GeneralSettings.DeviationManager).Count() > 0 ? true : false;
            bool IsReviewer = user.Roles.Where(p => p.Name == GeneralSettings.DeviationReviewer).Count() > 0 ? true : false;

            if (this.View is DetailView)
            {
                if (View.ObjectTypeInfo.Type == typeof(Deviation2025))
                {
                    Deviation2025 selectedobject = (Deviation2025)View.CurrentObject;
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusDraft || selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusUnderReview)
                    {
                        this.DeviationDraftExtend.Active.SetItemValue("Enabled", IsUser || IsManager);
                    }
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusUnderReview)
                    {
                        this.DeviationSubmitAck.Active.SetItemValue("Enabled", IsReviewer);
                        this.DeviationWithdraw.Active.SetItemValue("Enabled", IsReviewer);
                        this.DeviationSubmitApp.Active.SetItemValue("Enabled", IsReviewer || IsUser || IsManager);
                    }
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusPendingApproval)
                    {
                        this.DeviationApproval.Active.SetItemValue("Enabled", IsApprover);
                        this.DeviationApprovedExtend.Active.SetItemValue("Enabled", IsApprover);
                    }
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusApproved)
                    {
                        this.DeviationClose.Active.SetItemValue("Enabled", IsUser || IsManager);
                        this.DeviationDuplicate.Active.SetItemValue("Enabled", IsUser || IsManager);
                    }
                    if (selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusDraft
                        || selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusWithdrawn
                        || selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusDraftExtension
                        || selectedobject.DeviationStatus.BoCode == GeneralSettings.DeviationStatusApprovedExtension)
                    {
                        this.DeviationUnderReview.Active.SetItemValue("Enabled", IsUser || IsManager);
                        this.DeviationCancel.Active.SetItemValue("Enabled", IsUser || IsManager);
                    }
                    this.DeviationToWO.Active.SetItemValue("Enabled", IsUser || IsManager);
                }
                ((DetailView)View).ViewEditModeChanged += DeviationController_ViewEditModeChanged;
                EnableItem();
            }
            else if (this.View is ListView)
            {
                if (!View.IsRoot)
                {
                    this.DeviationNew.Active.SetItemValue("Enabled", IsUser || IsManager);
                    this.DeviationEdit.Active.SetItemValue("Enabled", IsUser || IsManager);
                }
            }

        }
        private void EnableItem()
        {
            this.DeviationUnderReview.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationSubmitAck.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationApproval.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationApprovedExtend.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationWithdraw.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationClose.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationCancel.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationNew.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationEdit.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationDuplicate.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationToWO.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationDraftExtend.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.DeviationSubmitApp.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);

        }
        private void DeviationController_ViewEditModeChanged(object sender, EventArgs e)
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
                ((DetailView)View).ViewEditModeChanged -= DeviationController_ViewEditModeChanged;
            }
        }



        private void StringParm_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            string msg = "Press OK to CONFIRM the action and SAVE, else press Cancel.";
            string newstatus = "";
            if (View.CurrentObject == null)
            {
                err = true;
                msg = "No deviation found.";
            }
            if (!err)
            {
                Deviation2025 selectedObject = (Deviation2025)View.CurrentObject;
                if (e.Action.Id == "DeviationClose")
                {
                    newstatus = GeneralSettings.DeviationStatusClose;
                }
                //else if (e.Action.Id == "DeviationSubmitAck")
                //{
                //    newstatus = GeneralSettings.DeviationStatusPendingApproval;
                //}
                else if (e.Action.Id == "DeviationApprovedExtend")
                {
                    newstatus = GeneralSettings.DeviationStatusApprovedExtension;
                }
                else if (e.Action.Id == "DeviationDraftExtend")
                {
                    newstatus = GeneralSettings.DeviationStatusDraftExtension;
                }
                else if (e.Action.Id == "DeviationWithdraw")
                {
                    newstatus = GeneralSettings.DeviationStatusWithdrawn;
                }
                else if (e.Action.Id == "DeviationUnderReview")
                {
                    newstatus = GeneralSettings.DeviationStatusUnderReview;
                }
                else if (e.Action.Id == "DeviationCancel")
                {
                    newstatus = GeneralSettings.DeviationStatusCancel;
                }
                else if (e.Action.Id == "DeviationSubmitApp")
                {
                    newstatus = GeneralSettings.DeviationStatusPendingApproval;
                }

                #region check sp
                sp_checkstatus(selectedObject, newstatus, ref err, ref msg);
                #endregion
            }

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new StringParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((StringParameters)dv.CurrentObject).IsErr = err;
            ((StringParameters)dv.CurrentObject).ActionMessage = msg;

            e.View = dv;

        }
        private void DeviationNew_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)((NestedFrame)Frame).ViewItem.View).ViewEditMode != ViewEditMode.View)
            {
                genCon.showMsg("Failed", "New Deviation cannot proceed while editing.", InformationType.Error);
                return;
            }

            WorkOrders selectedObject = ((NestedFrame)Frame).ViewItem.View.CurrentObject as WorkOrders;
            if (selectedObject.ID <= 0)
            {
                genCon.showMsg("Failed", "Work Order not yet saved.", InformationType.Error);
                return;
            }
            IObjectSpace os = Application.CreateObjectSpace();
            Deviation2025 obj = os.CreateObject<Deviation2025>();
            if (selectedObject.DetailDeviation.Count > 0)
            {
                int rank = 0;
                string docnum = "";
                foreach (Deviation2025 temp in selectedObject.DetailDeviation)
                {
                    if (temp.DeviationStatus.BoCode == GeneralSettings.DeviationStatusCancel) continue;
                    if ((int)temp.DeviationRank > rank)
                    {
                        rank = (int)temp.DeviationRank;
                        docnum = temp.DocNum;
                    }
                }
                if (rank > 0)
                {
                    obj.LastDeviationRank = (DeviationRankEnum)rank;
                    obj.LastDeviationNo = docnum;
                }
                rank++;
                obj.DeviationRank = (DeviationRankEnum)rank;
            }
            obj.WorkOrder = os.GetObjectByKey<WorkOrders>(selectedObject?.ID);
            obj.OriginalOLAFD = selectedObject.PlanEndDate;
            if (selectedObject.Detail.Count > 0)
                obj.Equipment = os.GetObjectByKey<Equipments>(selectedObject.Detail.FirstOrDefault().Equipment.ID);

            if (selectedObject.Detail2.Count > 0)
                obj.Component = os.GetObjectByKey<EquipmentComponents>(selectedObject.Detail2.FirstOrDefault().EquipmentComponent.ID);

            if (obj.Equipment != null)
            {
                if (obj.Equipment?.Criticality != null)
                    obj.Criticality = os.GetObjectByKey<Criticalities>(obj.Equipment?.Criticality.ID);
                if (obj.Equipment?.SCECategory != null)
                    obj.SCECategory = os.GetObjectByKey<SCECategories>(obj.Equipment?.SCECategory.ID);
                if (obj.Equipment?.SCESubCategory != null)
                    obj.SCESubCategory = os.GetObjectByKey<SCESubCategories>(obj.Equipment?.SCESubCategory.ID);
            }
            else if (obj.Component != null)
            {
                if (obj.Component?.Criticality != null)
                    obj.Criticality = os.GetObjectByKey<Criticalities>(obj.Component?.Criticality.ID);
                if (obj.Component?.SCECategory != null)
                    obj.SCECategory = os.GetObjectByKey<SCECategories>(obj.Component?.SCECategory.ID);
                if (obj.Component?.SCESubCategory != null)
                    obj.SCESubCategory = os.GetObjectByKey<SCESubCategories>(obj.Component?.SCESubCategory.ID);
            }

            genCon.showMsg("Successful", "New Deviation.", InformationType.Success);
            genCon.openNewView(os, obj, ViewEditMode.Edit);

            ActiveItem();

        }

        private void DeviationEdit_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)((NestedFrame)Frame).ViewItem.View).ViewEditMode != ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Edit Deviation cannot proceed while editing.", InformationType.Error);
                return;
            }
            WorkOrders selectedObject = ((NestedFrame)Frame).ViewItem.View.CurrentObject as WorkOrders;
            if (selectedObject.ID <= 0)
            {
                genCon.showMsg("Failed", "Work Order not yet saved.", InformationType.Error);
                return;
            }

            IObjectSpace os = Application.CreateObjectSpace();
            Deviation2025 obj = os.FindObject<Deviation2025>(CriteriaOperator.Parse("ID=?", ((Deviation2025)View.CurrentObject).ID));

            genCon.showMsg("Successful", "Edit Deviation.", InformationType.Success);
            genCon.openNewView(os, obj, ViewEditMode.Edit);

            ActiveItem();

            //ListPropertyEditor listviewDetail = null;
            //if (selectedObject.DetailDeviation.Count > 0)
            //{
            //    foreach (ViewItem item in ((DetailView)View).Items)
            //    {
            //        if ((item is ListPropertyEditor) && (item.Id == "DetailDeviation"))
            //            listviewDetail = item as ListPropertyEditor;
            //    }
            //    if (listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count == 1)
            //    {
            //        int id = 0;
            //        foreach (Deviation2025 selectedObjectdtl in listviewDetail.ListView.SelectedObjects)
            //        {
            //            id = selectedObjectdtl.ID;
            //        }
            //        IObjectSpace os = Application.CreateObjectSpace();
            //        Deviation2025 obj = os.FindObject<Deviation2025>(CriteriaOperator.Parse("ID=?", id));

            //        genCon.showMsg("Successful", "Edit Deviation.", InformationType.Success);
            //        genCon.openNewView(os, obj, ViewEditMode.Edit);

            //    }
            //    else
            //    {
            //        genCon.showMsg("Failed", "Please select ONE Deviation.", InformationType.Error);
            //        return;

            //    }
            //}


        }

        private void DeviationApproval_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DeviationAppParameters p = (DeviationAppParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            IObjectSpace ios = Application.CreateObjectSpace();
            Deviation2025 selectedObject = ios.GetObjectByKey< Deviation2025 >(((Deviation2025)e.CurrentObject).ID);

            DeviationDocStatus ds = ios.CreateObject<DeviationDocStatus>();
            ds.DocStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusApproved));
            ds.DocRemarks = p.ParamString;
            ds.CreateDate = DateTime.Now;
            ds.CreateUser = ios.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId); ;
            selectedObject.Detail4.Add(ds);
            selectedObject.OnPropertyChanged("Detail4");

            selectedObject.ApprovedValidDate = p.ParamDate;
            selectedObject.DeviationStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", ds.DocStatus.BoCode));

            ios.CommitChanges();
            RefreshController refcon = Frame.GetController<RefreshController>();
            if (refcon != null)
                refcon.RefreshAction.DoExecute();


            genCon.showMsg("Successful", "Approval Done.", InformationType.Success);
            ActiveItem();
        }

        private void DeviationApprovedExtend_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Deviation2025 selectedObject = (Deviation2025)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            add_deviation_docstatus(selectedObject, p, GeneralSettings.DeviationStatusApprovedExtension);

        }

        private void DeviationClose_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Deviation2025 selectedObject = (Deviation2025)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            add_deviation_docstatus(selectedObject, p, GeneralSettings.DeviationStatusClose);

        }

        private void DeviationCancel_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Deviation2025 selectedObject = (Deviation2025)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            add_deviation_docstatus(selectedObject, p, GeneralSettings.DeviationStatusCancel);

        }

        private void DeviationSubmitAck_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DeviationReviewerAck p = (DeviationReviewerAck)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;
            if (string.IsNullOrEmpty(p.ParamString))
            {
                genCon.showMsg("Failed", "Comment cannot empty.", InformationType.Error);
                return;
            }

            IObjectSpace ios = Application.CreateObjectSpace();
            Deviation2025 selectedObject = ios.GetObjectByKey<Deviation2025>(((Deviation2025)e.CurrentObject).ID);
            string newstatus = "";

            if (p.Reject)
            {
                foreach (DeviationReviewers reviewer in selectedObject.Detail2)
                {
                    if (reviewer.ID == p.ReviewID)
                    {
                        reviewer.ReviewDate = p.ParamDate;
                        reviewer.Comments = p.ParamString;
                        reviewer.ActReviewer = false;
                    }
                }

                newstatus = GeneralSettings.DeviationStatusWithdrawn;

                DeviationDocStatus ds = ios.CreateObject<DeviationDocStatus>();
                ds.DocStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", newstatus));
                ds.DocRemarks = $"[User Withdrawn] {p.ParamString}";
                ds.CreateDate = DateTime.Now;
                ds.CreateUser = ios.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                selectedObject.Detail4.Add(ds);
                selectedObject.OnPropertyChanged("Detail4");

                selectedObject.DeviationStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", ds.DocStatus.BoCode));
            }
            else
            {
                int reviewercount = 0;
                int reviewerackcount = 0;
                foreach (DeviationReviewers reviewer in selectedObject.Detail2)
                {
                    if (reviewer.ID == p.ReviewID)
                    {
                        reviewer.ReviewDate = p.ParamDate;
                        reviewer.Comments = p.ParamString;
                        reviewer.ActReviewer = true;
                    }
                    if (reviewer.ActReviewer) reviewerackcount++;
                    reviewercount++;
                }

                newstatus = GeneralSettings.DeviationStatusUnderReview;

                DeviationDocStatus ds = ios.CreateObject<DeviationDocStatus>();
                ds.DocStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", newstatus));
                ds.DocRemarks = $"[User Acknowledged] {p.ParamString}";
                ds.CreateDate = DateTime.Now;
                ds.CreateUser = ios.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                selectedObject.Detail4.Add(ds);
                selectedObject.OnPropertyChanged("Detail4");

                //selectedObject.DeviationStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", ds.DocStatus.BoCode));

                if (reviewerackcount == reviewercount)
                {
                    newstatus = GeneralSettings.DeviationStatusPendingApproval;

                    ds = ios.CreateObject<DeviationDocStatus>();
                    ds.DocStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", newstatus));
                    ds.DocRemarks = "Review completed.";
                    ds.CreateDate = DateTime.Now;
                    ds.CreateUser = ios.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    selectedObject.Detail4.Add(ds);
                    selectedObject.OnPropertyChanged("Detail4");

                    selectedObject.DeviationStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", ds.DocStatus.BoCode));
                    //ModificationsController controller = Frame.GetController<ModificationsController>();
                }
            }

            ios.CommitChanges();
            RefreshController refcon = Frame.GetController<RefreshController>();
            if (refcon != null)
                refcon.RefreshAction.DoExecute();

            genCon.showMsg("Successful", $"Review Acknowledge done", InformationType.Success);
            ActiveItem();
            
        }

        private void DeviationUnderReview_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Deviation2025 selectedObject = (Deviation2025)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            add_deviation_docstatus(selectedObject, p, GeneralSettings.DeviationStatusUnderReview);


        }

        private void DeviationApproval_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            string msg = "Press OK to CONFIRM the action and SAVE, else press Cancel.";
            string newstatus = "";
            if (View.CurrentObject == null)
            {
                err = true;
                msg = "No deviation found.";
            }

            if (!err)
            {
                Deviation2025 selectedObject = (Deviation2025)View.CurrentObject;

                if (!err)
                {
                    if (e.Action.Id == "DeviationApproval")
                    {
                        newstatus = GeneralSettings.DeviationStatusApproved;
                    }

                    #region check sp
                    sp_checkstatus(selectedObject, newstatus, ref err, ref msg);
                    #endregion
                }
            }

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new DeviationAppParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((DeviationAppParameters)dv.CurrentObject).ParamDate = DateTime.Today;
            ((DeviationAppParameters)dv.CurrentObject).IsErr = err;
            ((DeviationAppParameters)dv.CurrentObject).ActionMessage = msg;

            e.View = dv;

        }

        private void DeviationDuplicate_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)View).ViewEditMode != ViewEditMode.View)
            {
                genCon.showMsg("Failed", "New Deviation cannot proceed while editing.", InformationType.Error);
                return;
            }

            Deviation2025 selectedObject = View.CurrentObject as Deviation2025;
            if (selectedObject.ID <= 0)
            {
                genCon.showMsg("Failed", "Work Order not yet saved.", InformationType.Error);
                return;
            }
            IObjectSpace os = Application.CreateObjectSpace();
            Deviation2025 obj = os.CreateObject<Deviation2025>();

            obj.LastDeviationNo = selectedObject.DocNum;
            int rank = (int)selectedObject.DeviationRank;
            obj.LastDeviationRank = (DeviationRankEnum)rank;

            rank++;
            obj.DeviationRank = (DeviationRankEnum)rank;

            obj.WorkOrder = os.GetObjectByKey<WorkOrders>(selectedObject.WorkOrder.ID);

            if (selectedObject.Equipment != null)
                obj.Equipment = os.GetObjectByKey<Equipments>(selectedObject.Equipment.ID);

            if (selectedObject.Component != null)
                obj.Component = os.GetObjectByKey<EquipmentComponents>(selectedObject.Component.ID);

            if (obj.Equipment != null)
            {
                if (obj.Equipment?.Criticality != null)
                    obj.Criticality = os.GetObjectByKey<Criticalities>(obj.Equipment?.Criticality.ID);
                if (obj.Equipment?.SCECategory != null)
                    obj.SCECategory = os.GetObjectByKey<SCECategories>(obj.Equipment?.SCECategory.ID);
                if (obj.Equipment?.SCESubCategory != null)
                    obj.SCESubCategory = os.GetObjectByKey<SCESubCategories>(obj.Equipment?.SCESubCategory.ID);
            }
            else if (obj.Component != null)
            {
                if (obj.Component?.Criticality != null)
                    obj.Criticality = os.GetObjectByKey<Criticalities>(obj.Component?.Criticality.ID);
                if (obj.Component?.SCECategory != null)
                    obj.SCECategory = os.GetObjectByKey<SCECategories>(obj.Component?.SCECategory.ID);
                if (obj.Component?.SCESubCategory != null)
                    obj.SCESubCategory = os.GetObjectByKey<SCESubCategories>(obj.Component?.SCESubCategory.ID);
            }

            obj.ApprovedValidDate = selectedObject.ApprovedValidDate;
            obj.DeviationTitle = selectedObject.DeviationTitle;
            if (selectedObject.DeviationType != null)
                obj.DeviationType = os.GetObjectByKey<DeviationTypes>(selectedObject.DeviationType.ID);
            obj.OriginalOLAFD = selectedObject.OriginalOLAFD;
            obj.ProposedLAFDValidDate = selectedObject.ProposedLAFDValidDate;
            if (selectedObject.DeviationDiscipline != null)
                obj.DeviationDiscipline = os.GetObjectByKey<DeviationDiscipline>(selectedObject.DeviationDiscipline.ID);
            if (selectedObject.Location != null)
                obj.Location = os.GetObjectByKey<DeviationLocations>(selectedObject.Location.ID);
            obj.Dscription = selectedObject.Dscription;
            if (selectedObject.OverallRisk != null)
                obj.OverallRisk = os.GetObjectByKey<Risks>(selectedObject.OverallRisk.ID);
            if (selectedObject.People1 != null)
                obj.People1 = os.GetObjectByKey<RiskPeoples>(selectedObject.People1.ID);
            if (selectedObject.Assets != null)
                obj.Assets = os.GetObjectByKey<RiskAssets>(selectedObject.Assets.ID);
            if (selectedObject.Community != null)
                obj.Community = os.GetObjectByKey<RiskCommunities>(selectedObject.Community.ID);
            if (selectedObject.Environment != null)
                obj.Environment = os.GetObjectByKey<RiskEnvironments>(selectedObject.Environment.ID);
            obj.RiskAssessment = selectedObject.RiskAssessment;
            obj.RiskComment = selectedObject.RiskComment;
            obj.RiskIsolationID = selectedObject.RiskIsolationID;
            obj.RiskActionPlan = selectedObject.RiskActionPlan;

            if (selectedObject.Detail != null && selectedObject.Detail.Count > 0)
            {
                foreach (DeviationMitigations temp in selectedObject.Detail)
                {
                    DeviationMitigations dtl = os.CreateObject<DeviationMitigations>();
                    dtl.RowNumber = temp.RowNumber;
                    //if (temp.MitigationUser != null)
                    //    dtl.MitigationUser = os.GetObjectByKey<SystemUsers>(temp.MitigationUser.ID);
                    //if (temp.Position != null)
                    //    dtl.Position = os.GetObjectByKey<Positions>(temp.Position.ID);
                    dtl.MitigationUser = temp.MitigationUser;
                    dtl.Position = temp.Position;
                    dtl.Dscription = temp.Dscription;
                    dtl.Reason = temp.Reason;
                    dtl.AcceptanceCriteria = temp.AcceptanceCriteria;
                    if (temp.DeviationFrequency != null)
                        dtl.DeviationFrequency = os.GetObjectByKey<DeviationFrequencies>(temp.DeviationFrequency.ID);
                    dtl.DueDate = temp.DueDate;
                    dtl.CloseDate = temp.CloseDate;
                    if (temp.CloseUser != null)
                        dtl.CloseUser = os.GetObjectByKey<SystemUsers>(temp.CloseUser.ID);
                    if (temp.CancelUser != null)
                        dtl.CancelUser = os.GetObjectByKey<SystemUsers>(temp.CancelUser.ID);
                    dtl.CancelDate = temp.CancelDate;
                    obj.Detail.Add(dtl);
                }
            }
            if (selectedObject.Detail2 != null && selectedObject.Detail2.Count > 0)
            {
                foreach (DeviationReviewers temp in selectedObject.Detail2)
                {
                    DeviationReviewers dtl = os.CreateObject<DeviationReviewers>();
                    dtl.RowNumber = temp.RowNumber;
                    if (temp.Reviewer != null)
                        dtl.Reviewer = os.GetObjectByKey<SystemUsers>(temp.Reviewer.ID);
                    if (temp.Position != null)
                        dtl.Position = os.GetObjectByKey<Positions>(temp.Position.ID);
                    dtl.PositionRole = temp.PositionRole;
                    //dtl.KeyReviewer = temp.KeyReviewer;
                    obj.Detail2.Add(dtl);
                }
            }
            genCon.showMsg("Successful", "New Deviation.", InformationType.Success);
            genCon.openNewView(os, obj, ViewEditMode.Edit);

            ActiveItem();
        }

        private void DeviationToWO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)View).ViewEditMode != ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Goto Work Order cannot proceed while editing.", InformationType.Error);
                return;
            }
            Deviation2025 selectedObject = View.CurrentObject as Deviation2025;
            if (selectedObject.ID <= 0)
            {
                genCon.showMsg("Failed", "Deviation is not save yet.", InformationType.Error);
                return;
            }
            if (selectedObject.WorkOrder == null)
            {
                genCon.showMsg("Failed", "Work Order is empty.", InformationType.Error);
                return;
            }
            IObjectSpace os = Application.CreateObjectSpace();
            WorkOrders obj = os.FindObject<WorkOrders>(CriteriaOperator.Parse("ID=?", selectedObject.WorkOrder.ID));

            genCon.showMsg("Successful", "Back to work order.", InformationType.Success);
            genCon.openNewView(os, obj, ViewEditMode.Edit);

        }

        private void DeviationDraftExtend_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Deviation2025 selectedObject = (Deviation2025)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            add_deviation_docstatus(selectedObject, p, GeneralSettings.DeviationStatusDraftExtension);
        }

        private void add_deviation_docstatus(Deviation2025 currObject, StringParameters sp, string newstatus)
        {
            IObjectSpace ios = Application.CreateObjectSpace();
            Deviation2025 selectedObject = ios.GetObjectByKey<Deviation2025>(currObject.ID);

            if (newstatus == GeneralSettings.DeviationStatusUnderReview)
            {
                foreach(DeviationReviewers obj in selectedObject.Detail2)
                {
                    obj.ActReviewer = false;
                    obj.Comments = null;
                    obj.ReviewDate = null;
                }
            }
            DeviationDocStatus ds = ios.CreateObject<DeviationDocStatus>();
            ds.DocStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", newstatus));
            ds.DocRemarks = sp.ParamString;
            ds.CreateDate = DateTime.Now;
            ds.CreateUser = ios.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            selectedObject.Detail4.Add(ds);
            selectedObject.OnPropertyChanged("Detail4");

            selectedObject.DeviationStatus = ios.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", ds.DocStatus.BoCode));
            //ModificationsController controller = Frame.GetController<ModificationsController>();

            ios.CommitChanges();
            RefreshController refcon = Frame.GetController<RefreshController>();
            if (refcon != null)
                refcon.RefreshAction.DoExecute();

            genCon.showMsg("Successful", $"{newstatus} done", InformationType.Success);
            ActiveItem();

        }


        private void sp_checkstatus(Deviation2025 selectedObject, string newstatus, ref bool err, ref string msg)
        {
            if (newstatus != "" && !err)
            {
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

                SqlParameter param = new SqlParameter("@docid", selectedObject.ID);
                SqlParameter param1 = new SqlParameter("@newstatus", newstatus);
                SqlParameter param2 = new SqlParameter("@username", SecuritySystem.CurrentUserName);

                MyMsg mymsg = persistentObjectSpace.ObjectContext.ExecuteStoreQuery<MyMsg>("CheckDeviationStatus @docid, @newstatus, @username", param, param1, param2).FirstOrDefault();

                if (disposePersistentObjectSpace)
                {
                    persistentObjectSpace.Dispose();
                }

                if (mymsg != null && mymsg.id != 0)
                {
                    err = true;
                    msg = $"{mymsg.msg}";
                }
            }
        }

        private void DeviationSubmitAck_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            string msg = "Press OK to CONFIRM the action and SAVE, else press Cancel.";
            string newstatus = "";
            if (View.CurrentObject == null)
            {
                err = true;
                msg = "No deviation found.";
            }

            int ReviewID = 0;
            int RowNumber = 0;
            string UserID = "";
            string UserName = "";
            bool reject = false;
            if (!err)
            {
                Deviation2025 selectedObject = (Deviation2025)View.CurrentObject;
                if (e.Action.Id == "DeviationWithdraw")
                {
                    newstatus = GeneralSettings.DeviationStatusWithdrawn;
                    reject = true;
                }
                else if (e.Action.Id == "DeviationSubmitAck")
                {
                    newstatus = GeneralSettings.DeviationStatusSubmitAck;
                }

                #region check sp
                sp_checkstatus(selectedObject, newstatus, ref err, ref msg);
                #endregion

                DeviationReviewers reviewer = selectedObject.Detail2.Where(pp => pp.Reviewer.ID == ((SystemUsers)SecuritySystem.CurrentUser).ID).FirstOrDefault();
                if (reviewer != null)
                {
                    ReviewID = reviewer.ID;
                    RowNumber = reviewer.RowNumber;
                    UserID = reviewer.Reviewer.UserName;
                    UserName = reviewer.Reviewer.FullName;
                }
                else 
                {
                    err = true;
                    msg = "Current user is not under reviewer.";
                }
            }

            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new DeviationReviewerAck());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;

            ((DeviationReviewerAck)dv.CurrentObject).ReviewID = ReviewID;
            ((DeviationReviewerAck)dv.CurrentObject).RowNumber = RowNumber;
            ((DeviationReviewerAck)dv.CurrentObject).UserID = UserID;
            ((DeviationReviewerAck)dv.CurrentObject).UserName = UserName;

            ((DeviationReviewerAck)dv.CurrentObject).Reject = reject;
            ((DeviationReviewerAck)dv.CurrentObject).ParamDate = DateTime.Today;
            ((DeviationReviewerAck)dv.CurrentObject).IsErr = err;
            ((DeviationReviewerAck)dv.CurrentObject).ActionMessage = msg;

            e.View = dv;
        }

        private void DeviationSubmitApp_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            Deviation2025 selectedObject = (Deviation2025)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            add_deviation_docstatus(selectedObject, p, GeneralSettings.DeviationStatusPendingApproval);

        }
    }
}

