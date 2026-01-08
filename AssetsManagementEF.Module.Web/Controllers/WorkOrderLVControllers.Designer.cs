namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class WorkOrderLVControllers
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
            this.AwaitingPlanning = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.CancelWO = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // AwaitingPlanning
            // 
            this.AwaitingPlanning.AcceptButtonCaption = null;
            this.AwaitingPlanning.CancelButtonCaption = null;
            this.AwaitingPlanning.Caption = "Awaiting Planning";
            this.AwaitingPlanning.ConfirmationMessage = null;
            this.AwaitingPlanning.Id = "AwaitingPlanning";
            this.AwaitingPlanning.ToolTip = null;
            this.AwaitingPlanning.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.AwaitingPlanning_CustomizePopupWindowParams);
            this.AwaitingPlanning.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.AwaitingPlanning_Execute);
            // 
            // CancelWO
            // 
            this.CancelWO.AcceptButtonCaption = null;
            this.CancelWO.CancelButtonCaption = null;
            this.CancelWO.Caption = "Cancel WO";
            this.CancelWO.ConfirmationMessage = null;
            this.CancelWO.Id = "CancelWO";
            this.CancelWO.ToolTip = null;
            this.CancelWO.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CancelWO_CustomizePopupWindowParams);
            this.CancelWO.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CancelWO_Execute);
            // 
            // WorkOrderLVControllers
            // 
            this.Actions.Add(this.AwaitingPlanning);
            this.Actions.Add(this.CancelWO);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction AwaitingPlanning;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CancelWO;
    }
}
