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
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("RowNumber")]
    [XafDisplayName("Work Request Deviation")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("HideLink", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideUnlink", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[Appearance("HideEdit", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "DeviationStatus.BoCode = 'CANCEL'")]
    //[RuleCriteria("WorkRequestDeviationsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[RuleCriteria("WorkOrderEquipmentsEqSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    public class DeviationWorkRequests : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public DeviationWorkRequests()
        {
            // In the constructor, initialize collection properties, e.g.: 

        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string CreateStatusInfo
        {
            get
            {
                string temp = "";
                if (CreateUser != null)
                {
                    temp = string.Format("{0:yyyy/MM/dd H:mm:ss}", CreateDate);
                    temp += " [" + CreateUser.UserName + "]";
                }
                return temp;
            }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string UpdateStatusInfo
        {
            get
            {
                string temp = "";
                if (UpdateUser != null)
                {
                    temp = string.Format("{0:yyyy/MM/dd H:mm:ss}", UpdateDate);
                    temp += " [" + UpdateUser.UserName + "]";
                }
                return temp;
            }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string CloseStatusInfo
        {
            get
            {
                string temp = "";
                if (CloseUser != null)
                {
                    temp = string.Format("{0:yyyy/MM/dd H:mm:ss}", CloseDate);
                    temp += " [" + CloseUser.UserName + "]";
                }
                return temp;
            }
        }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [VisibleInDetailView(false), VisibleInListView(true)]
        public string CancelStatusInfo
        {
            get
            {
                string temp = "";
                if (CancelUser != null)
                {
                    temp = string.Format("{0:yyyy/MM/dd H:mm:ss}", CancelDate);
                    temp += " [" + CancelUser.UserName + "]";
                }
                return temp;
            }
        }

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

        private int _RowNumber;
        [XafDisplayName("Number")]
        //[Appearance("RowNumber", Enabled = false)]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("RowNumber", Enabled = false)]
        public int RowNumber
        {
            get { return _RowNumber; }
            set
            {
                if (_RowNumber != value)
                {
                    _RowNumber = value;
                    OnPropertyChanged("RowNumber");
                }
            }
        }
        private string _DeviationNo;
        [XafDisplayName("MOC No")]
        //[Appearance("DeviationNo", Enabled = false)]
        [Index(11), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("DeviationNo", Enabled = false, Criteria = "DeviationStatus.ID > 1")]
        public string DeviationNo
        {
            get { return _DeviationNo; }
            set
            {
                if (_DeviationNo != value)
                {
                    _DeviationNo = value;
                    OnPropertyChanged("DeviationNo");
                }
            }
        }
        private DateTime _DueDate;
        [Index(12), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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
        [XafDisplayName("Due Date")]
        [Appearance("DueDate", Enabled = false, Criteria = "DeviationStatus.ID > 1")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set
            {
                if (_DueDate != value)
                {
                    _DueDate = value;
                    OnPropertyChanged("DueDate");
                }
            }
        }

        private DeviationWRTypes _DeviationType;
        [Index(13), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("MOC Type")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("DeviationType", Enabled = false, Criteria = "DeviationStatus.ID > 1")]
        public virtual DeviationWRTypes DeviationType
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
        [Index(14), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [XafDisplayName("MOC Status")]
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
        private SystemUsers _CloseUser;
        [Index(100), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("CloseUser", Enabled = false)]
        public virtual SystemUsers CloseUser
        {
            get { return _CloseUser; }
            set
            {
                if (_CloseUser != value)
                {
                    _CloseUser = value;
                    OnPropertyChanged("CloseUser");
                }
            }
        }
        private DateTime? _CloseDate;
        [Index(101), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("CloseDate", Enabled = false)]
        public DateTime? CloseDate
        {
            get { return _CloseDate; }
            set
            {
                if (_CloseDate != value)
                {
                    _CloseDate = value;
                    OnPropertyChanged("CloseDate");
                }
            }
        }
        private SystemUsers _CancelUser;
        [Index(102), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("CancelUser", Enabled = false)]
        public virtual SystemUsers CancelUser
        {
            get { return _CancelUser; }
            set
            {
                if (_CancelUser != value)
                {
                    _CancelUser = value;
                    OnPropertyChanged("CancelUser");
                }
            }
        }
        private DateTime? _CancelDate;
        [Index(103), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("CancelDate", Enabled = false)]
        public DateTime? CancelDate
        {
            get { return _CancelDate; }
            set
            {
                if (_CancelDate != value)
                {
                    _CancelDate = value;
                    OnPropertyChanged("CancelDate");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false)]
        [Appearance("WorkRequest", Enabled = false)]
        public virtual WorkRequests WorkRequest { get; set; }

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
            DeviationStatus = objectSpace.FindObject<DeviationStatus>(CriteriaOperator.Parse("BoCode=?", "Open"));
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
            }
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
