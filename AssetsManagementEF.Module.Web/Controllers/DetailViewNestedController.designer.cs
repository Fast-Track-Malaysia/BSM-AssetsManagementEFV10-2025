namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class DetailViewNestedController
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
            this.DeviationCloseold = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.DeviationCancelold = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // DeviationCloseold
            // 
            this.DeviationCloseold.Caption = "Close";
            this.DeviationCloseold.Category = "ListView";
            this.DeviationCloseold.ConfirmationMessage = null;
            this.DeviationCloseold.Id = "DeviationCloseold";
            this.DeviationCloseold.ToolTip = null;
            this.DeviationCloseold.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationClose_Execute);
            // 
            // DeviationCancelold
            // 
            this.DeviationCancelold.Caption = "Cancel";
            this.DeviationCancelold.Category = "ListView";
            this.DeviationCancelold.ConfirmationMessage = null;
            this.DeviationCancelold.Id = "DeviationCancelold";
            this.DeviationCancelold.ToolTip = null;
            this.DeviationCancelold.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.DeviationCancel_Execute);
            // 
            // DetailViewNestedController
            // 
            this.Actions.Add(this.DeviationCloseold);
            this.Actions.Add(this.DeviationCancelold);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction DeviationCloseold;
        private DevExpress.ExpressApp.Actions.SimpleAction DeviationCancelold;
    }
}
