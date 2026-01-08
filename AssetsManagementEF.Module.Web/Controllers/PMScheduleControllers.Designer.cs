namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class PMScheduleControllers
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
            this.CreatePMWO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreatePMWO
            // 
            this.CreatePMWO.Caption = "Create PM WO";
            this.CreatePMWO.ConfirmationMessage = null;
            this.CreatePMWO.Id = "CreatePMWO";
            this.CreatePMWO.ToolTip = null;
            this.CreatePMWO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreatePMWO_Execute);
            // 
            // PMScheduleControllers
            // 
            this.Actions.Add(this.CreatePMWO);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SimpleAction CreatePMWO;
    }
}
