using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.EF;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using AssetsManagementEF.Module.BusinessObjects;

namespace AssetsManagementEF.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppUpdatingModuleUpdatertopic.aspx
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //EntityObject1 theObject = ObjectSpace.FindObject<EntityObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<EntityObject1>();
            //    theObject.Name = name;
            //}

            DocTypes typepm = ObjectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoPlantMaintenance));
            if (typepm == null)
            {
                typepm = ObjectSpace.CreateObject<DocTypes>();
                typepm.BoCode = GeneralSettings.WoPlantMaintenance;
                typepm.BoName = "Preventive WO";
            }

            DocTypes typecm = ObjectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoBreakdown));
            if (typecm == null)
            {
                typecm = ObjectSpace.CreateObject<DocTypes>();
                typecm.BoCode = GeneralSettings.WoBreakdown;
                typecm.BoName = "Corrective WO";
            }

            //DocTypes typespm = ObjectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoSurveillancePlantMaintenance));
            //if (typespm == null)
            //{
            //    typespm = ObjectSpace.CreateObject<DocTypes>();
            //    typespm.BoCode = GeneralSettings.WoSurveillancePlantMaintenance;
            //    typespm.BoName = "Surveillance Preventive Maintenance";
            //}
            //DocTypes typescm = ObjectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WoSurveillanceBreakdown));
            //if (typescm == null)
            //{
            //    typescm = ObjectSpace.CreateObject<DocTypes>();
            //    typescm.BoCode = GeneralSettings.WoSurveillanceBreakdown;
            //    typescm.BoName = "Surveillance Breakdown Maintenance";
            //}

            DocTypes typepr = ObjectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.PurchaseRequest));
            if (typepr == null)
            {
                typepr = ObjectSpace.CreateObject<DocTypes>();
                typepr.BoCode = GeneralSettings.PurchaseRequest;
                typepr.BoName = "Purchase Request";
            }

            DocTypes wr = ObjectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WR));
            if (wr == null)
            {
                wr = ObjectSpace.CreateObject<DocTypes>();
                wr.BoCode = GeneralSettings.WR;
                wr.BoName = "Work Request";
            }

            if (ObjectSpace.IsModified)
            {
                ObjectSpace.CommitChanges();
            }

            Companies Company = ObjectSpace.FindObject<Companies>(new BinaryOperator("BoCode", GeneralSettings.HQCompany));
            if (Company == null)
            {
                Company = ObjectSpace.CreateObject<Companies>();
                Company.BoCode = GeneralSettings.HQCompany;
                Company.BoName = GeneralSettings.HQCompany;
            }
            string temp = GeneralSettings.defaultcode;
            PMDepartments pmdepartment = ObjectSpace.FindObject<PMDepartments>(new BinaryOperator("BoCode", temp));
            if (pmdepartment == null)
            {
                pmdepartment = ObjectSpace.CreateObject<PMDepartments>();
                pmdepartment.BoCode = temp;
                pmdepartment.BoName = temp;
            }

            temp = GeneralSettings.defaultcode;
            PMClasses pmclass = ObjectSpace.FindObject<PMClasses>(new BinaryOperator("BoCode", temp));
            if (pmclass == null)
            {
                pmclass = ObjectSpace.CreateObject<PMClasses>();
                pmclass.BoCode = temp;
                pmclass.BoName = temp;
            }

            temp = GeneralSettings.defaultcode;
            Positions positiongen = ObjectSpace.FindObject<Positions>(new BinaryOperator("BoCode", temp));
            if (positiongen == null)
            {
                positiongen = ObjectSpace.CreateObject<Positions>();
                positiongen.BoCode = temp;
                positiongen.BoName = temp;
            }

            //SystemUsers sampleUser = ObjectSpace.FindObject<SystemUsers>(new BinaryOperator("UserName", "User"));
            //if(sampleUser == null) {
            //    sampleUser = ObjectSpace.CreateObject<SystemUsers>();
            //    sampleUser.UserName = "User";
            //    sampleUser.FullName = "User";
            //    sampleUser.Company = Company;
            //    sampleUser.position.Add(position);
            //    sampleUser.SetPassword("1234");
            //}
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if (defaultRole == null)
            {
                defaultRole = CreateDefaultRole();
                defaultRole.AddTypePermission<StringParameters>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<JobStringParameters>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EquipmentFilterParameters>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DateRangeFilterParameters>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<Areas>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<CheckLists>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Companies>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<CompanyDocs>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<ContractDocDtls>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<ContractDocs>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Contractors>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Criticalities>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DocTypes>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqClassDocs>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqClasses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqClassProperties>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqComponentClassDocs>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqComponentClasses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqComponentGroups>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EqGroups>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EquipmentComponents>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EquipmentProperties>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Equipments>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<ItemMasters>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<JobStatuses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Locations>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PlannerGroups>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMDepartments>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMFrequencies>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMSchedules>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMScheduleEqComponents>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMScheduleEquipments>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Positions>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Priorities>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PurchaseRequestDtls>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PurchaseRequests>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SubLocations>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Tasks>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<Technicians>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WOLongDescription>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkOrderOpTypes>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WRLongDescription>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<WorkOrderAttachments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkOrderPhotos>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkRequestAttachments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkRequestPhotos>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EquipmentAttachments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<EquipmentPhotos>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMClasses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<PMClassDocs>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<PMScheduleChecklists>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkOrderManHours>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<WorkRequests>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkRequestDocStatuses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkOrders>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkOrderJobStatuses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<WorkOrderDocStatuses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<SCECategories>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SCESubCategories>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationStatus>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationWOTypes>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationWRTypes>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<DeviationWorkOrders>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationWorkRequests>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<PermissionPolicyRole>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);

            }
            //sampleUser.Roles.Add(defaultRole);

            SystemUsers userAdmin = ObjectSpace.FindObject<SystemUsers>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<SystemUsers>();
                userAdmin.UserName = "Admin";
                userAdmin.FullName = "Admin";
                userAdmin.Company = Company;
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("super");

                if (ObjectSpace.IsModified)
                {
                    ObjectSpace.CommitChanges();
                }

                positiongen.CurrentUser = userAdmin;
            }
            
            // If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", GeneralSettings.AdminRole));
            if(adminRole == null) {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = GeneralSettings.AdminRole;
            }
            adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);

            temp = GeneralSettings.ManagerRole;
            PermissionPolicyRole managerRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (managerRole == null)
            {
                managerRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                managerRole.Name = temp; // Work Order super user

                managerRole.AddTypePermission<Areas>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<CheckLists>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<Criticalities>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EqClasses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EqClassProperties>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EqComponentClasses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EqComponentGroups>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EqGroups>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EquipmentComponents>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<EquipmentProperties>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<Equipments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<JobStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<Locations>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PlannerGroups>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PMClasses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PMDepartments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PMFrequencies>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PMSchedules>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PMScheduleEqComponents>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<PMScheduleEquipments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<Positions>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<Priorities>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<SubLocations>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                managerRole.AddTypePermission<WorkRequests>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<WorkRequestDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<WorkOrders>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<WorkOrderJobStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                managerRole.AddTypePermission<WorkOrderDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

                defaultRole.AddTypePermission<SCECategories>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<SCESubCategories>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationStatus>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationWOTypes>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermission<DeviationWRTypes>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

            }

            temp = GeneralSettings.ApproverRole;
            PermissionPolicyRole approverRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (approverRole == null)
            {
                approverRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                approverRole.Name = temp; // Work Order super user

                approverRole.AddTypePermission<WorkRequests>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                approverRole.AddTypePermission<WorkRequestDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                approverRole.AddTypePermission<WorkOrders>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                approverRole.AddTypePermission<WorkOrderJobStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                approverRole.AddTypePermission<WorkOrderDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

            }

            temp = GeneralSettings.PlannerRole;
            PermissionPolicyRole plannerRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (plannerRole == null)
            {
                plannerRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                plannerRole.Name = temp; // position = positions.IsPlanner

                plannerRole.AddTypePermission<WorkRequests>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkRequestDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrders>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrderJobStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrderDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrderEquipments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrderEquipmentOps>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrderEqComponents>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                plannerRole.AddTypePermission<WorkOrderEqComponentOps>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);

            }

            temp = GeneralSettings.WPSRole;
            PermissionPolicyRole wpsRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (wpsRole == null)
            {
                wpsRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                wpsRole.Name = temp; // WorkOrders.AssignUser

                wpsRole.AddTypePermission<WorkRequests>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrders>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderJobStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderEquipments>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderEquipmentOps>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderEqComponents>(SecurityOperations.FullObjectAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderEqComponentOps>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<PurchaseRequestDtls>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<PurchaseRequests>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<PurchaseRequestDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                wpsRole.AddTypePermission<WorkOrderManHours>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);


            }

            temp = GeneralSettings.SupervisorRole;
            PermissionPolicyRole supervisorRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (supervisorRole == null)
            {
                supervisorRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                supervisorRole.Name = temp; 
            }

            temp = GeneralSettings.TechnicianRole;
            PermissionPolicyRole technicianRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (technicianRole == null)
            {
                technicianRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                technicianRole.Name = temp;
            }

            temp = GeneralSettings.RequestorRole;
            PermissionPolicyRole requestorRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (requestorRole == null)
            {
                requestorRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                requestorRole.Name = temp; // WorkRequest Requestor

                requestorRole.AddTypePermission<WorkRequests>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkRequestEquipments>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkRequestEqComponents>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkRequestDocStatuses>(SecurityOperations.FullAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrders>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrderJobStatuses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrderDocStatuses>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrderEquipments>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrderEquipmentOps>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrderEqComponents>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);
                requestorRole.AddTypePermission<WorkOrderEqComponentOps>(SecurityOperations.ReadOnlyAccess, SecurityPermissionState.Allow);

            }

            temp = GeneralSettings.ContractorRole;
            PermissionPolicyRole contractorRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (contractorRole == null)
            {
                contractorRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                contractorRole.Name = temp;
            }

            temp = GeneralSettings.WRSupervisorRole;
            PermissionPolicyRole wrsupervisorRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (wrsupervisorRole == null)
            {
                wrsupervisorRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                wrsupervisorRole.Name = temp;
            }

            temp = GeneralSettings.AcknowledgePMRole;
            PermissionPolicyRole acknowledgePMRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (acknowledgePMRole == null)
            {
                acknowledgePMRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                acknowledgePMRole.Name = temp;
            }
            temp = GeneralSettings.GeneratePMRole;
            PermissionPolicyRole generatePMRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (generatePMRole == null)
            {
                generatePMRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                generatePMRole.Name = temp;
            }
            temp = GeneralSettings.WOPlanDateRole;
            PermissionPolicyRole WOPlanDateRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (WOPlanDateRole == null)
            {
                WOPlanDateRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                WOPlanDateRole.Name = temp;
            }
            temp = GeneralSettings.PostPRRole;
            PermissionPolicyRole PostPRRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (PostPRRole == null)
            {
                PostPRRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                PostPRRole.Name = temp;
            }
            temp = GeneralSettings.CancelPRRole;
            PermissionPolicyRole CancelPRRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (CancelPRRole == null)
            {
                CancelPRRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                CancelPRRole.Name = temp;
            }
            temp = GeneralSettings.CancelWRRole;
            PermissionPolicyRole CancelWRRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (CancelWRRole == null)
            {
                CancelWRRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                CancelWRRole.Name = temp;
            }
            temp = GeneralSettings.CancelWORole;
            PermissionPolicyRole CancelWORole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", temp));
            if (CancelWORole == null)
            {
                CancelWORole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                CancelWORole.Name = temp;
            }

            temp = GeneralSettings.defaultcode;
            PlannerGroups plannergroup = ObjectSpace.FindObject<PlannerGroups>(new BinaryOperator("BoCode", temp));
            if (plannergroup == null)
            {
                plannergroup = ObjectSpace.CreateObject<PlannerGroups>();
                plannergroup.BoCode = temp;
                plannergroup.BoName = temp;
            }

            temp = GeneralSettings.defaultcode;
            EqClasses eqclass = ObjectSpace.FindObject<EqClasses>(new BinaryOperator("BoCode", temp));
            if (eqclass == null)
            {
                eqclass = ObjectSpace.CreateObject<EqClasses>();
                eqclass.BoCode = temp;
                eqclass.BoName = temp;
            }
            temp = GeneralSettings.defaultcode;
            EqGroups eqgroup = ObjectSpace.FindObject<EqGroups>(new BinaryOperator("BoCode", temp));
            if (eqgroup == null)
            {
                eqgroup = ObjectSpace.CreateObject<EqGroups>();
                eqgroup.BoCode = temp;
                eqgroup.BoName = temp;
            }
            temp = GeneralSettings.defaultcode;
            EqComponentClasses eqcomponentclass = ObjectSpace.FindObject<EqComponentClasses>(new BinaryOperator("BoCode", temp));
            if (eqcomponentclass == null)
            {
                eqcomponentclass = ObjectSpace.CreateObject<EqComponentClasses>();
                eqcomponentclass.BoCode = temp;
                eqcomponentclass.BoName = temp;
            }
            temp = GeneralSettings.defaultcode;
            EqComponentGroups eqcomponentgroup = ObjectSpace.FindObject<EqComponentGroups>(new BinaryOperator("BoCode", temp));
            if (eqcomponentgroup == null)
            {
                eqcomponentgroup = ObjectSpace.CreateObject<EqComponentGroups>();
                eqcomponentgroup.BoCode = temp;
                eqcomponentgroup.BoName = temp;
            }

            temp = GeneralSettings.defaultcode;
            Areas area = ObjectSpace.FindObject<Areas>(new BinaryOperator("BoCode", temp));
            if (area == null)
            {
                area = ObjectSpace.CreateObject<Areas>();
                area.BoCode = temp;
                area.BoName = temp;
            }
            temp = GeneralSettings.defaultcode;
            Locations location = ObjectSpace.FindObject<Locations>(new BinaryOperator("BoCode", temp));
            if (location == null)
            {
                location = ObjectSpace.CreateObject<Locations>();
                location.BoCode = temp;
                location.BoName = temp;
            }
            temp = GeneralSettings.defaultcode;
            SubLocations sublocation = ObjectSpace.FindObject<SubLocations>(new BinaryOperator("BoCode", temp));
            if (sublocation == null)
            {
                sublocation = ObjectSpace.CreateObject<SubLocations>();
                sublocation.BoCode = temp;
                sublocation.BoName = temp;
            }

            string critical = "L";
            Criticalities criticalL = ObjectSpace.FindObject<Criticalities>(new BinaryOperator("BoCode", critical));
            if (criticalL == null)
            {
                criticalL = ObjectSpace.CreateObject<Criticalities>();
                criticalL.BoCode = critical;
                criticalL.BoName = "Low";
                criticalL.LevelOfCriticality = 1;
            }

            critical = "M";
            Criticalities criticalM = ObjectSpace.FindObject<Criticalities>(new BinaryOperator("BoCode", critical));
            if (criticalM == null)
            {
                criticalM = ObjectSpace.CreateObject<Criticalities>();
                criticalM.BoCode = critical;
                criticalM.BoName = "Medium";
                criticalM.LevelOfCriticality = 2;
            }

            critical = "H";
            Criticalities criticalH = ObjectSpace.FindObject<Criticalities>(new BinaryOperator("BoCode", critical));
            if (criticalH == null)
            {
                criticalH = ObjectSpace.CreateObject<Criticalities>();
                criticalH.BoCode = critical;
                criticalH.BoName = "High";
                criticalH.LevelOfCriticality = 3;
            }

            critical = "SCE";
            Criticalities criticalSCE = ObjectSpace.FindObject<Criticalities>(new BinaryOperator("BoCode", critical));
            if (criticalSCE == null)
            {
                criticalSCE = ObjectSpace.CreateObject<Criticalities>();
                criticalSCE.BoCode = critical;
                criticalSCE.BoName = "Safety Critical Equipment";
                criticalSCE.LevelOfCriticality = 4;
            }

            WorkOrderOpTypes eqdown = ObjectSpace.FindObject<WorkOrderOpTypes>(new BinaryOperator("BoCode", GeneralSettings.EqDown));
            if (eqdown == null)
            {
                eqdown = ObjectSpace.CreateObject<WorkOrderOpTypes>();
                eqdown.BoCode = GeneralSettings.EqDown;
                eqdown.BoName = "Isolate";
                eqdown.IsDown = true;
            }

            WorkOrderOpTypes equp = ObjectSpace.FindObject<WorkOrderOpTypes>(new BinaryOperator("BoCode", GeneralSettings.EqUp));
            if (equp == null)
            {
                equp = ObjectSpace.CreateObject<WorkOrderOpTypes>();
                equp.BoCode = GeneralSettings.EqUp;
                equp.BoName = "De-isolate";
                equp.IsUp = true;
            }

            JobStatuses jobaa = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "AA"));
            if (jobaa == null)
            {
                jobaa = ObjectSpace.CreateObject<JobStatuses>();
                jobaa.BoCode = "AA";
                jobaa.BoName = "Awaiting Acknowledge";
                jobaa.IsPlanning = true;
            }

            JobStatuses jobap = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "AP"));
            if (jobap == null)
            {
                jobap = ObjectSpace.CreateObject<JobStatuses>();
                jobap.BoCode = "AP";
                jobap.BoName = "Awaiting Planning";
                jobap.IsPlanning = true;
            }
            JobStatuses jobss = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "SS"));
            if (jobss == null)
            {
                jobss = ObjectSpace.CreateObject<JobStatuses>();
                jobss.BoCode = "SS";
                jobss.BoName = "Site Survey";
                jobss.IsPlanning = true;
            }
            JobStatuses jobwp = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "WP"));
            if (jobwp == null)
            {
                jobwp = ObjectSpace.CreateObject<JobStatuses>();
                jobwp.BoCode = "WP";
                jobwp.BoName = "Work Pack";
                jobwp.IsPlanning = true;
            }
            JobStatuses jobbq = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "BQ"));
            if (jobbq == null)
            {
                jobbq = ObjectSpace.CreateObject<JobStatuses>();
                jobbq.BoCode = "BQ";
                jobbq.BoName = "Bill of Quantity";
                jobbq.IsPlanning = true;
            }

            JobStatuses jobam = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "AM"));
            if (jobam == null)
            {
                jobam = ObjectSpace.CreateObject<JobStatuses>();
                jobam.BoCode = "AM";
                jobam.BoName = "Awaiting Materials";
                jobam.IsPreExecution = true;
            }
            JobStatuses jobapr = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "APR"));
            if (jobapr == null)
            {
                jobapr = ObjectSpace.CreateObject<JobStatuses>();
                jobapr.BoCode = "APR";
                jobapr.BoName = "Awaiting Purchase Request";
                jobapr.IsPreExecution = true;
            }
            JobStatuses jobao = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "AO"));
            if (jobao == null)
            {
                jobao = ObjectSpace.CreateObject<JobStatuses>();
                jobao.BoCode = "AO";
                jobao.BoName = "Awaiting Purchase Order";
                jobao.IsPreExecution = true;
            }
            JobStatuses jobpo = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "PO"));
            if (jobao == null)
            {
                jobpo = ObjectSpace.CreateObject<JobStatuses>();
                jobpo.BoCode = "PO";
                jobpo.BoName = "Awaiting PO";
                jobpo.IsPreExecution = true;
            }
            JobStatuses jobas = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "AS"));
            if (jobas == null)
            {
                jobas = ObjectSpace.CreateObject<JobStatuses>();
                jobas.BoCode = "AS";
                jobas.BoName = "Awaiting Schedule";
                jobas.IsPreExecution = true;
            }
            JobStatuses jobsd = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "SD"));
            if (jobsd == null)
            {
                jobsd = ObjectSpace.CreateObject<JobStatuses>();
                jobsd.BoCode = "SD";
                jobsd.BoName = "Awaiting Shutdown";
                jobsd.IsPreExecution = true;
            }
            JobStatuses jobar = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "AR"));
            if (jobar == null)
            {
                jobar = ObjectSpace.CreateObject<JobStatuses>();
                jobar.BoCode = "AR";
                jobar.BoName = "Awaiting Repair";
                jobar.IsPreExecution = true;
            }
            

            JobStatuses jobux = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "UX"));
            if (jobux == null)
            {
                jobux = ObjectSpace.CreateObject<JobStatuses>();
                jobux.BoCode = "UX";
                jobux.BoName = "Under Execution";
                jobux.IsExecution = true;
            }
            JobStatuses jobhd = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "HD"));
            if (jobhd == null)
            {
                jobhd = ObjectSpace.CreateObject<JobStatuses>();
                jobhd.BoCode = "HD";
                jobhd.BoName = "On Hold";
                jobhd.IsExecution = true;
            }
            JobStatuses jobpr = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "PR"));
            if (jobpr == null)
            {
                jobpr = ObjectSpace.CreateObject<JobStatuses>();
                jobpr.BoCode = "PR";
                jobpr.BoName = "Pending Report";
                jobpr.IsExecution = true;
            }


            JobStatuses jobhf = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "HF"));
            if (jobhf == null)
            {
                jobhf = ObjectSpace.CreateObject<JobStatuses>();
                jobhf.BoCode = "HF";
                jobhf.BoName = "History File";
                jobhf.IsPostExecution = true;
            }

            JobStatuses jobtc = ObjectSpace.FindObject<JobStatuses>(new BinaryOperator("BoCode", "TC"));
            if (jobtc == null)
            {
                jobtc = ObjectSpace.CreateObject<JobStatuses>();
                jobtc.BoCode = "TC";
                jobtc.BoName = "Technical Closure";
                jobtc.IsClosure = true;
            }

            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }
        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
        }
        private PermissionPolicyRole CreateDefaultRole() {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

                defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[ID] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[ID] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[ID] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }
    }
}
