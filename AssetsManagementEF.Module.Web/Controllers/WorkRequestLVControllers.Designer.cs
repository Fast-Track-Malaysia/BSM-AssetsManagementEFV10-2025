namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class WorkRequestLVControllers
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
            this.CancelWR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CancelWR
            // 
            this.CancelWR.AcceptButtonCaption = null;
            this.CancelWR.CancelButtonCaption = null;
            this.CancelWR.Caption = "Cancel WR";
            this.CancelWR.ConfirmationMessage = null;
            this.CancelWR.Id = "CancelWR";
            this.CancelWR.ToolTip = null;
            this.CancelWR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CancelWR_CustomizePopupWindowParams);
            this.CancelWR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CancelWR_Execute);
            // 
            // WorkRequestLVControllers
            // 
            this.Actions.Add(this.CancelWR);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CancelWR;
    }
}
