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
    // Register this entity in the DbContext using the "public DbSet<PurchaseQuotations> PurchaseQuotationss { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("Work Requests")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DocNum")]
    [XafDisplayName("Work Request")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("EditRecord", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsContractorChecking")]
    [RuleCriteria("WorkRequestsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[RuleCriteria("WorkRequestsSaveRule", DefaultContexts.Save, "isDetailValid", "There are no details in this document.")]   
    [RuleCriteria("WorkRequestsSaveRule", DefaultContexts.Save, "not (IsDeviationApproved and IsDeviationNotApproved)", "With & Without MOC approval cannot exist at the same time.")]
    //[RuleCriteria("WorkOrdersSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    public class WorkRequests : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private Int32 _OldPlannerGroup = 0;
        public WorkRequests()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.Detail = new List<WorkRequestEquipments>();
            this.Detail2 = new List<WorkRequestEqComponents>();
            this.Detail3 = new List<WorkOrders>();
            this.DetailDocStatus = new List<WorkRequestDocStatuses>();
            this.DetailAttachment = new List<WorkRequestAttachments>();
            this.DetailPhoto = new List<WorkRequestPhotos>();
            this.DetailDocPG = new List<WorkRequestDocPGs>();
            this.DetailDeviation = new List<DeviationWorkRequests>();

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

        private bool _AllowedSave;
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool AllowedSave
        {
            get { return _AllowedSave; }
            set
            {
                if (_AllowedSave != value)
                {
                    _AllowedSave = value;
                    OnPropertyChanged("AllowedSave");
                }
            }
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
        [XafDisplayName("Doc Type"), ToolTip("Enter Text")]
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


        private string _DocNum;
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Work Request No"), ToolTip("Enter Text")]
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
        [XafDisplayName("Work Request Date"), ToolTip("Enter Text")]
        [Appearance("DocDate", Enabled = false, Criteria = "not IsNew")]
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

        private string _RefNo;
        [Index(12), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("RefNo", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Approved")]        
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

        private DateTime? _WRTargetDate;
        [XafDisplayName("Target Date")]
        [Index(13), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        //[ModelDefault("EditMask", "dd/MM/yyyy")]
        [Appearance("WRTargetDate", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Approved")]
        public DateTime? WRTargetDate
        {
            get { return _WRTargetDate; }
            set
            {
                if (_WRTargetDate != value)
                {
                    _WRTargetDate = value;
                    OnPropertyChanged("WRTargetDate");
                }
            }
        }

        private string _Remarks;
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[FieldSizeAttribute(1024)]
        [XafDisplayName("Short Description")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("Remarks", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Approved")]
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

        private WRLongDescription _DetailDescription;
        [ImmediatePostData]
        [XafDisplayName("Long Description")]
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        [Index(31), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("DetailDescription", Enabled = false, Criteria = "(not IsNew and not IsRequestorChecking) or DocPassed or Approved")]
        public virtual WRLongDescription DetailDescription
        {
            get { return _DetailDescription; }
            set
            {
                if (_DetailDescription != value)
                {
                    _DetailDescription = value;
                    OnPropertyChanged("DetailDescription");
                }
            }
        }

        [XafDisplayName("Description Detail")]
        [EditorAlias("HTML")]
        [Index(32), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string DescDetail
        {
            get
            {
                return DetailDescription == null ? "" : DetailDescription.LongDescription;
            }
        }


        private Priorities _Priority;
        [Appearance("Priority", Enabled = false, Criteria = "Approved")]
        [Index(40), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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

        private PlannerGroups _PlannerGroup;
        [XafDisplayName("Planner Group"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("PlannerGroup", Enabled = false, Criteria = "Approved")]
        [Index(50), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public virtual PlannerGroups PlannerGroup
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

        //[Index(40), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        //[Index(41), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        private Companies _Company;
        [XafDisplayName("Company"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
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
        [XafDisplayName("Requestor Passed"), ToolTip("Enter Text")]
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
        private bool _IsDeviationApproved;
        //[ImmediatePostData]
        [XafDisplayName("Work with Approved MOC"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(204), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[Appearance("IsDeviationApproved", Enabled = false)]
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
        [XafDisplayName("Work without Approved MOC"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(205), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        //[Appearance("IsDeviationNotApproved", Enabled = false)]
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

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsSupervisorChecking { get; set; }
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsRequestorChecking { get; set; }
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsPlannerChecking { get; set; }
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsApproverChecking { get; set; }
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsCancelUserChecking { get; set; }
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
        //        from woeq in db.WorkRequestEquipments
        //        join woeqcom in db.WorkRequestEqComponents on new { id = woeq.WorkRequest.ID, woeq.Equipment.ID } equals new { id = woeqcom.WorkRequest.ID, woeqcom.Equipment.ID } into eqcom
        //        from result in eqcom.DefaultIfEmpty()
        //        where woeq.WorkRequest.ID == this.ID
        //        select new EquipmentList()
        //        { ID = 0, DocumentNo = this.DocNum, WorkRemarks = this.Remarks, PlannerGroup = this.PlannerGroup.BoName, EquipmentCode = woeq.Equipment.BoFullCode, EquipmentName = woeq.Equipment.BoName, EquipmentComponentCode = result.EquipmentComponent.BoFullCode, EquipmentComponentName = result.EquipmentComponent.BoName }
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

        [XafDisplayName("Equipment List")]
        [Appearance("Detail", Enabled = false)]
        public virtual IList<WorkRequestEquipments> Detail { get; set; }

        [XafDisplayName("Component List")]
        [Appearance("Detail2", Enabled = false)]
        public virtual IList<WorkRequestEqComponents> Detail2 { get; set; }

        [XafDisplayName("Work Order")]
        [Appearance("Detail3", Enabled = false)]
        public virtual IList<WorkOrders> Detail3 { get; set; }

        [XafDisplayName("Attachment")]
        public virtual IList<WorkRequestAttachments> DetailAttachment { get; set; }

        [XafDisplayName("Photo")]
        public virtual IList<WorkRequestPhotos> DetailPhoto { get; set; }

        [XafDisplayName("Doc Status List")]
        [Appearance("DetailDocStatus", Enabled = false)]
        public virtual IList<WorkRequestDocStatuses> DetailDocStatus { get; set; }

        [XafDisplayName("Doc Planner Group List")]
        [Appearance("DetailDocPG", Enabled = false)]
        public virtual IList<WorkRequestDocPGs> DetailDocPG { get; set; }

        [XafDisplayName("MOC")]
        //[Appearance("DetailDeviation", Enabled = false)]
        public virtual IList<DeviationWorkRequests> DetailDeviation { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();

            //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            //User currentUser = objectSpace.GetObjectByKey<User>(SecuritySystem.CurrentUserId);
            _AllowedSave = true;
            IsSupervisorChecking = false;
            IsRequestorChecking = false;
            IsPlannerChecking = false;
            IsApproverChecking = false;
            IsNew = true;
            CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;

            DocDate = DateTime.Today;
            Company = objectSpace.FindObject<Companies>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.HQCompany));

            DocType = objectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.WR));

        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
            if (PlannerGroup != null)
                _OldPlannerGroup = PlannerGroup.ID;
            AllowedSave = true;
            if (this.Cancelled || this.Approved)
                AllowedSave = false;

            IsNew = false;
            IsSupervisorChecking = false;
            IsRequestorChecking = false;
            IsPlannerChecking = false;
            IsApproverChecking = false;
            IsCancelUserChecking = false;
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            if (user != null)
            {
                this.IsContractorChecking = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;
                if (IsContractorChecking)
                {
                    return;
                }

                if (this.CreateUser != null)
                    if (user.Roles.Where(p => p.Name == GeneralSettings.RequestorRole).Count() > 0 && user.ID == this.CreateUser.ID)
                        this.IsRequestorChecking = true;

                Positions position = objectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                if (position != null && this.PlannerGroup != null)
                {
                    if (this.PlannerGroup.DetailPosition.Where(p => p.ID == position.ID).Count() > 0)
                    {
                        if (user.Roles.Where(p => p.Name == GeneralSettings.WRSupervisorRole).Count() > 0)
                            this.IsSupervisorChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0)
                            this.IsPlannerChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0)
                            this.IsApproverChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.CancelWRRole).Count() > 0)
                            this.IsCancelUserChecking = true;
                    }
                }
                //Positions position = objectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                //if (position != null)
                //{
                //    if (position.IsApprover)
                //        this.IsApproverChecking = true;

                //    if (position.IsPlanner)
                //        this.IsPlannerChecking = true;
                //}
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
                WorkRequestDocStatuses ds = objectSpace.CreateObject<WorkRequestDocStatuses>();
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
                if (_OldPlannerGroup > 0 && PlannerGroup != null)
                {
                    if (PlannerGroup.ID != _OldPlannerGroup)
                    {
                        WorkRequestDocPGs pg = objectSpace.CreateObject<WorkRequestDocPGs>();
                        pg.OldPlannerGroup = objectSpace.GetObjectByKey<PlannerGroups>(_OldPlannerGroup);
                        pg.NewPlannerGroup = objectSpace.GetObjectByKey<PlannerGroups>(PlannerGroup.ID);
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

            IsNew = false;

            string rtn = "";
            if (Rejected)
                rtn = "Rejected";
            else if (Approved)
                rtn = "Approved";
            else if (Cancelled)
                rtn = "Cancelled";
            else if (DocPassed)
                rtn = "Passed";
            else
                rtn = "Requestor Pending";
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
