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
    // Register this entity in the DbContext using the "public DbSet<Companies> Companiess { get; set; }" syntax.
    [DefaultClassOptions]
    //[NavigationItem("System Setup")]
    [XafDisplayName("Company")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Companies : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Companies()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
            this.Docs = new List<CompanyDocs>();
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

        private string _BoCode;
        [XafDisplayName("Company Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleUniqueValue]
        [Appearance("BoCode", Enabled = false, Criteria = "not IsNew")]
        [RuleRequiredField(DefaultContexts.Save)]
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
        [XafDisplayName("Company Name"), ToolTip("Enter Text")]
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

        private string _PrefixQuot;
        [XafDisplayName("Quotation Prefix"), ToolTip("Enter Text")]
        [Index(50), VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string PrefixQuot
        {
            get { return _PrefixQuot; }
            set
            {
                if (_PrefixQuot != value)
                {
                    _PrefixQuot = value;
                    OnPropertyChanged("PrefixQuot");
                }
            }
        }

        private long _NextQuotNo;
        [Index(200), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public long NextQuotNo
        {
            get { return _NextQuotNo; }
            set
            {
                if (_NextQuotNo != value)
                {
                    _NextQuotNo = value;
                    OnPropertyChanged("NextQuotNo");
                }
            }
        }

        private long _NextAssetsNo;
        [Index(201), VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public long NextAssetsNo
        {
            get { return _NextAssetsNo; }
            set
            {
                if (_NextAssetsNo != value)
                {
                    _NextAssetsNo = value;
                    OnPropertyChanged("NextAssetsNo");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        public virtual IList<CompanyDocs> Docs { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            PrefixQuot = "";
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
            if (IsNew)
            {
                CompanyDocs deptdoc = null;
                int cnt = 0;
                string temp = "";
                IList<DocTypes> docs = objectSpace.GetObjects<DocTypes>();

                //foreach (DocumentType doc in (DocumentType[])Enum.GetValues(typeof(DocumentType)))
                foreach (DocTypes doc in docs)
                {
                    cnt++;
                    deptdoc = new CompanyDocs();
                    deptdoc.Company = this;
                    deptdoc.DocType = doc;
                    temp = cnt.ToString() + "0000001";
                    deptdoc.NextDocNo = long.Parse(temp);
                    //if (Code == "HQ")
                    //    outletdocs.Prefix = Code;
                    //else
                    Docs.Add(deptdoc);

                }
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
