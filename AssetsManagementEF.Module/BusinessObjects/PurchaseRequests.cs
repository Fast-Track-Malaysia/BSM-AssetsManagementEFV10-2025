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
    [NavigationItem("Purchase Requests")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DocNum")]
    [Appearance("EditRecord", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsContractorChecking")]
    //[RuleCriteria("PurchaseRequestsSaveRule", DefaultContexts.Save, "AllowedContractDocSave", "Contract is not valid.")]
    //[RuleCriteria("PurchaseRequestsSaveRule2", DefaultContexts.Save, "AllowedContractSave", "PR Date is out of Contract Date.")]
    //[RuleCriteria("PurchaseRequestsSaveRule3", DefaultContexts.Save, "DetailValid", "There are no details in this document.")]
    //[RuleCriteria("PurchaseRequestsSaveRule4", DefaultContexts.Save, "SourceValid", "This Document has no source.")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PurchaseRequests : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public PurchaseRequests()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.Detail = new List<PurchaseRequestDtls>();
            this.DetailAttachment = new List<PurchaseRequestAttachments>();
            this.DetailDocStatus = new List<PurchaseRequestDocStatuses>();

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

        private bool _DetailAllowedSave;
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool DetailAllowedSave
        {
            get { return _DetailAllowedSave; }
            set
            {
                if (_DetailAllowedSave != value)
                {
                    _DetailAllowedSave = value;
                    OnPropertyChanged("DetailAllowedSave");
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
        //        return detailValid;
        //    }
        //}
        public bool isSourceValid()
        {
            bool sourceValid = false;
            if (WorkOrder != null)
            {
                sourceValid = true;
            }
            return sourceValid;
        }

        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool SourceValid
        //{
        //    get
        //    {
        //        bool sourceValid = false;
        //        if (WorkOrder != null)
        //        {
        //            sourceValid = true;
        //        }
        //        return sourceValid;
        //    }
        //}
        public bool isAllowedContractSave()
        {
            if (ContractDoc != null)
            {
                if (ContractDoc.StartDate != null)
                    if (DocDate < ContractDoc.StartDate)
                        return false;
                if (ContractDoc.EndDate != null)
                    if (DocDate > ContractDoc.EndDate)
                        return false;
            }

            return true;
        }
        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool AllowedContractSave
        //{
        //    get
        //    {
        //        if (ContractDoc != null)
        //        {
        //            if (ContractDoc.StartDate != null)
        //                if (DocDate < ContractDoc.StartDate) 
        //                    return false;
        //            if (ContractDoc.EndDate != null)
        //                if (DocDate > ContractDoc.EndDate)
        //                    return false;      
        //        }

        //        return true;
        //    }
        //}
        public bool isAllowedContractDocSave()
        {
            if (ContractDoc != null)
            {
                if (!ContractDoc.IsActive)
                    return false;
            }

            return true;
        }
        //[Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //public bool AllowedContractDocSave
        //{
        //    get
        //    {
        //        if (ContractDoc != null)
        //        {
        //            if (!ContractDoc.IsActive)
        //                return false;
        //        }

        //        return true;
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

        private ContractDocs _ContractDoc;
        [XafDisplayName("Contract"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(0), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[DataSourceProperty("ItemCode.DetailBO", DataSourcePropertyIsNullMode.CustomCriteria, "Outlet = '@This.Outlet' and OpenQty > 0")]
        //[DataSourceProperty("CreateUser.Contractor", DataSourcePropertyIsNullMode.CustomCriteria)]
        [Appearance("ContractDoc", Criteria = "Not IsNew", Enabled = false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public virtual ContractDocs ContractDoc
        {
            get { return _ContractDoc; }
            set
            {
                if (_ContractDoc != value)
                {
                    _ContractDoc = value;
                    OnPropertyChanged("ContractDoc");

                    if (_ContractDoc != null)
                    {
                        if (IsNew)
                        {
                            _Contractor = objectSpace.FindObject<Contractors>(CriteriaOperator.Parse("ID=?", value.Contractor.ID));
                            OnPropertyChanged("Contractor");
                            //ContractDocs doc = objectSpace.FindObject<ContractDocs>(CriteriaOperator.Parse("ID=?", value.ID));

                            //foreach (ContractDocDtls dtl in doc.Detail)
                            //{
                            //    PurchaseRequestDtls prdtl = objectSpace.CreateObject<PurchaseRequestDtls>();
                            //    if (dtl.ItemMaster != null)
                            //        prdtl.ItemMaster = objectSpace.FindObject<ItemMasters>(CriteriaOperator.Parse("ID=?", dtl.ItemMaster.ID));

                            //    prdtl.ItemDesc = dtl.ItemDesc;
                            //    prdtl.QTY = dtl.QTY;
                            //    prdtl.Price = dtl.Price;
                            //    Detail.Add(prdtl);
                            //}
                            //OnPropertyChanged("Detail");
                        }

                    }

                }
            }
        }

        private Contractors _Contractor;
        [XafDisplayName("Contractor"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[DataSourceProperty("ItemCode.DetailBO", DataSourcePropertyIsNullMode.CustomCriteria, "Outlet = '@This.Outlet' and OpenQty > 0")]
        //[DataSourceProperty("CreateUser.Contractor", DataSourcePropertyIsNullMode.CustomCriteria)]
        [Appearance("Contractor", Enabled = false, Criteria = "ContractDoc is not null or not IsNew")]
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

        [Index(11), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Contract Start"), ToolTip("Enter Text")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public DateTime? StartDate
        {
            get { return ContractDoc == null? null: ContractDoc.StartDate; }
        }

        [Index(12), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Contract End"), ToolTip("Enter Text")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public DateTime? EndDate
        {
            get { return ContractDoc == null ? null : ContractDoc.EndDate; }
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
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Purchase Request No"), ToolTip("Enter Text")]
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
        [Index(21), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Purchase Request Date"), ToolTip("Enter Text")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("DocDate", Enabled = false, Criteria = "DocPassed or DocPosted or Cancelled")]
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
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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

        private WorkOrders _WorkOrder;
        [XafDisplayName("Work Order No"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(40), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
                }
            }
        }

        private PlannerGroups _PlannerGroup;
        [XafDisplayName("Planner Group"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("PlannerGroup", Enabled = false)]
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

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Request Amount")]
        [Index(60), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public decimal TotalAmount
        {
            get
            {
                decimal rtn = 0;
                if (Detail != null)
                    rtn = Detail.Sum(p => p.Amount);
                return rtn;
            }
        }

        private Companies _Company;
        [XafDisplayName("Company"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(201), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
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

        private string _DocStatus;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Document Status")]
        [Appearance("DocStatusColor", FontColor = "Red", Criteria = "SAPPending")]
        [Index(40), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
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

        [Browsable(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool SAPPending
        {
            get
            {
                bool rtn = false;
                if (WorkOrder != null && WorkOrder.Approved)
                {
                    if (this.Cancelled || this.DocPosted)
                    { }
                    else
                        rtn = true;
                }
                return rtn;
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

        // ***********PR no need approver at this moment***********
        private bool _Approved;
        [XafDisplayName("Approved"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
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
        // ***********PR no need approver at this moment***********

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

        private bool _DocPosted;
        [ImmediatePostData]
        [XafDisplayName("Posted"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(203), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("DocPosted", Enabled = false)]
        public bool DocPosted
        {
            get { return _DocPosted; }
            set
            {
                if (_DocPosted != value)
                {
                    _DocPosted = value;
                    OnPropertyChanged("DocPosted");
                }
            }
        }

        private int _RevisionNo;
        [XafDisplayName("Revision No"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
        [Appearance("RevisionNo", Enabled = false)]
        public int RevisionNo
        {
            get { return _RevisionNo; }
            set
            {
                if (_RevisionNo != value)
                {
                    _RevisionNo = value;
                    OnPropertyChanged("RevisionNo");
                }
            }
        }

        private Int32 _OriginID;
        [XafDisplayName("Origin ID"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
        [Appearance("OriginID", Enabled = false)]
        public Int32 OriginID
        {
            get { return _OriginID; }
            set
            {
                if (_OriginID != value)
                {
                    _OriginID = value;
                    OnPropertyChanged("OriginID");
                }
            }
        }

        private string _OriginRejectRemarks;
        [XafDisplayName("Origin Reject Remarks"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
        [Appearance("OriginRejectRemarks", Enabled = false)]
        public string OriginRejectRemarks
        {
            get { return _OriginRejectRemarks; }
            set
            {
                if (_OriginRejectRemarks != value)
                {
                    _OriginRejectRemarks = value;
                    OnPropertyChanged("OriginRejectRemarks");
                }
            }
        }

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
        public bool IsContractorChecking { get; set; }

        // Collection property:
        public virtual IList<PurchaseRequestDtls> Detail { get; set; }

        [XafDisplayName("Attachment")]
        public virtual IList<PurchaseRequestAttachments> DetailAttachment { get; set; }

        [XafDisplayName("Doc Status")]
        [Appearance("DetailDocStatus", Enabled = false)]
        public virtual IList<PurchaseRequestDocStatuses> DetailDocStatus { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();

            //SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            //User currentUser = objectSpace.GetObjectByKey<User>(SecuritySystem.CurrentUserId);
            DetailAllowedSave = true;
            IsNew = true;
            RevisionNo = 0;
            OriginID = 0;
            IsPreventiveMaintenance = false;

            DocDate = DateTime.Today;
            CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;
            Company = objectSpace.FindObject<Companies>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.HQCompany));

            DocType = objectSpace.FindObject<DocTypes>(new BinaryOperator("BoCode", GeneralSettings.PurchaseRequest));

            foreach (Contractors dtl in CreateUser.Contractor)
            {
                Contractor = objectSpace.GetObjectByKey<Contractors>(dtl.ID);
                break;
            }


        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
            DetailAllowedSave = true;
            if (this.DocPassed || this.Cancelled || this.DocPosted)
                DetailAllowedSave = false;

            IsNew = false;
            IsApproverChecking = false;
            IsWPSChecking = false;
            IsCancelUserChecking = false;
            SystemUsers user = (SystemUsers)SecuritySystem.CurrentUser;
            if (user != null)
            {
                this.IsContractorChecking = user.Roles.Where(p => p.Name == GeneralSettings.ContractorRole).Count() > 0 ? true : false;
                if (IsContractorChecking)
                {
                    return;
                }
                Positions position = objectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", user.ID));
                if (position != null && PlannerGroup != null)
                {
                    if (this.PlannerGroup.DetailPosition.Where(p => p.ID == position.ID).Count() > 0)
                    {
                        if (user.Roles.Where(p => p.Name == GeneralSettings.ApproverRole).Count() > 0)
                            this.IsApproverChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.PlannerRole).Count() > 0)
                            this.IsPlannerChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.WPSRole).Count() > 0)
                            this.IsWPSChecking = true;

                        if (user.Roles.Where(p => p.Name == GeneralSettings.CancelPRRole).Count() > 0)
                            this.IsCancelUserChecking = true;
                    }
                }
            }
        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
            if (IsNew)
            {

                if (RevisionNo == 0 && OriginID == 0)
                {
                    CompanyDocs doc = objectSpace.FindObject<CompanyDocs>(CriteriaOperator.Parse("Company.ID=? and DocType.ID=?", Company.ID, DocType.ID));
                    DocNumSeq = doc.NextDocNo;
                    DocNum = DocNumSeq.ToString();
                    doc.NextDocNo++;
                }
                else if (RevisionNo > 0 && OriginID > 0)
                {
                    PurchaseRequests oldpr = objectSpace.FindObject<PurchaseRequests>(CriteriaOperator.Parse("ID=?", OriginID));
                    oldpr.Rejected = false;
                    oldpr.DocPassed = false;
                    //selectedObject.Approved = false;
                    oldpr.Cancelled = true;
                    oldpr.DocStatus = "Cancel Posted";
                    //PurchaseRequestDocStatuses oldprds = objectSpace.CreateObject<PurchaseRequestDocStatuses>();
                    //oldprds.DocStatus = DocumentStatus.Rejected;
                    //oldprds.DocRemarks = OriginRejectRemarks;
                    //oldpr.DetailDocStatus.Add(oldprds);

                    PurchaseRequestDocStatuses oldprds = objectSpace.CreateObject<PurchaseRequestDocStatuses>();
                    oldprds.DocStatus = DocumentStatus.Cancelled;
                    oldprds.DocRemarks = OriginRejectRemarks;
                    oldpr.DetailDocStatus.Add(oldprds);
                }

                PurchaseRequestDocStatuses ds = objectSpace.CreateObject<PurchaseRequestDocStatuses>();
                ds.DocStatus = DocumentStatus.Create;
                ds.DocRemarks = "";
                ds.CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                ds.CreateDate = DateTime.Now;
                ds.UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                ds.UpdateDate = DateTime.Now;
                this.DetailDocStatus.Add(ds);

            }

            UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            UpdateDate = DateTime.Now;
            IsNew = false;

            string rtn = "";
            if (Rejected)
                rtn = "Approver Rejected";
            else if (Cancelled)
            {
                if (DocPosted)
                    rtn = "Cancel Posted";
                else
                    rtn = "Cancelled";
            }
            else if (DocPosted)
                rtn = "SAP B1 Posted";
            else if (DocPassed)
                rtn = "PR Requestor Passed";
            else
                rtn = "PR Requestor Pending";
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
