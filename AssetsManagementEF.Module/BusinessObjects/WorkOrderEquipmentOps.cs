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
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [XafDisplayName("Operation Logs")]
    [RuleCriteria("WorkOrderEquipmentOpsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    [RuleCriteria("WorkOrderEquipmentOpsSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WorkOrderEquipmentOps : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public WorkOrderEquipmentOps()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        #region Ops
        [Action(Caption = "Cancel", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = false)]
        public void CancelMethod()
        {
            // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
            this.IsCancel = true;
        }

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

        private WorkOrderOpTypes _WorkOrderOpType;
        [XafDisplayName("Operation Type"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(5), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public virtual WorkOrderOpTypes WorkOrderOpType
        {
            get { return _WorkOrderOpType; }
            set
            {
                if (_WorkOrderOpType != value)
                {
                    _WorkOrderOpType = value;
                    OnPropertyChanged("WorkOrderOpType");
                }
            }
        }

        private Technicians _Technician;
        [XafDisplayName("Technician"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "IsDone")]
        public virtual Technicians Technician
        {
            get { return _Technician; }
            set
            {
                if (_Technician != value)
                {
                    _Technician = value;
                    OnPropertyChanged("Technician");
                }
            }
        }


        private DateTime? _FDate;
        [Index(0), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[ModelDefault("DisplayFormat", "{0:dd/MM/yyyy hh:mm}")]
        //[ModelDefault("EditMask", "dd/MM/yyyy hh:mm")]
        public DateTime? FDate
        {
            get { return _FDate; }
            set
            {
                if (_FDate != value)
                {
                    _FDate = value;
                    OnPropertyChanged("FDate");
                }
            }
        }

        private DateTime? _TDate;
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[ModelDefault("DisplayFormat", "{0:dd/MM/yyyy hh:mm}")]
        //[ModelDefault("EditMask", "dd/MM/yyyy hh:mm")]
        public DateTime? TDate
        {
            get { return _TDate; }
            set
            {
                if (_TDate != value)
                {
                    _TDate = value;
                    OnPropertyChanged("TDate");
                }
            }
        }

        [Index(23), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public TimeSpan? TimeSpend
        {
            get {
                TimeSpan? rtn = new TimeSpan();
                if (FDate.HasValue && TDate.HasValue)
                    rtn = TDate - FDate;
                return rtn;
            }
        }

        private long _ManHours;
        [Browsable(false)]
        public long ManHours
        {
            get { return _ManHours; }
            set
            {
                if (_ManHours != value)
                {
                    _ManHours = value;
                    OnPropertyChanged("ManHours");
                }
            }
        }


        private string _Operation;
        [XafDisplayName("Operation"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "IsDone")]
        public string Operation
        {
            get { return _Operation; }
            set
            {
                if (_Operation != value)
                {
                    _Operation = value;
                    OnPropertyChanged("Operation");
                }
            }
        }

        private bool _IsDone;
        [XafDisplayName("Completed")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(40), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public bool IsDone
        {
            get { return _IsDone; }
            set
            {
                if (_IsDone != value)
                {
                    _IsDone = value;
                    OnPropertyChanged("IsDone");
                }
            }
        }

        private bool _IsCancel;
        [XafDisplayName("Cancel")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(41), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[Appearance("BoCode", Enabled = false, Criteria = "not IsNew")]
        [ModelDefault("AllowEdit", "False")]
        public bool IsCancel
        {
            get { return _IsCancel; }
            set
            {
                if (_IsCancel != value)
                {
                    _IsCancel = value;
                    OnPropertyChanged("IsCancel");
                }
            }
        }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool ValidDate
        {
            get {
                if (_IsDone)
                {
                    if (TDate.HasValue && FDate.HasValue)
                    {
                        if (FDate > TDate)
                            return false;
                    }
                    else
                        return false;
                }
                return true;
            }
        }

        [Browsable(false)]
        public DateTime EqDownTime
        {
            get;
            set;
        }

        [Browsable(false)]
        public DateTime EqUpTime
        {
            get;
            set;
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }
        #endregion Ops

        [Browsable(false)]
        [Appearance("WorkOrderEquipment", Enabled = false)]
        public virtual WorkOrderEquipments WorkOrderEquipment { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
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
            if (IsNew)
            {
            }

            UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            UpdateDate = DateTime.Now;

            EqDownTime = DateTime.Parse("2099-12-31");
            if (FDate != null)
                if (WorkOrderOpType.IsDown && IsDone && !IsCancel)
                    EqDownTime = (DateTime)FDate;          

            EqUpTime = DateTime.Parse("1900-01-01");
            if (TDate != null)
                if (WorkOrderOpType.IsUp && IsDone && !IsCancel)
                    EqUpTime = (DateTime)TDate;

            ManHours = 0;
            if (FDate.HasValue && TDate.HasValue && IsDone && !IsCancel)
            {
                TimeSpan temp = (TimeSpan)TimeSpend;
                ManHours = temp.Minutes;
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
