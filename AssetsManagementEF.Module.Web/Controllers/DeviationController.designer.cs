namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class DeviationController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DeviationApproval = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationNew = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationEdit = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationWithdraw = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationApprovedExtend = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationClose = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationCancel = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationSubmitAck = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationUnderReview = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationDuplicate = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationToWO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationDraftExtend = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DeviationSubmitApp = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // DeviationApproval
            // 
            this.DeviationApproval.AcceptButtonCaption = null;
            this.DeviationApproval.CancelButtonCaption = null;
            this.DeviationApproval.Caption = "Approval";
            this.DeviationApproval.ConfirmationMessage = null;
            this.DeviationApproval.Id = "DeviationApproval";
            this.DeviationApproval.ToolTip = null;
            this.DeviationApproval.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.DeviationApproval_CustomizePopupWindowParams);
            this.DeviationApproval.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationApproval_Execute);
            // 
            // DeviationNew
            // 
            this.DeviationNew.Caption = "New Deviation";
            this.DeviationNew.ConfirmationMessage = null;
            this.DeviationNew.Id = "DeviationNew";
            this.DeviationNew.ToolTip = null;
            this.DeviationNew.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationNew_Execute);
            // 
            // DeviationEdit
            // 
            this.DeviationEdit.Caption = "Edit Deviation";
            this.DeviationEdit.ConfirmationMessage = null;
            this.DeviationEdit.Id = "Deviation Edit";
            this.DeviationEdit.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.DeviationEdit.ToolTip = null;
            this.DeviationEdit.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationEdit_Execute);
            // 
            // DeviationWithdraw
            // 
            this.DeviationWithdraw.AcceptButtonCaption = null;
            this.DeviationWithdraw.CancelButtonCaption = null;
            this.DeviationWithdraw.Caption = "Withdraw";
            this.DeviationWithdraw.ConfirmationMessage = null;
            this.DeviationWithdraw.Id = "DeviationWithdraw";
            this.DeviationWithdraw.ToolTip = null;
            this.DeviationWithdraw.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.DeviationSubmitAck_CustomizePopupWindowParams);
            this.DeviationWithdraw.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationSubmitAck_Execute);
            // 
            // DeviationApprovedExtend
            // 
            this.DeviationApprovedExtend.AcceptButtonCaption = null;
            this.DeviationApprovedExtend.CancelButtonCaption = null;
            this.DeviationApprovedExtend.Caption = "Approved Extend";
            this.DeviationApprovedExtend.ConfirmationMessage = null;
            this.DeviationApprovedExtend.Id = "DeviationApprovedExtend";
            this.DeviationApprovedExtend.ToolTip = null;
            this.DeviationApprovedExtend.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParm_CustomizePopupWindowParams);
            this.DeviationApprovedExtend.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationApprovedExtend_Execute);
            // 
            // DeviationClose
            // 
            this.DeviationClose.AcceptButtonCaption = null;
            this.DeviationClose.CancelButtonCaption = null;
            this.DeviationClose.Caption = "Close";
            this.DeviationClose.ConfirmationMessage = null;
            this.DeviationClose.Id = "DeviationClose";
            this.DeviationClose.ToolTip = null;
            this.DeviationClose.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParm_CustomizePopupWindowParams);
            this.DeviationClose.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationClose_Execute);
            // 
            // DeviationCancel
            // 
            this.DeviationCancel.AcceptButtonCaption = null;
            this.DeviationCancel.CancelButtonCaption = null;
            this.DeviationCancel.Caption = "Cancel";
            this.DeviationCancel.ConfirmationMessage = null;
            this.DeviationCancel.Id = "DeviationCancel";
            this.DeviationCancel.ToolTip = null;
            this.DeviationCancel.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParm_CustomizePopupWindowParams);
            this.DeviationCancel.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationCancel_Execute);
            // 
            // DeviationSubmitAck
            // 
            this.DeviationSubmitAck.AcceptButtonCaption = null;
            this.DeviationSubmitAck.CancelButtonCaption = null;
            this.DeviationSubmitAck.Caption = "Acknowledge";
            this.DeviationSubmitAck.ConfirmationMessage = null;
            this.DeviationSubmitAck.Id = "DeviationSubmitAck";
            this.DeviationSubmitAck.ToolTip = null;
            this.DeviationSubmitAck.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.DeviationSubmitAck_CustomizePopupWindowParams);
            this.DeviationSubmitAck.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationSubmitAck_Execute);
            // 
            // DeviationUnderReview
            // 
            this.DeviationUnderReview.AcceptButtonCaption = null;
            this.DeviationUnderReview.CancelButtonCaption = null;
            this.DeviationUnderReview.Caption = "Pass for Review";
            this.DeviationUnderReview.ConfirmationMessage = null;
            this.DeviationUnderReview.Id = "DeviationUnderReview";
            this.DeviationUnderReview.ToolTip = null;
            this.DeviationUnderReview.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParm_CustomizePopupWindowParams);
            this.DeviationUnderReview.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationUnderReview_Execute);
            // 
            // DeviationDuplicate
            // 
            this.DeviationDuplicate.Caption = "Duplicate";
            this.DeviationDuplicate.ConfirmationMessage = null;
            this.DeviationDuplicate.Id = "DeviationDuplicate";
            this.DeviationDuplicate.ToolTip = null;
            this.DeviationDuplicate.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationDuplicate_Execute);
            // 
            // DeviationToWO
            // 
            this.DeviationToWO.Caption = "Goto WO";
            this.DeviationToWO.ConfirmationMessage = null;
            this.DeviationToWO.Id = "DeviationToWO";
            this.DeviationToWO.ToolTip = null;
            this.DeviationToWO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationToWO_Execute);
            // 
            // DeviationDraftExtend
            // 
            this.DeviationDraftExtend.AcceptButtonCaption = null;
            this.DeviationDraftExtend.CancelButtonCaption = null;
            this.DeviationDraftExtend.Caption = "Draft Extend";
            this.DeviationDraftExtend.ConfirmationMessage = null;
            this.DeviationDraftExtend.Id = "DeviationDraftExtend";
            this.DeviationDraftExtend.ToolTip = null;
            this.DeviationDraftExtend.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParm_CustomizePopupWindowParams);
            this.DeviationDraftExtend.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationDraftExtend_Execute);
            // 
            // DeviationSubmitApp
            // 
            this.DeviationSubmitApp.AcceptButtonCaption = null;
            this.DeviationSubmitApp.CancelButtonCaption = null;
            this.DeviationSubmitApp.Caption = "Submit for Approval";
            this.DeviationSubmitApp.ConfirmationMessage = null;
            this.DeviationSubmitApp.Id = "DeviationSubmitApp";
            this.DeviationSubmitApp.ToolTip = null;
            this.DeviationSubmitApp.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParm_CustomizePopupWindowParams);
            this.DeviationSubmitApp.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DeviationSubmitApp_Execute);
            // 
            // DeviationController
            // 
            this.Actions.Add(this.DeviationApproval);
            this.Actions.Add(this.DeviationNew);
            this.Actions.Add(this.DeviationEdit);
            this.Actions.Add(this.DeviationWithdraw);
            this.Actions.Add(this.DeviationApprovedExtend);
            this.Actions.Add(this.DeviationClose);
            this.Actions.Add(this.DeviationCancel);
            this.Actions.Add(this.DeviationSubmitAck);
            this.Actions.Add(this.DeviationUnderReview);
            this.Actions.Add(this.DeviationDuplicate);
            this.Actions.Add(this.DeviationToWO);
            this.Actions.Add(this.DeviationDraftExtend);
            this.Actions.Add(this.DeviationSubmitApp);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationApproval;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationNew;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationEdit;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationWithdraw;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationApprovedExtend;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationClose;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationCancel;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationSubmitAck;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationUnderReview;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationDuplicate;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationToWO;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationDraftExtend;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DeviationSubmitApp;
    }
}
