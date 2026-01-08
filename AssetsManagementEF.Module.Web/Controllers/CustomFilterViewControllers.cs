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
using AssetsManagementEF.Module.BusinessObjects;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CustomFilterViewControllers : ViewController<ListView>
    {
        public CustomFilterViewControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(PurchaseQuotations);
            PopupWindowShowAction action = new PopupWindowShowAction(this, "DateRangeFilter", DevExpress.Persistent.Base.PredefinedCategory.Filters);
            action.CustomizePopupWindowParams += new CustomizePopupWindowParamsEventHandler(action_CustomizePopupWindowParams);
            action.Execute += new PopupWindowShowActionExecuteEventHandler(action_Execute);
        }
        void action_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new DateRangeFilterParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            e.View = dv;
        }
        void action_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DateRangeFilterParameters p = (DateRangeFilterParameters)e.PopupWindow.View.CurrentObject;
            CriteriaOperator op = null;
            if (p.From == null)
            {
                if (p.To != null)
                {
                    op = CriteriaOperator.Parse("DocDate<?", p.To);
                }
            }
            else if (p.To == null)
            {
                op = CriteriaOperator.Parse("DocDate>=?", p.From);
            }
            else
            {
                op = CriteriaOperator.Parse("DocDate>=? and DocDate<?", p.From, p.To);
            }
            if (Equals(op, null))
            {
                View.CollectionSource.Criteria.Remove("DateRange");
            }
            else
            {
                View.CollectionSource.Criteria["DateRange"] = op;
            }
        }

        //protected override void OnActivated()
        //{
        //    base.OnActivated();
        //    // Perform various tasks depending on the target View.
        //}
        //protected override void OnViewControlsCreated()
        //{
        //    base.OnViewControlsCreated();
        //    // Access and customize the target View control.
        //}
        //protected override void OnDeactivated()
        //{
        //    // Unsubscribe from previously subscribed events and release other references and resources.
        //    base.OnDeactivated();
        //}
    }

}
