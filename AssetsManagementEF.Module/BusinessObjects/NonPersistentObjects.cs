using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo.DB;
using System.ComponentModel.DataAnnotations;

namespace AssetsManagementEF.Module.BusinessObjects
{
    [DomainComponent]
    public class EquipmentList : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        public EquipmentList()
        {
        }

        [Key]
        [Browsable(false)]
        public int ID;
        public string DocumentNo;
        public string WorkRemarks;
        public string PlannerGroup;
        public string EquipmentCode;
        public string EquipmentName;
        public string EquipmentComponentCode;
        public string EquipmentComponentName;

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BooleanParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public BooleanParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Order with Equipment?")]
        [Appearance("ParamBoolean", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("ParamBoolean2", Enabled = false, Criteria = "IsDisable")]
        public bool ParamBoolean { get; set; }

        [XafDisplayName("Important")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("ActionMessage2", Enabled = false, FontColor = "Red")]
        public string ActionMessage { get; set; }

        [Browsable(false)]
        public bool IsDisable { get; set; }

        [Browsable(false)]
        public bool IsErr { get; set; }
        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class StringParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public StringParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Remarks")]
        [Appearance("ParamString", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        public string ParamString { get; set; }

        [XafDisplayName("Important")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("ActionMessage2", Enabled = false, FontColor = "Red")]
        public string ActionMessage { get; set; }

        [Browsable(false)]
        public bool IsErr { get; set; }
        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class JobStringParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public JobStringParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Job Status")]
        [DataSourceCriteria("not IsClosure")]
        [Appearance("JobStatus", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        public JobStatuses JobStatus { get; set; }

        [XafDisplayName("Remarks")]
        [Appearance("ParamString", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        public string ParamString { get; set; }

        [XafDisplayName("Current Job Status")]
        [Appearance("OldJobStatus", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("OldJobStatus2", Enabled = false)]
        public JobStatuses OldJobStatus { get; set; }

        [XafDisplayName("Important")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("ActionMessage2", Enabled = false, FontColor = "Red")]
        public string ActionMessage { get; set; }

        [Browsable(false)]
        public bool IsPlanning { get; set; }
        [Browsable(false)]
        public bool IsPreExecution { get; set; }
        [Browsable(false)]
        public bool IsExecution { get; set; }
        [Browsable(false)]
        public bool IsPostExecution { get; set; }

        [Browsable(false)]
        public bool IsErr { get; set; }
        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class EquipmentFilterParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public EquipmentFilterParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        public Equipments EQ { get; set; }

        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DateRangeFilterParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public DateRangeFilterParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }


    [DomainComponent]
    //[DefaultClassOptions]
    //[NavigationItem("PM Schedule Setup")]
    //[XafDisplayName("PM Schedule Equipment Calender")]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMScheduleDC : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public PMScheduleDC()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Equipment")]
        [Appearance("Equipment", Enabled = false)]
        public Equipments Equipment { get; set; }

        [XafDisplayName("Equipment Name")]
        [Appearance("EquipmentName", Enabled = false)]
        public string EquipmentName { get { return Equipment == null ? "" : Equipment.BoName; } }

        [XafDisplayName("EQ Interface Code")]
        [Appearance("BoFullCode", Enabled = false)]
        public string BoFullCode { get { return Equipment == null ? "" : Equipment.BoFullCode; } }

        [XafDisplayName("PM Schedule")]
        [Appearance("PMSchedule", Enabled = false)]
        public PMSchedules PMSchedule { get; set; }

        [XafDisplayName("PM Name")]
        [Appearance("PMName", Enabled = false)]
        public string PMName { get { return PMSchedule == null ? "" : PMSchedule.BoName; } }

        [XafDisplayName("Criticality")]
        [Appearance("Criticality", Enabled = false)]
        public Criticalities Criticality { get { return Equipment == null ? null : Equipment.Criticality; } }

        [XafDisplayName("Planner Group")]
        [Appearance("PlannerGroup", Enabled = false)]
        public PlannerGroups PlannerGroup { get { return PMSchedule == null ? null : PMSchedule.PlannerGroup; } }

        [XafDisplayName("Check List")]
        [Appearance("ChecklistNV", Enabled = false)]
        public string ChecklistNV { get { return PMSchedule == null ? "" : PMSchedule.ChecklistNV; } }

        [XafDisplayName("PM Interface Code")]
        [Appearance("PMBoFullCode", Enabled = false)]
        public string PMBoFullCode { get { return PMSchedule == null ? "" : PMSchedule.BoFullCode; } }

        [XafDisplayName("Nested")]
        [Appearance("IsNested", Enabled = false)]
        public bool IsNested { get; set; }

        [XafDisplayName("Year")]
        [Appearance("PMYear", Enabled = false)]
        public string PMYear { get; set; }

        [XafDisplayName("1st JAN")]
        [Appearance("PMSchedule101", Enabled = false)]
        public string PMSchedule101 { get; set; }

        [XafDisplayName("1st FEB")]
        [Appearance("PMSchedule102", Enabled = false)]
        public string PMSchedule102 { get; set; }

        [XafDisplayName("1st MAR")]
        [Appearance("PMSchedule103", Enabled = false)]
        public string PMSchedule103 { get; set; }

        [XafDisplayName("1st APR")]
        [Appearance("PMSchedule104", Enabled = false)]
        public string PMSchedule104 { get; set; }

        [XafDisplayName("1st MAY")]
        [Appearance("PMSchedule105", Enabled = false)]
        public string PMSchedule105 { get; set; }

        [XafDisplayName("1st JUN")]
        [Appearance("PMSchedule106", Enabled = false)]
        public string PMSchedule106 { get; set; }

        [XafDisplayName("1st JUL")]
        [Appearance("PMSchedule107", Enabled = false)]
        public string PMSchedule107 { get; set; }

        [XafDisplayName("1st AUG")]
        [Appearance("PMSchedule108", Enabled = false)]
        public string PMSchedule108 { get; set; }

        [XafDisplayName("1st SEP")]
        [Appearance("PMSchedule109", Enabled = false)]
        public string PMSchedule109 { get; set; }

        [XafDisplayName("1st OCT")]
        [Appearance("PMSchedule110", Enabled = false)]
        public string PMSchedule110 { get; set; }

        [XafDisplayName("1st NOV")]
        [Appearance("PMSchedule111", Enabled = false)]
        public string PMSchedule111 { get; set; }

        [XafDisplayName("1st DEC")]
        [Appearance("PMSchedule112", Enabled = false)]
        public string PMSchedule112 { get; set; }

        [XafDisplayName("2nd JAN")]
        [Appearance("PMSchedule201", Enabled = false, FontColor = "Green")]
        public string PMSchedule201 { get; set; }

        [XafDisplayName("2nd FEB")]
        [Appearance("PMSchedule202", Enabled = false, FontColor = "Green")]
        public string PMSchedule202 { get; set; }

        [XafDisplayName("2nd MAR")]
        [Appearance("PMSchedule203", Enabled = false, FontColor = "Green")]
        public string PMSchedule203 { get; set; }

        [XafDisplayName("2nd APR")]
        [Appearance("PMSchedule204", Enabled = false, FontColor = "Green")]
        public string PMSchedule204 { get; set; }

        [XafDisplayName("2nd MAY")]
        [Appearance("PMSchedule205", Enabled = false, FontColor = "Green")]
        public string PMSchedule205 { get; set; }

        [XafDisplayName("2nd JUN")]
        [Appearance("PMSchedule206", Enabled = false, FontColor = "Green")]
        public string PMSchedule206 { get; set; }

        [XafDisplayName("2nd JUL")]
        [Appearance("PMSchedule207", Enabled = false, FontColor = "Green")]
        public string PMSchedule207 { get; set; }

        [XafDisplayName("2nd AUG")]
        [Appearance("PMSchedule208", Enabled = false, FontColor = "Green")]
        public string PMSchedule208 { get; set; }

        [XafDisplayName("2nd SEP")]
        [Appearance("PMSchedule209", Enabled = false, FontColor = "Green")]
        public string PMSchedule209 { get; set; }

        [XafDisplayName("2nd OCT")]
        [Appearance("PMSchedule210", Enabled = false, FontColor = "Green")]
        public string PMSchedule210 { get; set; }

        [XafDisplayName("2nd NOV")]
        [Appearance("PMSchedule211", Enabled = false, FontColor = "Green")]
        public string PMSchedule211 { get; set; }

        [XafDisplayName("2nd DEC")]
        [Appearance("PMSchedule212", Enabled = false, FontColor = "Green")]
        public string PMSchedule212 { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[DefaultProperty("Equipment")]
    //[NavigationItem("PM Schedule Setup")]
    //[XafDisplayName("PM Schedule Equipment & Component Calender")]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMScheduleCOMDC : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public PMScheduleCOMDC()
        {
            
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [Browsable(false)]
        public int EquipmentID { get; set; }

        [XafDisplayName("Equipment")]
        [Appearance("Equipment", Enabled = false)]
        public Equipments Equipment
        { get; set; }

        [XafDisplayName("Equipment Name")]
        [Appearance("EquipmentName", Enabled = false)]
        public string EquipmentName { get { return Equipment == null ? "" : Equipment.BoName; } }

        [XafDisplayName("EQ Interface Code")]
        [Appearance("BoFullCode", Enabled = false)]
        public string BoFullCode { get { return Equipment == null ? "" : Equipment.BoFullCode; } }

        [Browsable(false)]
        public int EquipmentComponentID { get; set; }

        [XafDisplayName("Component")]
        [Appearance("EquipmentComponent", Enabled = false)]
        public EquipmentComponents EquipmentComponent
        { get; set; }


        [XafDisplayName("Component Name")]
        [Appearance("EquipmentComponentName", Enabled = false)]
        public string EquipmentComponentName { get { return EquipmentComponent == null ? "" : EquipmentComponent.BoName; } }

        [XafDisplayName("Com Interface Code")]
        [Appearance("ComBoFullCode", Enabled = false)]
        public string ComBoFullCode { get { return EquipmentComponent == null ? "" : EquipmentComponent.BoFullCode; } }

        [Browsable(false)]
        public int PMScheduleID { get; set; }

        [XafDisplayName("PM Schedule")]
        [Appearance("PMSchedule", Enabled = false)]
        public PMSchedules PMSchedule
        { get; set; }


        [XafDisplayName("PM Name")]
        [Appearance("PMName", Enabled = false)]
        public string PMName { get { return PMSchedule == null ? "" : PMSchedule.BoName; } }

        [XafDisplayName("Criticality")]
        [Appearance("Criticality", Enabled = false)]
        public Criticalities Criticality
        {
            get
            {
                if (EquipmentComponent == null && Equipment == null) return null;
                return EquipmentComponent == null ? Equipment.Criticality : EquipmentComponent.Criticality;
            }
        }

        [XafDisplayName("Planner Group")]
        [Appearance("PlannerGroup", Enabled = false)]
        public PlannerGroups PlannerGroup { get { return PMSchedule == null ? null : PMSchedule.PlannerGroup; } }

        [XafDisplayName("Check List")]
        [Appearance("ChecklistNV", Enabled = false)]
        public string ChecklistNV { get { return PMSchedule == null ? "" : PMSchedule.ChecklistNV; } }

        [XafDisplayName("PM Interface Code")]
        [Appearance("PMBoFullCode", Enabled = false)]
        public string PMBoFullCode { get { return PMSchedule == null ? "" : PMSchedule.BoFullCode; } }

        [XafDisplayName("Nested")]
        [Appearance("IsNested", Enabled = false)]
        public bool IsNested { get; set; }

        [XafDisplayName("Year")]
        [Appearance("PMYear", Enabled = false)]
        public string PMYear { get; set; }

        [XafDisplayName("1st JAN")]
        [Appearance("PMSchedule101", Enabled = false)]
        public string PMSchedule101 { get; set; }

        [XafDisplayName("1st FEB")]
        [Appearance("PMSchedule102", Enabled = false)]
        public string PMSchedule102 { get; set; }

        [XafDisplayName("1st MAR")]
        [Appearance("PMSchedule103", Enabled = false)]
        public string PMSchedule103 { get; set; }

        [XafDisplayName("1st APR")]
        [Appearance("PMSchedule104", Enabled = false)]
        public string PMSchedule104 { get; set; }

        [XafDisplayName("1st MAY")]
        [Appearance("PMSchedule105", Enabled = false)]
        public string PMSchedule105 { get; set; }

        [XafDisplayName("1st JUN")]
        [Appearance("PMSchedule106", Enabled = false)]
        public string PMSchedule106 { get; set; }

        [XafDisplayName("1st JUL")]
        [Appearance("PMSchedule107", Enabled = false)]
        public string PMSchedule107 { get; set; }

        [XafDisplayName("1st AUG")]
        [Appearance("PMSchedule108", Enabled = false)]
        public string PMSchedule108 { get; set; }

        [XafDisplayName("1st SEP")]
        [Appearance("PMSchedule109", Enabled = false)]
        public string PMSchedule109 { get; set; }

        [XafDisplayName("1st OCT")]
        [Appearance("PMSchedule110", Enabled = false)]
        public string PMSchedule110 { get; set; }

        [XafDisplayName("1st NOV")]
        [Appearance("PMSchedule111", Enabled = false)]
        public string PMSchedule111 { get; set; }

        [XafDisplayName("1st DEC")]
        [Appearance("PMSchedule112", Enabled = false)]
        public string PMSchedule112 { get; set; }

        [XafDisplayName("2nd JAN")]
        [Appearance("PMSchedule201", Enabled = false, FontColor = "Green")]
        public string PMSchedule201 { get; set; }

        [XafDisplayName("2nd FEB")]
        [Appearance("PMSchedule202", Enabled = false, FontColor = "Green")]
        public string PMSchedule202 { get; set; }

        [XafDisplayName("2nd MAR")]
        [Appearance("PMSchedule203", Enabled = false, FontColor = "Green")]
        public string PMSchedule203 { get; set; }

        [XafDisplayName("2nd APR")]
        [Appearance("PMSchedule204", Enabled = false, FontColor = "Green")]
        public string PMSchedule204 { get; set; }

        [XafDisplayName("2nd MAY")]
        [Appearance("PMSchedule205", Enabled = false, FontColor = "Green")]
        public string PMSchedule205 { get; set; }

        [XafDisplayName("2nd JUN")]
        [Appearance("PMSchedule206", Enabled = false, FontColor = "Green")]
        public string PMSchedule206 { get; set; }

        [XafDisplayName("2nd JUL")]
        [Appearance("PMSchedule207", Enabled = false, FontColor = "Green")]
        public string PMSchedule207 { get; set; }

        [XafDisplayName("2nd AUG")]
        [Appearance("PMSchedule208", Enabled = false, FontColor = "Green")]
        public string PMSchedule208 { get; set; }

        [XafDisplayName("2nd SEP")]
        [Appearance("PMSchedule209", Enabled = false, FontColor = "Green")]
        public string PMSchedule209 { get; set; }

        [XafDisplayName("2nd OCT")]
        [Appearance("PMSchedule210", Enabled = false, FontColor = "Green")]
        public string PMSchedule210 { get; set; }

        [XafDisplayName("2nd NOV")]
        [Appearance("PMSchedule211", Enabled = false, FontColor = "Green")]
        public string PMSchedule211 { get; set; }

        [XafDisplayName("2nd DEC")]
        [Appearance("PMSchedule212", Enabled = false, FontColor = "Green")]
        public string PMSchedule212 { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class YearParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public YearParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Year")]
        public int MyYear { get; set; }

        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("Equipment")]
    [NavigationItem("PM Schedule Setup")]
    [XafDisplayName("PM Schedule Full Calender")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMCalenderTemp : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public PMCalenderTemp()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        public string Equipment { get; set; }
        [XafDisplayName("Equipment Name")]
        public string EquipmentName { get; set; }
        [XafDisplayName("EQ Interface Code")]
        public string BoFullCode { get; set; }

        public string EquipmentComponent { get; set; }
        [XafDisplayName("Component Name")]
        public string EquipmentComponentName { get; set; }
        [XafDisplayName("Com Interface Code")]
        public string ComBoFullCode { get; set; }

        public string PMSchedule { get; set; }
        [XafDisplayName("PM Name")]
        public string PMName { get; set; }
        [XafDisplayName("Criticality")]
        public string Criticality { get; set; }
        [XafDisplayName("Planner Group")]
        public string PlannerGroup { get; set; }
        [XafDisplayName("Check List")]
        public string ChecklistNV { get; set; }
        [XafDisplayName("PM Interface Code")]
        public string PMBoFullCode { get; set; }

        [XafDisplayName("Nested")]
        public int IsNested { get; set; }

        [XafDisplayName("Year")]
        public string PMYear { get; set; }

        [XafDisplayName("1st JAN")]
        public string PMSchedule101 { get; set; }

        [XafDisplayName("1st FEB")]
        public string PMSchedule102 { get; set; }

        [XafDisplayName("1st MAR")]
        public string PMSchedule103 { get; set; }

        [XafDisplayName("1st APR")]
        public string PMSchedule104 { get; set; }

        [XafDisplayName("1st MAY")]
        public string PMSchedule105 { get; set; }

        [XafDisplayName("1st JUN")]
        public string PMSchedule106 { get; set; }

        [XafDisplayName("1st JUL")]
        public string PMSchedule107 { get; set; }

        [XafDisplayName("1st AUG")]
        public string PMSchedule108 { get; set; }

        [XafDisplayName("1st SEP")]
        public string PMSchedule109 { get; set; }

        [XafDisplayName("1st OCT")]
        public string PMSchedule110 { get; set; }

        [XafDisplayName("1st NOV")]
        [Appearance("PMSchedule111", Enabled = false)]
        public string PMSchedule111 { get; set; }

        [XafDisplayName("1st DEC")]
        public string PMSchedule112 { get; set; }

        [XafDisplayName("2nd JAN")]
        [Appearance("PMSchedule201", FontColor = "Green")]
        public string PMSchedule201 { get; set; }

        [XafDisplayName("2nd FEB")]
        [Appearance("PMSchedule202", FontColor = "Green")]
        public string PMSchedule202 { get; set; }

        [XafDisplayName("2nd MAR")]
        [Appearance("PMSchedule203", FontColor = "Green")]
        public string PMSchedule203 { get; set; }

        [XafDisplayName("2nd APR")]
        [Appearance("PMSchedule204", FontColor = "Green")]
        public string PMSchedule204 { get; set; }

        [XafDisplayName("2nd MAY")]
        [Appearance("PMSchedule205", FontColor = "Green")]
        public string PMSchedule205 { get; set; }

        [XafDisplayName("2nd JUN")]
        [Appearance("PMSchedule206", FontColor = "Green")]
        public string PMSchedule206 { get; set; }

        [XafDisplayName("2nd JUL")]
        [Appearance("PMSchedule207", FontColor = "Green")]
        public string PMSchedule207 { get; set; }

        [XafDisplayName("2nd AUG")]
        [Appearance("PMSchedule208", FontColor = "Green")]
        public string PMSchedule208 { get; set; }

        [XafDisplayName("2nd SEP")]
        [Appearance("PMSchedule209", FontColor = "Green")]
        public string PMSchedule209 { get; set; }

        [XafDisplayName("2nd OCT")]
        [Appearance("PMSchedule210", FontColor = "Green")]
        public string PMSchedule210 { get; set; }

        [XafDisplayName("2nd NOV")]
        [Appearance("PMSchedule211", FontColor = "Green")]
        public string PMSchedule211 { get; set; }

        [XafDisplayName("2nd DEC")]
        [Appearance("PMSchedule212", FontColor = "Green")]
        public string PMSchedule212 { get; set; }

        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }


    [DomainComponent]
    //[DefaultClassOptions]
    //[DefaultProperty("Equipment")]
    //[NavigationItem("PM Schedule Setup")]
    //[XafDisplayName("PM Schedule Monthly Generation")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMWorkOrderTemp : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public PMWorkOrderTemp()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        public string Equipment { get; set; }
        [XafDisplayName("Equipment Name")]
        public string EquipmentName { get; set; }
        [XafDisplayName("EQ Interface Code")]
        public string BoFullCode { get; set; }

        public string EquipmentComponent { get; set; }
        [XafDisplayName("Component Name")]
        public string EquipmentComponentName { get; set; }
        [XafDisplayName("Com Interface Code")]
        public string ComBoFullCode { get; set; }

        public string PMSchedule { get; set; }
        [XafDisplayName("PM Name")]
        public string PMName { get; set; }
        [XafDisplayName("Criticality")]
        public string Criticality { get; set; }
        [XafDisplayName("Planner Group")]
        public string PlannerGroup { get; set; }
        [XafDisplayName("Check List")]
        public string ChecklistNV { get; set; }
        [XafDisplayName("PM Interface Code")]
        public string PMBoFullCode { get; set; }

        [XafDisplayName("Nested")]
        public int IsNested { get; set; }

        [XafDisplayName("Year")]
        public string PMYear { get; set; }


        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("TodayDate")]
    [NavigationItem("Reports")]
    [XafDisplayName("Work Full List")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WRWOFullStatus : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public WRWOFullStatus()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Report Date")]
        public DateTime TodayDate { get; set; }
        [XafDisplayName("WR No")]
        public string WRNo { get; set; }
        [XafDisplayName("WR Date")]
        public DateTime? WRDate { get; set; }
        public string EquipmentCode { get; set; }
        public string EquipmentName { get; set; }
        [XafDisplayName("Equipment Criticality")]
        public string EqCritical { get; set; }
        public string ComponentCode { get; set; }
        public string ComponentName { get; set; }
        [XafDisplayName("Component Criticality")]
        public string ComCritical { get; set; }
        [XafDisplayName("WR Status")]
        public string WRStatus { get; set; }
        [XafDisplayName("WO No")]
        public string WONo { get; set; }
        [XafDisplayName("WO Date")]
        public DateTime? WODate { get; set; }
        [XafDisplayName("WO Type")]
        public string WOType { get; set; }
        [XafDisplayName("WO Remarks")]
        public string WORemarks { get; set; }
        public string PlannerGroup { get; set; }
        [XafDisplayName("WO Status")]
        public string WOStatus { get; set; }
        [XafDisplayName("WO Job Status")]
        public string WOJobStatus { get; set; }
        [XafDisplayName("WO Approve Date")]
        public DateTime? WOApproveDate { get; set; }
        [Browsable(false)]
        public int PRID { get; set; }
        [XafDisplayName("PR No")]
        public string PRNo { get; set; }
        [XafDisplayName("PR Date")]
        public DateTime? PRDate { get; set; }
        [XafDisplayName("PR Status")]
        public string PRStatus { get; set; }
        [XafDisplayName("PR Amount")]
        public decimal? PRAmount { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }


    [DomainComponent]
    //[DefaultClassOptions]
    //[DefaultProperty("ID")]
    //[NavigationItem("Reports")]
    //[XafDisplayName("Current AMS Measure")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WeeklyTodayAMSMeasure : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public WeeklyTodayAMSMeasure()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        //[Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Today")]
        public DateTime ThisDate { get; set; }

        [XafDisplayName("Weekly Measure")]
        public string AnalysisName { get; set; }
        [XafDisplayName("Measure")]
        public string AnalysisRemarks { get; set; }

        [Appearance("W1PG01", BackColor = "yellow")]
        [XafDisplayName("1-7 days PG01")]
        public int W1PG01 { get; set; }
        [Appearance("W1PG02", BackColor = "yellow")]
        [XafDisplayName("1-7 days PG02")]
        public int W1PG02 { get; set; }
        [Appearance("W1PG03", BackColor = "yellow")]
        [XafDisplayName("1-7 days PG03")]
        public int W1PG03 { get; set; }
        [Appearance("W1PG04", BackColor = "yellow")]
        [XafDisplayName("1-7 days PG04")]
        public int W1PG04 { get; set; }

        [Appearance("W2PG01", BackColor = "green")]
        [XafDisplayName("8-14 days PG01")]
        public int W2PG01 { get; set; }
        [Appearance("W2PG02", BackColor = "green")]
        [XafDisplayName("8-14 days PG02")]
        public int W2PG02 { get; set; }
        [Appearance("W2PG03", BackColor = "green")]
        [XafDisplayName("8-14 days PG03")]
        public int W2PG03 { get; set; }
        [Appearance("W2PG04", BackColor = "green")]
        [XafDisplayName("8-14 days PG04")]
        public int W2PG04 { get; set; }

        [Appearance("W3PG01", BackColor = "pink")]
        [XafDisplayName("15-21 days PG01")]
        public int W3PG01 { get; set; }
        [Appearance("W3PG02", BackColor = "pink")]
        [XafDisplayName("15-21 days PG02")]
        public int W3PG02 { get; set; }
        [Appearance("W3PG03", BackColor = "pink")]
        [XafDisplayName("15-21 days PG03")]
        public int W3PG03 { get; set; }
        [Appearance("W3PG04", BackColor = "pink")]
        [XafDisplayName("15-21 days PG04")]
        public int W3PG04 { get; set; }

        [XafDisplayName(">22 days PG01")]
        public int W4PG01 { get; set; }
        [XafDisplayName(">22 days PG02")]
        public int W4PG02 { get; set; }
        [XafDisplayName(">22 days PG03")]
        public int W4PG03 { get; set; }
        [XafDisplayName(">22 days PG04")]
        public int W4PG04 { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }



    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    [RuleCriteria("YearMonthParametersSaveRule", DefaultContexts.Save, "IsValid", "Year Month is invalid.")]
    public class YearMonthParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public YearMonthParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [Browsable(false)]
        public bool IsValid
        {
            get
            {
                if (MyYear < 2000 || MyYear > 3000)
                    return false;
                if (MyMonth < 1 || MyMonth > 12)
                    return false;

                return true;
            }
        }
        private int _MyYear;
        //[DataSourceProperty("ListYear", DataSourcePropertyIsNullMode.SelectNothing)]
        [XafDisplayName("Year")]
        public int MyYear
        {
            get
            {
                return _MyYear;
            }
            set 
            {
                if (_MyYear != value)
                {
                    _MyYear = value;
                    OnPropertyChanged("MyYear");
                }
            }
        }

        private int _MyMonth;
        //[DataSourceProperty("ListMonth", DataSourcePropertyIsNullMode.SelectNothing)]
        [XafDisplayName("Month")]
        public int MyMonth
        {
            get
            {
                return _MyMonth;
            }
            set
            {
                if (_MyMonth != value)
                {
                    _MyMonth = value;
                    OnPropertyChanged("MyMonth");
                }
            }
        }

        //private BindingList<int> _ListYear;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Browsable(false)]
        //public IList<int> ListYear
        //{
        //    get
        //    {
        //        _ListYear = new BindingList<int>();
        //        for (int x = 10; x > 0; x--)
        //        {
        //            _ListYear.Add(MyYear - x);
        //        }

        //        for (int x = 1; x <= 10; x++)
        //        {
        //            _ListYear.Add(MyYear + x);
        //        }
        //        return _ListYear;
        //    }
        //}

        //private BindingList<int> _ListMonth;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        //[Browsable(false)]
        //public IList<int> ListMonth
        //{
        //    get
        //    {
        //        _ListMonth = new BindingList<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        //        return _ListMonth;
        //    }
        //}
        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }



    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("ID")]
    [NavigationItem("Reports")]
    [XafDisplayName("Bad Actor")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BadActorDetails : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public BadActorDetails()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        //[Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Work Order No")]
        public string WorkOrder { get; set; }

        [XafDisplayName("Work Order Date")]
        public DateTime? WorkOrderDate { get; set; }

        [XafDisplayName("PR No")]
        public string PurchaseRequest { get; set; }

        [XafDisplayName("PR Date")]
        public DateTime? PurchaseRequestDate { get; set; }

        [XafDisplayName("Equipment Group")]
        public string EquipmentGroup { get; set; }

        [XafDisplayName("Equipment Class")]
        public string EquipmentClass { get; set; }

        [XafDisplayName("Equipment Code")]
        public string EquipmentCode { get; set; }

        [XafDisplayName("PR Amount")]
        public decimal PRAmount { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }



    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("ID")]
    [NavigationItem("Reports")]
    [XafDisplayName("ME Monthly KPI Measures")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class MEMonthlyKPIMeasuresDetails : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public MEMonthlyKPIMeasuresDetails()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        //[Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Analysis Name")]
        public string AnalysisName { get; set; }

        [XafDisplayName("Remakrs")]
        public string AnalysisRemarks { get; set; }

        [XafDisplayName("Work Order No")]
        public string WorkOrder { get; set; }

        [XafDisplayName("Work Order Date")]
        public DateTime? WorkOrderDate { get; set; }

        [XafDisplayName("Work Request No")]
        public string WorkRequest { get; set; }

        [XafDisplayName("Work Request Date")]
        public DateTime? WorkRequestDate { get; set; }

        [XafDisplayName("Planner Group")]
        public string PlannerGroup { get; set; }

        [XafDisplayName("Plan Start Date")]
        public DateTime? PlanStartDate { get; set; }

        [XafDisplayName("Plan End Date")]
        public DateTime? PlanEndDate { get; set; }

        [XafDisplayName("Schedule Start Date")]
        public DateTime? ScheduleStartDate { get; set; }

        [XafDisplayName("Schedule End Date")]
        public DateTime? ScheduleEndDate { get; set; }

        [XafDisplayName("Actual Start Date")]
        public DateTime? ActualStartDate { get; set; }

        [XafDisplayName("Actual End Date")]
        public DateTime? ActualEndDate { get; set; }

        [XafDisplayName("Doc Status")]
        public DocumentStatus DocStatus { get; set; }

        [XafDisplayName("Equipment Code")]
        public string Equipment { get; set; }

        [XafDisplayName("Equipment Name")]
        public string EquipmentName { get; set; }

        [XafDisplayName("Component Code")]
        public string Component { get; set; }

        [XafDisplayName("Component Name")]
        public string ComponentName { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("ID")]
    [NavigationItem("Reports")]
    [XafDisplayName("AI Monthly KPI Measures")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class IAMonthlyKPIMeasuresDetails : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public IAMonthlyKPIMeasuresDetails()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        //[Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Analysis Name")]
        public string AnalysisName { get; set; }

        [XafDisplayName("Remakrs")]
        public string AnalysisRemarks { get; set; }

        [XafDisplayName("Work Order No")]
        public string WorkOrder { get; set; }

        [XafDisplayName("Work Order Date")]
        public DateTime? WorkOrderDate { get; set; }

        [XafDisplayName("Work Request No")]
        public string WorkRequest { get; set; }

        [XafDisplayName("Work Request Date")]
        public DateTime? WorkRequestDate { get; set; }

        [XafDisplayName("Planner Group")]
        public string PlannerGroup { get; set; }

        [XafDisplayName("Plan Start Date")]
        public DateTime? PlanStartDate { get; set; }

        [XafDisplayName("Plan End Date")]
        public DateTime? PlanEndDate { get; set; }

        [XafDisplayName("Schedule Start Date")]
        public DateTime? ScheduleStartDate { get; set; }

        [XafDisplayName("Schedule End Date")]
        public DateTime? ScheduleEndDate { get; set; }

        [XafDisplayName("Reschedule Start Date")]
        public DateTime? RescheduleStartDate { get; set; }

        [XafDisplayName("Reschedule End Date")]
        public DateTime? RescheduleEndDate { get; set; }
        [XafDisplayName("Actual Start Date")]
        public DateTime? ActualStartDate { get; set; }

        [XafDisplayName("Actual End Date")]
        public DateTime? ActualEndDate { get; set; }

        [XafDisplayName("Doc Status")]
        public DocumentStatus DocStatus { get; set; }

        [XafDisplayName("Equipment Code")]
        public string Equipment { get; set; }

        [XafDisplayName("Equipment Name")]
        public string EquipmentName { get; set; }

        [XafDisplayName("Component Code")]
        public string Component { get; set; }

        [XafDisplayName("Component Name")]
        public string ComponentName { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }



    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("ID")]
    [NavigationItem("Reports")]
    [XafDisplayName("Deviation via LAFD Isolation")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WODeviationList : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public WODeviationList()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        //[Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Equipment")]
        public string Equipment { get; set; }

        [XafDisplayName("Component")]
        public string Component { get; set; }

        [XafDisplayName("Work Order No")]
        public string DocNo { get; set; }

        [XafDisplayName("Work Order Date")]
        public DateTime? DocDate { get; set; }

        [XafDisplayName("Planner Group")]
        public string PlannerGroup { get; set; }

        [XafDisplayName("Last OLAFD Deviation No.")]
        public string LastDeviationDocNo { get; set; }

        [XafDisplayName("Last OLAFD Deviation Due")]
        public DateTime? LastDeviationDocDate { get; set; }

        [XafDisplayName("Last Isolation Deviation No.")]
        public string LastIsolatedDeviationDocNo { get; set; }

        [XafDisplayName("Last Isolation Deviation Due")]
        public DateTime? LastIsolatedDeviationDocDate { get; set; }


        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

    [DomainComponent]
    [DefaultClassOptions]
    [DefaultProperty("ID")]
    [NavigationItem("Reports")]
    [XafDisplayName("Deviation via MOC")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WRDeviationList : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public WRDeviationList()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        //[Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Equipment")]
        public string Equipment { get; set; }

        [XafDisplayName("Component")]
        public string Component { get; set; }

        [XafDisplayName("Work Request No")]
        public string DocNo { get; set; }

        [XafDisplayName("Work Request Date")]
        public DateTime? DocDate { get; set; }

        [XafDisplayName("Planner Group")]
        public string PlannerGroup { get; set; }

        [XafDisplayName("Last MOC No.")]
        public string LastDeviationDocNo { get; set; }

        [XafDisplayName("Last MOC Due")]
        public DateTime? LastDeviationDocDate { get; set; }

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }


    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DeviationAppParameters : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public DeviationAppParameters()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [XafDisplayName("Date To")]
        [Appearance("ParamDate", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        public DateTime ParamDate { get; set; }

        [XafDisplayName("Remarks")]
        [Appearance("ParamString", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        public string ParamString { get; set; }

        [XafDisplayName("Important")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("ActionMessage2", Enabled = false, FontColor = "Red")]
        public string ActionMessage { get; set; }

        [Browsable(false)]
        public bool IsErr { get; set; }
        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }


    [DomainComponent]
    //[DefaultClassOptions]
    //[ImageName("BO_Unknown")]
    //[DefaultProperty("SampleProperty")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DeviationReviewerAck : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        private IObjectSpace objectSpace;
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public DeviationReviewerAck()
        {
        }
        // Add this property as the key member in the CustomizeTypesInfo event
        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ID { get; set; }

        [Browsable(false)]  // Hide the entity identifier from UI.
        public int ReviewID { get; set; }

        [XafDisplayName("#")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("RowNumber", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("RowNumber2", Enabled = false)]
        public int RowNumber { get; set; }

        [XafDisplayName("User ID")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("UserID", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("UserID2", Enabled = false)]
        public string UserID { get; set; }

        [XafDisplayName("User Name")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("UserName", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("UserName2", Enabled = false)]
        public string UserName { get; set; }

        [XafDisplayName("Withdrawn")]
        [Appearance("Reject", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("Reject2", Enabled = false)]
        public bool Reject { get; set; }

        [XafDisplayName("Date Ack")]
        [Appearance("ParamDate", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        public DateTime ParamDate { get; set; }

        [XafDisplayName("Comment")]
        [Appearance("ParamString", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [FieldSize(1024)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ParamString { get; set; }

        [XafDisplayName("Important")]
        //[Appearance("ActionMessage", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Criteria = "IsErr")]
        [Appearance("ActionMessage2", Enabled = false, FontColor = "Red")]
        [FieldSize(1024)]
        public string ActionMessage { get; set; }

        [Browsable(false)]
        public bool IsErr { get; set; }
        //private string sampleProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //public string SampleProperty
        //{
        //    get { return sampleProperty; }
        //    set
        //    {
        //        if (sampleProperty != value)
        //        {
        //            sampleProperty = value;
        //            OnPropertyChanged("SampleProperty");
        //        }
        //    }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.SampleProperty = "Paid";
        //}

        #region IXafEntityObject members (see https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppIXafEntityObjecttopic.aspx)
        void IXafEntityObject.OnCreated()
        {
            // Place the entity initialization code here.
            // You can initialize reference properties using Object Space methods; e.g.:
            // this.Address = objectSpace.CreateObject<Address>();
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
        IObjectSpace IObjectSpaceLink.ObjectSpace
        {
            get { return objectSpace; }
            set { objectSpace = value; }
        }
        #endregion

        #region INotifyPropertyChanged members (see http://msdn.microsoft.com/en-us/library/system.componentmodel.inotifypropertychanged(v=vs.110).aspx)
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }

}