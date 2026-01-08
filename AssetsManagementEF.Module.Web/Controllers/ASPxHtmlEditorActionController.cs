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
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.ExpressApp.HtmlPropertyEditor.Web;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ASPxHtmlEditorActionController : ViewController<DetailView>
    {
        ASPxHtmlPropertyEditor editor;
        public ASPxHtmlEditorActionController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            editor = View.FindItem("LongDescription") as ASPxHtmlPropertyEditor;
            if (editor != null)
            {
                if (editor.Control != null)
                {
                    CustomizeEditor(editor.Editor);
                }
                else
                {
                    editor.ControlCreated += new EventHandler<EventArgs>(editor_ControlCreated);
                }
            }

        }
        private static void CustomizeEditor(ASPxHtmlEditor htmlEditor)
        {
            //htmlEditor.Toolbars[0].Items.Add(new ToolbarPrintButton());
            //ToolbarExportDropDownButton export = new ToolbarExportDropDownButton();
            //export.CreateDefaultItems();
            //htmlEditor.Toolbars[0].Items.Add(export);
            if (htmlEditor != null)
            {
                htmlEditor.Settings.AllowHtmlView = false;
                htmlEditor.Toolbars[0].Items.FindByCommandName("insertimagedialog").Visible = false;
            }

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
            if (editor != null)
            {
                editor.ControlCreated -= new EventHandler<EventArgs>(editor_ControlCreated);
                editor = null;
            }

        }
        void editor_ControlCreated(object sender, EventArgs e)
        {
            ASPxHtmlPropertyEditor item = (ASPxHtmlPropertyEditor)sender;
            CustomizeEditor(item.Editor);
        }

    }
}
