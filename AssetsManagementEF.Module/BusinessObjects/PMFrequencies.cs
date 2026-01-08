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
    // Register this entity in the DbContext using the "public DbSet<Classes> Classess { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("PM Schedule Setup")]
    //[ImageName("BO_Contact")]
    [XafDisplayName("PM Frequency")]
    [DefaultProperty("BoShortName")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMFrequencies : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public PMFrequencies()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
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

        private string _BoCode;
        [XafDisplayName("Frequency Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleUniqueValue]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("BoCode", Enabled = false, Criteria = "not IsNew")]
        public string BoCode
        {
            get { return _BoCode; }
            set
            {
                if (_BoCode != value)
                {
                    _BoCode = value;
                    OnPropertyChanged("BoCode");
                }
            }
        }

        private string _BoName;
        [XafDisplayName("Frequency Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string BoName
        {
            get { return _BoName; }
            set
            {
                if (_BoName != value)
                {
                    _BoName = value;
                    OnPropertyChanged("BoName");
                }
            }
        }

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Index(1), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[ModelDefault("Caption", "Code")]
        //public string FrequencyShortName
        //{
        //    get { return String.Format("{0}:{1}", ID.ToString().PadLeft(2, '0'), BoShortName); }
        //}

        private string _BoShortName;
        [XafDisplayName("Short Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string BoShortName
        {
            get { return _BoShortName; }
            set
            {
                if (_BoShortName != value)
                {
                    _BoShortName = value;
                    OnPropertyChanged("BoShortName");
                }
            }
        }

        private MaintenanceFrequency _Frequency;
        [XafDisplayName("Frequency"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("Frequency", Enabled = false, Criteria = "not IsNew")]
        public MaintenanceFrequency Frequency
        {
            get { return _Frequency; }
            set
            {
                if (_Frequency != value)
                {
                    _Frequency = value;
                    OnPropertyChanged("Frequency");
                }
            }
        }

        private int _CycleCount;
        [XafDisplayName("Cycle Count"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Appearance("CycleCount", Enabled = false, Criteria = "not IsNew")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int CycleCount
        {
            get { return _CycleCount; }
            set
            {
                if (_CycleCount != value)
                {
                    _CycleCount = value;
                    OnPropertyChanged("CycleCount");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }


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
            BoShortName = "";
            Frequency = MaintenanceFrequency.MONTH;
        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
            IsNew = false;

        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
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
