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

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<SystemUsers> SystemUserss { get; set; }" syntax.
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SystemUsers : PermissionPolicyUser, IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public SystemUsers()
        {
            // In the constructor, initialize collection properties, e.g.: 
            this.Contractor = new List<Contractors>();
            this.Task = new List<Tasks>();

        }
        //[Browsable(false)]  // Hide the entity identifier from UI.
        //public Int32 ID { get; protected set; }

        // You can use the regular Code First syntax:
        //public string Name { get; set; }

        // Alternatively, specify more UI options: 
        //private string _FullName;
        [XafDisplayName("Full Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string FullName
        {
            get; set;
            //get { return _FullName; }
            //set
            //{
            //    if (_FullName != value)
            //    {
            //        _FullName = value;
            //        OnPropertyChanged("FullName");
            //    }
            //}
        }

        //private Companies _Company;
        [XafDisplayName("Company"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual Companies Company
        {
            get; set;
            //get { return _Company; }
            //set
            //{
            //    if (_Company != value)
            //    {
            //        _Company = value;
            //        OnPropertyChanged("Company");
            //    }
            //}
        }

        [XafDisplayName("B1 Employee ID"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int B1EmployeeID
        {
            get; set;
            //get { return _Company; }
            //set
            //{
            //    if (_Company != value)
            //    {
            //        _Company = value;
            //        OnPropertyChanged("Company");
            //    }
            //}
        }

        [XafDisplayName("Email"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public string UserEmail
        {
            get; set;
            //get { return _Company; }
            //set
            //{
            //    if (_Company != value)
            //    {
            //        _Company = value;
            //        OnPropertyChanged("Company");
            //    }
            //}
        }

        [XafDisplayName("Contractor ID"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public string ContractorID
        {
            get; set;
            //get { return _Company; }
            //set
            //{
            //    if (_Company != value)
            //    {
            //        _Company = value;
            //        OnPropertyChanged("Company");
            //    }
            //}
        }

        //[XafDisplayName("Position"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        ////[RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInDetailView(false)]
        //public virtual Positions Position
        //{
        //    get; set;
        //}


        [Browsable(false)]
        public virtual IList<Contractors> Contractor { get; set; }
        [Browsable(false)]
        public virtual IList<Tasks> Task { get; set; }

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
            Company = objectSpace.FindObject<Companies>(CriteriaOperator.Parse("BoCode=?", GeneralSettings.HQCompany));
            B1EmployeeID = 0;
            UserEmail = "";
        }
        void IXafEntityObject.OnLoaded()
        {
            // Place the code that is executed each time the entity is loaded here.
        }
        void IXafEntityObject.OnSaving()
        {
            // Place the code that is executed each time the entity is saved here.
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
        //private void OnPropertyChanged(String propertyName)
        //{
        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        //    }
        //}
        //public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
