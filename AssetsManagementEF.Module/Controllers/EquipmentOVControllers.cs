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

namespace AssetsManagementEF.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class EquipmentOVControllers : ObjectViewController<ObjectView, Equipments>
    {
        public EquipmentOVControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
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
            //ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged;
        }
        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            //if (View.CurrentObject == e.Object &&
            //       e.PropertyName == "EqGroup" &&
            //       ObjectSpace.IsModified
            //       //&& e.OldValue != e.NewValue) // Entity Framework e.OldValue & e.NewValue always null
            //       )
            //{
            //    Equipments changedObj = (Equipments)e.Object;
            //    if (changedObj.EqGroup != null && changedObj.EqClass != null)
            //    {
            //        if (changedObj.EqClass.EqGroup.ID != changedObj.EqGroup.ID)
            //            changedObj.EqClass = null;
            //    }
            //    else if (changedObj.EqGroup == null)
            //        changedObj.EqClass = null;
            //}
        }
    }
}
