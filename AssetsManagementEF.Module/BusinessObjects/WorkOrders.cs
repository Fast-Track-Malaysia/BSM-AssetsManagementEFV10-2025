using System;
using System.Linq;
using DevExpress.Data.Linq;
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
using System.Text.RegularExpressions;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<PurchaseQuotations> PurchaseQuotationss { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("Work Orders")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DocNum")]
    [XafDisplayName("Work Order")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [RuleCriteria("WorkOrdersDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    [RuleCriteria("WorkOrdersSaveRule", DefaultContexts.Save, "not (IsDeviationApproved and IsDeviationNotApproved)", "With & Without OLFD approval cannot exist at the same time.")]
    //[RuleCriteria("WorkOrdersSaveRule", DefaultContexts.Save, "AllowedSave", "Cannot Save after Cancelled or Closure.")]
    //[RuleCriteria("WorkOrdersSaveRule2", DefaultContexts.Save, "DetailValid", "There are no details in this document.")]
    //[RuleCriteria("WorkOrdersSaveRule3", DefaultContexts.Save, "SourceValid", "This Document has no source.")]
    //[RuleCriteria("WorkOrdersSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    public class WorkOrders : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private Int32 _OldPlannerGroup = 0;
        public WorkOrders()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.DetailManHours = new List<WorkOrderManHours>();
            this.Detail = new List<WorkOrderEquipments>();
            this.Detail2 = new List<WorkOrderEqComponents>();
            this.DetailJobStatus = new List<WorkOrderJobStatuses>();
            this.DetailDocStatus = new List<WorkOrderDocStatuses>();
            this.DetailRequest = new List<PurchaseRequests>();
            this.DetailAttachment = new List<WorkOrderAttachments>();
            this.DetailPhoto = new List<WorkRequestPhotos>();
            this.DetailDocPG = new List<WorkOrderDocPGs>();
            this.DetailDeviation = new List<Deviation2025>();

        }
        private bool _IsCancelling;
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsCancelling
        {
            get { return _IsCancelling; }
            set
            {
                if (_IsCancelling != value)
                {
                    _IsCancelling = value;
                    OnPropertyChanged("IsCancelling");
                }
            }
        }
        public bool isDetailValid()
        {
            bool detailValid = false;
            if (Detail != null && Detail.Count() > 0)
            {
                detailValid = true;
            }
            if (Detail2 != null && Detail2.Count() > 0)
            {
                detailValid = true;
            }
            return detailValid;
        }

        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool DetailValid
        //{
        //    get
        //    {
        //        bool detailValid = false;
        //        if (Detail != null && Detail.Count() > 0)
        //        {
        //            detailValid = true;
        //        }
        //        if (Detail2 != null && Detail2.Count() > 0)
        //        {
        //            detailValid = true;
        //        }
        //        return detailValid;
        //    }
        //}
        public bool isSourceValid()
        {
            bool sourceValid = false;
            if (IsPreventiveMaintenance)
            {
                if (PMSchedule != null)
                {
                    sourceValid = true;
                }
            }
            else
            {
                if (WorkRequest != null)
                {
                    sourceValid = true;
                }
            }
            return sourceValid;
        }

        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool SourceValid
        //{
        //    get
        //    {
        //        bool sourceValid = false;
        //        if (IsPreventiveMaintenance)
        //        {
        //            if (PMSchedule != null)
        //            {
        //                sourceValid = true;
        //            }
        //        }
        //        else
        //        {
        //            if (WorkRequest != null)
        //            {
        //                sourceValid = true;
        //            }
        //        }
        //        return sourceValid;
        //    }
        //}
        public bool isAllowedSave()
        {
            if (this.Cancelled || this.IsClosed)
                return false;
            return true;
        }
        //private bool _AllowedSave;
        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool AllowedSave
        //{
        //    get { return _AllowedSave; }
        //    set
        //    {
        //        if (_AllowedSave != value)
        //        {
        //            _AllowedSave = value;
        //            OnPropertyChanged("AllowedSave");
        //        }
        //    }
        //}

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

        private SystemUsers _CreateUser;
        [XafDisplayName("Create User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(300), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        [Index(301), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        private DocTypes _DocType;
        [XafDisplayName("Type"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Appearance("DocType", Enabled = false)]
        [Index(201), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Target")]
        [Index(0)]
        [Appearance("DocTarget", FontColor = "Black")]
        public string DocTarget
        {
            get
            {
                string rtn = PMClass == null || PMClass.BoShortName == null ? "" : PMClass.BoShortName.Trim() == "-" ? "" : PMClass.BoShortName;
                if (PMClass != null && PMClass.BoCode != "-")
                {
                    if (rtn == "")
                        rtn = PMClass.BoCode;
                }
                if (rtn != "") rtn += "-";
                rtn = IsPreventiveMaintenance ? "Preventive Maintenance Work Order" : "Corrective Maintenance Work Order";
                if (PMClass != null)
                    if (PMClass.BoShortName != null)
                    {
                        if (PMClass.BoShortName.Trim() == "-" || PMClass.BoShortName.Trim() == "")
                        {
                            if (PMClass.BoCode.Trim() != "-")
                                rtn = "(" + PMClass.BoCode + ")" + rtn;
                        }
                        else
                        {
                            rtn = "(" + PMClass.BoShortName + ")" + rtn;
                        }
                    }

                return rtn;
            }
        }

        private PMClasses _PMClass;
        [ImmediatePostData]
        [XafDisplayName("PM Class"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[DataSourceProperty("EqClassList", DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("PMClass", Enabled = false)]
        [Browsable(false)]
        public virtual PMClasses PMClass
        {
            get { return _PMClass; }
            set
            {
                if (_PMClass != value)
                {
                    _PMClass = value;
                    OnPropertyChanged("PMClass");
                }
            }
        }

        private string _DocStatus;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Document Status")]
        [Appearance("DocStatus", Enabled = false)]
        public string DocStatus
        {
            get { return _DocStatus; }
            set
            {
                if (_DocStatus != value)
                {
                    _DocStatus = value;
                    OnPropertyChanged("DocStatus");
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

        private WorkRequests _WorkRequest;
        [XafDisplayName("Work Request No"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[DataSourceProperty("CreateUser.position", DataSourcePropertyIsNullMode.CustomCriteria)]
        [Appearance("WorkRequest", Enabled = false)]
        [Index(5), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public virtual WorkRequests WorkRequest
        {
            get { return _WorkRequest; }
            set
            {
                if (_WorkRequest != value)
                {
                    _WorkRequest = value;
                    OnPropertyChanged("WorkRequest");
                }
            }
        }

        private string _DocNum;
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Work Order No"), ToolTip("Enter Text")]
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

        private DateTime _DocDate;
        [Index(11), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("DisplayFormat", "{0:D}")] //LongDate
        //[ModelDefault("EditMask", "D")]
        //[ModelDefault("DisplayFormat", "{0:d}")] //ShortDate
        //[ModelDefault("EditMask", "d")]
        //[ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        //[ModelDefault("EditMask", "dd/MM/yyyy")]
        //[ModelDefault("DisplayFormat", "{0:f}")] //LongDate+ShortTime
        //[ModelDefault("EditMask", "f")]
        //[ModelDefault("DisplayFormat", "{0: ddd, dd MMMM yyyy hh:mm:ss tt}")]
        //[ModelDefault("EditMask", "ddd, dd MMMM yyyy hh:mm:ss tt")]
        [XafDisplayName("Work Order Date"), ToolTip("Enter Text")]
        [Appearance("DocDate", Enabled = false, Criteria = "IsContractorChecking")]
        public DateTime DocDate
        {
            get { return _DocDate; }
            set
            {
                if (_DocDate != value)
                {
                    _DocDate = value;
                    OnPropertyChanged("DocDate");
                }
            }
        }

        private DateTime? _PMDate;
        [XafDisplayName("PM Date")]
        [Index(12), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("PMDate", Enabled = false, Criteria = "not IsNew or not IsPreventiveMaintenance")]
        [Appearance("PMDateCon", Enabled = false, Criteria = "IsContractorChecking")]
        public DateTime? PMDate
        {
            get { return _PMDate; }
            set
            {
                if (_PMDate != value)
                {
                    _PMDate = value;
                    OnPropertyChanged("PMDate");
                }
            }
        }

        private string _RefNo;
        [Index(13), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("RefNo", Enabled = false, Criteria = "IsContractorChecking")]
        public string RefNo
        {
            get { return _RefNo; }
            set
            {
                if (_RefNo != value)
                {
                    _RefNo = value;
                    OnPropertyChanged("RefNo");
                }
            }
        }

        //private DateTime _WRTargetDate;
        [XafDisplayName("WR Target Date")]
        [Index(14), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        //[ModelDefault("EditMask", "dd/MM/yyyy")]
        //[Appearance("WRTargetDate", Enabled = false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public DateTime? WRTargetDate
        {
            get { return WorkRequest == null ? null : WorkRequest.WRTargetDate; }
            //set
            //{
            //    if (_WRTargetDate != value)
            //    {
            //        _WRTargetDate = value;
            //        OnPropertyChanged("WRTargetDate");
            //    }
            //}
        }


        ////private Priorities _Priority;
        //[XafDisplayName("WR Priority")]
        //[Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public Priorities WRPriority
        //{
        //    get { return WorkRequest == null ? null : WorkRequest.Priority; }
        //    //set
        //    //{
        //    //    if (_Priority != value)
        //    //    {
        //    //        _Priority = value;
        //    //        OnPropertyChanged("Priority");
        //    //    }
        //    //}
        //}

        private Priorities _Priority;
        [XafDisplayName("Priority")]
        [Index(31), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Priority", Enabled = false, Criteria = "IsContractorChecking")]
        public virtual Priorities Priority
        {
            get { return _Priority; }
            set
            {
                if (_Priority != value)
                {
                    _Priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }

        [XafDisplayName("WR Short Description")]
        [Index(40), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("WRRemarks", Enabled = false, Criteria = "IsContractorChecking")]
        //[FieldSizeAttribute(1024)]
        public string WRRemarks
        {
            get
            {
                string rtn = "";
                if (WorkRequest != null)
                    rtn = WorkRequest.Remarks;
                return rtn;
            }
        }

        [XafDisplayName("WR Long Description")]
        [Index(41), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[FieldSizeAttribute(1024)]
        [EditorAlias("HTML")]
        [Appearance("WRLongDescription", Enabled = false, Criteria = "IsContractorChecking")]
        public string WRLongDescription
        {
            get
            {
                string rtn = "";
                if (WorkRequest != null)
                    if (WorkRequest.DetailDescription != null)
                        rtn = WorkRequest.DetailDescription.LongDescription;
                return rtn;
            }
        }

        //private WRLongDescription _WRDetailDescription;
        //[Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[Index(41), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[XafDisplayName("WR Description")]
        //public virtual WRLongDescription WRDetailDescription
        //{
        //    get { return _WRDetailDescription; }
        //    set
        //    {
        //        if (_WRDetailDescription != value)
        //        {
        //            _WRDetailDescription = value;
        //            OnPropertyChanged("WRDetailDescription");
        //        }
        //    }
        //}

        private string _Remarks;
        //[FieldSizeAttribute(1024)]
        [XafDisplayName("WO Short Description")]
        [Index(50), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Remarks", Enabled = false, Criteria = "IsContractorChecking")]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                if (_Remarks != value)
                {
                    _Remarks = value;
                    OnPropertyChanged("Remarks");
                }
            }
        }

        private WOLongDescription _WorkDescription;
        [XafDisplayName("Work History")]
        [ImmediatePostData]
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [Index(53), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("WorkDescription", Enabled = false, Criteria = "IsClosed")]
        [Appearance("WorkDescription1", Enabled = false, Criteria = "IsContractorChecking")]
        public virtual WOLongDescription WorkDescription
        {
            get { return _WorkDescription; }
            set
            {
                if (_WorkDescription != value)
                {
                    _WorkDescription = value;
                    OnPropertyChanged("WorkDescription");
                }
            }
        }

        [XafDisplayName("Detail Work History")]
        [EditorAlias("HTML")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(54), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DescDetail
        {
            get
            {
                return WorkDescription == null ? "" : WorkDescription.LongDescription;
            }
        }


        private int _PlanManCount;
        [XafDisplayName("Estimate Man Count"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(60), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("PlanManCount", Enabled = false, Criteria = "IsContractorChecking")]
        public int PlanManCount
        {
            get { return _PlanManCount; }
            set
            {
                if (_PlanManCount != value)
                {
                    _PlanManCount = value;
                    OnPropertyChanged("PlanManCount");
                }
            }
        }

        private TimeSpan? _PlanManHour;
        [XafDisplayName("Estimate Man Hr (hh:mm)"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("EditMaskType", "DateTime")]
        [ModelDefault("EditMask", @"hh:mm")]
        [ModelDefault("DisplayFormat", "{0:hh\\:mm}")]
        [Index(61), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("PlanManHour", Enabled = false, Criteria = "IsContractorChecking")]
        public TimeSpan? PlanManHour
        {
            get { return _PlanManHour; }
            set
            {
                if (_PlanManHour != value)
                {
                    _PlanManHour = value;
                    OnPropertyChanged("PlanManHour");
                }
            }
        }


        private DateTime? _PlanStartDate;
        [XafDisplayName("Planning Start Date")]
        [Index(62), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("PlanStartDate", Enabled = false, Criteria = "not (IsWOPlanDateChecking or IsContractorChecking)")]
        [Appearance("PlanStartDate1", Enabled = false, Criteria = "Cancelled or IsClosed")]
        //[Appearance("PlanStartDate2", Enabled = false, Criteria = "not IsPreventiveMaintenance")]
        public DateTime? PlanStartDate
        {
            get { return _PlanStartDate; }
            set
            {
                if (_PlanStartDate != value)
                {
                    _PlanStartDate = value;
                    OnPropertyChanged("PlanStartDate");
                }
            }
        }

        private DateTime? _PlanEndDate;
        [XafDisplayName("Planning End Date")]
        [Index(63), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("PlanEndDate", Enabled = false, Criteria = "not (IsWOPlanDateChecking or IsContractorChecking)")]
        [Appearance("PlanEndDate1", Enabled = false, Criteria = "Cancelled or IsClosed")]
        //[Appearance("PlanEndDate2", Enabled = false, Criteria = "not IsPreventiveMaintenance")]
        public DateTime? PlanEndDate
        {
            get { return _PlanEndDate; }
            set
            {
                if (_PlanEndDate != value)
                {
                    _PlanEndDate = value;
                    OnPropertyChanged("PlanEndDate");
                }
            }
        }
        private DateTime? _ScheduleStartDate;
        [XafDisplayName("Schedule Start Date")]
        [Index(64), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("ScheduleStartDate", Enabled = false, Criteria = "DocPassed or Cancelled or IsClosed or Approved")]
        public DateTime? ScheduleStartDate
        {
            get { return _ScheduleStartDate; }
            set
            {
                if (_ScheduleStartDate != value)
                {
                    _ScheduleStartDate = value;
                    OnPropertyChanged("ScheduleStartDate");
                }
            }
        }
        private DateTime? _ScheduleEndDate;
        [XafDisplayName("Schedule End Date")]
        [Index(65), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("ScheduleEndDate", Enabled = false, Criteria = "DocPassed or Cancelled or IsClosed or Approved")]
        public DateTime? ScheduleEndDate
        {
            get { return _ScheduleEndDate; }
            set
            {
                if (_ScheduleEndDate != value)
                {
                    _ScheduleEndDate = value;
                    OnPropertyChanged("ScheduleEndDate");
                }
            }
        }

        private DateTime? _RescheduleStartDate;
        [XafDisplayName("Reschedule Start Date")]
        [Index(66), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("RescheduleStartDate", Enabled = false, Criteria = "not Approved")]
        public DateTime? RescheduleStartDate
        {
            get { return _RescheduleStartDate; }
            set
            {
                if (_RescheduleStartDate != value)
                {
                    _RescheduleStartDate = value;
                    OnPropertyChanged("RescheduleStartDate");
                }
            }
        }

        private DateTime? _RescheduleEndDate;
        [XafDisplayName("Reschedule End Date")]
        [Index(67), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("RescheduleEndDate", Enabled = false, Criteria = "not Approved")]
        public DateTime? RescheduleEndDate
        {
            get { return _RescheduleEndDate; }
            set
            {
                if (_RescheduleEndDate != value)
                {
                    _RescheduleEndDate = value;
                    OnPropertyChanged("RescheduleEndDate");
                }
            }
        }

        private DateTime? _ActualStartDate;
        [XafDisplayName("Actual Start Date")]
        [Index(68), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("ActualStartDate", Enabled = false, Criteria = "not Approved")]
        public DateTime? ActualStartDate
        {
            get { return _ActualStartDate; }
            set
            {
                if (_ActualStartDate != value)
                {
                    _ActualStartDate = value;
                    OnPropertyChanged("ActualStartDate");
                }
            }
        }

        private DateTime? _ActualEndDate;
        [XafDisplayName("Actual End Date")]
        [Index(69), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("ActualEndDate", Enabled = false, Criteria = "not Approved")]
        public DateTime? ActualEndDate
        {
            get { return _ActualEndDate; }
            set
            {
                if (_ActualEndDate != value)
                {
                    _ActualEndDate = value;
                    OnPropertyChanged("ActualEndDate");
                }
            }
        }


        private JobStatuses _JobStatus;
        [ImmediatePostData]
        [XafDisplayName("Job Status"), ToolTip("Enter Text")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(70), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("JobStatus", Enabled = false)]
        public virtual JobStatuses JobStatus
        {
            get { return _JobStatus; }
            set
            {
                if (_JobStatus != value)
                {
                    _JobStatus = value;
                    OnPropertyChanged("JobStatus");

                }
            }
        }

        //private string _JobRemarks;
        //[XafDisplayName("Job Remarks"), ToolTip("Enter Text")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[Index(71), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //public string JobRemarks
        //{
        //    get { return _JobRemarks; }
        //    set
        //    {
        //        if (_JobRemarks != value)
        //        {
        //            _JobRemarks = value;
        //            OnPropertyChanged("JobRemarks");
        //        }
        //    }
        //}

        private CheckLists _CheckList;
        [XafDisplayName("Checklist"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(72), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("CheckList", Enabled = false, Criteria = "IsContractorChecking")]
        public virtual CheckLists CheckList
        {
            get { return _CheckList; }
            set
            {
                if (_CheckList != value)
                {
                    _CheckList = value;
                    OnPropertyChanged("CheckList");
                }
            }
        }

        private string _CheckListName;
        [XafDisplayName("CheckList Ref"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleUniqueValue]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(73), VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("CheckListName", Enabled = false)]
        public string CheckListName
        {
            get { return _CheckListName; }
            set
            {
                if (_CheckListName != value)
                {
                    _CheckListName = value;
                    OnPropertyChanged("CheckListName");
                }
            }
        }

        private string _CheckListLink;
        [XafDisplayName("CheckList Link"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleUniqueValue]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(74), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("CheckListLink", Enabled = false)]
        public string CheckListLink
        {
            get { return _CheckListLink; }
            set
            {
                if (_CheckListLink != value)
                {
                    _CheckListLink = value;
                    OnPropertyChanged("CheckListLink");
                }
            }
        }

        private string _WorkInstruction;
        [XafDisplayName("Work Instruction"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(75), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("WorkInstruction", Enabled = false, Criteria = "IsContractorChecking")]
        public string WorkInstruction
        {
            get { return _WorkInstruction; }
            set
            {
                if (_WorkInstruction != value)
                {
                    _WorkInstruction = value;
                    OnPropertyChanged("WorkInstruction");
                }
            }
        }

        private PlannerGroups _AssignPlannerGroup;
        //[ImmediatePostData]
        [XafDisplayName("Planner Group"), ToolTip("Enter Text")]
        //[DataSourceProperty("PlannerList", DataSourcePropertyIsNullMode.SelectNothing)]
        //[DataSourceCriteria("[Roles][[Name] = 'Supervisor'] and [[position] = @This.position]")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("AssignPlannerGroup", Enabled = false, Criteria = "IsPR or DocPassed or Approved or Cancelled or IsClosed")]
        [Appearance("PlannerGroups1", Enabled = false, Criteria = "IsContractorChecking")]
        [Index(79), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public virtual PlannerGroups AssignPlannerGroup
        {
            get { return _AssignPlannerGroup; }
            set
            {
                if (_AssignPlannerGroup != value)
                {
                    _AssignPlannerGroup = value;
                    OnPropertyChanged("AssignPlannerGroup");
                }
            }
        }


        private PMSchedules _PMSchedule;
        [XafDisplayName("PM Schedule"), ToolTip("Enter Text")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(90), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("PMSchedule", Enabled = false)]
        public virtual PMSchedules PMSchedule
        {
            get { return _PMSchedule; }
            set
            {
                if (_PMSchedule != value)
                {
                    _PMSchedule = value;
                    OnPropertyChanged("PMSchedule");

                }
            }
        }


        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Index(100), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //public int OpCount
        //{
        //    get
        //    {
        //        if (Detail == null)
        //            return 0;
        //        return Detail.Sum(p => p.OpCount);
        //    }
        //}

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Index(101), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //public int OpDoneCount
        //{
        //    get
        //    {
        //        if (Detail == null)
        //            return 0;
        //        return Detail.Sum(p => p.OpDoneCount);
        //    }
        //}

        //[ModelDefault("DisplayFormat", "{0:f}")]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Index(110), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //public DateTime? ActDownTime
        //{
        //    get
        //    {
        //        if (Detail == null || Detail.Sum(p => p.OpCount) == 0)
        //            return null;

        //        return Detail.Min(p => p.EqDownTime);
        //    }
        //}
        //[ModelDefault("DisplayFormat", "{0:f}")]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Index(111), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //public DateTime? ActUpTime
        //{
        //    get
        //    {
        //        if (Detail == null || Detail.Sum(p => p.OpCount) == 0)
        //            return null;

        //        return Detail.Max(p => p.EqUpTime);
        //    }
        //}

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Actual Man Hr (hh:mm)")]
        [ModelDefault("EditMask", @"hh:mm")]
        [ModelDefault("DisplayFormat", "{0:hh\\:mm}")]
        [Index(112), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public TimeSpan ManHours
        {
            get
            {
                long rtn = 0;
                if (DetailManHours != null)
                    rtn = DetailManHours.Sum(p => p.ManHours);
                return TimeSpan.FromMinutes(rtn);
            }
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Actual Man Counts")]
        [Index(113), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public int ManCount
        {
            get
            {
                int rtn = 0;
                if (DetailManHours != null)
                    rtn = DetailManHours.Sum(p => p.ManCount);
                return rtn;
            }
        }
        private DateTime? _OriginalOLAFD;
        [XafDisplayName("Original LAFD")]
        [Index(114), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [XafDisplayName("Latest Allowed Finish Date")]
        [Index(115), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("ProposedLAFDValidDate", Enabled = false)]
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


        [Index(20), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(true)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Equipment
        {
            get
            {
                if (Detail.Count > 0)
                    return Detail.FirstOrDefault().Equipment.FullCode;
                if (Detail2.Count > 0)
                    return Detail2.FirstOrDefault().EquipmentComponent.ComponentFullCode;
                return "";
            }
        }

        [Index(21), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string EquipmentName
        {
            get
            {
                if (Detail.Count > 0)
                    return Detail.FirstOrDefault().EquipmentName;
                if (Detail2.Count > 0)
                    return "<C> " + Detail2.FirstOrDefault().EquipmentComponentName;
                return null;
            }
        }

        [Index(22), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public Criticalities Criticality
        {
            get
            {
                if (Detail.Count > 0)
                    return Detail.FirstOrDefault().Equipment.Criticality;
                if (Detail2.Count > 0)
                    return Detail2.FirstOrDefault().EquipmentComponent.Criticality;
                return null;
            }
        }

        //[Index(120), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public SystemUsers DocCreateUser
        //{
        //    get
        //    {
        //        if (DetailDocStatus.Count > 0)
        //            return DetailDocStatus.Where(p => p.DocStatus == DocumentStatus.Create).First().CreateUser;
        //        return null;
        //    }
        //}

        //[Index(121), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public DateTime? DocCreateDate
        //{
        //    get
        //    {
        //        if (DetailDocStatus.Count > 0)
        //            return DetailDocStatus.Where(p => p.DocStatus == DocumentStatus.Create).First().CreateDate;
        //        return null;
        //    }
        //}

        [Index(30), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsPR
        {
            get
            {
                bool rtn = false;
                if (DetailRequest.Count > 0)
                    if (DetailRequest.Where(p => !p.Cancelled).Count() > 0)
                        rtn = true;
                return rtn;
            }
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(999), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public bool JobValid
        {
            get { return true; }
        }

        private PMPatches _PMPatch;
        [XafDisplayName("PM Patch"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(201), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("PMPatch", Enabled = false)]
        public virtual PMPatches PMPatch
        {
            get { return _PMPatch; }
            set
            {
                if (_PMPatch != value)
                {
                    _PMPatch = value;
                    OnPropertyChanged("PMPatch");
                }
            }
        }

        private Companies _Company;
        [XafDisplayName("Company"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(201), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("Company", Enabled = false)]
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

        private bool _DocPassed;
        [XafDisplayName("Planner Passed"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(200), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("DocPassed", Enabled = false)]
        public bool DocPassed
        {
            get { return _DocPassed; }
            set
            {
                if (_DocPassed != value)
                {
                    _DocPassed = value;
                    OnPropertyChanged("DocPassed");
                }
            }
        }


        private bool _Approved;
        [XafDisplayName("Approved"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(201), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("Approved", Enabled = false)]
        public bool Approved
        {
            get { return _Approved; }
            set
            {
                if (_Approved != value)
                {
                    _Approved = value;
                    OnPropertyChanged("Approved");
                }
            }
        }

        private bool _Cancelled;
        [XafDisplayName("Cancelled"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(202), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("Cancelled", Enabled = false)]
        public bool Cancelled
        {
            get { return _Cancelled; }
            set
            {
                if (_Cancelled != value)
                {
                    _Cancelled = value;
                    OnPropertyChanged("Cancelled");
                }
            }
        }

        private bool _Rejected;
        [ImmediatePostData]
        [XafDisplayName("Rejected"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(203), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("Rejected", Enabled = false)]
        public bool Rejected
        {
            get { return _Rejected; }
            set
            {
                if (_Rejected != value)
                {
                    _Rejected = value;
                    OnPropertyChanged("Rejected");
                }
            }
        }

        private bool _IsClosed;
        [ImmediatePostData]
        [XafDisplayName("Closed"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(203), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("IsClosed", Enabled = false)]
        public bool IsClosed
        {
            get { return _IsClosed; }
            set
            {
                if (_IsClosed != value)
                {
                    _IsClosed = value;
                    OnPropertyChanged("IsClosed");
                }
            }
        }
        private bool _IsDeviationApproved;
        //[ImmediatePostData]
        [XafDisplayName("Overdue OLFD with Approved Deviation"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(204), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[Appearance("IsDeviationApproved", Enabled = false)]
        [Appearance("IsDeviationApproved", Enabled = false, Criteria = "IsContractorChecking")]
        public bool IsDeviationApproved
        {
            get { return _IsDeviationApproved; }
            set
            {
                if (_IsDeviationApproved != value)
                {
                    _IsDeviationApproved = value;
                    OnPropertyChanged("IsDeviationApproved");
                }
            }
        }
        private bool _IsDeviationNotApproved;
        //[ImmediatePostData]
        [XafDisplayName("Overdue OLFD without Approved Deviation"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(205), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[Appearance("IsDeviationNotApproved", Enabled = false)]
        [Appearance("IsDeviationNotApproved", Enabled = false, Criteria = "IsContractorChecking")]
        public bool IsDeviationNotApproved
        {
            get { return _IsDeviationNotApproved; }
            set
            {
                if (_IsDeviationNotApproved != value)
                {
                    _IsDeviationNotApproved = value;
                    OnPropertyChanged("IsDeviationNotApproved");
                }
            }
        }
        private Contractors _Contractor;
        [XafDisplayName("Contractor"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(206), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[DataSourceProperty("ItemCode.DetailBO", DataSourcePropertyIsNullMode.CustomCriteria, "Outlet = '@This.Outlet' and OpenQty > 0")]
        //[DataSourceProperty("CreateUser.Contractor", DataSourcePropertyIsNullMode.CustomCriteria)]
        [Appearance("Contractor", Enabled = false, Criteria = "IsContractorChecking")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public virtual Contractors Contractor
        {
            get { return _Contractor; }
            set
            {
                if (_Contractor != value)
                {
                    _Contractor = value;
                    OnPropertyChanged("Contractor");
                }
            }
        }

        //private string _RejectRemarks;
        //[Index(204), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("RejectRemarks", Enabled = false, Criteria = "not Rejected")]
        ////[FieldSizeAttribute(1024)]
        //public string RejectRemarks
        //{
        //    get { return _RejectRemarks; }
        //    set
        //    {
        //        if (_RejectRemarks != value)
        //        {
        //            _RejectRemarks = value;
        //            OnPropertyChanged("RejectRemarks");
        //        }
        //    }
        //}

        private bool _IsPreventiveMaintenance;
        [XafDisplayName("PreventiveMaintenance"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
        public bool IsPreventiveMaintenance
        {
            get { return _IsPreventiveMaintenance; }
            set
            {
                if (_IsPreventiveMaintenance != value)
                {
                    _IsPreventiveMaintenance = value;
                    OnPropertyChanged("IsPreventiveMaintenance");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsSupervisorChecking { get; set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsPlannerChecking { get; set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsApproverChecking { get; set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsWPSChecking { get; set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsCancelUserChecking { get; set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsWOPlanDateChecking { get; set; }

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsContractorChecking { get; set; }
        // Collection property:
        //private BindingList<EquipmentList> _EquipmentTable;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public virtual IList<EquipmentList> EquipmentTable
        //{
        //    get
        //    {
        //        var db = new AssetsManagementEFDbContext();

        //        var innerJoinQuery =
        //            (
        //        from woeq in db.WorkOrderEquipments
        //        join woeqcom in db.WorkOrderEqComponents on new { id = woeq.WorkOrder.ID, woeq.Equipment.ID } equals new { id = woeqcom.WorkOrder.ID, woeqcom.Equipment.ID } into eqcom
        //        from result in eqcom.DefaultIfEmpty()
        //        where woeq.WorkOrder.ID == this.ID
        //        select new EquipmentList()
        //        { ID = 0, DocumentNo = this.DocNum, WorkRemarks = this.Remarks, PlannerGroup = this.AssignPlannerGroup.BoName, EquipmentCode = woeq.Equipment.BoFullCode, EquipmentName = woeq.Equipment.BoName, EquipmentComponentCode = result.EquipmentComponent.BoFullCode, EquipmentComponentName = result.EquipmentComponent.BoName }
        //        ).OrderBy(c => c.EquipmentCode + c.EquipmentComponentCode);

        //        _EquipmentTable = new BindingList<EquipmentList>(innerJoinQuery.ToList());

        //        //_EquipmentTable = new BindingList<EquipmentList>(innerJoinQuery.ToList<EquipmentList>());
        //        //int id = 0;
        //        //foreach (var dtl in innerJoinQuery)
        //        //{
        //        //    id++;
        //        //    _EquipmentTable.Add(new EquipmentList()
        //        //    {
        //        //        ID = id,
        //        //        WorkOrderNo = dtl.WorkOrderNo,
        //        //        WorkOrderRemarks = dtl.WorkOrderRemarks,
        //        //        Equipment = dtl.Equipment
        //        //    });

        //        //}
        //        return _EquipmentTable;
        //    }
        //}


        [XafDisplayName("Man Hours")]
        public virtual IList<WorkOrderManHours> DetailManHours { get; set; }

        [XafDisplayName("Attachment")]
        public virtual IList<WorkOrderAttachments> DetailAttachment { get; set; }

        [XafDisplayName("Photo")]
        public virtual IList<WorkRequestPhotos> DetailPhoto { get; set; }

        [XafDisplayName("Equipment List")]
        [Appearance("Detail", Enabled = false)]
        public virtual IList<WorkOrderEquipments> Detail { get; set; }

        [XafDisplayName("Component List")]
        [Appearance("Detail2", Enabled = false)]
        public virtual IList<WorkOrderEqComponents> Detail2 { get; set; }

        [XafDisplayName("Purchase Request")]
        [Appearance("DetailRequest", Enabled = false)]
        public virtual IList<PurchaseRequests> DetailRequest { get; set; }

        [XafDisplayName("Job Status List")]
        [Appearance("DetailJobStatus", Enabled = false)]
        public virtual IList<WorkOrderJobStatuses> DetailJobStatus { get; set; }

        [XafDisplayName("Doc Status List")]
        [Appearance("DetailDocStatus", Enabled = false)]
        public virtual IList<WorkOrderDocStatuses> DetailDocStatus { get; set; }

        [XafDisplayName("Doc Planner Group List")]
        [Appearance("DetailDocPG", Enabled = false)]
        public virtual IList<WorkOrderDocPGs> DetailDocPG { get; set; }

        [XafDisplayName("Deviation")]
        [Appearance("DetailDeviation", Enabled = false)]
        public virtual IList<Deviation2025> DetailDeviation { get; set; }

        //private IList<Positions> _PlannerList;
        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public virtual IList<Positions> PlannerList
        //{
        //    get
        //    {
        //        if (PlannerGroup == null || objectSpace == null)
        //        {
        //            _PlannerList = null;
        //        }
        //        else
        //        {
        //            RefreshPlanner();
        //        }
        //        return _PlannerList;
        //    }
        //}
        //private void RefreshPlanner()
        //{
        //    string sql = "";
        //    if (IsPreventiveMaintenance)
        //        sql = "[PlannerGroup.ID] = '" + PlannerGroup.ID + "'";
        //    else
        //    {
        //        sql = "[IsPlanner] and ( (" + IsPreventiveMaintenance + " and [IsPreventiveMaintenance]) or (not " + IsPreventiveMaintenance + " and [IsCorrectiveMaintenance]) ) and ( (" + IsSurveillance + " and [IsSurveillance]) or (not " + IsSurveillance + " and [IsTechnical]) )";
        //    }
        //    _PlannerList = objectSpace.GetObjects<Positions>(CriteriaOperator.Parse(sql));
        //    //_Planner = objectSpace.GetObjects<SystemUsers>(CriteriaOperator.Parse("[Position][ID] = " + AssignPosition.ID + " and [Roles][[Name] = '" + GeneralSettings.WPSRole + "']"));
        //    //_Supervisor = objectSpace.GetObjects<SystemUsers>(new ContainsOperator("Roles", new BinaryOperator("Name", "Supervisor", BinaryOperatorType.Equal)));
        //}


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();

            //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            //User currentUser = objectSpace.GetObjectByKey<User>(SecuritySystem.CurrentUserId);
            //AllowedSave = true;
            IsPreventiveMaintenance = false;
            IsSupervisorChecking = false;
            IsPlannerChecking = false;
            IsApproverChecking = false;
            IsWPSChecking = false;
            IsWOPlanDateChecking = false;
            IsContractorChecking = false;

            IsNew = true;
            CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;

            DocDate = DateTime.Today;
            Company = objectSpace.FindObject<Companies>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.HQCompany));

            PMClass = objectSpace.FindObject<PMClasses>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.defaultcode));
            DocType = objectSpace.FindObject<DocTypes>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.WoBreakdown));
            //AssignPlannerGroup = objectSpace.FindObject<PlannerGroups>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.defaultcode));

            PlanManHour = TimeSpan.FromMinutes(0);

        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
            if (AssignPlannerGroup != null)
                _OldPlannerGroup = AssignPlannerGroup.ID;
            //AllowedSave = true;
            //if (this.Cancelled || this.IsClosed)
            //    AllowedSave = false;

            IsNew = false;
            IsSupervisorChecking = false;
            IsPlannerChecking = false;
            IsApproverChecking = false;
            IsWPSChecking = false;
            IsCancelUserChecking = false;
            IsWOPlanDateChecking = false;
            IsContractorChecking = false;

            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            if (user != null)
            {
                this.IsContractorChecking = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;
                if (IsContractorChecking)
                {
                    return;
                }

                Positions position = objectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                if (position != null && AssignPlannerGroup != null)
                {
                    if (this.AssignPlannerGroup.DetailPosition.Where(p => p.ID == position.ID).Count() > 0)
                    {
                        if (user.Roles.Where(p => p.Name == GeneralSettings.SupervisorRole).Count() > 0)
                            this.IsSupervisorChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0)
                            this.IsApproverChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0)
                            this.IsPlannerChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0)
                            this.IsWPSChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.CancelWORole).Count() > 0)
                            this.IsCancelUserChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.WOPlanDateRole).Count() > 0)
                            this.IsWOPlanDateChecking = true;
                    }
                }
            }

        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
            if (IsNew)
            {
                CompanyDocs doc = objectSpace.FindObject<CompanyDocs>(CriteriaOperator.Parse("Company.ID=? and DocType.ID=?", Company.ID, DocType.ID));
                DocNumSeq = doc.NextDocNo;
                DocNum = DocNumSeq.ToString();
                doc.NextDocNo++;

                //DocNumSeq = com.n

                if (WorkRequest != null)
                {
                    this.WorkRequest.Approved = true;
                    this.WorkRequest.DocPassed = false;
                    WorkRequestDocStatuses dswr = objectSpace.CreateObject<WorkRequestDocStatuses>();
                    dswr.DocStatus = DocumentStatus.Approved;
                    dswr.DocRemarks = "";
                    dswr.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    dswr.CreateDate = DateTime.Now;
                    dswr.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                    dswr.UpdateDate = DateTime.Now;
                    WorkRequest.DetailDocStatus.Add(dswr);

                }

                WorkOrderJobStatuses js = objectSpace.CreateObject<WorkOrderJobStatuses>();
                if (IsPreventiveMaintenance)
                {
                    this.JobStatus = objectSpace.FindObject<JobStatuses>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.InitPMJobStatus));
                    js.JobStatus = objectSpace.FindObject<JobStatuses>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.InitPMJobStatus));
                }
                else
                {
                    this.JobStatus = objectSpace.FindObject<JobStatuses>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.InitCMJobStatus));
                    js.JobStatus = objectSpace.FindObject<JobStatuses>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.InitCMJobStatus));
                }

                js.JobRemarks = "";
                js.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                js.CreateDate = DateTime.Now;
                js.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                js.UpdateDate = DateTime.Now;
                this.DetailJobStatus.Add(js);

                WorkOrderDocStatuses ds = objectSpace.CreateObject<WorkOrderDocStatuses>();
                ds.DocStatus = DocumentStatus.Create;
                ds.DocRemarks = "";
                ds.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                ds.CreateDate = DateTime.Now;
                ds.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                ds.UpdateDate = DateTime.Now;
                this.DetailDocStatus.Add(ds);

            }
            else
            {
                if (_OldPlannerGroup > 0 && AssignPlannerGroup != null)
                {
                    if (AssignPlannerGroup.ID != _OldPlannerGroup)
                    {
                        WorkOrderDocPGs pg = objectSpace.CreateObject<WorkOrderDocPGs>();
                        pg.OldPlannerGroup = objectSpace.GetObjectByKey<PlannerGroups>(_OldPlannerGroup);
                        pg.NewPlannerGroup = objectSpace.GetObjectByKey<PlannerGroups>(AssignPlannerGroup.ID);
                        pg.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                        pg.CreateDate = DateTime.Now;
                        pg.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                        pg.UpdateDate = DateTime.Now;
                        this.DetailDocPG.Add(pg);

                    }
                }

            }

            UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            UpdateDate = DateTime.Now;
            if (this.PlanEndDate != null)
            {
                if (this.OriginalOLAFD == null)
                    this.OriginalOLAFD = this.PlanEndDate;
                if (this.ProposedLAFDValidDate == null)
                    this.ProposedLAFDValidDate = this.PlanEndDate;
            }

            IsNew = false;

            string rtn = "";
            if (IsClosed)
                rtn = "WO Closed";
            else if (Rejected)
                rtn = "Rejected";
            else if (Approved)
                rtn = "Approved";
            else if (Cancelled)
                rtn = "Cancelled";
            else if (DocPassed)
                rtn = "Passed";
            else
            {
                if (IsPreventiveMaintenance)
                    if (JobStatus != null)
                        if (JobStatus.BoCode == GeneralSettings.InitPMJobStatus)
                            rtn = "Waiting Acknowledge";
                        else
                            rtn = "Planner Pending";
                    else
                        rtn = "Waiting Acknowledge";
                else
                    rtn = "Planner Pending";
            }
            if (DocStatus != rtn)
                DocStatus = rtn;

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
