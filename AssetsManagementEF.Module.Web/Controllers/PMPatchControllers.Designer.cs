namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class PMPatchControllers
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
            this.PMPatchGotoWO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // PMPatchGotoWO
            // 
            this.PMPatchGotoWO.Caption = "Goto WO";
            this.PMPatchGotoWO.ConfirmationMessage = null;
            this.PMPatchGotoWO.Id = "PMPatchGotoWO";
            this.PMPatchGotoWO.ToolTip = null;
            this.PMPatchGotoWO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.PMPatchGotoWO_Execute);
            // 
            // PMPatchControllers
            // 
            this.Actions.Add(this.PMPatchGotoWO);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction PMPatchGotoWO;
    }
}
