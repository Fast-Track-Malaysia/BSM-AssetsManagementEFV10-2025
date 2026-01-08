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
    [DefaultClassOptions]
    [NavigationItem("System Setup")]
    [XafDisplayName("Position")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Positions : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Positions()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
            //this.PMClass = new List<PMClasses>();
            this.DetailPlannerGroup = new List<PlannerGroups>();
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
        [XafDisplayName("Position Code"), ToolTip("Enter Text")]
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
        [XafDisplayName("Position Name"), ToolTip("Enter Text")]
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

        private SystemUsers _CurrentUser;
        [XafDisplayName("User"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Appearance("CurrentUser", Enabled = false, Criteria = "IsNew")]
        public virtual SystemUsers CurrentUser
        {
            get { return _CurrentUser; }
            set
            {
                if (_CurrentUser != value)
                {
                    SystemUsers prevValue = _CurrentUser;
                    _CurrentUser = value;
                    OnPropertyChanged("CurrentUser");
                    //if (objectSpace != null && objectSpace.IsModified)
                    //{
                    //    if (_CurrentUser != null)
                    //    {
                    //        if (prevValue != null && _CurrentUser.Position == this)
                    //            prevValue.Position = null;

                    //        _CurrentUser.Position = this;
                    //    }
                    //    else
                    //    {
                    //        if (prevValue != null)
                    //            prevValue.Position = null;

                    //    }

                    //}
                }

            }
        }

        //private PlannerGroups _PlannerGroup;
        //[XafDisplayName("Planner Group"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //public virtual PlannerGroups PlannerGroup
        //{
        //    get { return _PlannerGroup; }
        //    set
        //    {
        //        if (_PlannerGroup != value)
        //        {
        //            _PlannerGroup = value;
        //            OnPropertyChanged("PlannerGroup");
        //        }
        //    }
        //}

        private bool _IsPlanner;
        [Browsable(false)]
        [XafDisplayName("Is Planner"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsPlanner
        {
            get { return _IsPlanner; }
            set
            {
                if (_IsPlanner != value)
                {
                    _IsPlanner = value;
                    OnPropertyChanged("IsPlanner");
                }
            }
        }

        private bool _IsApprover;
        [Browsable(false)]
        [XafDisplayName("Is Approver"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsApprover
        {
            get { return _IsApprover; }
            set
            {
                if (_IsApprover != value)
                {
                    _IsApprover = value;
                    OnPropertyChanged("IsApprover");
                }
            }
        }

        private bool _IsWPS;
        [Browsable(false)]
        [XafDisplayName("Is WPS"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsWPS
        {
            get { return _IsWPS; }
            set
            {
                if (_IsWPS != value)
                {
                    _IsWPS = value;
                    OnPropertyChanged("IsWPS");
                }
            }
        }


        private bool _IsPreventiveMaintenance;
        [XafDisplayName("Preventive Maintenance"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsPreventiveMaintenance
        {
            get { return _IsPreventiveMaintenance; }
            set
            {
                if (_IsPreventiveMaintenance != value)
                {
                    _IsPreventiveMaintenance = value;
                    OnPropertyChanged("IsPreventiveMaintenance");
                }
            }
        }

        private bool _IsCorrectiveMaintenance;
        [XafDisplayName("Corrective Maintenance"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsCorrectiveMaintenance
        {
            get { return _IsCorrectiveMaintenance; }
            set
            {
                if (_IsCorrectiveMaintenance != value)
                {
                    _IsCorrectiveMaintenance = value;
                    OnPropertyChanged("IsCorrectiveMaintenance");
                }
            }
        }

        private bool _IsPRApprove;
        [XafDisplayName("PR Approval"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        public bool IsPRApprove
        {
            get { return _IsPRApprove; }
            set
            {
                if (_IsPRApprove != value)
                {
                    _IsPRApprove = value;
                    OnPropertyChanged("IsPRApprove");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        // Collection property:
        //public virtual IList<PMClasses> PMClass { get; set; }

        [XafDisplayName("Planner Group List")]
        //[Appearance("DetailPlannerGroup", Enabled = false)]
        public virtual IList<PlannerGroups> DetailPlannerGroup { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
            IsNew = true;
            IsPlanner = false;
            IsApprover = false;
            IsWPS = false;
            IsPreventiveMaintenance = false;
            IsCorrectiveMaintenance = false;
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
            else
            {
                if (CurrentUser != null)
                {

                    IList<Positions> pos = objectSpace.GetObjects<Positions>(CriteriaOperator.Parse("ID<>? and CurrentUser.ID=?", this.ID, CurrentUser.ID));
                    foreach (Positions po in pos)
                    {
                        if (po.CurrentUser != null)
                            po.CurrentUser = null;
                    }

                    //IList<SystemUsers> usrs = objectSpace.GetObjects<SystemUsers>();
                    //foreach (SystemUsers usr in usrs)
                    //{
                    //    if (usr.ID == CurrentUser.ID)
                    //        usr.Position = this;
                    //    else if (usr.Position != null)
                    //        if (usr.Position.ID == this.ID)
                    //            usr.Position = null;
                    //}
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
