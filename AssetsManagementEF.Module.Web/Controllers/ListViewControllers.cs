using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.Web;
using DevExpress.ExpressApp.Scheduler.Web;
using DevExpress.Persistent.Base.General;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ListViewControllers : ViewController<ListView>
    {
        public ListViewControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            var listEditor1 = View.Editor as ASPxGridListEditor;
            if (listEditor1 != null && listEditor1.Model.DataAccessMode == CollectionSourceDataAccessMode.Server)
            {
                foreach (var column in listEditor1.Grid.Columns)
                {
                    var commandColumn = column as GridViewCommandColumn;
                    if (commandColumn != null && commandColumn.ShowSelectCheckbox)
                    {
                        commandColumn.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;
                    }
                }
            }

            ListView view = (ListView)View;
            try
            {
                ASPxSchedulerListEditor SchedulerlistEditor = (ASPxSchedulerListEditor)view.Editor;
                ASPxScheduler scheduler = SchedulerlistEditor.SchedulerControl;
                scheduler.Views.DayView.VisibleTime =
                    new TimeOfDayInterval(new TimeSpan(8, 0, 0), new TimeSpan(17, 0, 0));
            }
            catch
            {
                ASPxGridView gridView = ((ASPxGridListEditor)View.Editor).Grid;
                gridView.Settings.ShowHeaderFilterButton = true;
                ASPxGridListEditor listEditor = ((ListView)View).Editor as ASPxGridListEditor;
                if (listEditor != null)
                {
                    foreach (GridViewDataColumn column in listEditor.Grid.DataColumns)
                    {
                        if (!(column is GridViewDataDateColumn || column is GridViewDataCheckColumn))
                        {
                            column.SettingsHeaderFilter.Mode = GridHeaderFilterMode.CheckedList;

                        }
                    }
                }

            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
