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
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ContractDocDtls : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public ContractDocDtls()
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


        private ItemMasters _ItemMaster;
        [XafDisplayName("Item Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(10), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Appearance("ItemMaster", Criteria = "Not IsNew", Enabled = false)]
        public virtual ItemMasters ItemMaster
        {
            get { return _ItemMaster; }
            set
            {
                if (_ItemMaster != value)
                {
                    _ItemMaster = value;
                    OnPropertyChanged("ItemMaster");
                    _ItemDesc = value == null ? "" : ItemMaster.BoName;
                    OnPropertyChanged("ItemDesc");
                }
            }
        }

        //private string _ItemCode;
        //[XafDisplayName("Item Code"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[Index(12), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //public string ItemCode
        //{
        //    get { return _ItemCode; }
        //    set
        //    {
        //        if (_ItemCode != value)
        //        {
        //            _ItemCode = value;
        //            OnPropertyChanged("ItemCode");
        //        }
        //    }
        //}

        private string _ItemDesc;
        [XafDisplayName("Description"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(15), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string ItemDesc
        {
            get { return _ItemDesc; }
            set
            {
                if (_ItemDesc != value)
                {
                    _ItemDesc = value;
                    OnPropertyChanged("ItemDesc");
                }
            }
        }

        private double _QTY;
        [XafDisplayName("Quantity"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(19), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        public double QTY
        {
            get { return _QTY; }
            set
            {
                if (_QTY != value)
                {
                    _QTY = value;
                    OnPropertyChanged("QTY");
                }
            }
        }

        private string _Currency;
        [XafDisplayName("Currency")]
        [Index(20), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
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

        private decimal _Price;
        [XafDisplayName("Price"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(21), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[ModelDefault("DisplayFormat", "{n:2}")]
        public decimal Price
        {
            get { return _Price; }
            set
            {
                if (_Price != value)
                {
                    _Price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [XafDisplayName("Amount")]
        [Index(22), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(false)]
        //[ModelDefault("DisplayFormat", "{n:2}")]
        public decimal Amount
        {
            get
            {
                return (decimal)QTY * Price;
            }
        }

        private Int32 _BaseLine;
        [Browsable(false)]
        public Int32 BaseLine
        {
            get { return _BaseLine; }
            set
            {
                if (_BaseLine != value)
                {
                    _BaseLine = value;
                    OnPropertyChanged("BaseLine");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [Browsable(false)]
        public virtual ContractDocs ContractDoc { get; set; }

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
