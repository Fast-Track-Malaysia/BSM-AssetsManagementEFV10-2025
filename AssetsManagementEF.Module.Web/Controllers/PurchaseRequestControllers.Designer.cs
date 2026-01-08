namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class PurchaseRequestControllers
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
            this.PassPR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.CancelPR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.BackToWO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.RejectPR = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.GetContractItem = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.PostPR = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // PassPR
            // 
            this.PassPR.AcceptButtonCaption = null;
            this.PassPR.CancelButtonCaption = null;
            this.PassPR.Caption = "Pass PR";
            this.PassPR.ConfirmationMessage = null;
            this.PassPR.Id = "PassPR";
            this.PassPR.ToolTip = null;
            this.PassPR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.PassPR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PassPR_Execute);
            // 
            // CancelPR
            // 
            this.CancelPR.AcceptButtonCaption = null;
            this.CancelPR.CancelButtonCaption = null;
            this.CancelPR.Caption = "Cancel PR";
            this.CancelPR.ConfirmationMessage = null;
            this.CancelPR.Id = "CancelPR";
            this.CancelPR.ToolTip = null;
            this.CancelPR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.CancelPR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CancelPR_Execute);
            // 
            // BackToWO
            // 
            this.BackToWO.Caption = "Back To WO";
            this.BackToWO.ConfirmationMessage = null;
            this.BackToWO.Id = "BackToWO";
            this.BackToWO.ToolTip = null;
            this.BackToWO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.BackToWO_Execute);
            // 
            // RejectPR
            // 
            this.RejectPR.AcceptButtonCaption = null;
            this.RejectPR.CancelButtonCaption = null;
            this.RejectPR.Caption = "Revise PR";
            this.RejectPR.ConfirmationMessage = null;
            this.RejectPR.Id = "RejectPR";
            this.RejectPR.ToolTip = null;
            this.RejectPR.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.StringParametersCustomizePopupWindowParams);
            this.RejectPR.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RejectPR_Execute);
            // 
            // GetContractItem
            // 
            this.GetContractItem.AcceptButtonCaption = null;
            this.GetContractItem.CancelButtonCaption = null;
            this.GetContractItem.Caption = "Get Contract Item";
            this.GetContractItem.ConfirmationMessage = null;
            this.GetContractItem.Id = "GetContractItem";
            this.GetContractItem.ToolTip = null;
            this.GetContractItem.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.GetContractItem_CustomizePopupWindowParams);
            this.GetContractItem.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.GetContractItem_Execute);
            // 
            // PostPR
            // 
            this.PostPR.Caption = "Post PR";
            this.PostPR.ConfirmationMessage = null;
            this.PostPR.Id = "PostPR";
            this.PostPR.ToolTip = null;
            this.PostPR.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PostPR_Execute);
            // 
            // PurchaseRequestControllers
            // 
            this.Actions.Add(this.PassPR);
            this.Actions.Add(this.CancelPR);
            this.Actions.Add(this.BackToWO);
            this.Actions.Add(this.RejectPR);
            this.Actions.Add(this.GetContractItem);
            this.Actions.Add(this.PostPR);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PassPR;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CancelPR;
        private DevExpress.ExpressApp.Actions.SimpleAction BackToWO;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RejectPR;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction GetContractItem;
        private DevExpress.ExpressApp.Actions.SimpleAction PostPR;
    }
}
