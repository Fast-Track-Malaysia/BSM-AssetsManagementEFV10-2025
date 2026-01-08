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
    public partial class ListViewNestedControllers : ViewController<ListView>
    {
        public ListViewNestedControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetViewType = ViewType.ListView;
            TargetViewNesting = Nesting.Nested;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View.ObjectTypeInfo.Type == typeof(DevExpress.Persistent.BaseImpl.EF.PermissionPolicy.PermissionPolicyRole))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", true);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", true);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
            }
            if (View.ObjectTypeInfo.Type == typeof(PlannerGroups))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", true);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", true);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
            }
            if (View.ObjectTypeInfo.Type == typeof(PMClasses))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", true);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", true);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
            }
            if (View.ObjectTypeInfo.Type == typeof(EquipmentComponents))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(EquipmentProperties))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(EquipmentPhotos))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(EquipmentAttachments))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(WorkRequestPhotos))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(WorkRequestAttachments))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }

            if (View.ObjectTypeInfo.Type == typeof(WorkOrderAttachments))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(WorkOrderPhotos))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(WorkOrderManHours))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }
            if (View.ObjectTypeInfo.Type == typeof(PurchaseRequestDtls))
            {
                PropertyCollectionSource collectionSource = ((ListView)View).CollectionSource as PropertyCollectionSource;
                PurchaseRequests masterObject = (PurchaseRequests)collectionSource.MasterObject;

                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                if (masterObject.DocPassed || masterObject.Cancelled || masterObject.DocPosted)
                {
                    Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
                    Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
                }
                else
                {
                    Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                    Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
                }
            }
            if (View.ObjectTypeInfo.Type == typeof(PurchaseRequestAttachments))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", false);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", false);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", true);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", true);
            }

            if (View.ObjectTypeInfo.Type == typeof(Positions))
            {
                Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", true);
                Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", true);
                Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
                Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
            }

            //if (View.ObjectTypeInfo.Type == typeof(PMSchedules))
            //{
            //    Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", true);
            //    Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", true);
            //    Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
            //    Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
            //}
            //if (View.ObjectTypeInfo.Type == typeof(Equipments))
            //{
            //    Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("", true);
            //    Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("", true);
            //    Frame.GetController<DeleteObjectsViewController>().DeleteAction.Active.SetItemValue("", false);
            //    Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("", false);
            //}
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
