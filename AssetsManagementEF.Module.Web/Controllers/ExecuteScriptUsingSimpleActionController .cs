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
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using DevExpress.ExpressApp.Web.Templates.ActionContainers.Menu;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class MyController : ProcessActionContainerHolderController
    {
        private const string ActionId = "PrintWO";
        public void ExecuteScriptUsingSimpleActionController()
        {
            SimpleAction action = new SimpleAction(this, ActionId, DevExpress.Persistent.Base.PredefinedCategory.Edit);
        }
        protected override bool NeedSubscribeToHolderMenuItemsCreated(DevExpress.ExpressApp.Web.Templates.ActionContainers.WebActionContainer webActionContainer)
        {
            return webActionContainer.ContainerId == "Edit";
        }
        protected override void OnActionContainerHolderActionItemCreated(DevExpress.ExpressApp.Web.Templates.ActionContainers.WebActionBaseItem item)
        {
            base.OnActionContainerHolderActionItemCreated(item);
            //if (item.Action.Id == ActionId)
            //{
            //    ClickableMenuActionItem clickableMenuActionItem = item as ClickableMenuActionItem;
            //    if (clickableMenuActionItem != null)
            //    {
            //        clickableMenuActionItem.ClientClickScript = "alert(0);";
            //        //By default, after executing a client-side script, the Action's Execute event handler is still executed on the server side. You can set the ProcessOnServer property to False to avoid this callback.
            //        clickableMenuActionItem.ProcessOnServer = false;
            //    }
            //    // Customize other Action's control settings here.
            //}


        }
    }
}
