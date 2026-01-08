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
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;


namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PurchaseRequestControllers : ViewController
    {
        GenControllers genCon;
        public PurchaseRequestControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(PurchaseRequests);
            TargetViewType = ViewType.DetailView;
            TargetViewNesting = Nesting.Root;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            PurchaseRequests selectedObject = (PurchaseRequests)View.CurrentObject;
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
            bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
            bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;
            //bool IsAdmin = user.Roles.Where(p => p.Name == DevExpress.ExpressApp.Security.SecurityStrategy.AdministratorRoleName).Count() > 0 ? true : false;
            bool IsAdmin = user.Roles.Where(p => p.IsAdministrative).Count() > 0 ? true : false;
            bool IsCancelRequest = user.Roles.Where(p => p.Name == GeneralSettings.CancelPRRole).Count() > 0 ? true : false;
            bool isPostPR = user.Roles.Where(p => p.Name == GeneralSettings.PostPRRole).Count() > 0 ? true : false;
            bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

            this.RejectPR.Active.SetItemValue("Enabled", false);
            this.PassPR.Active.SetItemValue("Enabled", false);
            this.CancelPR.Active.SetItemValue("Enabled", false);
            this.GetContractItem.Active.SetItemValue("Enabled", false);
            this.PostPR.Active.SetItemValue("Enabled", false);

            if (isContractor)
            { }
            else
            {
                if (isPostPR && selectedObject.DocPassed && !selectedObject.Cancelled && !selectedObject.DocPosted)
                {
                    this.PostPR.Active.SetItemValue("Enabled", true);
                }

                if (!selectedObject.IsNew)
                {
                    if (IsPlanner || IsWPS)
                    {
                        if (selectedObject.Cancelled)
                        {
                        }
                        else
                        {
                            this.RejectPR.Active.SetItemValue("Enabled", selectedObject.DocPassed || selectedObject.DocPosted);
                        }
                        if (selectedObject.DocPassed || selectedObject.Cancelled || selectedObject.DocPosted)
                        { }
                        else
                        {
                            this.GetContractItem.Active.SetItemValue("Enabled", true);

                            if (selectedObject.WorkOrder != null)
                                this.PassPR.Active.SetItemValue("Enabled", selectedObject.WorkOrder.Approved);

                        }

                    }
                    if (IsPlanner || IsWPS || IsCancelRequest)
                    {
                        if (selectedObject.Cancelled)
                        {
                        }
                        else
                        {
                            this.CancelPR.Active.SetItemValue("Enabled", true);
                        }
                    }

                }
                else
                {
                    this.GetContractItem.Active.SetItemValue("Enabled", true);
                }
            }
            this.RejectPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.CancelPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.PassPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);

            this.GetContractItem.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            this.BackToWO.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            this.PostPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);

            if (View.GetType() == typeof(DetailView))
                ((DetailView)View).ViewEditModeChanged += PurchaseRequestControllers_ViewEditModeChanged;
        }

        private void PurchaseRequestControllers_ViewEditModeChanged(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                this.RejectPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.CancelPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.PassPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);

                this.GetContractItem.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                this.BackToWO.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
                this.PostPR.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
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
                ((DetailView)View).ViewEditModeChanged -= PurchaseRequestControllers_ViewEditModeChanged;
            }

        }

        private void BackToWO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)View.CurrentObject;
            if (selectedObject.WorkOrder != null)
            {
                IObjectSpace os = Application.CreateObjectSpace();
                WorkOrders obj = os.FindObject<WorkOrders>(CriteriaOperator.Parse("ID=?", selectedObject.WorkOrder.ID));

                genCon.openNewView(os, obj, ViewEditMode.View);
                genCon.showMsg("Successful", "Back to Work Order.", InformationType.Success);
            }
            else
                genCon.showMsg("Failed", "There is no Work Order.", InformationType.Error);

        }

        private void StringParametersCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            PurchaseRequests selectedObject = (PurchaseRequests)View.CurrentObject;
            if (selectedObject.IsNew && !err)
            {
                genCon.showMsg("Failed", "New Purchase Request cannot proceed.", InformationType.Error);
                err = true;
            }
            else
            {
                if (e.Action.Id == this.CancelPR.Id)
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.View)
                    {
                        genCon.showMsg("Failed", "Viewing Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                    //if (selectedObject.DocPosted && !err)
                    //{
                    //    genCon.showMsg("Failed", "SAP Posted Purchase Request cannot proceed.", InformationType.Error);
                    //    err = true;
                    //}

                    if (selectedObject.Cancelled && !err)
                    {
                        genCon.showMsg("Failed", "The Purchase Request is Cancelled.", InformationType.Error);
                        err = true;
                    }

                    //if (selectedObject.Approved && !err)
                    //{
                    //    genCon.showMsg("Failed", "Approved Purchase Request cannot proceed.", InformationType.Error);
                    //    err = true;
                    //}
                    if (!err)
                    {
                        if (selectedObject.IsWPSChecking || selectedObject.IsPlannerChecking)
                        { }
                        else if (selectedObject.IsCancelUserChecking)
                        { }
                        else
                        {
                            genCon.showMsg("Failed", "This is not a Planner process.", InformationType.Error);
                            err = true;
                        }
                    }
                    //if (selectedObject.DocPassed && !err)
                    //{
                    //    genCon.showMsg("Failed", "Passed Purchase Request cannot proceed.", InformationType.Error);
                    //    err = true;
                    //}
                }
                else if (e.Action.Id == this.PassPR.Id && !err)
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.View)
                    {
                        genCon.showMsg("Failed", "Viewing Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                    if (selectedObject.DocPosted && !err)
                    {
                        genCon.showMsg("Failed", "SAP Posted Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                    //if (selectedObject.Approved && !err)
                    //{
                    //    genCon.showMsg("Failed", "Approved Purchase Request cannot proceed.", InformationType.Error);
                    //    err = true;
                    //}

                    if (!selectedObject.IsWPSChecking && !err)
                    {
                        genCon.showMsg("Failed", "This is not a Planner process.", InformationType.Error);
                        err = true;
                    }

                    if (selectedObject.Cancelled && !err)
                    {
                        genCon.showMsg("Failed", "Cancelled Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                    if (selectedObject.DocPassed && !err)
                    {
                        genCon.showMsg("Failed", "Confirmed Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                }
                else if (e.Action.Id == this.RejectPR.Id)
                {
                    if (((DetailView)View).ViewEditMode != ViewEditMode.View)
                    {
                        genCon.showMsg("Failed", "Editing Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                    if (selectedObject.Cancelled && !err)
                    {
                        genCon.showMsg("Failed", "Cancelled Purchase Request cannot proceed.", InformationType.Error);
                        err = true;
                    }

                    if (!err)
                    {
                        if (selectedObject.DocPassed || selectedObject.DocPosted)
                        { }
                        else
                        {
                            genCon.showMsg("Failed", "Only Passed or Posted Purchase Request can proceed.", InformationType.Error);
                            err = true;
                        }
                    }

                    if (!selectedObject.IsWPSChecking && !err)
                    {
                        genCon.showMsg("Failed", "This is not a WPS process.", InformationType.Error);
                        err = true;
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

        private void PassPR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.DocPassed)
            {
                selectedObject.DocPassed = false;

                PurchaseRequestDocStatuses ds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                ds.DocStatus = DocumentStatus.DocPassed;
                ds.DocRemarks = p.ParamString;
                ds.IsReverse = true;
                selectedObject.DetailDocStatus.Add(ds);
                selectedObject.OnPropertyChanged("DetailDocStatus");

                ModificationsController controller = Frame.GetController<ModificationsController>();
                if (controller != null)
                {
                    controller.SaveAction.DoExecute();
                }

                genCon.showMsg("Successful", "Undo Passing Done.", InformationType.Success);
            }
            else
            {
                selectedObject.DocPassed = true;
                selectedObject.Rejected = false;
                //selectedObject.Approved = false;
                selectedObject.Cancelled = false;

                PurchaseRequestDocStatuses ds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
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

            }

        }

        private void CancelPR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.Cancelled)
            {
                //selectedObject.Cancelled = false;

                //PurchaseRequestDocStatuses ds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
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
                //selectedObject.Approved = false;

                PurchaseRequestDocStatuses ds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
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

        private void RejectPR_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)e.CurrentObject;
            StringParameters p = (StringParameters)e.PopupWindow.View.CurrentObject;
            if (p.IsErr) return;

            if (selectedObject.Rejected)
            {
                //selectedObject.Rejected = false;

                //PurchaseRequestDocStatuses ds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                //ds.DocStatus = DocumentStatus.Rejected;
                //ds.DocRemarks = p.ParamString;
                //ds.IsReverse = true;
                //selectedObject.DetailDocStatus.Add(ds);
                //selectedObject.OnPropertyChanged("DetailDocStatus");

                //genCon.showMsg("Successful", "Undo Reject Done. Please save changes afterwards.", InformationType.Success);
            }
            else
            {
                //selectedObject.Rejected = true;
                //selectedObject.DocPassed = false;
                ////selectedObject.Approved = false;
                //selectedObject.Cancelled = false;

                //PurchaseRequestDocStatuses ds = ObjectSpace.CreateObject<PurchaseRequestDocStatuses>();
                //ds.DocStatus = DocumentStatus.Rejected;
                //ds.DocRemarks = p.ParamString;
                //selectedObject.DetailDocStatus.Add(ds);
                //selectedObject.OnPropertyChanged("DetailDocStatus");

                IObjectSpace os;
                os = Application.CreateObjectSpace();
                PurchaseRequests obj = os.CreateObject<PurchaseRequests>();

                obj.DocNumSeq = selectedObject.DocNumSeq;
                obj.OriginID = selectedObject.ID;
                obj.OriginRejectRemarks = p.ParamString;
                obj.RevisionNo = selectedObject.RevisionNo + 1;
                obj.DocNum = selectedObject.DocNumSeq.ToString() + "-" + obj.RevisionNo.ToString().PadLeft(2, '0');
                obj.RefNo = selectedObject.RefNo;

                if (selectedObject.ContractDoc != null)
                    obj.ContractDoc = os.GetObjectByKey<ContractDocs>(selectedObject.ContractDoc.ID);

                if (selectedObject.Contractor != null)
                    obj.Contractor = os.GetObjectByKey<Contractors>(selectedObject.Contractor.ID);

                if (selectedObject.WorkOrder != null)
                    obj.WorkOrder = os.GetObjectByKey<WorkOrders>(selectedObject.WorkOrder.ID);

                if (selectedObject.PlannerGroup != null)
                    obj.PlannerGroup = os.GetObjectByKey<PlannerGroups>(selectedObject.PlannerGroup.ID);

                if (selectedObject.Detail != null)
                {
                    foreach (PurchaseRequestDtls dtl in selectedObject.Detail)
                    {
                        PurchaseRequestDtls prdtl = os.CreateObject<PurchaseRequestDtls>();
                        if (dtl.ItemMaster != null)
                            prdtl.ItemMaster = os.GetObjectByKey<ItemMasters>(dtl.ItemMaster.ID);
                        prdtl.ItemDesc = dtl.ItemDesc;
                        prdtl.QTY = dtl.QTY;
                        prdtl.Price = dtl.Price;
                        obj.Detail.Add(prdtl);

                    }
                }

                genCon.openNewView(os, obj, ViewEditMode.Edit);
                genCon.showMsg("Successful", "Reject Done. Please the Purchase Request in new Revision.", InformationType.Success);
            }
        }

        private void GetContractItem_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (((DetailView)View).ViewEditMode == ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Viewing Purchase Request cannot proceed.", InformationType.Error);
            }
            else
            {

                PurchaseRequests selectedobject = (PurchaseRequests)View.CurrentObject;
                if (selectedobject.ContractDoc != null)
                {
                    if (selectedobject.DocPassed || selectedobject.Cancelled || selectedobject.DocPosted)
                    {
                        genCon.showMsg("Failed", "Confirmed/Cancelled/Posted Request cannot proceed.", InformationType.Error);
                    }
                    else
                        GeneralSettings.contractdocid = selectedobject.ContractDoc.ID;
                }
                else
                {
                    genCon.showMsg("Failed", "Please select contract.", InformationType.Error);
                }
            }
            e.View = Application.CreateListView(typeof(ContractDocDtls), true);
            /*
            IList<ContractDocDtlDC> objects = new List<ContractDocDtlDC>();
            PurchaseRequests selectedobject = (PurchaseRequests)View.CurrentObject;
            if (selectedobject.ContractDoc != null)
            {
                ListView lv = Application.CreateListView(typeof(ContractDocDtlDC), true);
                foreach (ContractDocDtls dtl in selectedobject.ContractDoc.Detail)
                {
                    ContractDocDtlDC obj = new ContractDocDtlDC();
                    obj.ItemCode = os.GetObjectByKey<ItemMasters>(dtl.ItemMaster.ID);
                    obj.ItemDesc = dtl.ItemDesc;
                    obj.QTY = dtl.QTY;
                    obj.Price = dtl.Price;
                }
                e.View = lv;
            }
            else
            {
                genCon.showMsg("Failed", "Must select a contract.", InformationType.Error);
                e.View = Application.CreateListView(typeof(ContractDocDtlDC), true);
            }
            */
        }

        private void GetContractItem_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace os = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            PurchaseRequests selectedObject = (PurchaseRequests)e.CurrentObject;
            foreach (ContractDocDtls dtl in e.PopupWindow.View.SelectedObjects)
            {
                if (dtl.QTY > 0)
                {
                    PurchaseRequestDtls prdtl = os.CreateObject<PurchaseRequestDtls>();
                    if (dtl.ItemMaster != null)
                        prdtl.ItemMaster = os.FindObject<ItemMasters>(CriteriaOperator.Parse("ID=?", dtl.ItemMaster.ID));

                    prdtl.ItemDesc = dtl.ItemDesc;
                    prdtl.QTY = dtl.QTY;
                    prdtl.Price = dtl.Price;
                    prdtl.ContractDocDtl = os.FindObject<ContractDocDtls>(CriteriaOperator.Parse("ID=?", dtl.ID));
                    selectedObject.Detail.Add(prdtl);
                }
            }
            selectedObject.OnPropertyChanged("Detail");

        }

        private void PostPR_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            PurchaseRequests selectedObject = (PurchaseRequests)View.CurrentObject;
            if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
            {
                genCon.showMsg("Failed", "Viewing Purchase Request cannot proceed.", InformationType.Error);
                return;
            }
            if (selectedObject.WorkOrder == null)
            {
                genCon.showMsg("Failed", "Purchase Request without Work Order cannot proceed.", InformationType.Error);
                return;
            }
            if (!selectedObject.WorkOrder.Approved)
            {
                genCon.showMsg("Failed", "Purchase Request without Work Order Approved cannot proceed.", InformationType.Error);
                return;
            }
            if (selectedObject.DocPosted)
            {
                genCon.showMsg("Failed", "Posted Purchase Request cannot proceed.", InformationType.Error);
                return;
            }
            if (selectedObject.Cancelled)
            {
                genCon.showMsg("Failed", "Cancelled Purchase Request cannot proceed.", InformationType.Error);
                return;
            }

            IObjectSpace ios = Application.CreateObjectSpace();
            PurchaseRequests iobj = ios.GetObjectByKey<PurchaseRequests>(selectedObject.ID);

            ShowViewParameters svp = new ShowViewParameters();
            DetailView dv = Application.CreateDetailView(ios, iobj);
            dv.ViewEditMode = ViewEditMode.View;
            svp.CreatedView = dv;

            if (iobj.WorkOrder != null && iobj.WorkOrder.Approved)
            {
                if (iobj.DocPassed && !iobj.DocPosted && !iobj.Cancelled)
                {
                    if (genCon.ConnectSAP())
                    {
                        GeneralSettings.oCompany.StartTransaction();
                        int temp = genCon.PostPRtoSAP(iobj);
                        if (temp == 1)
                        {
                            iobj.DocPassed = false;
                            iobj.DocPosted = true;

                            PurchaseRequestDocStatuses ds = ios.CreateObject<PurchaseRequestDocStatuses>();
                            ds.DocStatus = DocumentStatus.Posted;
                            ds.DocRemarks = "";
                            iobj.DetailDocStatus.Add(ds);
                            iobj.OnPropertyChanged("DetailDocStatus");

                            ios.CommitChanges();
                        }
                        else if (temp == -1)
                        {
                        }
                        if (GeneralSettings.oCompany.InTransaction)
                            GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                    }
                }
            }

            RefreshController refreshController = Frame.GetController<RefreshController>();
            if (refreshController != null)
            {
                refreshController.RefreshAction.DoExecute();
            }
            genCon.showMsg("Successful", "Post to SAP B1 Done.", InformationType.Success);

            if (GeneralSettings.EmailSend)
            {
                try
                {
                    IList<Positions> positionlist = ObjectSpace.GetObjects<Positions>(new ContainsOperator("DetailPlannerGroup", new BinaryOperator("ID", selectedObject.PlannerGroup.ID, BinaryOperatorType.Equal)));
                    List<string> ToEmails = new List<string>();

                    foreach (Positions position in positionlist)
                    {
                        if (position.IsPRApprove && position.CurrentUser.UserEmail.Trim() != "")
                            ToEmails.Add(position.CurrentUser.UserEmail.Trim());

                    }
                    if (ToEmails.Count > 0)
                        genCon.SendEmail("Purchase Request Pending For Approval.", "Planner Group = " + selectedObject.PlannerGroup.BoCode + System.Environment.NewLine + "Document No = " + selectedObject.DocNum, ToEmails);
                }
                catch (Exception ex)
                {
                    genCon.showMsg("Error", ex.Message, InformationType.Error);
                }
            }

        }
    }
}
