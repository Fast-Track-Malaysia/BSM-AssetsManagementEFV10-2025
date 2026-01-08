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
    public partial class FilterContractDocDtlContrallers : ObjectViewController<ListView, ContractDocDtls>
    {
        public FilterContractDocDtlContrallers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View.IsRoot)
            {
                if (GeneralSettings.contractdocid > 0)
                {
                    View.CollectionSource.Criteria["Filter1"] = new BinaryOperator("ContractDoc.ID", GeneralSettings.contractdocid, BinaryOperatorType.Equal);
                    View.CollectionSource.Criteria["Filter2"] = new BinaryOperator("ContractDoc.IsActive", true, BinaryOperatorType.Equal);
                }
                else
                    View.CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
            }
            GeneralSettings.contractdocid = 0;

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
