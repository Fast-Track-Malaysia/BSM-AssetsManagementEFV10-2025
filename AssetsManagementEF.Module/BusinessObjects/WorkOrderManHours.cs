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
    [RuleCriteria("WorkOrderOpsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[RuleCriteria("WorkOrderOpsSaveRule", DefaultContexts.Save, "ValidDate", "Date From and Date To is not valid.")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WorkOrderManHours : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public WorkOrderManHours()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

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
        [XafDisplayName("Type of work/Labour"), ToolTip("Enter Text")]
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
        private WorkOrderTrades _WorkOrderTrade;
        [XafDisplayName("Type of work/Labour"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(6), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public virtual WorkOrderTrades WorkOrderTrade
        {
            get { return _WorkOrderTrade; }
            set
            {
                if (_WorkOrderTrade != value)
                {
                    _WorkOrderTrade = value;
                    OnPropertyChanged("WorkOrderTrade");
                }
            }
        }

        //private Technicians _Technician;
        //[XafDisplayName("Technician"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        ////[RuleRequiredField(DefaultContexts.Save)]
        //public virtual Technicians Technician
        //{
        //    get { return _Technician; }
        //    set
        //    {
        //        if (_Technician != value)
        //        {
        //            _Technician = value;
        //            OnPropertyChanged("Technician");
        //        }
        //    }
        //}
        private int _EManCount;
        [ImmediatePostData]
        [XafDisplayName("Estimate Nos of Worker"), ToolTip("Enter Number")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(91), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [Appearance("EManCount", BackColor = "yellow")]
        public int EManCount
        {
            get { return _EManCount; }
            set
            {
                if (_EManCount != value)
                {
                    _EManCount = value;
                    OnPropertyChanged("EManCount");
                }
            }
        }
        private long _EManHours;
        [ImmediatePostData]
        [XafDisplayName("Estimate Nos of Hours"), ToolTip("Enter Number")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("EManHours", BackColor = "yellow")]
        [Index(92), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public long EManHours
        {
            get { return _EManHours; }
            set
            {
                if (_EManHours != value)
                {
                    _EManHours = value;
                    OnPropertyChanged("EManHours");
                }
            }
        }

        [XafDisplayName("Total Estimate Man Hours")]
        [Index(93), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [Appearance("ETotalHours", BackColor = "yellow", FontColor = "Black")]
        public long ETotalHours
        {
            get { return _EManCount * _EManHours; }
        }
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("dummy01", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public string dummy01
        {
            get { return ""; }
        }
        //[VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Appearance("dummy02", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //public string dummy02
        //{
        //    get { return ""; }
        //}
        //[VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Appearance("dummy03", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //public string dummy03
        //{
        //    get { return ""; }
        //}

        private int _ManCount;
        [ImmediatePostData]
        [XafDisplayName("Actual Nos of Worker"), ToolTip("Enter Number")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Appearance("ManCount", BackColor = "lightgreen")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(101), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int ManCount
        {
            get { return _ManCount; }
            set
            {
                if (_ManCount != value)
                {
                    _ManCount = value;
                    OnPropertyChanged("ManCount");
                }
            }
        }
        private long _ManHours;
        [ImmediatePostData]
        [XafDisplayName("Actual Nos of Hours"), ToolTip("Enter Number")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Appearance("ManHours", BackColor = "lightgreen")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(102), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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
        [XafDisplayName("Total Actual Man Hours")]
        [Appearance("TotalHours", BackColor = "lightgreen", FontColor = "Black")]
        [Index(103), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public long TotalHours
        {
            get { return _ManCount * _ManHours; }
        }


        private TimeSpan? _TimeSpend;
        //[XafDisplayName("Total Man Hr (hh:mm)"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[ModelDefault("EditMask", @"hh:mm")]
        //[ModelDefault("DisplayFormat", "{0:hh\\:mm}")]
        //[Index(102), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public TimeSpan? TimeSpend
        {
            get { return _TimeSpend; }
            set
            {
                if (_TimeSpend != value)
                {
                    _TimeSpend = value;
                    OnPropertyChanged("TimeSpend");
                }
            }
        }



        private string _Remarks;
        [Browsable(false)]
        //[XafDisplayName("Remarks"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[Index(30), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "IsDone")]
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

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false)]
        [Appearance("WorkOrder", Enabled = false)]
        public virtual WorkOrders WorkOrder { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
            CreateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            CreateDate = DateTime.Now;
            ManCount = 1;
            TimeSpend = TimeSpan.FromMinutes(0);
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

            //ManHours = 0;
            //if (TimeSpend != null)
            //{
            //    TimeSpan temp = (TimeSpan)TimeSpend;
            //    ManHours = (long)temp.TotalMinutes;
            //}

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
