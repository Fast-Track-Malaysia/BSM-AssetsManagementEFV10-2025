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
using DevExpress.ExpressApp.Web.SystemModule;
using AssetsManagementEF.Module.BusinessObjects;
using AssetsManagementEF.Module.Controllers;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class WebModificationController : WebModificationsController
    {
        GenControllers genCon;

        public WebModificationController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.SaveAndCloseAction.Active.SetItemValue("A", false);
            this.SaveAndNewAction.Active.SetItemValue("A", false);
            //this.SaveAction.Active.SetItemValue("A", false);
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
        protected override void Save(SimpleActionExecuteEventArgs args)
        {
            if (View.ObjectTypeInfo.Type == typeof(WorkRequests))
            {
                WorkRequests selectObject = (WorkRequests)View.CurrentObject;
                if (!selectObject.IsCancelling)
                {
                    if (!selectObject.isDetailValid())
                    {
                        throw new Exception("There are no details in this document");
                    }
                }

            }
            else if (View.ObjectTypeInfo.Type == typeof(WorkOrders))
            {
                WorkOrders selectObject = (WorkOrders)View.CurrentObject;
                if (!selectObject.IsCancelling)
                {
                    if (!selectObject.isDetailValid())
                    {
                        throw new Exception("There are no details in this document.");
                    }
                    if (!selectObject.isSourceValid())
                    {
                        throw new Exception("This Document has no source.");
                    }
                    if (!selectObject.isAllowedSave())
                    {
                        if (ObjectSpace.ModifiedObjects.Count > 0)
                            throw new Exception("Cannot Save after Cancelled or Closure.");
                    }
                }

            }
            else if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
            {
                PurchaseRequests selectObject = (PurchaseRequests)View.CurrentObject;
                if (!selectObject.IsCancelling)
                {
                    if (!selectObject.isDetailValid())
                    {
                        throw new Exception("There are no details in this document.");
                    }
                    if (!selectObject.isSourceValid())
                    {
                        throw new Exception("This Document has no source.");
                    }
                    if (!selectObject.isAllowedContractDocSave())
                    {
                        throw new Exception("Contract is not valid.");
                    }
                    if (!selectObject.isAllowedContractSave())
                    {
                        if (ObjectSpace.IsModified)
                            throw new Exception("PR Date is out of Contract Date.");
                    }
                }

            }
            base.Save(args);

            //if (View.ObjectTypeInfo.Type == typeof(PurchaseRequestAttachments))
            //{
            //    PurchaseRequestAttachments obj = (PurchaseRequestAttachments)View.CurrentObject;
            //    string fullpath = GeneralSettings.B1AttachmentPath + obj.AttachFile.FileName;

                //    //System.IO.Stream str = new System.IO.MemoryStream();
                //    //obj.AttachFile.SaveToStream(str);

                //    using (System.IO.FileStream fs = System.IO.File.OpenRead("D:\\T540367.xlsx"))
                //    {
                //        if (obj.AttachFile == null)
                //        {
                //            obj.AttachFile = View.ObjectSpace.CreateObject<DevExpress.Persistent.BaseImpl.EF.FileData>();
                //        }
                //        obj.AttachFile.LoadFromStream("T540367.xlsx", fs);
                //        View.ObjectSpace.CommitChanges();

                //    }
                //    using (System.IO.FileStream fs = System.IO.File.OpenWrite(fullpath))
                //    {
                //        obj.AttachFile.SaveToStream(fs);
                //    }

                //}
            if (View.ObjectTypeInfo.Type == typeof(Deviation2025))
            {
                Deviation2025 obj = (Deviation2025)View.CurrentObject;
                IObjectSpace ios = Application.CreateObjectSpace();
                Deviation2025 iobj = ios.GetObjectByKey<Deviation2025>(obj.ID);

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(ios, iobj);
                dv.ViewEditMode = ViewEditMode.View;
                svp.CreatedView = dv;

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
            else if (View.ObjectTypeInfo.Type == typeof(Equipments))
            {
                Equipments obj = (Equipments)View.CurrentObject;
                IObjectSpace ios = Application.CreateObjectSpace();
                Equipments iobj = ios.GetObjectByKey<Equipments>(obj.ID);

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(ios, iobj);
                dv.ViewEditMode = ViewEditMode.View;
                svp.CreatedView = dv;

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
            else if (View.ObjectTypeInfo.Type == typeof(EquipmentComponents))
            {
                //EquipmentComponents obj = (EquipmentComponents)View.CurrentObject;
                //IObjectSpace ios = Application.CreateObjectSpace();
                //EquipmentComponents iobj = ios.GetObjectByKey<EquipmentComponents>(obj.ID);

                //ShowViewParameters svp = new ShowViewParameters();
                //DetailView dv = Application.CreateDetailView(ios, iobj);
                //dv.ViewEditMode = ViewEditMode.View;
                //svp.CreatedView = dv;

                //Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
            else if (View.ObjectTypeInfo.Type == typeof(PMSchedules))
            {
                PMSchedules obj = (PMSchedules)View.CurrentObject;
                IObjectSpace ios = Application.CreateObjectSpace();
                PMSchedules iobj = ios.GetObjectByKey<PMSchedules>(obj.ID);

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(ios, iobj);
                dv.ViewEditMode = ViewEditMode.View;
                svp.CreatedView = dv;

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
            else if (View.ObjectTypeInfo.Type == typeof(WorkRequests))
            {
                WorkRequests obj = (WorkRequests)View.CurrentObject;
                IObjectSpace ios = Application.CreateObjectSpace();
                WorkRequests iobj = ios.GetObjectByKey<WorkRequests>(obj.ID);

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(ios, iobj);
                dv.ViewEditMode = ViewEditMode.View;
                svp.CreatedView = dv;

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
            else if (View.ObjectTypeInfo.Type == typeof(WorkOrders))
            {
                //RefreshController refreshController = Frame.GetController<RefreshController>();
                //if (refreshController != null)
                //{
                //    refreshController.RefreshAction.DoExecute();
                //}
                WorkOrders obj = (WorkOrders)View.CurrentObject;
                IObjectSpace ios = Application.CreateObjectSpace();
                WorkOrders iobj = ios.GetObjectByKey<WorkOrders>(obj.ID);

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(ios, iobj);
                dv.ViewEditMode = ViewEditMode.View;
                svp.CreatedView = dv;
                if (iobj.Approved)
                {
                    bool found = false;
                    foreach (PurchaseRequests dtl in iobj.DetailRequest)
                    {
                        if (dtl.DocPassed && !dtl.DocPosted && !dtl.Cancelled)
                        {
                            found = true;
                        }
                    }
                    if (found)
                    {
                        bool prupdate = false;

                        if (genCon.ConnectSAP())
                        {
                            string docs = "";
                            GeneralSettings.oCompany.StartTransaction();
                            foreach (PurchaseRequests dtl in iobj.DetailRequest)
                            {
                                int temp = genCon.PostPRtoSAP(dtl);
                                if (temp == 1)
                                {
                                    dtl.DocPassed = false;
                                    dtl.DocPosted = true;

                                    PurchaseRequestDocStatuses ds = ios.CreateObject<PurchaseRequestDocStatuses>();
                                    ds.DocStatus = DocumentStatus.Posted;
                                    ds.DocRemarks = "";
                                    dtl.DetailDocStatus.Add(ds);
                                    dtl.OnPropertyChanged("DetailDocStatus");
                                    docs = docs + System.Environment.NewLine + "Document No = " + obj.DocNum;
                                    prupdate = true;
                                }
                                else if (temp == -1)
                                {
                                }
                            }
                            if (GeneralSettings.oCompany.InTransaction)
                                GeneralSettings.oCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                            if (prupdate)
                            {
                                ios.CommitChanges();

                                if (GeneralSettings.EmailSend)
                                {
                                    try
                                    {
                                        IList<Positions> positionlist = ObjectSpace.GetObjects<Positions>(new ContainsOperator("DetailPlannerGroup", new BinaryOperator("ID", obj.AssignPlannerGroup.ID, BinaryOperatorType.Equal)));
                                        List<string> ToEmails = new List<string>();

                                        foreach (Positions position in positionlist)
                                        {
                                            if (position.IsPRApprove && position.CurrentUser.UserEmail.Trim() != "")
                                                ToEmails.Add(position.CurrentUser.UserEmail.Trim());

                                        }
                                        if (ToEmails.Count > 0)
                                            genCon.SendEmail("PUrchase Request Pending For Approval.", "Planner Group = " + obj.AssignPlannerGroup.BoCode + docs, ToEmails);
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
                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));

            }
            else if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
            {
                PurchaseRequests obj = (PurchaseRequests)View.CurrentObject;
                IObjectSpace ios = Application.CreateObjectSpace();
                PurchaseRequests iobj = ios.GetObjectByKey<PurchaseRequests>(obj.ID);

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

                            if (GeneralSettings.EmailSend)
                            {
                                try
                                {
                                    IList<Positions> positionlist = ObjectSpace.GetObjects<Positions>(new ContainsOperator("DetailPlannerGroup", new BinaryOperator("ID", obj.PlannerGroup.ID, BinaryOperatorType.Equal)));
                                    List<string> ToEmails = new List<string>();

                                    foreach (Positions position in positionlist)
                                    {
                                        if (position.IsPRApprove && position.CurrentUser.UserEmail.Trim() != "")
                                            ToEmails.Add(position.CurrentUser.UserEmail.Trim());

                                    }
                                    if (ToEmails.Count > 0)
                                        genCon.SendEmail("Purchase Request Pending For Approval.", "Planner Group = " + obj.PlannerGroup.BoCode + System.Environment.NewLine + "Document No = " + obj.DocNum, ToEmails);
                                }
                                catch (Exception ex)
                                {
                                    genCon.showMsg("Error", ex.Message, InformationType.Error);
                                }
                            }

                        }
                    }
                }
                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            }
        }
        protected override void SaveAndClose(SimpleActionExecuteEventArgs args)
        {
            base.SaveAndClose(args);
        }
        protected override void SaveAndNew(SingleChoiceActionExecuteEventArgs args)
        {
            base.SaveAndNew(args);
        }
    }
}
