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
using DevExpress.Persistent.BaseImpl.EF.PermissionPolicy;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel.DataAnnotations;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<positions> positionss { get; set; }" syntax.
    //[DefaultClassOptions]
    [NavigationItem("PM Schedule Setup")]
    [XafDisplayName("PM Class")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMClasses : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public PMClasses()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.PlannerGroup = new List<PlannerGroups>();
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
        [XafDisplayName("PM Class Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleUniqueValue]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("BoCode", Enabled = false, Criteria = "not IsNew")]
        [VisibleInLookupListView(true)]
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
        [XafDisplayName("PM Class Name"), ToolTip("Enter Text")]
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

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        // Collection property:
        public virtual IList<PlannerGroups> PlannerGroup { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
            BoShortName = "";
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
                //positionDocs deptdoc = null;
                //int cnt = 0;
                //string temp = "";
                //IList<WorkOrderTypes> docs = objectSpace.GetObjects<WorkOrderTypes>();

                ////foreach (DocumentType doc in (DocumentType[])Enum.GetValues(typeof(DocumentType)))
                //foreach (WorkOrderTypes doc in docs)
                //{
                //    cnt++;
                //    deptdoc = new positionDocs();
                //    deptdoc.position = this;
                //    deptdoc.DocType = doc;
                //    temp = cnt.ToString() + "0000001";
                //    deptdoc.NextDocNo = long.Parse(temp);
                //    //if (Code == "HQ")
                //    //    outletdocs.Prefix = Code;
                //    //else
                //    Docs.Add(deptdoc);

                //}

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
