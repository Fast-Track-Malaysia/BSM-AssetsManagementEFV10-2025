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
using DevExpress.ExpressApp.Web.SystemModule;
using AssetsManagementEF.Module.BusinessObjects;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ListViewEditActionControllers : ListViewController
    {
        bool ok;
        SimpleActionExecuteEventArgs defaultArgs;
        protected override void ExecuteEdit(SimpleActionExecuteEventArgs args)
        {
            if (View.Id == "Contact_ListView" && !ok)
            {
                defaultArgs = args;
                args.ShowViewParameters.CreatedView = Application.CreateDetailView(
                                                      View.ObjectSpace,
                                                      "Contact_DetailView", false,
                                                      View.CurrentObject
                );
                args.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                args.ShowViewParameters.Context = TemplateContext.View;
                args.ShowViewParameters.CreateAllControllers = true;
                ((DetailView)args.ShowViewParameters.CreatedView).ViewEditMode = ViewEditMode.Edit;
                DialogController dc = new DialogController();
                dc.SaveOnAccept = false;
                args.ShowViewParameters.Controllers.Add(dc);
                if (dc != null)
                {
                    dc.Accepting += delegate (object s, DialogControllerAcceptingEventArgs e)
                    {
                        ok = true;
                        ExecuteEdit(defaultArgs);
                        ((WindowController)(s)).Window.View.ObjectSpace.CommitChanges();
                    };
                }
                return;

            }
            if (View.Id == "WorkOrders_DetailRequest_ListView")
            {
                IObjectSpace os = Application.CreateObjectSpace();
                PurchaseRequests target = os.GetObjectByKey<PurchaseRequests>(((PurchaseRequests)View.CurrentObject).ID);

                ShowViewParameters svp = new ShowViewParameters();
                DetailView dv = Application.CreateDetailView(os, target);
                dv.ViewEditMode = ViewEditMode.Edit;
                svp.CreatedView = dv;

                Application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
                return;
            }

            base.ExecuteEdit(args);
            ok = false;
        }
    }
}
