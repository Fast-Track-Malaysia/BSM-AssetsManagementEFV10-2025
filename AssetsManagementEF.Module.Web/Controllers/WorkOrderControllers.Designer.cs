namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class WorkOrderControllers
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
            this.CreatePR = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.PrintCheckList = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CancelOrder = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.PassOrder = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.RejectOrder = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ApproveOrder = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ChangeJobStatus = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.TechnicalClosure = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.DuplicateWO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GoToPR = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.BackToPMPatch = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreatePR
            // 
            this.CreatePR.Caption = "Create PR";
            this.CreatePR.ConfirmationMessage = null;
            this.CreatePR.Id = "CreatePR";
            this.CreatePR.ToolTip = null;
            this.CreatePR.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreatePR_Execute);
            // 
            // PrintCheckList
            // 
            this.PrintCheckList.Caption = "Print Check List";
            this.PrintCheckList.ConfirmationMessage = null;
            this.PrintCheckList.Id = "PrintCheckList";
            this.PrintCheckList.ToolTip = null;
            this.PrintCheckList.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PrintCheckList_Execute);
            // 
            // CancelOrder
            // 
            this.CancelOrder.AcceptButtonCaption = null;
            this.CancelOrder.CancelButtonCaption = null;
            this.CancelOrder.Caption = "Cancel Order";
            this.CancelOrder.ConfirmationMessage = null;
            this.CancelOrder.Id = "CancelOrder";
            this.CancelOrder.ToolTip = null;
            this.CancelOrder.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.CancelOrder.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CancelOrder_Execute);
            // 
            // PassOrder
            // 
            this.PassOrder.AcceptButtonCaption = null;
            this.PassOrder.CancelButtonCaption = null;
            this.PassOrder.Caption = "Pass Order";
            this.PassOrder.ConfirmationMessage = null;
            this.PassOrder.Id = "PassOrder";
            this.PassOrder.ToolTip = null;
            this.PassOrder.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.PassOrder.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PassOrder_Execute);
            // 
            // RejectOrder
            // 
            this.RejectOrder.AcceptButtonCaption = null;
            this.RejectOrder.CancelButtonCaption = null;
            this.RejectOrder.Caption = "Reject Order";
            this.RejectOrder.ConfirmationMessage = null;
            this.RejectOrder.Id = "RejectOrder";
            this.RejectOrder.ToolTip = null;
            this.RejectOrder.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.RejectOrder.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RejectOrder_Execute);
            // 
            // ApproveOrder
            // 
            this.ApproveOrder.AcceptButtonCaption = null;
            this.ApproveOrder.CancelButtonCaption = null;
            this.ApproveOrder.Caption = "Approve Order";
            this.ApproveOrder.ConfirmationMessage = null;
            this.ApproveOrder.Id = "ApproveOrder";
            this.ApproveOrder.ToolTip = null;
            this.ApproveOrder.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.ApproveOrder.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ApproveOrder_Execute);
            // 
            // ChangeJobStatus
            // 
            this.ChangeJobStatus.AcceptButtonCaption = null;
            this.ChangeJobStatus.CancelButtonCaption = null;
            this.ChangeJobStatus.Caption = "Change Job Status";
            this.ChangeJobStatus.ConfirmationMessage = null;
            this.ChangeJobStatus.Id = "ChangeJobStatus";
            this.ChangeJobStatus.ToolTip = null;
            this.ChangeJobStatus.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ChangeJobStatus_CustomizePopupWindowParams);
            this.ChangeJobStatus.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ChangeJobStatus_Execute);
            // 
            // TechnicalClosure
            // 
            this.TechnicalClosure.AcceptButtonCaption = null;
            this.TechnicalClosure.CancelButtonCaption = null;
            this.TechnicalClosure.Caption = "Technical Closure";
            this.TechnicalClosure.ConfirmationMessage = null;
            this.TechnicalClosure.Id = "TechnicalClosure";
            this.TechnicalClosure.ToolTip = null;
            this.TechnicalClosure.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.TechnicalClosure.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.TechnicalClosure_Execute);
            // 
            // DuplicateWO
            // 
            this.DuplicateWO.Caption = "Duplicate WO";
            this.DuplicateWO.ConfirmationMessage = null;
            this.DuplicateWO.Id = "DuplicateWO";
            this.DuplicateWO.ToolTip = null;
            this.DuplicateWO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DuplicateWO_Execute);
            // 
            // GoToPR
            // 
            this.GoToPR.Caption = "Go To PR";
            this.GoToPR.ConfirmationMessage = null;
            this.GoToPR.Id = "GoToPR";
            this.GoToPR.ToolTip = null;
            this.GoToPR.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GoToPR_Execute);
            // 
            // BackToPMPatch
            // 
            this.BackToPMPatch.Caption = "Back To PM Patch";
            this.BackToPMPatch.ConfirmationMessage = null;
            this.BackToPMPatch.Id = "BackToPMPatch";
            this.BackToPMPatch.ToolTip = null;
            this.BackToPMPatch.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.BackToPMPatch_Execute);
            // 
            // WorkOrderControllers
            // 
            this.Actions.Add(this.CreatePR);
            this.Actions.Add(this.PrintCheckList);
            this.Actions.Add(this.CancelOrder);
            this.Actions.Add(this.PassOrder);
            this.Actions.Add(this.RejectOrder);
            this.Actions.Add(this.ApproveOrder);
            this.Actions.Add(this.ChangeJobStatus);
            this.Actions.Add(this.TechnicalClosure);
            this.Actions.Add(this.DuplicateWO);
            this.Actions.Add(this.GoToPR);
            this.Actions.Add(this.BackToPMPatch);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction CreatePR;
        private DevExpress.ExpressApp.Actions.SimpleAction PrintCheckList;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CancelOrder;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PassOrder;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RejectOrder;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ApproveOrder;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ChangeJobStatus;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction TechnicalClosure;
        private DevExpress.ExpressApp.Actions.SimpleAction DuplicateWO;
        private DevExpress.ExpressApp.Actions.SimpleAction GoToPR;
        private DevExpress.ExpressApp.Actions.SimpleAction BackToPMPatch;
    }
}
