namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class DeviationMitigationController
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
            this.DeviationMitigationClose = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationMitigationCancel = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationMitigationReopen = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // DeviationMitigationClose
            // 
            this.DeviationMitigationClose.Caption = "Close";
            this.DeviationMitigationClose.ConfirmationMessage = null;
            this.DeviationMitigationClose.Id = "DeviationMitigationClose";
            this.DeviationMitigationClose.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.DeviationMitigationClose.ToolTip = null;
            this.DeviationMitigationClose.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationMitigationClose_Execute);
            // 
            // DeviationMitigationCancel
            // 
            this.DeviationMitigationCancel.Caption = "Cancel";
            this.DeviationMitigationCancel.Id = "DeviationMitigationCancel";
            this.DeviationMitigationCancel.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.DeviationMitigationCancel.ToolTip = null;
            this.DeviationMitigationCancel.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationMitigationCancel_Execute);
            // 
            // DeviationMitigationReopen
            // 
            this.DeviationMitigationReopen.Caption = "Reopen";
            this.DeviationMitigationReopen.ConfirmationMessage = null;
            this.DeviationMitigationReopen.Id = "DeviationMitigationReopen";
            this.DeviationMitigationReopen.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.DeviationMitigationReopen.ToolTip = null;
            this.DeviationMitigationReopen.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationMitigationReopen_Execute);
            // 
            // DeviationMitigationController
            // 
            this.Actions.Add(this.DeviationMitigationClose);
            this.Actions.Add(this.DeviationMitigationCancel);
            this.Actions.Add(this.DeviationMitigationReopen);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction DeviationMitigationClose;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationMitigationCancel;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationMitigationReopen;
    }
}
