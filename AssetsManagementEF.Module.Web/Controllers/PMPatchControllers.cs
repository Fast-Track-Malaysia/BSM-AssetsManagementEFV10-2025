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
    public partial class PMPatchControllers : ViewController
    {
        GenControllers genCon;
        public PMPatchControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(PMPatches);
            TargetViewType = ViewType.DetailView;
            TargetViewNesting = Nesting.Root;

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
            genCon = Frame.GetController<GenControllers>();
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void PMPatchGotoWO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (((DetailView)View).ViewEditMode != ViewEditMode.View)
            {
                genCon.showMsg("Failed", "Editing Work Order cannot proceed.", InformationType.Error);
                return;
            }
            PMPatches selectedObject = (PMPatches)e.CurrentObject;

            ListPropertyEditor listviewDetail = null;
            if (selectedObject.DetailWorkOrder.Count > 0)
            {
                foreach (ViewItem item in ((DetailView)View).Items)
                {
                    if ((item is ListPropertyEditor) && (item.Id == "DetailWorkOrder"))
                        listviewDetail = item as ListPropertyEditor;
                }
                if (listviewDetail != null && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count == 1)
                {
                    int id = 0;
                    foreach (WorkOrders selectedObjectdtl in listviewDetail.ListView.SelectedObjects)
                    {
                        id = selectedObjectdtl.ID;
                    }
                    IObjectSpace os = Application.CreateObjectSpace();
                    WorkOrders obj = os.FindObject<WorkOrders>(CriteriaOperator.Parse("ID=?", id));

                    genCon.openNewView(os, obj, ViewEditMode.View);
                    genCon.showMsg("Successful", "Back to Work Order.", InformationType.Success);

                }
                else
                {
                    genCon.showMsg("Failed", "Please select ONE Work Order.", InformationType.Error);
                    return;

                }
            }

        }

    }
}
