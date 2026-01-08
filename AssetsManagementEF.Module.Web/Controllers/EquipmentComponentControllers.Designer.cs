namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class EquipmentComponentControllers
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
            this.CreateComWorkRequest = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.EqComCopySCE = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CreateComWorkRequest
            // 
            this.CreateComWorkRequest.AcceptButtonCaption = null;
            this.CreateComWorkRequest.CancelButtonCaption = null;
            this.CreateComWorkRequest.Caption = "Create Work Request";
            this.CreateComWorkRequest.ConfirmationMessage = null;
            this.CreateComWorkRequest.Id = "CreateComWorkRequest";
            this.CreateComWorkRequest.ToolTip = null;
            this.CreateComWorkRequest.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.BooleanParametersCustomizePopupWindowParams);
            // 
            // EqComCopySCE
            // 
            this.EqComCopySCE.AcceptButtonCaption = null;
            this.EqComCopySCE.CancelButtonCaption = null;
            this.EqComCopySCE.Caption = "SCE Category";
            this.EqComCopySCE.ConfirmationMessage = null;
            this.EqComCopySCE.Id = "EqComCopySCE";
            this.EqComCopySCE.ToolTip = null;
            this.EqComCopySCE.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.EqComCopySCE_CustomizePopupWindowParams);
            this.EqComCopySCE.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.EqComCopySCE_Execute);
            // 
            // EquipmentComponentControllers
            // 
            this.Actions.Add(this.CreateComWorkRequest);
            this.Actions.Add(this.EqComCopySCE);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CreateComWorkRequest;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction EqComCopySCE;
    }
}
