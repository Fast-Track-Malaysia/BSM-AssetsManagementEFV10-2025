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
    // Register this entity in the DbContext using the "public DbSet<Priorities> Prioritiess { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("System Setup")]
    //[ImageName("BO_Contact")]
    [DefaultProperty("FullCode")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Priorities : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Priorities()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }

        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        private string _FullCode;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(0)]
        [Appearance("Priority Code", Enabled = false)]
        [Appearance("FullCode", Enabled = false)]
        public string FullCode
        {
            get { return _FullCode; }
            set
            {
                if (_FullCode != value)
                {
                    _FullCode = value;
                    OnPropertyChanged("FullCode");
                }
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
        private string _BoCode;
        [XafDisplayName("Priority Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleUniqueValue]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("BoCode", Enabled = false, Criteria = "not IsNew")]
        [Index(1)]
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
        [XafDisplayName("Priority Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(10)]
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

        private string _BoDescription;
        [XafDisplayName("Priority Description"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(11)]
        public string BoDescription
        {
            get { return _BoDescription; }
            set
            {
                if (_BoDescription != value)
                {
                    _BoDescription = value;
                    OnPropertyChanged("BoDescription");
                }
            }
        }

        private int _AllowedDayFrom;
        [XafDisplayName("Allowed Date From"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(20)]
        public int AllowedDayFrom
        {
            get { return _AllowedDayFrom; }
            set
            {
                if (_AllowedDayFrom != value)
                {
                    _AllowedDayFrom = value;
                    OnPropertyChanged("AllowedDayFrom");
                }
            }
        }

        private int _AllowedDayTo;
        [XafDisplayName("Allowed Date To"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(21)]
        public int AllowedDayTo
        {
            get { return _AllowedDayTo; }
            set
            {
                if (_AllowedDayTo != value)
                {
                    _AllowedDayTo = value;
                    OnPropertyChanged("AllowedDayTo");
                }
            }
        }

        private int _RiskSourceFrom;
        [XafDisplayName("Risk Source From"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(30)]
        public int RiskSourceFrom
        {
            get { return _RiskSourceFrom; }
            set
            {
                if (_RiskSourceFrom != value)
                {
                    _RiskSourceFrom = value;
                    OnPropertyChanged("RiskSourceFrom");
                }
            }
        }
        private int _RiskSourceTo;
        [XafDisplayName("Risk Source To"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(31)]
        public int RiskSourceTo
        {
            get { return _RiskSourceTo; }
            set
            {
                if (_RiskSourceTo != value)
                {
                    _RiskSourceTo = value;
                    OnPropertyChanged("RiskSourceTo");
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
            AllowedDayFrom = 0;
            AllowedDayTo= 0;
            RiskSourceFrom = 0;
            RiskSourceTo = 0;
            BoCode = "";
            BoName = "";
            BoDescription = "";
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

            string rtn = BoCode == null ? "" : BoCode.Trim() + "-";
            rtn += BoName == null ? "" : BoName.Trim();
            rtn += BoDescription == null ? "" : " [" + BoDescription.Trim() + "]";
            FullCode = rtn;

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
