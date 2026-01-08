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
using SAPbobsCOM;
using System.Net.Mail;
using System.Net;
using AssetsManagementEF.Module.BusinessObjects;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GenControllers : ViewController
    {
        SimpleAction GoToDoc;
        SimpleAction ConnectToSAP;
        public GenControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            GoToDoc = new SimpleAction(this, "GotoDoc", "Unspecified") { Caption = "Goto Origin" };
            ConnectToSAP = new SimpleAction(this, "ConnectToSAP", "Unspecified") { Caption = "Test SAP Connection" };
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            GoToDoc.Execute += GoToDoc_Execute;
            ConnectToSAP.Execute += ConnectToSAP_Execute;
            GoToDoc.Active.SetItemValue("Enabled", false);
            ConnectToSAP.Active.SetItemValue("Enabled", false);

            if (View.ObjectTypeInfo.Type == typeof(Companies))
                ConnectToSAP.Active.SetItemValue("Enabled", true);

            if (View.IsRoot)
            {
                if (View.ObjectTypeInfo.Type == typeof(PMScheduleEquipments))
                    GoToDoc.Active.SetItemValue("Enabled", true);
                if (View.ObjectTypeInfo.Type == typeof(PMScheduleEqComponents))
                    GoToDoc.Active.SetItemValue("Enabled", true);
                if (View.ObjectTypeInfo.Type == typeof(WorkOrderEquipments))
                    GoToDoc.Active.SetItemValue("Enabled", true);
                if (View.ObjectTypeInfo.Type == typeof(WorkOrderEqComponents))
                    GoToDoc.Active.SetItemValue("Enabled", true);
                if (View.ObjectTypeInfo.Type == typeof(WorkRequestEquipments))
                    GoToDoc.Active.SetItemValue("Enabled", true);
                if (View.ObjectTypeInfo.Type == typeof(WorkRequestEqComponents))
                    GoToDoc.Active.SetItemValue("Enabled", true);

                if (View.ObjectTypeInfo.Type == typeof(EquipmentComponents))
                    GoToDoc.Active.SetItemValue("Enabled", true);

            }
            if (View.GetType() == typeof(DetailView))
            {
                ((DetailView)View).ViewEditModeChanged += GenControllers_ViewEditModeChanged;

            }

            NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                //controller.NewObjectAction.Execute += NewObjectAction_Execute;
                controller.ObjectCreated += Controller_ObjectCreated;

            }
            // hide new button in lookup listview and lookup property
            if (Frame.Context == TemplateContext.LookupControl || Frame.Context == TemplateContext.LookupWindow)
            {
                NewObjectViewController newcon = Frame.GetController<NewObjectViewController>();
                if (newcon != null)
                    newcon.NewObjectAction.Active.SetItemValue("LookupNew", false);
                DeleteObjectsViewController deletecon = Frame.GetController<DeleteObjectsViewController>();
                if (deletecon != null)
                    deletecon.DeleteAction.Active.SetItemValue("LookupDelete", false);
            }
        }

        private void ConnectToSAP_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (!ConnectSAP()) throw new Exception("Test SAP Connection Failed.");
            showMsg("Successful", "Connected to sap.", InformationType.Success);
        }

        private void Controller_ObjectCreated(object sender, ObjectCreatedEventArgs e)
        {
            if (View is ListView)
            {
                if (View.ObjectTypeInfo.Type == typeof(DeviationWorkOrders)
                    || View.ObjectTypeInfo.Type == typeof(DeviationWorkRequests)
                    || View.ObjectTypeInfo.Type == typeof(DeviationMitigations)
                    || View.ObjectTypeInfo.Type == typeof(DeviationReviewers)

                    )
                {
                    ListView lv = ((ListView)View);
                    if (lv.CollectionSource is PropertyCollectionSource)
                    {
                        PropertyCollectionSource collectionSource = (PropertyCollectionSource)lv.CollectionSource;
                        if (collectionSource.MasterObject != null)
                        {
                            int maxvisorder = 0;
                            int comparevisorder = 0;

                            object masterobject = collectionSource.MasterObject;
                            object currentobject = e.CreatedObject;

                            string detailprop = "";
                            if (View.ObjectTypeInfo.Type == typeof(DeviationWorkOrders)
                                || View.ObjectTypeInfo.Type == typeof(DeviationWorkRequests))
                                detailprop = "DetailDeviation";
                            else if (View.ObjectTypeInfo.Type == typeof(DeviationMitigations))
                                detailprop = "Detail";
                            else if (View.ObjectTypeInfo.Type == typeof(DeviationReviewers))
                                detailprop = "Detail2";

                            var propCollect = masterobject.GetType().GetProperty(detailprop);

                            if (propCollect != null)
                            {
                                object detail = propCollect.GetValue(masterobject);
                                if (View.ObjectTypeInfo.Type == typeof(DeviationWorkOrders))
                                {
                                    if ((detail as System.Collections.IEnumerable).OfType<DeviationWorkOrders>().Count() > 0)
                                    {
                                        comparevisorder = (detail as System.Collections.IEnumerable).OfType<DeviationWorkOrders>().Max(pp => pp.RowNumber);
                                    }
                                }
                                else if (View.ObjectTypeInfo.Type == typeof(DeviationWorkRequests))
                                {
                                    if ((detail as System.Collections.IEnumerable).OfType<DeviationWorkRequests>().Count() > 0)
                                    {
                                        comparevisorder = (detail as System.Collections.IEnumerable).OfType<DeviationWorkRequests>().Max(pp => pp.RowNumber);
                                    }
                                }
                                else if (View.ObjectTypeInfo.Type == typeof(DeviationMitigations))
                                {
                                    if ((detail as System.Collections.IEnumerable).OfType<DeviationMitigations>().Count() > 0)
                                    {
                                        comparevisorder = (detail as System.Collections.IEnumerable).OfType<DeviationMitigations>().Max(pp => pp.RowNumber);
                                    }
                                }
                                else if (View.ObjectTypeInfo.Type == typeof(DeviationReviewers))
                                {
                                    if ((detail as System.Collections.IEnumerable).OfType<DeviationReviewers>().Count() > 0)
                                    {
                                        comparevisorder = (detail as System.Collections.IEnumerable).OfType<DeviationReviewers>().Max(pp => pp.RowNumber);
                                    }
                                }

                                if (comparevisorder >= maxvisorder) maxvisorder = comparevisorder + 1;
                                Type type = currentobject.GetType();
                                type.GetProperty("RowNumber").SetValue(currentobject, maxvisorder);

                            }
                        }
                    }
                }
            }
        }
        private void GenControllers_ViewEditModeChanged(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                //this.GoToDoc.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.View);
            }
        }

        private void GoToDoc_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is ListView)
            {
                if (((ListView)View).SelectedObjects.Count != 1)
                {
                    showMsg("Failed", "Please select one Component.", InformationType.Error);
                    return;
                }

                foreach (object dtl in ((ListView)View).SelectedObjects)
                {
                    if (View.ObjectTypeInfo.Type == typeof(PMScheduleEquipments))
                        openOriginFromObject<PMSchedules>("PMSchedule", dtl);
                    if (View.ObjectTypeInfo.Type == typeof(PMScheduleEqComponents))
                        openOriginFromObject<PMSchedules>("PMSchedule", dtl);
                    if (View.ObjectTypeInfo.Type == typeof(WorkOrderEquipments))
                        openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkOrders>("WorkOrder", dtl);
                    if (View.ObjectTypeInfo.Type == typeof(WorkOrderEqComponents))
                        openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkOrders>("WorkOrder", dtl);
                    if (View.ObjectTypeInfo.Type == typeof(WorkRequestEquipments))
                        openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkRequests>("WorkRequest", dtl);
                    if (View.ObjectTypeInfo.Type == typeof(WorkRequestEqComponents))
                        openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkRequests>("WorkRequest", dtl);

                    if (View.ObjectTypeInfo.Type == typeof(EquipmentComponents))
                        openOriginFromObject<Equipments>("Equipment", dtl);
                }

            }
            else if (View is DetailView)
            {
                if (View.ObjectTypeInfo.Type == typeof(PMScheduleEquipments))
                    openOriginFromObject<PMSchedules>("PMSchedule", e.CurrentObject);
                if (View.ObjectTypeInfo.Type == typeof(PMScheduleEqComponents))
                    openOriginFromObject<PMSchedules>("PMSchedule", e.CurrentObject);
                if (View.ObjectTypeInfo.Type == typeof(WorkOrderEquipments))
                    openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkOrders>("WorkOrder", e.CurrentObject);
                if (View.ObjectTypeInfo.Type == typeof(WorkOrderEqComponents))
                    openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkOrders>("WorkOrder", e.CurrentObject);
                if (View.ObjectTypeInfo.Type == typeof(WorkRequestEquipments))
                    openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkRequests>("WorkRequest", e.CurrentObject);
                if (View.ObjectTypeInfo.Type == typeof(WorkRequestEqComponents))
                    openOriginFromObject<AssetsManagementEF.Module.BusinessObjects.WorkRequests>("WorkRequest", e.CurrentObject);

                if (View.ObjectTypeInfo.Type == typeof(EquipmentComponents))
                    openOriginFromObject<Equipments>("Equipment", e.CurrentObject);
            }
        }

        private void openOriginFromObject<T>(string DocumentProp, object selectedobject)
        {
            int id = 0;
            object value = selectedobject.GetType().GetProperty(DocumentProp).GetValue(selectedobject);
            if (value != null)
                id = Convert.ToInt32(value.GetType().GetProperty("ID").GetValue(value));

            if (id > 0)
            {
                IObjectSpace os = Application.CreateObjectSpace();
                T obj = os.FindObject<T>(CriteriaOperator.Parse("ID=?", id));
                openNewView(os, obj, ViewEditMode.View);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            GoToDoc.Execute -= GoToDoc_Execute;
            ConnectToSAP.Execute -= ConnectToSAP_Execute;
            if (View.GetType() == typeof(DetailView))
            {
                ((DetailView)View).ViewEditModeChanged -= GenControllers_ViewEditModeChanged;
            }
            NewObjectViewController controller = Frame.GetController<NewObjectViewController>();
            if (controller != null)
            {
                controller.ObjectCreated -= Controller_ObjectCreated;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        //protected override void OnViewControlsDestroying()
        //{
        //    base.OnViewControlsDestroying();
        //    if (GeneralSettings.oCompany != null)
        //    {
        //        if (GeneralSettings.oCompany.Connected)
        //            GeneralSettings.oCompany.Disconnect();
        //    }
        //}
        public void openNewView(IObjectSpace os, object target, ViewEditMode viewmode)
        {
            ShowViewParameters svp = new ShowViewParameters();
            DetailView dv = Application.CreateDetailView(os, target);
            dv.ViewEditMode = viewmode;
            dv.IsRoot = true;
            svp.CreatedView = dv;

            Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));

        }
        public void showMsg(string caption, string msg, InformationType msgtype)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 3000;
            //options.Message = string.Format("{0} task(s) have been successfully updated!", e.SelectedObjects.Count);
            options.Message = string.Format("{0}", msg);
            options.Type = msgtype;
            options.Web.Position = InformationPosition.Right;
            options.Win.Caption = caption;
            options.Win.Type = WinMessageType.Flyout;
            Application.ShowViewStrategy.ShowMessage(options);
            
        }
        public bool ConnectSAP()
        {
            if (GeneralSettings.B1Post)
            {
                if (GeneralSettings.oCompany == null)
                {
                    GeneralSettings.oCompany = new SAPbobsCOM.Company();
                }

                if (GeneralSettings.oCompany != null && !GeneralSettings.oCompany.Connected)
                {
                    bool exist = Enum.IsDefined(typeof(SAPbobsCOM.BoDataServerTypes), GeneralSettings.B1DbServerType);
                    if (exist)
                        GeneralSettings.oCompany.DbServerType = (SAPbobsCOM.BoDataServerTypes)Enum.Parse(typeof(SAPbobsCOM.BoDataServerTypes), GeneralSettings.B1DbServerType);

                    exist = Enum.IsDefined(typeof(SAPbobsCOM.BoSuppLangs), GeneralSettings.B1Language);
                    if (exist)
                        GeneralSettings.oCompany.language = (SAPbobsCOM.BoSuppLangs)Enum.Parse(typeof(SAPbobsCOM.BoSuppLangs), GeneralSettings.B1Language);

                    GeneralSettings.oCompany.Server = GeneralSettings.B1Server;
                    GeneralSettings.oCompany.CompanyDB = GeneralSettings.B1CompanyDB;
                    if (!string.IsNullOrEmpty(GeneralSettings.B1License))
                        GeneralSettings.oCompany.LicenseServer = GeneralSettings.B1License;
                    if (!string.IsNullOrEmpty(GeneralSettings.B1Sld))
                        GeneralSettings.oCompany.SLDServer = GeneralSettings.B1Sld;
                    GeneralSettings.oCompany.DbUserName = GeneralSettings.B1DbUserName;
                    GeneralSettings.oCompany.DbPassword = GeneralSettings.B1DbPassword;
                    GeneralSettings.oCompany.UserName = GeneralSettings.B1UserName;
                    GeneralSettings.oCompany.Password = GeneralSettings.B1Password;
                    if (GeneralSettings.oCompany.Connect() != 0)
                    {
                        showMsg("Failed", GeneralSettings.oCompany.GetLastErrorDescription(), InformationType.Error);
                    }

                }

                return GeneralSettings.oCompany.Connected;
            }
            else
            {
                return false;
            }
        }

        public int PostPRtoSAP(AssetsManagementEF.Module.BusinessObjects.PurchaseRequests oPR)
        {
            // return 0 = post nothing
            // return -1 = posting error
            // return 1 = posting successful
            if (!GeneralSettings.B1Post)
                return 0;

            Guid g;
            // Create and display the value of two GUIDs.
            g = Guid.NewGuid();

            if (oPR.DetailAttachment != null && oPR.DetailAttachment.Count > 0)
            {
                foreach (AssetsManagementEF.Module.BusinessObjects.PurchaseRequestAttachments obj in oPR.DetailAttachment)
                {
                    string fullpath = GeneralSettings.B1AttachmentPath + g.ToString() + obj.AttachFile.FileName;
                    using (System.IO.FileStream fs = System.IO.File.OpenWrite(fullpath))
                    {
                        obj.AttachFile.SaveToStream(fs);
                    }
                }
            }
            if (oPR.WorkOrder != null && oPR.WorkOrder.Approved)
            {
                if (oPR.DocPassed && !oPR.DocPosted && !oPR.Cancelled)
                {
                    SAPbobsCOM.Documents oDoc = (SAPbobsCOM.Documents)GeneralSettings.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oDrafts);

                    try
                    {
                        bool found = false;
                        foreach (AssetsManagementEF.Module.BusinessObjects.PurchaseRequestDtls dtl in oPR.Detail)
                        {
                            if (dtl.QTY > 0)
                            {
                                found = true;
                            }
                        }
                        if (!found) return 0;

                        int sapempid = 0;
                        DateTime reqdate = DateTime.Today;
                        foreach (AssetsManagementEF.Module.BusinessObjects.PurchaseRequestDocStatuses dtl in oPR.DetailDocStatus)
                        {
                            if (dtl.DocStatus == BusinessObjects.DocumentStatus.DocPassed && !dtl.IsReverse)
                            {
                                sapempid = dtl.CreateUser.B1EmployeeID;
                                reqdate = (DateTime)dtl.CreateDate;
                            }
                        }


                        int series = GeneralSettings.B1PRseries;

                        oDoc.DocObjectCode = SAPbobsCOM.BoObjectTypes.oPurchaseRequest;
                        oDoc.UserFields.Fields.Item(GeneralSettings.B1PRCol).Value = "Y";
                        oDoc.UserFields.Fields.Item(GeneralSettings.B1PRNoCol).Value = oPR.DocNum;
                        oDoc.UserFields.Fields.Item(GeneralSettings.B1WONoCol).Value = oPR.WorkOrder.DocNum;
                        if (sapempid > 0)
                            oDoc.DocumentsOwner = sapempid;
                        oDoc.RequriedDate = reqdate.Date;
                        oDoc.DocDate = oPR.DocDate;

                        if (GeneralSettings.B1PRseries > 0)
                            oDoc.Series = GeneralSettings.B1PRseries;

                        string contractno = "";
                        if (oPR.ContractDoc != null)
                        {
                            oDoc.UserFields.Fields.Item(GeneralSettings.B1CTCol).Value = "Y";
                            contractno = oPR.ContractDoc.DocNum == null ? "" : oPR.ContractDoc.DocNum.Trim();
                        }
                        int cnt = 0;
                        foreach (AssetsManagementEF.Module.BusinessObjects.PurchaseRequestDtls dtl in oPR.Detail)
                        {
                            if (dtl.QTY > 0)
                            {
                                cnt++;
                                if (cnt == 1)
                                {
                                }
                                else
                                {
                                    oDoc.Lines.Add();
                                    oDoc.Lines.SetCurrentLine(oDoc.Lines.Count - 1);
                                }
                                oDoc.Lines.ItemCode = dtl.ItemMaster.BoCode;
                                oDoc.Lines.ItemDetails = dtl.ItemDesc;
                                //oDoc.Lines.ItemDescription = dtl.ItemDesc;
                                oDoc.Lines.Quantity = dtl.QTY;
                                oDoc.Lines.Price = (double)dtl.Price;
                                if (oPR.Contractor != null)
                                    oDoc.Lines.LineVendor = oPR.Contractor.BoCode;
                                oDoc.Lines.UserFields.Fields.Item(GeneralSettings.B1PRLineIDCol).Value = dtl.ID;
                                if (contractno.Trim() != "")
                                    oDoc.Lines.UserFields.Fields.Item(GeneralSettings.B1CTNoCol).Value = contractno;

                            }
                        }
                        if (oPR.DetailAttachment != null && oPR.DetailAttachment.Count > 0)
                        {
                            cnt = 0;
                            SAPbobsCOM.Attachments2 oAtt = (SAPbobsCOM.Attachments2)GeneralSettings.oCompany.GetBusinessObject(BoObjectTypes.oAttachments2);
                            foreach (AssetsManagementEF.Module.BusinessObjects.PurchaseRequestAttachments dtl in oPR.DetailAttachment)
                            {

                                cnt++;
                                if (cnt == 1)
                                {
                                    if (oAtt.Lines.Count == 0)
                                        oAtt.Lines.Add();
                                }
                                else
                                    oAtt.Lines.Add();

                                string attfile = "";
                                string[] fexe = dtl.AttachFile.FileName.Split('.');
                                if (fexe.Length <= 2)
                                    attfile = fexe[0];
                                else
                                {
                                    for (int x = 0; x < fexe.Length - 1; x++)
                                    {
                                        if (attfile == "")
                                            attfile = fexe[x];
                                        else
                                            attfile += "." + fexe[x];
                                    }
                                }
                                oAtt.Lines.FileName = g.ToString() + attfile;
                                if (fexe.Length > 1)
                                    oAtt.Lines.FileExtension = fexe[fexe.Length - 1];
                                string path = GeneralSettings.B1AttachmentPath;
                                path = path.Replace("\\\\", "\\");
                                path = path.Substring(0, path.Length - 1);
                                oAtt.Lines.SourcePath = path;
                                oAtt.Lines.Override = SAPbobsCOM.BoYesNoEnum.tYES;
                            }
                            int iAttEntry = -1;
                            if (oAtt.Add() == 0)
                            {
                                iAttEntry = int.Parse(GeneralSettings.oCompany.GetNewObjectKey());
                            }
                            else
                            {
                                string temp = GeneralSettings.oCompany.GetLastErrorDescription();
                                if (GeneralSettings.oCompany.InTransaction)
                                {
                                    GeneralSettings.oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                                }
                                showMsg("Failed", temp, InformationType.Error);
                                return -1;
                            }
                            oDoc.AttachmentEntry = iAttEntry;
                        }

                        int rc = oDoc.Add();
                        if (rc != 0)
                        {
                            string temp = GeneralSettings.oCompany.GetLastErrorDescription();
                            if (GeneralSettings.oCompany.InTransaction)
                            {
                                GeneralSettings.oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                            }
                            showMsg("Failed", temp, InformationType.Error);
                            return -1;
                        }
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        GeneralSettings.oCompany.EndTransaction(BoWfTransOpt.wf_RollBack);
                        showMsg("Failed", ex.Message, InformationType.Error);
                        return -1;
                    }
                    finally
                    {
                        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oDoc);
                        oDoc = null;
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }

                }
                else
                {

                }
            }
            else
            {

            }
            return 0;
        }

        public int SendEmail(string MailSubject, string MailBody, List<string> ToEmails)
        {
            // return 0 = sent nothing
            // return -1 = sent error
            // return 1 = sent successful
            if (!GeneralSettings.EmailSend) return 0;
            if (ToEmails.Count <= 0) return 0;

            MailMessage mailMsg = new MailMessage();

            mailMsg.From = new MailAddress(GeneralSettings.Email, GeneralSettings.EmailName);

            foreach (string ToEmail in ToEmails)
            {
                mailMsg.To.Add(ToEmail);
            }

            mailMsg.Subject = MailSubject;
            //mailMsg.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMsg.Body = MailBody;

            SmtpClient smtpClient = new SmtpClient
            {
                EnableSsl = GeneralSettings.EmailSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = GeneralSettings.EmailUseDefaultCredential,
                Host = GeneralSettings.EmailHost,
                Port = int.Parse(GeneralSettings.EmailPort),
            };

            if (!smtpClient.UseDefaultCredentials)
            {
                smtpClient.Credentials = new NetworkCredential(GeneralSettings.Email, GeneralSettings.EmailPassword);
            }

            smtpClient.Send(mailMsg);

            mailMsg.Dispose();
            smtpClient.Dispose();

            return 1;
        }
    }
}
