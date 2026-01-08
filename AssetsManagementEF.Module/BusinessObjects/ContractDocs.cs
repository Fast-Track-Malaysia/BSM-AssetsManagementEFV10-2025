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
    // Register this entity in the DbContext using the "public DbSet<Contracts> Contractss { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("Setup")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DocFullCode")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ContractDocs : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public ContractDocs()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
            this.Detail = new List<ContractDocDtls>();

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

        private SystemUsers _CreateUser;
        [XafDisplayName("Create User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(300), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        [Index(301), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        private Contractors _Contractor;
        [XafDisplayName("Contrator"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[DataSourceProperty("ItemCode.DetailBO", DataSourcePropertyIsNullMode.CustomCriteria, "Outlet = '@This.Outlet' and OpenQty > 0")]
        //[DataSourceProperty("CreateUser.Contractor", DataSourcePropertyIsNullMode.CustomCriteria)]
        [Appearance("Contrator", Criteria = "Not IsNew", Enabled = false)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        private string _Currency;
        [Browsable(false)]
        [XafDisplayName("Currency")]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public string Currency
        {
            get { return _Currency; }
            set
            {
                if (_Currency != value)
                {
                    _Currency = value;
                    OnPropertyChanged("Currency");
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
        [Appearance("DocNum", Enabled = false, Criteria = "not IsNew", FontColor = "Black")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [Index(1), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public string DocFullCode
        {
            get { return Contractor == null ? "" : DocNum + "[" + Contractor.BoCode + ":" + Contractor.BoName + "]"; }
        }

        private DateTime _DocDate;
        [Index(11), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [Index(12), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Ref No."), ToolTip("Enter Text")]
        [Appearance("RefNo", Enabled = false)]
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

        private string _Remarks;
        [XafDisplayName("Remarks"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(15), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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


        private DateTime? _StartDate;
        [Index(21), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Contract Start"), ToolTip("Enter Text")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime? StartDate
        {
            get { return _StartDate; }
            set
            {
                if (_StartDate != value)
                {
                    _StartDate = value;
                    OnPropertyChanged("StartDate");
                }
            }
        }

        private DateTime? _EndDate;
        [Index(22), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("Contract End"), ToolTip("Enter Text")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set
            {
                if (_EndDate != value)
                {
                    _EndDate = value;
                    OnPropertyChanged("EndDate");
                }
            }
        }

        private bool _IsActive;
        [XafDisplayName("Active"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        public virtual IList<ContractDocDtls> Detail { get; set; }

        // Collection property:
        //public virtual IList<AssociatedEntityObject> AssociatedEntities { get; set; }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
            DocDate = DateTime.Today;
            CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;

        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
            IsNew = false;

        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
            UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            UpdateDate = DateTime.Now;
            IsNew = false;
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
        private void OnPropertyChanged(String propertyName)
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
