namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class WorkRequestController
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
            this.CreateWorkOrder = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CancelRequest = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.RejectRequest = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.PassRequest = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CreateWorkOrder
            // 
            this.CreateWorkOrder.Caption = "Create Work Order";
            this.CreateWorkOrder.ConfirmationMessage = null;
            this.CreateWorkOrder.Id = "CreateWorkOrder";
            this.CreateWorkOrder.ToolTip = null;
            this.CreateWorkOrder.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreateWorkOrder_Execute);
            // 
            // CancelRequest
            // 
            this.CancelRequest.AcceptButtonCaption = null;
            this.CancelRequest.CancelButtonCaption = "";
            this.CancelRequest.Caption = "Cancel Request";
            this.CancelRequest.ConfirmationMessage = null;
            this.CancelRequest.Id = "CancelRequest";
            this.CancelRequest.ToolTip = null;
            this.CancelRequest.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.CancelRequest.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CancelRequest_Execute);
            // 
            // RejectRequest
            // 
            this.RejectRequest.AcceptButtonCaption = null;
            this.RejectRequest.CancelButtonCaption = null;
            this.RejectRequest.Caption = "Reject Request";
            this.RejectRequest.ConfirmationMessage = null;
            this.RejectRequest.Id = "RejectRequest";
            this.RejectRequest.ToolTip = null;
            this.RejectRequest.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.RejectRequest.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RejectRequest_Execute);
            // 
            // PassRequest
            // 
            this.PassRequest.AcceptButtonCaption = null;
            this.PassRequest.CancelButtonCaption = null;
            this.PassRequest.Caption = "Pass Request";
            this.PassRequest.ConfirmationMessage = null;
            this.PassRequest.Id = "PassRequest";
            this.PassRequest.ToolTip = null;
            this.PassRequest.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.PassRequest.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PassRequest_Execute);
            // 
            // WorkRequestController
            // 
            this.Actions.Add(this.CreateWorkOrder);
            this.Actions.Add(this.CancelRequest);
            this.Actions.Add(this.RejectRequest);
            this.Actions.Add(this.PassRequest);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreateWorkOrder;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CancelRequest;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RejectRequest;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PassRequest;
    }
}
