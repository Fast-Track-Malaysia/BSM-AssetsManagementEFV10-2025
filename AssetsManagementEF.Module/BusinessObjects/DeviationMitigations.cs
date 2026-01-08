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
    [XafDisplayName("Mitigation")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [Appearance("HideLink", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideUnlink", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[Appearance("HideEdit", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "DeviationStatus.BoCode = 'Cancel'")]
    [Appearance("HideEdit", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsClosed or IsCancel")]
    //[RuleCriteria("WorkOrderDeviationsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[RuleCriteria("WorkOrderEquipmentsEqSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    public class DeviationMitigations : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public DeviationMitigations()
        {
            // In the constructor, initialize collection properties, e.g.: 

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
        [Browsable(false)]
        public bool IsClosed
        {
            get { return CloseUser != null;  }
        }
        [Browsable(false)]
        public bool IsCancel
        {
            get { return CancelUser != null; }
        }
        private SystemUsers _CreateUser;
        [XafDisplayName("Raised By")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(300), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        [XafDisplayName("#")]
        //[Appearance("RowNumber", Enabled = false)]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
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

        //private SystemUsers _MitigationUser;
        //[XafDisplayName("User"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("MitigationUser", Enabled = false, Criteria = "not IsUser")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[ImmediatePostData(true)]
        //public virtual SystemUsers MitigationUser
        //{
        //    get { return _MitigationUser; }
        //    set
        //    {
        //        if (_MitigationUser != value)
        //        {
        //            _MitigationUser = value;
        //            OnPropertyChanged("MitigationUser");
        //            if (_MitigationUser != null)
        //            {
        //                Position = objectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", _MitigationUser.ID));
        //                //OnPropertyChanged("Position");
        //            }

        //        }
        //    }
        //}

        //private Positions _Position;
        //[XafDisplayName("Position"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[Appearance("Position", Enabled = false, Criteria = "not IsUser")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public virtual Positions Position
        //{
        //    get { return _Position; }
        //    set
        //    {
        //        if (_Position != value)
        //        {
        //            _Position = value;
        //            OnPropertyChanged("Position");
        //        }
        //    }
        //}

        private string _MitigationUser;
        [XafDisplayName("User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("MitigationUser", Enabled = false, Criteria = "not IsUser")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData(true)]
        public string MitigationUser
        {
            get { return _MitigationUser; }
            set
            {
                if (_MitigationUser != value)
                {
                    _MitigationUser = value;
                    OnPropertyChanged("MitigationUser");
                }
            }
        }

        private string _Position;
        [XafDisplayName("Position"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(3), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("Position", Enabled = false, Criteria = "not IsUser")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Position
        {
            get { return _Position; }
            set
            {
                if (_Position != value)
                {
                    _Position = value;
                    OnPropertyChanged("Position");
                }
            }
        }
        private string _Dscription;
        [XafDisplayName("Mitigation"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(4), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
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

        private string _Reason;
        [XafDisplayName("Reason of Mitigation"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(4), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string Reason
        {
            get { return _Reason; }
            set
            {
                if (_Reason != value)
                {
                    _Reason = value;
                    OnPropertyChanged("Reason");
                }
            }
        }
        private string _AcceptanceCriteria;
        [XafDisplayName("Acceptance Criteria"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string AcceptanceCriteria
        {
            get { return _AcceptanceCriteria; }
            set
            {
                if (_AcceptanceCriteria != value)
                {
                    _AcceptanceCriteria = value;
                    OnPropertyChanged("AcceptanceCriteria");
                }
            }
        }

        private DeviationFrequencies _DeviationFrequency;
        [XafDisplayName("Frequency"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(6), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual DeviationFrequencies DeviationFrequency
        {
            get { return _DeviationFrequency; }
            set
            {
                if (_DeviationFrequency != value)
                {
                    _DeviationFrequency = value;
                    OnPropertyChanged("DeviationFrequency");
                }
            }
        }

        private DateTime _DueDate;
        [Index(7), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        [XafDisplayName("Due Date")]
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
        private DateTime? _CloseDate;
        [XafDisplayName("Completed Date")]
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[Appearance("CloseDate", Enabled = false)]
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
        private DateTime? _CancelDate;
        [Index(102), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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
        private SystemUsers _CancelUser;
        [Index(103), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
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

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false)]
        [Appearance("Deviation", Enabled = false)]
        public virtual Deviation2025 Deviation { get; set; }

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
            //Position = objectSpace.FindObject<Positions>(CriteriaOperator.Parse("CurrentUser.ID=?", CreateUser.ID));

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
            if (objectSpace != null)
            {
                UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                UpdateDate = DateTime.Now;
            }
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
