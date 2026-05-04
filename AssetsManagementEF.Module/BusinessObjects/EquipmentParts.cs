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
    // Register this entity in the DbContext using the "public DbSet<EquipmentProperties> EquipmentPropertiess { get; set; }" syntax.
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    [XafDisplayName("Part Lists")]
    [Appearance("HideLink", AppearanceItemType.Action, "True", TargetItems = "Link", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]
    [Appearance("HideUnlink", AppearanceItemType.Action, "True", TargetItems = "Unlink", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "Any")]    
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class EquipmentParts : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public EquipmentParts()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        // You can use the regular Code First syntax:
        //public string Name { get; set; }

        // Alternatively, specify more UI options: 
        private int _RowNo;
        [XafDisplayName("No")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(1), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int RowNo
        {
            get { return _RowNo; }
            set
            {
                if (_RowNo != value)
                {
                    _RowNo = value;
                    OnPropertyChanged("RowNo");
                }
            }
        }

        private string _PartDescription;
        [XafDisplayName("Part Description")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(2), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PartDescription
        {
            get { return _PartDescription; }
            set
            {
                if (_PartDescription != value)
                {
                    _PartDescription = value;
                    OnPropertyChanged("PartDescription");
                }
            }
        }

        private string _PartNumber;
        [XafDisplayName("Part Number")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(3), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PartNumber
        {
            get { return _PartNumber; }
            set
            {
                if (_PartNumber != value)
                {
                    _PartNumber = value;
                    OnPropertyChanged("PartNumber");
                }
            }
        }

        private string _PartReference;
        [XafDisplayName("Drawing / Reference")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(4), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PartReference
        {
            get { return _PartReference; }
            set
            {
                if (_PartReference != value)
                {
                    _PartReference = value;
                    OnPropertyChanged("PartReference");
                }
            }
        }

        private int _Quantity;
        [XafDisplayName("Quantity")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(5), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                if (_Quantity != value)
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        private string _PartUOM;
        [XafDisplayName("UOM")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(6), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PartUOM
        {
            get { return _PartUOM; }
            set
            {
                if (_PartUOM != value)
                {
                    _PartUOM = value;
                    OnPropertyChanged("PartUOM");
                }
            }
        }

        private TimeSpan _LeadTime;
        [XafDisplayName("Estimate Lead Time"), ToolTip("(day.hh:mm)")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [ModelDefault("EditMask", "dd\\.hh\\:mm")]
        [ModelDefault("DisplayFormat", "{0:dd\\.hh\\:mm}")]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(7), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public TimeSpan LeadTime
        {
            get { return _LeadTime; }
            set
            {
                if (_LeadTime != value)
                {
                    _LeadTime = value;
                    OnPropertyChanged("LeadTime");
                }
            }
        }

        private string _PartRemarks;
        [XafDisplayName("Remarks")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[Appearance("RowNo", Enabled = false, Criteria = "not IsNew")]
        [Index(8), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PartRemarks
        {
            get { return _PartRemarks; }
            set
            {
                if (_PartRemarks != value)
                {
                    _PartRemarks = value;
                    OnPropertyChanged("PartRemarks");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false)]
        public virtual Equipments Equipment { get; set; }

        //private Equipments _Equipment;
        //[Browsable(false)]
        //public virtual Equipments Equipment
        //{
        //    get { return _Equipment; }
        //    set
        //    {
        //        if (Equipment == value) return;
        //        _Equipment = value;
        //        if (value != null)
        //        {
        //            //this.BoName = value.EqClass.BoName;
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

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
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
