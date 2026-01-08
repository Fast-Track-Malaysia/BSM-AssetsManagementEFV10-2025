namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class EquipmentControllers
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
            this.CreateWorkRequest = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.CreatePMSchedule = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.EqCopySCE = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CreateWorkRequest
            // 
            this.CreateWorkRequest.AcceptButtonCaption = null;
            this.CreateWorkRequest.CancelButtonCaption = null;
            this.CreateWorkRequest.Caption = "Create Work Request";
            this.CreateWorkRequest.ConfirmationMessage = null;
            this.CreateWorkRequest.Id = "CreateWorkRequest";
            this.CreateWorkRequest.ToolTip = null;
            this.CreateWorkRequest.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.BooleanParametersCustomizePopupWindowParams);
            this.CreateWorkRequest.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CreateWorkRequest_Execute);
            // 
            // CreatePMSchedule
            // 
            this.CreatePMSchedule.AcceptButtonCaption = null;
            this.CreatePMSchedule.CancelButtonCaption = null;
            this.CreatePMSchedule.Caption = "Create PM Schedule";
            this.CreatePMSchedule.ConfirmationMessage = null;
            this.CreatePMSchedule.Id = "CreatePMSchedule";
            this.CreatePMSchedule.ToolTip = null;
            this.CreatePMSchedule.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.BooleanParametersCustomizePopupWindowParams);
            this.CreatePMSchedule.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CreatePMSchedule_Execute);
            // 
            // EqCopySCE
            // 
            this.EqCopySCE.AcceptButtonCaption = null;
            this.EqCopySCE.CancelButtonCaption = null;
            this.EqCopySCE.Caption = "SCE Category";
            this.EqCopySCE.ConfirmationMessage = null;
            this.EqCopySCE.Id = "EqCopySCE";
            this.EqCopySCE.ToolTip = null;
            this.EqCopySCE.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.EqCopySCE_CustomizePopupWindowParams);
            this.EqCopySCE.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.EqCopySCE_Execute);
            // 
            // EquipmentControllers
            // 
            this.Actions.Add(this.CreateWorkRequest);
            this.Actions.Add(this.CreatePMSchedule);
            this.Actions.Add(this.EqCopySCE);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CreateWorkRequest;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CreatePMSchedule;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction EqCopySCE;
    }
}
