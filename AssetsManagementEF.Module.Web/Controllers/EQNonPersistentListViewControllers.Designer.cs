namespace AssetsManagementEF.Module.Web.Controllers
{
    partial class EQNonPersistentListViewControllers
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
            this.GenFullList = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.GenAMSMeasure = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.GetYearMonth = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.GetDateRange = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // GenFullList
            // 
            this.GenFullList.AcceptButtonCaption = null;
            this.GenFullList.CancelButtonCaption = null;
            this.GenFullList.Caption = "Gen Full List";
            this.GenFullList.ConfirmationMessage = null;
            this.GenFullList.Id = "GenFullList";
            this.GenFullList.ToolTip = null;
            this.GenFullList.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.GenFullList_CustomizePopupWindowParams);
            this.GenFullList.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.GenFullList_Execute);
            // 
            // GenAMSMeasure
            // 
            this.GenAMSMeasure.Caption = "Gen AMS Measure";
            this.GenAMSMeasure.ConfirmationMessage = null;
            this.GenAMSMeasure.Id = "GenAMSMeasure";
            this.GenAMSMeasure.ToolTip = null;
            this.GenAMSMeasure.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.GenAMSMeasure_Execute);
            // 
            // GetYearMonth
            // 
            this.GetYearMonth.AcceptButtonCaption = null;
            this.GetYearMonth.CancelButtonCaption = null;
            this.GetYearMonth.Caption = "Generate PM WO";
            this.GetYearMonth.ConfirmationMessage = null;
            this.GetYearMonth.Id = "GetYearMonth";
            this.GetYearMonth.ToolTip = null;
            this.GetYearMonth.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.GetYearMonth_CustomizePopupWindowParams);
            this.GetYearMonth.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.GetYearMonth_Execute);
            // 
            // GetDateRange
            // 
            this.GetDateRange.AcceptButtonCaption = null;
            this.GetDateRange.CancelButtonCaption = null;
            this.GetDateRange.Caption = "Date Range";
            this.GetDateRange.ConfirmationMessage = null;
            this.GetDateRange.Id = "GetDateRange";
            this.GetDateRange.ToolTip = null;
            this.GetDateRange.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.GetDateRange_CustomizePopupWindowParams);
            this.GetDateRange.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.GetDateRange_Execute);
            // 
            // EQNonPersistentListViewControllers
            // 
            this.Actions.Add(this.GenFullList);
            this.Actions.Add(this.GenAMSMeasure);
            this.Actions.Add(this.GetYearMonth);
            this.Actions.Add(this.GetDateRange);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction GenFullList;
        private DevExpress.ExpressApp.Actions.SimpleAction GenAMSMeasure;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction GetYearMonth;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction GetDateRange;
    }
}
