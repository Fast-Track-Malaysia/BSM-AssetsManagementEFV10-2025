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
    public partial class PMScheduleControllers : ViewController
    {
        GenControllers genCon;
        public PMScheduleControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            TargetObjectType = typeof(PMSchedules);
            TargetViewType = ViewType.DetailView;
            TargetViewNesting = Nesting.Root;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.CreatePMWO.Active.SetItemValue("Enabled", false);

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            IObjectSpace ios = Application.CreateObjectSpace();
            Positions position = ios.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
            if (user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0)
            {
                if (position.IsPreventiveMaintenance)
                    this.CreatePMWO.Active.SetItemValue("Enabled", true);
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
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void CreatePMWO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    genCon.showMsg("Failed", "Editing PM Schedule cannot create Work Order.", InformationType.Error);
                    return;
                }
                PMSchedules selectedObject = (PMSchedules)e.CurrentObject;
                if (selectedObject.IsNew)
                {
                    genCon.showMsg("Failed", "New PM Schedule cannot proceed.", InformationType.Error);
                    return;
                }
                if (!selectedObject.IsApproved)
                {
                    genCon.showMsg("Failed", "Not Approved PM Schedule cannot proceed.", InformationType.Error);
                    return;
                }
                if (!selectedObject.IsActive)
                {
                    genCon.showMsg("Failed", "Inactive PM Schedule cannot proceed.", InformationType.Error);
                    return;
                }

                IObjectSpace os;
                os = Application.CreateObjectSpace();
                WorkOrders obj = os.CreateObject<WorkOrders>();

                //if (selectedObject.DetailDescription != null)
                //{
                //    WRLongDescription desc = os.CreateObject<WRLongDescription>();
                //    desc.LongDescription = selectedObject.DetailDescription.LongDescription;
                //    obj.WRDetailDescription = desc;
                //}

                if (selectedObject.PlannerGroup != null)
                    obj.AssignPlannerGroup = os.FindObject<PlannerGroups>(CriteriaOperator.Parse("ID=?", selectedObject.PlannerGroup.ID));

                if (selectedObject.Priority != null)
                    obj.Priority = os.FindObject<Priorities>(CriteriaOperator.Parse("ID=?", selectedObject.Priority.ID));

                obj.PMSchedule = os.FindObject<PMSchedules>(CriteriaOperator.Parse("ID=?", selectedObject.ID));

                obj.PMDate = DateTime.Today;

                //if (selectedObject.IsSurveillance)
                //    obj.DocType = os.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoSurveillancePlantMaintenance));
                //else
                obj.DocType = os.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoPlantMaintenance));

                obj.PMClass = os.FindObject<PMClasses>(CriteriaOperator.Parse("ID=?", selectedObject.PMClass.ID));
                obj.IsPreventiveMaintenance = true;

                obj.Remarks = selectedObject.BoName;
                obj.WorkInstruction = selectedObject.WorkInstruction;
                obj.CheckListName = selectedObject.CheckListName;
                obj.CheckListLink = selectedObject.CheckListLink;
                if (selectedObject.CheckList != null)
                    obj.CheckList = os.FindObject<CheckLists>(CriteriaOperator.Parse("ID=?", selectedObject.CheckList.ID));

                ListPropertyEditor listviewDetail = null;
                ListPropertyEditor listviewDetail2 = null;
                if (((PMSchedules)View.CurrentObject).Detail.Count > 0 || ((PMSchedules)View.CurrentObject).Detail2.Count > 0)
                {
                    foreach (ViewItem item in ((DetailView)View).Items)
                    {
                        if ((item is ListPropertyEditor) && (item.Id == "Detail"))
                            listviewDetail = item as ListPropertyEditor;
                        if ((item is ListPropertyEditor) && (item.Id == "Detail2"))
                            listviewDetail2 = item as ListPropertyEditor;
                    }
                    if ((listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0) || (listviewDetail2.ListView != null && listviewDetail2.ListView.SelectedObjects.Count > 0))
                    {
                        if (((PMSchedules)View.CurrentObject).Detail.Count > 0 && listviewDetail.ListView != null && listviewDetail.ListView.SelectedObjects.Count > 0)
                        {
                            foreach (PMScheduleEquipments dtl in listviewDetail.ListView.SelectedObjects)
                            {
                                if (dtl.IsActive && dtl.Equipment.IsApproved && dtl.Equipment.IsActive)
                                {
                                    WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                    objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl.Equipment.ID));

                                    obj.Detail.Add(objdtl);

                                }
                            }

                        }
                        if (((PMSchedules)View.CurrentObject).Detail2.Count > 0 && listviewDetail2.ListView != null && listviewDetail2.ListView.SelectedObjects.Count > 0)
                        {
                            foreach (PMScheduleEqComponents dtl2 in listviewDetail2.ListView.SelectedObjects)
                            {
                                if (dtl2.IsActive && dtl2.Equipment.IsApproved && dtl2.Equipment.IsActive)
                                {
                                    if (obj.Detail.Count(p => p.Equipment.ID == dtl2.Equipment.ID) <= 0)
                                    {
                                        WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                        objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));

                                        obj.Detail.Add(objdtl);

                                    }
                                    WorkOrderEqComponents objdtl2 = os.CreateObject<WorkOrderEqComponents>();

                                    objdtl2.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));
                                    objdtl2.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", dtl2.EquipmentComponent.ID));

                                    obj.Detail2.Add(objdtl2);
                                }
                            }
                        }

                        //if (listviewDetail != null && listviewDetail.ListView.SelectedObjects.Count == 0)
                        //    throw new UserFriendlyException("WARNING  -  You must select only ONE charge split to add a credit split !");
                    }
                    else
                    {
                        genCon.showMsg("Failed", "Please choose from item Equipments or Components.", InformationType.Error);
                        return;
                        /*
                        foreach (PMScheduleEquipments dtl in selectedObject.Detail)
                        {
                            if (dtl.IsActive && dtl.Equipment.IsApproved && dtl.Equipment.IsActive)
                            {
                                WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl.Equipment.ID));

                                obj.Detail.Add(objdtl);

                            }
                        }
                        foreach (PMScheduleEqComponents dtl2 in selectedObject.Detail2)
                        {
                            if (dtl2.IsActive && dtl2.Equipment.IsApproved && dtl2.Equipment.IsActive)
                            {
                                if (obj.Detail.Count(p => p.Equipment.ID == dtl2.Equipment.ID) <= 0)
                                {
                                    WorkOrderEquipments objdtl = os.CreateObject<WorkOrderEquipments>();

                                    objdtl.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));

                                    obj.Detail.Add(objdtl);

                                }
                                WorkOrderEqComponents objdtl2 = os.CreateObject<WorkOrderEqComponents>();

                                objdtl2.Equipment = os.FindObject<Equipments>(CriteriaOperator.Parse("ID=?", dtl2.Equipment.ID));
                                objdtl2.EquipmentComponent = os.FindObject<EquipmentComponents>(CriteriaOperator.Parse("ID=?", dtl2.EquipmentComponent.ID));

                                obj.Detail2.Add(objdtl2);
                            }
                        }
                        */

                    }

                }

                genCon.openNewView(os, obj, ViewEditMode.Edit);
                genCon.showMsg("Successful", "PM Work Order Created.", InformationType.Success);
            }
            catch (Exception ex)
            {
                genCon.showMsg("Failed", ex.Message, InformationType.Error);

            }

        }
    }
}
