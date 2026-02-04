using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel.DataAnnotations;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<PurchaseQuotationDtls> PurchaseQuotationDtlss { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [NavigationItem("Deviation")]
    [DefaultProperty("DocNum")]
    [XafDisplayName("Deviation")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("HideNew", AppearanceItemType = "Action", TargetItems = "New", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideLink", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideUnlink", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[Appearance("HideEdit", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "not IsDraft")]
    //[RuleCriteria("WorkOrderDeviationsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[RuleCriteria("WorkOrderEquipmentsEqSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    public class Deviation2025 : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Deviation2025()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.Detail = new List<DeviationMitigations>();
            this.Detail2 = new List<DeviationReviewers>();
            this.Detail3 = new List<DeviationAttachments>();
            this.Detail4 = new List<DeviationDocStatus>();

        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        // You can use the regular Code First syntax:
        //public string Name { get; set; }

        // Alternatively, specify more UI options: 
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set {
        //        if (_PersistentProperty != value) {
        //            _PersistentProperty = value;
        //            OnPropertyChanged("PersistentProperty");
        //        }
        //    }
        //}

        // Collection property:
        //public virtual IList<AssociatedEntityObject> AssociatedEntities { get; set; }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        private SystemUsers _UpdateUser;
        [XafDisplayName("Update User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(302), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual SystemUsers UpdateUser
        {
            get { return _UpdateUser; }
            set
            {
                if (_UpdateUser != value)
                {
                    _UpdateUser = value;
                    OnPropertyChanged("UpdateUser");
                }
            }
        }

        private DateTime? _UpdateDate;
        [Index(303), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public DateTime? UpdateDate
        {
            get { return _UpdateDate; }
            set
            {
                if (_UpdateDate != value)
                {
                    _UpdateDate = value;
                    OnPropertyChanged("UpdateDate");
                }
            }
        }
        private Companies _Company;
        public virtual Companies Company
        {
            get { return _Company; }
            set
            {
                if (_Company != value)
                {
                    _Company = value;
                    OnPropertyChanged("Company");
                }
            }
        }
        private DocTypes _DocType;
        [Browsable(false)]
        public virtual DocTypes DocType
        {
            get { return _DocType; }
            set
            {
                if (_DocType != value)
                {
                    _DocType = value;
                    OnPropertyChanged("DocType");
                }
            }
        }
        private long _DocNumSeq;
        [Browsable(false)]
        public long DocNumSeq
        {
            get { return _DocNumSeq; }
            set
            {
                if (_DocNumSeq != value)
                {
                    _DocNumSeq = value;
                    OnPropertyChanged("DocNumSeq");
                }
            }
        }
        private string _DocNum;
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Deviation No"), ToolTip("Enter Text")]
        [Appearance("DocNum", Enabled = false)]
        public string DocNum
        {
            get { return _DocNum; }
            set
            {
                if (_DocNum != value)
                {
                    _DocNum = value;
                    OnPropertyChanged("DocNum");
                }
            }
        }
        private DeviationRankEnum _DeviationRank;
        [XafDisplayName("Deviation Rank"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("DeviationRank", Enabled = false, Criteria = "not IsDraft")]
        public DeviationRankEnum DeviationRank
        {
            get { return _DeviationRank; }
            set
            {
                if (_DeviationRank != value)
                {
                    _DeviationRank = value;
                    OnPropertyChanged("DeviationRank");
                }
            }
        }
        private DeviationRankEnum _LastDeviationRank;
        [XafDisplayName("Last Deviation Rank"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("LastDeviationRank", Enabled = false)]
        [Appearance("LastDeviationRank", Enabled = false)]
        public DeviationRankEnum LastDeviationRank
        {
            get { return _LastDeviationRank; }
            set
            {
                if (_LastDeviationRank != value)
                {
                    _LastDeviationRank = value;
                    OnPropertyChanged("LastDeviationRank");
                }
            }
        }
        private string _LastDeviationNo;
        [XafDisplayName("Last Deviation ID"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("LastDeviationNo", Enabled = false)]
        public string LastDeviationNo
        {
            get { return _LastDeviationNo; }
            set
            {
                if (_LastDeviationNo != value)
                {
                    _LastDeviationNo = value;
                    OnPropertyChanged("LastDeviationNo");
                }
            }
        }

        private DateTime? _ApprovedValidDate;
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("ApprovedValidDate", Enabled = false)]
        //[Appearance("ApprovedValidDate", Enabled = false, Criteria = "not IsDraft")]
        public DateTime? ApprovedValidDate
        {
            get { return _ApprovedValidDate; }
            set
            {
                if (_ApprovedValidDate != value)
                {
                    _ApprovedValidDate = value;
                    OnPropertyChanged("ApprovedValidDate");
                }
            }
        }
        private string _DeviationTitle;
        [Index(4), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("DeviationTitle", Enabled = false)]
        [FieldSize(1024)]
        [Appearance("DeviationTitle", Enabled = false, Criteria = "not IsDraft")]
        public string DeviationTitle
        {
            get { return _DeviationTitle; }
            set
            {
                if (_DeviationTitle != value)
                {
                    _DeviationTitle = value;
                    OnPropertyChanged("_DeviationTitle");
                }
            }
        }
        private DeviationTypes _DeviationType;
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("DeviationType", Enabled = false)]
        [Appearance("DeviationType", Enabled = false, Criteria = "not IsDraft")]
        public virtual DeviationTypes DeviationType
        {
            get { return _DeviationType; }
            set
            {
                if (_DeviationType != value)
                {
                    _DeviationType = value;
                    OnPropertyChanged("DeviationType");
                }
            }
        }

        private DeviationStatus _DeviationStatus;
        [Index(6), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("DeviationStatus", Enabled = false)]
        public virtual DeviationStatus DeviationStatus
        {
            get { return _DeviationStatus; }
            set
            {
                if (_DeviationStatus != value)
                {
                    _DeviationStatus = value;
                    OnPropertyChanged("DeviationStatus");
                }
            }
        }

        private DateTime? _OriginalOLAFD;
        [XafDisplayName("Original OLAFD")]
        [Index(7), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("OriginalOLAFD", Enabled = false)]
        public DateTime? OriginalOLAFD
        {
            get { return _OriginalOLAFD; }
            set
            {
                if (_OriginalOLAFD != value)
                {
                    _OriginalOLAFD = value;
                    OnPropertyChanged("OriginalOLAFD");
                }
            }
        }

        private DateTime? _ProposedLAFDValidDate;
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("ProposedLAFDValidDate", Enabled = false)]
        [Appearance("ProposedLAFDValidDate", Enabled = false, Criteria = "not IsDraft")]
        public DateTime? ProposedLAFDValidDate
        {
            get { return _ProposedLAFDValidDate; }
            set
            {
                if (_ProposedLAFDValidDate != value)
                {
                    _ProposedLAFDValidDate = value;
                    OnPropertyChanged("ProposedLAFDValidDate");
                }
            }
        }

        private Criticalities _Criticality;
        [Index(9), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("Criticality", Enabled = false)]
        public virtual Criticalities Criticality
        {
            get { return _Criticality; }
            set
            {
                if (_Criticality != value)
                {
                    _Criticality = value;
                    OnPropertyChanged("Criticality");
                }
            }
        }

        private Equipments _Equipment;
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Equipment", Enabled = false)]
        public virtual Equipments Equipment
        {
            get { return _Equipment; }
            set
            {
                if (_Equipment != value)
                {
                    _Equipment = value;
                    OnPropertyChanged("Equipment");
                }
            }
        }

        private EquipmentComponents _Component;
        [Index(21), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Component", Enabled = false)]
        public virtual EquipmentComponents Component
        {
            get { return _Component; }
            set
            {
                if (_Component != value)
                {
                    _Component = value;
                    OnPropertyChanged("Component");
                }
            }
        }


        private SCECategories _SCECategory;
        [XafDisplayName("SCE Category")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(10), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DataSourceProperty("Criticality.SCECateogry", DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("SCECategory", Enabled = false)]
        public virtual SCECategories SCECategory
        {
            get { return _SCECategory; }
            set
            {
                if (_SCECategory != value)
                {
                    _SCECategory = value;
                    OnPropertyChanged("SCECategory");
                }
            }
        }
        private SCESubCategories _SCESubCategory;
        [XafDisplayName("SCE Sub Category")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(11), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DataSourceProperty("SCESubCategoryList", DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("SCESubCategory", Enabled = false)]
        public virtual SCESubCategories SCESubCategory
        {
            get { return _SCESubCategory; }
            set
            {
                if (_SCESubCategory != value)
                {
                    _SCESubCategory = value;
                    OnPropertyChanged("SCESubCategory");
                }
            }
        }

        private DeviationDiscipline _DeviationDiscipline;
        [XafDisplayName("Discipline")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(13), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DataSourceProperty("SCESubCategoryList", DataSourcePropertyIsNullMode.SelectNothing)]
        //[Appearance("DeviationDiscipline", Enabled = false)]
        [Appearance("DeviationDiscipline", Enabled = false, Criteria = "not IsDraft")]
        public virtual DeviationDiscipline DeviationDiscipline
        {
            get { return _DeviationDiscipline; }
            set
            {
                if (_DeviationDiscipline != value)
                {
                    _DeviationDiscipline = value;
                    OnPropertyChanged("DeviationDiscipline");
                }
            }
        }

        private string _PlannerGroup;
        [XafDisplayName("Planner Group")]
        [Index(14), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("PlannerGroup", Enabled = false)]
        public string PlannerGroup
        {
            get { return _PlannerGroup; }
            set
            {
                if (_PlannerGroup != value)
                {
                    _PlannerGroup = value;
                    OnPropertyChanged("PlannerGroup");
                }
            }

        }

        private string _WorkOrderType;
        [XafDisplayName("WO Type")]
        [Index(14), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("WorkOrderType", Enabled = false)]
        public string WorkOrderType
        {
            get { return _WorkOrderType; }
            set
            {
                if (_WorkOrderType != value)
                {
                    _WorkOrderType = value;
                    OnPropertyChanged("WorkOrderType");
                }
            }
        }

        private string _WorkOrderPriority;
        [XafDisplayName("WO Priority")]
        [Index(15), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("WorkOrderPriority", Enabled = false)]
        public string WorkOrderPriority
        {
            get { return _WorkOrderPriority; }
            set
            {
                if (_WorkOrderPriority != value)
                {
                    _WorkOrderPriority = value;
                    OnPropertyChanged("WorkOrderPriority");
                }
            }
        }

        private DeviationLocations _Location;
        [XafDisplayName("Location")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(16), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DataSourceProperty("SCESubCategoryList", DataSourcePropertyIsNullMode.SelectNothing)]
        //[Appearance("DeviationDiscipline", Enabled = false)]
        [Appearance("Location", Enabled = false, Criteria = "not IsDraft")]
        public virtual DeviationLocations Location
        {
            get { return _Location; }
            set
            {
                if (_Location != value)
                {
                    _Location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private WorkOrders _WorkOrder;
        [Index(17), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [Appearance("WorkOrder", Enabled = false)]
        public virtual WorkOrders WorkOrder
        {
            get { return _WorkOrder; }
            set
            {
                if (_WorkOrder != value)
                {
                    _WorkOrder = value;
                    OnPropertyChanged("WorkOrder");

                    if (value != null)
                    {
                        if (IsNew)
                        {

                            PlannerGroup = WorkOrder.AssignPlannerGroup?.BoName;
                            WorkOrderType = WorkOrder.IsPreventiveMaintenance ? "PM" : "CM";
                            WorkOrderPriority = WorkOrder.Priority?.BoName;
                        }
                    }
                    //get
                    //{
                    //    return WorkOrder?.AssignPlannerGroup?.BoName;
                    //}
                    //get 
                    //{
                    //    if (WorkOrder == null) return null;
                    //    return WorkOrder.IsPreventiveMaintenance? "PM": "CM"; 
                    //}
                    //get { return WorkOrder?.Priority?.BoName; }

                }
            }
        }

        private SystemUsers _CreateUser;
        [XafDisplayName("Raised By")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(300), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("CreateUser", Enabled = false)]
        public virtual SystemUsers CreateUser
        {
            get { return _CreateUser; }
            set
            {
                if (_CreateUser != value)
                {
                    _CreateUser = value;
                    OnPropertyChanged("CreateUser");
                }
            }
        }

        private DateTime? _CreateDate;
        [XafDisplayName("Raised Date")]
        [Index(18), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("CreateDate", Enabled = false)]
        public DateTime? CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        private string _Dscription;
        [XafDisplayName("Description")]
        [Index(19), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [FieldSize(4096)]
        [Appearance("Dscription", Enabled = false, Criteria = "not IsDraft")]
        public string Dscription
        {
            get { return _Dscription; }
            set
            {
                if (_Dscription != value)
                {
                    _Dscription = value;
                    OnPropertyChanged("Dscription");
                }
            }
        }

        [XafDisplayName("Dscription Note")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DscriptionNote
        {
            get 
            {
                return @"The deviation description in the deviation
control form shall describe:
1. What is the purpose/function of the
equipment?
2. What is the nature of the proposed
deviation?
3. What mandatory requirements
cannot be made?
4. What is the reason the deviation is
need? – e.g. why timely";
            }
        }

        private Risks _OverallRisk;
        [XafDisplayName("Overall Risk")]
        [Index(20), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("OverallRisk", Enabled = false, Criteria = "not IsDraft")]
        public virtual Risks OverallRisk
        {
            get { return _OverallRisk; }
            set
            {
                if (_OverallRisk != value)
                {
                    _OverallRisk = value;
                    OnPropertyChanged("OverallRisk");
                }
            }
        }
        private RiskPeoples _People1;
        [XafDisplayName("People")]
        [Index(21), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("People1", Enabled = false, Criteria = "not IsDraft")]
        public virtual RiskPeoples People1
        {
            get { return _People1; }
            set
            {
                if (_People1 != value)
                {
                    _People1 = value;
                    OnPropertyChanged("People1");
                }
            }
        }
        private RiskAssets _Assets;
        [XafDisplayName("Assets")]
        [Index(22), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("Assets", Enabled = false, Criteria = "not IsDraft")]
        public virtual RiskAssets Assets
        {
            get { return _Assets; }
            set
            {
                if (_Assets != value)
                {
                    _Assets = value;
                    OnPropertyChanged("Assets");
                }
            }
        }
        private RiskCommunities _Community;
        [XafDisplayName("Community")]
        [Index(23), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("Community", Enabled = false, Criteria = "not IsDraft")]
        public virtual RiskCommunities Community
        {
            get { return _Community; }
            set
            {
                if (_Community != value)
                {
                    _Community = value;
                    OnPropertyChanged("Community");
                }
            }
        }

        private RiskEnvironments _Environment;
        [XafDisplayName("Environment")]
        [Index(24), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("Environment", Enabled = false, Criteria = "not IsDraft")]
        public virtual RiskEnvironments Environment
        {
            get { return _Environment; }
            set
            {
                if (_Environment != value)
                {
                    _Environment = value;
                    OnPropertyChanged("Environment");
                }
            }
        }

        private string _RiskAssessment;
        [XafDisplayName("Risk Assessment Attendees/Participants")]
        [Index(30), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("RiskAssessment", Enabled = false, Criteria = "not IsDraft")]
        [FieldSize(4096)]
        public string RiskAssessment
        {
            get { return _RiskAssessment; }
            set
            {
                if (_RiskAssessment != value)
                {
                    _RiskAssessment = value;
                    OnPropertyChanged("RiskAssessment");
                }
            }
        }
        private string _RiskComment;
        [XafDisplayName("Comment on overall or specific Risks")]
        [Index(31), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("RiskComment", Enabled = false, Criteria = "not IsDraft")]
        [FieldSize(4096)]
        public string RiskComment
        {
            get { return _RiskComment; }
            set
            {
                if (_RiskComment != value)
                {
                    _RiskComment = value;
                    OnPropertyChanged("RiskComment");
                }
            }
        }
        private string _EscalationComment;
        [XafDisplayName("Escalation Comments")]
        [Index(31), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("EscalationComment", Enabled = false, Criteria = "not IsDraft")]
        [FieldSize(4096)]
        public string EscalationComment
        {
            get { return _EscalationComment; }
            set
            {
                if (_EscalationComment != value)
                {
                    _EscalationComment = value;
                    OnPropertyChanged("EscalationComment");
                }
            }
        }
        [XafDisplayName("Escalation Comment Note")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string EscalationCommentNote
        {
            get
            {
                return @"The escalation comments assumes that the incident has
occurred and shall be describe how the effects may be
contained to prevent further escalation and so limit
consequences of an incident.
• Possible escalations require resulting from the
release of each hazard.
• Concurrent activities and issues that were
considered.
• Constraints or weakness if any of the Barriers
defend against escalation.";
            }
        }
        private string _RiskIsolationIDIsolationID;
        [XafDisplayName("PTW Isolation ID")]
        [Index(32), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("RiskIsolationID", Enabled = false, Criteria = "not IsDraft")]
        [FieldSize(255)]
        public string RiskIsolationID
        {
            get { return _RiskIsolationIDIsolationID; }
            set
            {
                if (_RiskIsolationIDIsolationID != value)
                {
                    _RiskIsolationIDIsolationID = value;
                    OnPropertyChanged("RiskIsolationID");
                }
            }
        }
        private string _RiskActionPlan;
        [XafDisplayName("Enter Remedial Action Plan to Return to Normal Operations")]
        [Index(33), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("RiskActionPlan", Enabled = false, Criteria = "not IsDraft")]
        [FieldSize(4096)]
        public string RiskActionPlan
        {
            get { return _RiskActionPlan; }
            set
            {
                if (_RiskActionPlan != value)
                {
                    _RiskActionPlan = value;
                    OnPropertyChanged("RiskActionPlan");
                }
            }
        }

        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy01", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy01
        {
            get { return ""; }
        }
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy02", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy02
        {
            get { return ""; }
        }
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy03", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy03
        {
            get { return ""; }
        }
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy04", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy04
        {
            get { return ""; }
        }
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy05", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy05
        {
            get { return ""; }
        }
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy06", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy06
        {
            get { return ""; }
        }
        [XafDisplayName("Mitigations")]
        //[Appearance("Detail", Enabled = false)]
        [Appearance("Detail", Enabled = false, Criteria = "not IsDraft")]
        public virtual IList<DeviationMitigations> Detail { get; set; }

        [XafDisplayName("Reviewer")]
        [Appearance("Detail2", Enabled = false, Criteria = "not IsDraft")]
        public virtual IList<DeviationReviewers> Detail2 { get; set; }

        [XafDisplayName("Attachment")]
        [Appearance("Detail3")]
        public virtual IList<DeviationAttachments> Detail3 { get; set; }

        [XafDisplayName("Doc Status List")]
        [Appearance("Detail4", Enabled = false)]
        public virtual IList<DeviationDocStatus> Detail4 { get; set; }

        [Browsable(false)]
        public bool IsDraft
        {
            get
            {
                if (DeviationStatus == null) return false;

                if (DeviationStatus.BoCode == GeneralSettings.DeviationStatusNew
                    || DeviationStatus.BoCode == GeneralSettings.DeviationStatusDraft
                    || DeviationStatus.BoCode == GeneralSettings.DeviationStatusWithdrawn
                    || DeviationStatus.BoCode == GeneralSettings.DeviationStatusDraftExtension
                    || DeviationStatus.BoCode == GeneralSettings.DeviationStatusApprovedExtension)
                {
                    return true;
                }
                return false;
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        // Collection property:


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
            CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;

            DeviationRank = DeviationRankEnum.Deviation_01;
            LastDeviationRank = DeviationRankEnum.NA;
            DeviationStatus = objectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusNew));
            //DeviationType = objectSpace.FindObject<DeviationTypes>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationTypeOLAFD));
            Company = objectSpace.FindObject<Companies>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.HQCompany));
            DocType = objectSpace.FindObject<DocTypes>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.Deviation));
        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
            IsNew = false;
        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
            if (IsNew)
            {
                CompanyDocs doc = objectSpace.FindObject<CompanyDocs>(CriteriaOperator.Parse("Company.ID=? and DocType.ID=?", Company.ID, DocType.ID));
                if (doc != null)
                {
                    DocNumSeq = doc.NextDocNo;
                    DocNum = DocNumSeq.ToString();
                    doc.NextDocNo++;
                }
                else
                {
                    int runningnumber = 50000001;
                    doc = objectSpace.CreateObject<CompanyDocs>();
                    doc.Company = objectSpace.GetObjectByKey<Companies>(Company.ID);
                    doc.DocType = objectSpace.GetObjectByKey<DocTypes>(DocType.ID);
                    doc.NextDocNo = runningnumber + 1;
                    
                    DocNumSeq = runningnumber;
                    DocNum = DocNumSeq.ToString();
                }

                this.DeviationStatus = objectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusDraft));

                DeviationDocStatus dswr = objectSpace.CreateObject<DeviationDocStatus>();
                dswr.DocStatus = objectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusDraft));
                dswr.DocRemarks = "";
                dswr.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                dswr.CreateDate = DateTime.Now;
                dswr.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                dswr.UpdateDate = DateTime.Now;
                Detail4.Add(dswr);

                if (!string.IsNullOrEmpty(LastDeviationNo))
                {
                    Deviation2025 olddoc = objectSpace.FindObject<Deviation2025>(CriteriaOperator.Parse("DocNum=?", LastDeviationNo));

                    olddoc.DeviationStatus = objectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusClose));
                    DeviationDocStatus olddswr = objectSpace.CreateObject<DeviationDocStatus>();
                    olddswr.DocStatus = objectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.DeviationStatusClose));
                    olddswr.DocRemarks = "";
                    olddswr.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    olddswr.CreateDate = DateTime.Now;
                    olddswr.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    olddswr.UpdateDate = DateTime.Now;
                    olddoc.Detail4.Add(olddswr);

                }

            }
            UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            UpdateDate = DateTime.Now;
            IsNew = false;

            int cnt = 0;
            if (this.Detail != null && this.Detail.Count > 0)
            {
                cnt = 0;
                foreach (DeviationMitigations obj in this.Detail.OrderBy(pp => pp.CreateDate))
                {
                    cnt++;
                    obj.RowNumber = cnt;
                }
            }
            if (this.Detail2 != null && this.Detail2.Count > 0)
            {
                cnt = 0;
                foreach (DeviationReviewers obj in this.Detail2.OrderBy(pp => pp.CreateDate))
                {
                    cnt++;
                    obj.RowNumber = cnt;
                }
            }
            if (this.DeviationStatus.BoCode == GeneralSettings.DeviationStatusApproved)
                this.WorkOrder.ProposedLAFDValidDate = this.ProposedLAFDValidDate;
        }
        #endregion

        #region IObjectSpaceLink members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIObjectSpaceLinktopic.aspx)
        // Use the Object Space to access other entities from IXafEntityObject methods (see https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113707.aspx).
        private IObjectSpace objectSpace;
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
