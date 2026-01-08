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
    [XafDisplayName("PM Schedule")]
    [DefaultProperty("FullCode")]
    [RuleCriteria("PMSchedulesDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PMSchedules : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public PMSchedules()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
            this.Detail = new List<PMScheduleEquipments>();
            this.Detail2 = new List<PMScheduleEqComponents>();
            this.PMScheduleChecklist = new List<PMScheduleChecklists>();
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

        private string _FullCode;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(10), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("PM Schedule Code", Enabled = false)]
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

        private int _BoInt;
        [Browsable(false)]
        public int BoInt
        {
            get { return _BoInt; }
            set
            {
                if (_BoInt != value)
                {
                    _BoInt = value;
                    OnPropertyChanged("BoInt");
                }
            }
        }

        private string _BoFullCode;
        [XafDisplayName("Interface Code")]
        public string BoFullCode
        {
            get { return _BoFullCode; }
            set
            {
                if (_BoFullCode != value)
                {
                    _BoFullCode = value;
                    OnPropertyChanged("BoFullCode");
                }
            }
        }

        private string _BoCode;
        [XafDisplayName("Schedule Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleUniqueValue]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        [Appearance("BoCode", Enabled = false)]
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
        [XafDisplayName("Schedule Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(20), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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

        private string _PMDescription;
        [XafDisplayName("PM Description"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(30), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PMDescription
        {
            get { return _PMDescription; }
            set
            {
                if (_PMDescription != value)
                {
                    _PMDescription = value;
                    OnPropertyChanged("PMDescription");
                }
            }
        }

        //private Equipments _Equipment;
        //[Browsable(false)]
        //[XafDisplayName("Equipment"), ToolTip("Enter Text")]
        ////[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        ////[RuleRequiredField(DefaultContexts.Save)]
        //public virtual Equipments Equipment
        //{
        //    get { return _Equipment; }
        //    set
        //    {
        //        if (_Equipment != value)
        //        {
        //            _Equipment = value;
        //            OnPropertyChanged("Equipment");
        //        }
        //    }
        //}

        private DateTime? _FromDate;
        [XafDisplayName("Start Schedule Date")]
        [Index(50), VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "{0:dd/MM/yyyy}")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        //[Appearance("FromDate", Enabled = false, Criteria = "not IsNew and not IsWPSChecking")]
        public DateTime? FromDate
        {
            get { return _FromDate; }
            set
            {
                if (_FromDate != value)
                {
                    _FromDate = value;
                    OnPropertyChanged("FromDate");
                }
            }
        }

        private PMFrequencies _PMFrequency;
        [XafDisplayName("PM Frequency"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(11), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [RuleRequiredField(DefaultContexts.Save)]
        public virtual PMFrequencies PMFrequency
        {
            get { return _PMFrequency; }
            set
            {
                if (_PMFrequency != value)
                {
                    _PMFrequency = value;
                    OnPropertyChanged("PMFrequency");
                }
            }
        }

        private PMClasses _PMClass;
        [ImmediatePostData]
        [XafDisplayName("PM Class"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[DataSourceProperty("EqClassList", DataSourcePropertyIsNullMode.SelectNothing)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("PMClass", Enabled = false, Criteria = "not IsNew", FontColor = "Black")]
        [Index(1), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual PMClasses PMClass
        {
            get { return _PMClass; }
            set
            {
                if (_PMClass != value)
                {
                    _PMClass = value;
                    OnPropertyChanged("PMClass");
                }
            }
        }

        private PMDepartments _PMDepartment;
        [XafDisplayName("PM Department"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(40), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual PMDepartments PMDepartment
        {
            get { return _PMDepartment; }
            set
            {
                if (_PMDepartment != value)
                {
                    _PMDepartment = value;
                    OnPropertyChanged("PMDepartment");
                }
            }
        }

        private CheckLists _CheckList;
        [XafDisplayName("Checklist"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(60), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public virtual CheckLists CheckList
        {
            get { return _CheckList; }
            set
            {
                if (_CheckList != value)
                {
                    _CheckList = value;
                    OnPropertyChanged("CheckList");
                }
            }
        }

        private string _CheckListName;
        [XafDisplayName("CheckList Ref"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleUniqueValue]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(61), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[Appearance("CheckListName", Enabled = false)]
        public string CheckListName
        {
            get { return _CheckListName; }
            set
            {
                if (_CheckListName != value)
                {
                    _CheckListName = value;
                    OnPropertyChanged("CheckListName");
                }
            }
        }

        private string _CheckListLink;
        [XafDisplayName("CheckList Link"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleUniqueValue]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(61), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("CheckListLink", Enabled = false)]
        public string CheckListLink
        {
            get { return _CheckListLink; }
            set
            {
                if (_CheckListLink != value)
                {
                    _CheckListLink = value;
                    OnPropertyChanged("CheckListLink");
                }
            }
        }

        private PlannerGroups _PlannerGroup;
        [XafDisplayName("Planner Group"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(70), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual PlannerGroups PlannerGroup
        {
            get { return _PlannerGroup; }
            set
            {
                if (_PlannerGroup != value)
                {
                    _PlannerGroup = value;
                    OnPropertyChanged("PlannerGroup");
                }
            }
        }

        private Priorities _Priority;
        [XafDisplayName("Priority"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(80), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual Priorities Priority
        {
            get { return _Priority; }
            set
            {
                if (_Priority != value)
                {
                    _Priority = value;
                    OnPropertyChanged("Priority");
                }
            }
        }

        private int _BufferMonth;
        [XafDisplayName("Buffer"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(90), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int BufferMonth
        {
            get { return _BufferMonth; }
            set
            {
                if (_BufferMonth != value)
                {
                    _BufferMonth = value;
                    OnPropertyChanged("BufferMonth");
                }
            }
        }

        private double _ReleaseWindow;
        [XafDisplayName("Release Window"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(90), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public double ReleaseWindow
        {
            get { return _ReleaseWindow; }
            set
            {
                if (_ReleaseWindow != value)
                {
                    _ReleaseWindow = value;
                    OnPropertyChanged("ReleaseWindow");
                }
            }
        }

        private string _WorkInstruction;
        [XafDisplayName("Work Instruction"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(100), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string WorkInstruction
        {
            get { return _WorkInstruction; }
            set
            {
                if (_WorkInstruction != value)
                {
                    _WorkInstruction = value;
                    OnPropertyChanged("WorkInstruction");
                }
            }
        }

        private int _PlanManCount;
        [XafDisplayName("Estimate Worker Count"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(101), VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public int PlanManCount
        {
            get { return _PlanManCount; }
            set
            {
                if (_PlanManCount != value)
                {
                    _PlanManCount = value;
                    OnPropertyChanged("PlanManCount");
                }
            }
        }

        private TimeSpan? _PlanManHour;
        [XafDisplayName("Estimate Man Hours"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(102), VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public TimeSpan? PlanManHour
        {
            get { return _PlanManHour; }
            set
            {
                if (_PlanManHour != value)
                {
                    _PlanManHour = value;
                    OnPropertyChanged("PlanManHour");
                }
            }
        }

        [Index(103), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(true)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Equipment
        {
            get
            {
                if (Detail.Count > 0)
                    return Detail.FirstOrDefault().Equipment.FullCode;
                if (Detail2.Count > 0)
                    return Detail2.FirstOrDefault().EquipmentComponent.ComponentFullCode;
                return "";
            }
        }


        [Index(104), VisibleInListView(true), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string EquipmentName
        {
            get
            {
                if (Detail.Count > 0)
                    return Detail.FirstOrDefault().EquipmentName;
                if (Detail2.Count > 0)
                    return "<C> " + Detail2.FirstOrDefault().EqComponentName;
                return null;
            }
        }

        private bool _IsNested;
        [XafDisplayName("Is Nested"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(110), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public bool IsNested
        {
            get { return _IsNested; }
            set
            {
                if (_IsNested != value)
                {
                    _IsNested = value;
                    OnPropertyChanged("IsNested");
                }
            }
        }

        private bool _IsConsistent;
        [XafDisplayName("Consistent"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Browsable(false)]
        public bool IsConsistent
        {
            get { return _IsConsistent; }
            set
            {
                if (_IsConsistent != value)
                {
                    _IsConsistent = value;
                    OnPropertyChanged("IsConsistent");
                }
            }
        }

        private bool _IsApproved;
        [XafDisplayName("Approved"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(130), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public bool IsApproved
        {
            get { return _IsApproved; }
            set
            {
                if (_IsApproved != value)
                {
                    _IsApproved = value;
                    OnPropertyChanged("IsApproved");
                }
            }
        }

        private bool _IsActive;
        [XafDisplayName("Active"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(140), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged("IsActive");
                }
            }
        }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Browsable(false)]
        public string ChecklistNV
        {
            get { return CheckList == null? CheckListName: CheckList.BoName; }
        }

        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Browsable(false)]
        public int SortingBy
        {
            get; set;
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        // Collection property:
        [XafDisplayName("Equipment List")]
        public virtual IList<PMScheduleEquipments> Detail { get; set; }

        [XafDisplayName("Component List")]
        public virtual IList<PMScheduleEqComponents> Detail2 { get; set; }

        [XafDisplayName("Check List")]
        //[Browsable(false)]
        public virtual IList<PMScheduleChecklists> PMScheduleChecklist { get; set; }

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
            IsApproved = false;
            IsActive = false;
            IsNew = true;
            IsConsistent = false;
            IsNested = false;
            BufferMonth = 0;
            PlanManCount = 0;
            PlanManHour = TimeSpan.FromMinutes(0);

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
                long temp = 0;
                PMClassDocs doc = objectSpace.FindObject<PMClassDocs>(CriteriaOperator.Parse("PMClass.ID=?", this.PMClass.ID));
                if (doc == null)
                {
                    temp = 1;
                    doc = objectSpace.CreateObject<PMClassDocs>();
                    doc.PMClass = objectSpace.FindObject<PMClasses>(CriteriaOperator.Parse("ID=?", this.PMClass.ID));
                    doc.NextDocNo = temp;
                }
                else
                {
                    temp = doc.NextDocNo;
                }

                if (temp <= 0)
                {
                    temp = 1;
                    doc.NextDocNo = 2;
                }
                else
                    doc.NextDocNo++;

                BoCode = temp.ToString().PadLeft(6, '0');
                BoInt = (int)temp;

            }
            IsNew = false;

            string rtn = "";
            rtn += PMClass == null || PMClass.BoShortName == null ? "" : PMClass.BoShortName.Trim() == "-" ? "" : PMClass.BoShortName;
            rtn += BoCode;
            FullCode = rtn;

            int nest = IsNested ? 1 : 0;
            SortingBy = PMFrequency == null ? 0 : ((int)PMFrequency.Frequency * 10000) + (nest * 1000) + PMFrequency.CycleCount;
            


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
