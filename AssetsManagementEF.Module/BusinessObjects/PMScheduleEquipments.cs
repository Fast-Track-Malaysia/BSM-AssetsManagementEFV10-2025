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
    [DefaultProperty("Equipment")]
    [XafDisplayName("Schedule Equipment")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [RuleCriteria("PMScheduleEquipmentsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[RuleCriteria("WorkOrderEquipmentsEqSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    public class PMScheduleEquipments : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public PMScheduleEquipments()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.Detail = new List<PMScheduleEqComponents>();

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

        private Equipments _Equipment;
        [XafDisplayName("Equipment"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
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
        [XafDisplayName("Equipment Name")]
        [Index(11), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string EquipmentName
        {
            get { return Equipment == null ? "" : Equipment.BoName; }
        }

        [XafDisplayName("PM Name")]
        [Index(8), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string MasterBoName
        {
            get { return PMSchedule == null ? "" : PMSchedule.BoName; }
        }

        [XafDisplayName("PM Interface Code")]
        [Index(9), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string MasterBoFullCode
        {
            get { return PMSchedule == null ? "" : PMSchedule.BoFullCode; }
        }


        private bool _IsActive;
        [XafDisplayName("Active"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
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

        private bool _IsCombine;
        [XafDisplayName("PM With Com."), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public bool IsCombine
        {
            get { return _IsCombine; }
            set
            {
                if (_IsCombine != value)
                {
                    _IsCombine = value;
                    OnPropertyChanged("IsCombine");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Appearance("PMSchedule", Enabled = false)]
        [Index(0), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("PM Schedule")]
        public virtual PMSchedules PMSchedule { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Frequency")]
        [Appearance("PMFrequencyShortName", Enabled = false)]
        public string PMFrequencyShortName
        {
            get
            {
                if (PMSchedule != null)
                    if (PMSchedule.PMFrequency != null)
                        return PMSchedule.PMFrequency.BoShortName;

                return "";
            }
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(2), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [XafDisplayName("Nested")]
        [Appearance("FrequencyIsNested", Enabled = false)]
        public bool FrequencyIsNested
        {
            get { return PMSchedule == null ? false : PMSchedule.IsNested; }
        }


        // Collection property:
        [Browsable(false)]
        [XafDisplayName("Component")]
        public virtual IList<PMScheduleEqComponents> Detail { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsCombine = false;
            IsActive = true;
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
