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
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<PMScheduleCalenders> PMScheduleCalenderss { get; set; }" syntax.
    //[DefaultClassOptions]
    //[DefaultProperty("Equipment")]
    //[NavigationItem("PM Schedule Setup")]
    //[XafDisplayName("PM Schedule Equipment & Component Calender")]
    //[ImageName("BO_Unknown")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMScheduleCalenders : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public PMScheduleCalenders()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        [XafDisplayName("Equipment")]
        [Appearance("Equipment", Enabled = false)]
        public Equipments Equipment { get; set; }

        [XafDisplayName("Equipment Name")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("EquipmentName", Enabled = false)]
        public string EquipmentName { get { return Equipment == null ? "" : Equipment.BoName; } }

        [XafDisplayName("EQ Interface Code")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("BoFullCode", Enabled = false)]
        public string BoFullCode { get { return Equipment == null ? "" : Equipment.BoFullCode; } }

        [XafDisplayName("Component")]
        [Appearance("EquipmentComponent", Enabled = false)]
        public EquipmentComponents EquipmentComponent { get; set; }

        [XafDisplayName("Component Name")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("EquipmentComponentName", Enabled = false)]
        public string EquipmentComponentName { get { return EquipmentComponent == null ? "" : EquipmentComponent.BoName; } }

        [XafDisplayName("Com Interface Code")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("ComBoFullCode", Enabled = false)]
        public string ComBoFullCode { get { return EquipmentComponent == null ? "" : EquipmentComponent.BoFullCode; } }

        [XafDisplayName("PM Schedule")]
        [Appearance("PMSchedule", Enabled = false)]
        public PMSchedules PMSchedule { get; set; }

        [XafDisplayName("PM Name")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("PMName", Enabled = false)]
        public string PMName { get { return PMSchedule == null ? "" : PMSchedule.BoName; } }

        [XafDisplayName("Criticality")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("Criticality", Enabled = false)]
        public Criticalities Criticality { get { return Equipment == null ? null : Equipment.Criticality; } }

        [XafDisplayName("Planner Group")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("PlannerGroup", Enabled = false)]
        public PlannerGroups PlannerGroup { get { return PMSchedule == null ? null : PMSchedule.PlannerGroup; } }

        [XafDisplayName("Check List")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("ChecklistNV", Enabled = false)]
        public string ChecklistNV { get { return PMSchedule == null ? "" : PMSchedule.ChecklistNV; } }

        [XafDisplayName("PM Interface Code")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
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
        public PMFrequencies PMSchedule101 { get; set; }

        [XafDisplayName("1st FEB")]
        [Appearance("PMSchedule102", Enabled = false)]
        public PMFrequencies PMSchedule102 { get; set; }

        [XafDisplayName("1st MAR")]
        [Appearance("PMSchedule103", Enabled = false)]
        public PMFrequencies PMSchedule103 { get; set; }

        [XafDisplayName("1st APR")]
        [Appearance("PMSchedule104", Enabled = false)]
        public PMFrequencies PMSchedule104 { get; set; }

        [XafDisplayName("1st MAY")]
        [Appearance("PMSchedule105", Enabled = false)]
        public PMFrequencies PMSchedule105 { get; set; }

        [XafDisplayName("1st JUN")]
        [Appearance("PMSchedule106", Enabled = false)]
        public PMFrequencies PMSchedule106 { get; set; }

        [XafDisplayName("1st JUL")]
        [Appearance("PMSchedule107", Enabled = false)]
        public PMFrequencies PMSchedule107 { get; set; }

        [XafDisplayName("1st AUG")]
        [Appearance("PMSchedule108", Enabled = false)]
        public PMFrequencies PMSchedule108 { get; set; }

        [XafDisplayName("1st SEP")]
        [Appearance("PMSchedule109", Enabled = false)]
        public PMFrequencies PMSchedule109 { get; set; }

        [XafDisplayName("1st OCT")]
        [Appearance("PMSchedule110", Enabled = false)]
        public PMFrequencies PMSchedule110 { get; set; }

        [XafDisplayName("1st NOV")]
        [Appearance("PMSchedule111", Enabled = false)]
        public PMFrequencies PMSchedule111 { get; set; }

        [XafDisplayName("1st DEC")]
        [Appearance("PMSchedule112", Enabled = false)]
        public PMFrequencies PMSchedule112 { get; set; }

        [XafDisplayName("2nd JAN")]
        [Appearance("PMSchedule201", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule201 { get; set; }

        [XafDisplayName("2nd FEB")]
        [Appearance("PMSchedule202", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule202 { get; set; }

        [XafDisplayName("2nd MAR")]
        [Appearance("PMSchedule203", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule203 { get; set; }

        [XafDisplayName("2nd APR")]
        [Appearance("PMSchedule204", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule204 { get; set; }

        [XafDisplayName("2nd MAY")]
        [Appearance("PMSchedule205", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule205 { get; set; }

        [XafDisplayName("2nd JUN")]
        [Appearance("PMSchedule206", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule206 { get; set; }

        [XafDisplayName("2nd JUL")]
        [Appearance("PMSchedule207", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule207 { get; set; }

        [XafDisplayName("2nd AUG")]
        [Appearance("PMSchedule208", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule208 { get; set; }

        [XafDisplayName("2nd SEP")]
        [Appearance("PMSchedule209", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule209 { get; set; }

        [XafDisplayName("2nd OCT")]
        [Appearance("PMSchedule210", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule210 { get; set; }

        [XafDisplayName("2nd NOV")]
        [Appearance("PMSchedule211", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule211 { get; set; }

        [XafDisplayName("2nd DEC")]
        [Appearance("PMSchedule212", Enabled = false, FontColor = "Green")]
        public PMFrequencies PMSchedule212 { get; set; }

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
