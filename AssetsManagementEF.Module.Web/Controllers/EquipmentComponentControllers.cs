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
    public partial class EquipmentComponentControllers : ViewController
    {
        GenControllers genCon;
        public EquipmentComponentControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(EquipmentComponents);
            TargetViewNesting = Nesting.Root;
            //TargetViewType = ViewType.ListView;
            //TargetViewId = "EquipmentComponents_ListView_Copy";
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.CreateComWorkRequest.Active.SetItemValue("Enabled", false);
            this.EqComCopySCE.Active.SetItemValue("Enabled", false);
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            IObjectSpace ios = Application.CreateObjectSpace();
            Positions position = ios.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));

            if (View.GetType() == typeof(ListView))
            {
                if (user.Roles.Where(p => p.Name == GeneralSettings.RequestorRole).Count() > 0)
                {
                    //if (position.IsCorrectiveMaintenance && View is DetailView)
                    //    this.CreateComWorkRequest.Active.SetItemValue("Enabled", true);
                }
            }
            if (View.GetType() == typeof(DetailView))
            {
                this.EqComCopySCE.Active.SetItemValue("Enabled", true);
                this.EqComCopySCE.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                
                ((DetailView)View).ViewEditModeChanged += EquipmentComponentControllers_ViewEditModeChanged;

            }

        }

        private void EquipmentComponentControllers_ViewEditModeChanged(object sender, EventArgs e)
        {
            if (View.GetType() == typeof(DetailView))
            {
                this.EqComCopySCE.Enabled.SetItemValue("EditMode", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            genCon = Frame.GetController<GenControllers>();
        }
        protected override void OnDeactivated()
        {
            if (View.GetType() == typeof(DetailView))
            {
                ((DetailView)View).ViewEditModeChanged -= EquipmentComponentControllers_ViewEditModeChanged;
            }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        private void BooleanParametersCustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool err = false;
            bool witheq = true;
            bool disable = true;
            string msg = "Press OK to continue.";

            if (this.View is ListView)
            {

                if (e.Action.Id == this.CreateComWorkRequest.Id)
                {
                    if (((ListView)View).SelectedObjects.Count == 0)
                    {
                        genCon.showMsg("Failed", "Please select Component.", InformationType.Error);
                        return;
                    }
                    foreach (EquipmentComponents dtl in ((ListView)View).SelectedObjects)
                    {
                        if (dtl.Equipment == null)
                            err = true;
                        if (!err)
                            if (!dtl.IsActive || !dtl.Equipment.IsActive)
                                err = true;

                        if (err)
                        {
                            genCon.showMsg("Failed", "Please select Valid Component.", InformationType.Error);
                            return;
                        }
                    }
                    if (!err)
                    {
                        witheq = false;
                        disable = false;
                        msg = "Please tick the Order with Equipment? box if you want to combine Equipment with Component in one order.";
                    }
                }
            }
            else
            {
            }
            IObjectSpace os = Application.CreateObjectSpace();
            DetailView dv = Application.CreateDetailView(os, new BooleanParameters());
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            ((BooleanParameters)dv.CurrentObject).ParamBoolean = witheq;
            ((BooleanParameters)dv.CurrentObject).IsErr = err;
            ((BooleanParameters)dv.CurrentObject).ActionMessage = msg;
            ((BooleanParameters)dv.CurrentObject).IsDisable = disable;


            e.View = dv;

        }

        private void ComToEq_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is ListView)
            {
                if (((ListView)View).SelectedObjects.Count != 1)
                {
                    genCon.showMsg("Failed", "Please select one Component.", InformationType.Error);
                    return;
                }
                int id = 0;
                foreach (EquipmentComponents dtl in ((ListView)View).SelectedObjects)
                {
                    if (dtl.Equipment == null)
                    {
                        genCon.showMsg("Failed", "Please select Valid Component.", InformationType.Error);
                        return;
                    }
                    id = dtl.Equipment.ID;
                }
                IObjectSpace os = Application.CreateObjectSpace();
                Equipments obj = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", id));
                genCon.openNewView(os, obj, ViewEditMode.View);
            }
            else if (View is DetailView)
            {
                EquipmentComponents selectedobj = (EquipmentComponents)View.CurrentObject;
                if (selectedobj.Equipment == null)
                {
                    genCon.showMsg("Failed", "Please select Valid Component.", InformationType.Error);
                    return;
                }
                IObjectSpace os = Application.CreateObjectSpace();
                Equipments obj = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", selectedobj.Equipment.ID));
                genCon.openNewView(os, obj, ViewEditMode.View);

            }
        }

        private void EqComCopySCE_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            EquipmentComponents masterobject = (EquipmentComponents)View.CurrentObject;

            int id = GeneralSettings.sceid; //masterobject.Criticality.ID;

            IObjectSpace newObjectSpace = Application.CreateObjectSpace(typeof(SCESubCategories));
            string listViewId = Application.FindLookupListViewId(typeof(SCESubCategories));
            CollectionSourceBase collectionSource = Application.CreateCollectionSource(newObjectSpace, typeof(SCESubCategories), listViewId);
            collectionSource.Criteria["Filter01"] = CriteriaOperator.Parse("SCECategory.Criticality.ID = ?", id);

            e.View = Application.CreateListView(listViewId, collectionSource, true);
        }

        private void EqComCopySCE_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            try
            {
                EquipmentComponents masterobject = (EquipmentComponents)View.CurrentObject;

                if (((ListView)e.PopupWindow.View).SelectedObjects.Count != 1)
                {
                    genCon.showMsg("Cannot proceed", "Please select only 1 item.", InformationType.Error);
                    return;
                }
                int catid = 0;
                int id = 0;
                foreach (var dtl in ((ListView)e.PopupWindow.View).SelectedObjects)
                {
                    id = ((SCESubCategories)dtl).ID;
                    catid = ((SCESubCategories)dtl).SCECategory.ID;
                }
                masterobject.Criticality = View.ObjectSpace.GetObjectByKey<Criticalities>(GeneralSettings.sceid);
                masterobject.SCECategory = View.ObjectSpace.GetObjectByKey<SCECategories>(catid);
                masterobject.SCESubCategory = View.ObjectSpace.GetObjectByKey<SCESubCategories>(id);

            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);
                throw new Exception(ex.Message);
            }

        }
    }
}
