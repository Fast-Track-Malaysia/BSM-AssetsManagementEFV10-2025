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
using AssetsManagementEF.Module;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;

namespace AssetsManagementEF.Module.Web.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilterListViewControllers : ViewController<ListView>
    {
        public FilterListViewControllers()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //TargetViewNesting = Nesting.Root;

        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (!View.IsRoot)
            {
                if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
                {
                    SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                    IObjectSpace ios = Application.CreateObjectSpace();
                    PermissionPolicyRole admin = ios.FindObject<PermissionPolicyRole>(CriteriaOperator.Parse("IsCurrentUserInRole('" + DevExpress.ExpressApp.Security.SecurityStrategy.AdministratorRoleName + "')"));
                    //Positions position = ios.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));

                    bool isSupervisor = user.Roles.Where(p => p.Name == GeneralSettings.SupervisorRole).Count() > 0 ? true : false;
                    bool isWRSupervisor = user.Roles.Where(p => p.Name == GeneralSettings.WRSupervisorRole).Count() > 0 ? true : false;

                    bool IsManager = user.Roles.Where(p => p.Name == GeneralSettings.ManagerRole).Count() > 0 ? true : false;
                    bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
                    bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
                    bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;

                    bool isRequstor = user.Roles.Where(p => p.Name == GeneralSettings.RequestorRole).Count() > 0 ? true : false;
                    bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

                    if (isContractor && !isSupervisor)
                    {
                        if (!string.IsNullOrEmpty(user.ContractorID))
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Contractor.BoCode=?", user.ContractorID);
                        else
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                    }
                    else if (isSupervisor)
                    { }
                    else if (IsManager || IsApprover || IsPlanner || IsWPS)
                    { }
                    else
                        ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");

                }
            }
            else if (View.IsRoot)
            {
                SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
                IObjectSpace ios = Application.CreateObjectSpace();
                PermissionPolicyRole admin = ios.FindObject<PermissionPolicyRole>(CriteriaOperator.Parse("IsCurrentUserInRole('" + DevExpress.ExpressApp.Security.SecurityStrategy.AdministratorRoleName + "')"));
                Positions position = ios.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));

                bool isSupervisor = user.Roles.Where(p => p.Name == GeneralSettings.SupervisorRole).Count() > 0 ? true : false;
                bool isWRSupervisor = user.Roles.Where(p => p.Name == GeneralSettings.WRSupervisorRole).Count() > 0 ? true : false;

                bool IsManager = user.Roles.Where(p => p.Name == GeneralSettings.ManagerRole).Count() > 0 ? true : false;
                bool IsApprover = user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0 ? true : false;
                bool IsPlanner = user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0 ? true : false;
                bool IsWPS = user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0 ? true : false;

                bool isRequstor = user.Roles.Where(p => p.Name == GeneralSettings.RequestorRole).Count() > 0 ? true : false;
                bool isContractor = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;

                if (View.ObjectTypeInfo.Type == typeof(PurchaseRequests))
                {
                    if (position == null)
                    {
                        if (isContractor && !isSupervisor)
                        {
                            if (!string.IsNullOrEmpty(user.ContractorID))
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Contractor.BoCode=?", user.ContractorID);
                            else
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                        else
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                    }
                    else
                    {
                        if (isContractor && !isSupervisor)
                        {
                            if (!string.IsNullOrEmpty(user.ContractorID))
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("Contractor.BoCode=?", user.ContractorID);
                            else
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                        else if (isSupervisor)
                        { }
                        else if (IsManager || IsApprover || IsPlanner || IsWPS)
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[PlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                        }
                        else
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                    }
                }
                else if (View.ObjectTypeInfo.Type == typeof(WorkRequests))
                {
                    if (position == null)
                    {
                        if (isRequstor)
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = new BinaryOperator("CreateUser.ID", user.ID, BinaryOperatorType.Equal);
                        }
                        else
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                    }
                    else
                    {
                        if (isWRSupervisor)
                        { }
                        else if (IsManager || IsApprover || IsPlanner || IsWPS)
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("CreateUser.ID=? or ( DocPassed and not Approved and not Cancelled and not Rejected and [PlannerGroup.DetailPosition][[ID]=?] and true=?)", user.ID, position.ID, position.IsCorrectiveMaintenance);
                        }
                        else if (isRequstor)
                        {
                            string sql = user.ID.ToString();
                            foreach (PlannerGroups dtl in position.DetailPlannerGroup)
                            {
                                foreach (Positions usr in dtl.DetailPosition)
                                {
                                    //if (usr.CurrentUser != null)
                                    //{
                                    if (sql == "")
                                        sql += usr.CurrentUser.ID.ToString();
                                    else
                                        sql += "," + usr.CurrentUser.ID.ToString();
                                    //}
                                }
                            }
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("CreateUser in (" + sql + ") and true=?", position.IsCorrectiveMaintenance);
                            //((ListView)View).CollectionSource.Criteria["Filter1"] = new BinaryOperator("CreateUser.ID", user.ID, BinaryOperatorType.Equal);
                        }
                        else
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                    }
                }
                else if (View.ObjectTypeInfo.Type == typeof(WorkOrders))
                {
                    if (position == null)
                    {
                        if (isContractor && !isSupervisor)
                        {
                            if (!string.IsNullOrEmpty(user.ContractorID))
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[DetailRequest][[Contractor.BoCode]=?] or [Contractor.BoCode]=?", user.ContractorID, user.ContractorID);
                            else
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                        else
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                    }
                    else
                    {
                        if (isContractor && !isSupervisor)
                        {
                            if (!string.IsNullOrEmpty(user.ContractorID))
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("[DetailRequest][[Contractor.BoCode]=?] or [Contractor.BoCode]=?", user.ContractorID, user.ContractorID);
                            else
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                        else if (isSupervisor)
                        { }
                        else if (IsManager || IsApprover || IsPlanner || IsWPS)
                        {
                            if (IsApprover && IsPlanner && IsWPS)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else if (IsApprover && IsPlanner)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( not Approved and not IsClosed and [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else if (IsApprover && IsWPS)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( ( DocPassed or Approved or IsClosed ) and [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else if (IsPlanner && IsWPS)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else if (IsApprover)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( DocPassed and not IsClosed and [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else if (IsWPS)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( ( Approved or IsClosed ) and [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else if (IsPlanner)
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("( not DocPassed and not Approved and not IsClosed and [AssignPlannerGroup.DetailPosition][[ID]=?] and ( (IsPreventiveMaintenance and IsPreventiveMaintenance=?) or (not IsPreventiveMaintenance and true=?) ) )", position.ID, position.IsPreventiveMaintenance, position.IsCorrectiveMaintenance);
                            else
                                ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                        else
                        {
                            ((ListView)View).CollectionSource.Criteria["Filter1"] = CriteriaOperator.Parse("1=0");
                        }
                    }
                    if (View.Id == "WorkOrders_ListView_PM")
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter2"] = CriteriaOperator.Parse("IsPreventiveMaintenance and JobStatus.BoCode<>?", GeneralSettings.InitPMJobStatus);
                    }
                    else if (View.Id == "WorkOrders_ListView_PMACK")
                    {
                        if (isSupervisor || IsManager || IsApprover || IsPlanner || IsWPS)
                            ((ListView)View).CollectionSource.Criteria["Filter2"] = CriteriaOperator.Parse("IsPreventiveMaintenance and JobStatus.BoCode=?", GeneralSettings.InitPMJobStatus);
                        else
                            ((ListView)View).CollectionSource.Criteria["Filter2"] = CriteriaOperator.Parse("1=0");
                    }
                    else
                    {
                        ((ListView)View).CollectionSource.Criteria["Filter2"] = CriteriaOperator.Parse("not IsPreventiveMaintenance");
                    }
                }
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
        }
    }
}
