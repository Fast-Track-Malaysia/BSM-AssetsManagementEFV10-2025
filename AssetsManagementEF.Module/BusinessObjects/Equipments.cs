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
//using System.Drawing;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<Equipments> Equipmentss { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("Equipments")]
    //[ImageName("BO_Contact")]
    [XafDisplayName("Equipment")]
    [DefaultProperty("FullCode")]
    [RuleCriteria("EquipmentsDeleteRule", DefaultContexts.Delete, "1=0", "Cannot Delete.")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Equipments : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public Equipments()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
            this.EquipmentComponent = new List<EquipmentComponents>();
            this.EquipmentProperty = new List<EquipmentProperties>();
            this.EquipmentAttachment = new List<EquipmentAttachments>();
            this.EquipmentPhoto = new List<EquipmentPhotos>();

            this.PMScheduleEquipment = new List<PMScheduleEquipments>();
            this.WorkRequestEquipment = new List<WorkRequestEquipments>();
            this.WorkOrderEquipment = new List<WorkOrderEquipments>();
        }

        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

        // You can use the regular Code First syntax:
        //public string Name { get; set; }

        //[Browsable(false), Column("Color")]
        //public int Argb
        //{
        //    get { return fColor.ToArgb(); }
        //    set { fColor = Color.FromArgb(value); }
        //}
        //private Color fColor;
        //[NotMapped]
        //public Color Color
        //{
        //    get { return fColor; }
        //    set { fColor = value; }
        //}

        // Alternatively, specify more UI options: 

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

        private string _FullCode;
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Index(1), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("FullCode", Enabled = false)]
        [XafDisplayName("Equipment Full Code")]
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


        private string _BoCode;
        [XafDisplayName("Equipment Code"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleUniqueValue]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Appearance("BoCode", Enabled = false)]
        [Index(0), VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [XafDisplayName("Equipment Name"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(2), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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

        private EqClasses _EqClass;
        [ImmediatePostData]
        [XafDisplayName("Equipment Class"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[DataSourceProperty("EqClassList", DataSourcePropertyIsNullMode.SelectNothing)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Appearance("EqClass", Enabled = false, Criteria = "not IsNew", FontColor = "Black")]
        [Index(50), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual EqClasses EqClass
        {
            get { return _EqClass; }
            set
            {
                if (_EqClass != value)
                {
                    _EqClass = value;
                    OnPropertyChanged("EqClass");
                    if (value != null)
                    {
                        if (IsNew)
                        {
                            if (objectSpace != null && objectSpace.IsModified && _EqClass != null)
                            {
                                EqClasses eqclass = objectSpace.FindObject<EqClasses>(CriteriaOperator.Parse("ID=?", value.ID));

                                foreach (EqClassProperties dtl in eqclass.EqClassProperty)
                                {
                                    EquipmentProperties eqproperty = objectSpace.CreateObject<EquipmentProperties>();
                                    eqproperty.EqClassProperty = objectSpace.FindObject<EqClassProperties>(CriteriaOperator.Parse("ID=?", dtl.ID));
                                    EquipmentProperty.Add(eqproperty);
                                }
                                OnPropertyChanged("EquipmentProperty");
                            }
                        }
                    }
                }
            }
        }

        private Areas _Area;
        [XafDisplayName("Area"), ToolTip("Enter Text")]
        [ImmediatePostData]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(10), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual Areas Area
        {
            get { return _Area; }
            set
            {
                if (_Area != value)
                {
                    _Area = value;
                    OnPropertyChanged("Area");
                    //if (objectSpace != null && objectSpace.IsModified && _Area != null)
                    //{
                    //    _Location = null;
                    //}
                }
            }
        }

        private Locations _Location;
        //[ImmediatePostData]
        [XafDisplayName("Location"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[DataSourceProperty("LocationList", DataSourcePropertyIsNullMode.SelectNothing)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(20), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual Locations Location
        {
            get { return _Location; }
            set
            {
                if (_Location != value)
                {
                    _Location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private SubLocations _SubLocation;
        [ImmediatePostData]
        [XafDisplayName("Sub Location"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(30), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public virtual SubLocations SubLocation
        {
            get { return _SubLocation; }
            set
            {
                if (_SubLocation != value)
                {
                    _SubLocation = value;
                    OnPropertyChanged("SubLocation");
                }
            }
        }

        private string _Legacy;
        [XafDisplayName("Legacy"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(70), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Legacy
        {
            get { return _Legacy; }
            set
            {
                if (_Legacy != value)
                {
                    _Legacy = value;
                    OnPropertyChanged("Legacy");
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

        private EqGroups _EqGroup;
        [XafDisplayName("Equipment Group"), ToolTip("Enter Text")]
        [ImmediatePostData]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(40), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public virtual EqGroups EqGroup
        {
            get { return _EqGroup; }
            set
            {
                if (_EqGroup != value)
                {
                    _EqGroup = value;
                    OnPropertyChanged("EqGroup");
                    //if (_EqGroup != null && _EqClass != null)
                    //{
                    //    if (_EqClass.EqGroup.ID != _EqGroup.ID)
                    //    {
                    //        _EqClass = null;
                    //        OnPropertyChanged("EqClass");
                    //    }
                    //}
                    //else if (_EqGroup == null)
                    //{
                    //    _EqClass = null;
                    //    OnPropertyChanged("EqClass");
                    //}
                }
            }
        }

        private Criticalities _Criticality;
        [ImmediatePostData]
        [XafDisplayName("Criticality"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [Index(60), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public virtual Criticalities Criticality
        {
            get { return _Criticality; }
            set
            {
                if (_Criticality != value)
                {
                    _Criticality = value;
                    OnPropertyChanged("Criticality");
                    if (objectSpace != null && objectSpace.IsModified && _Criticality != null)
                    {
                        SCECategory = null;
                        SCESubCategory = null;
                    }
                }
            }
        }
        private SCECategories _SCECategory;
        [ImmediatePostData]
        [XafDisplayName("SCE Category"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(61), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DataSourceProperty("Criticality.SCECateogry", DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("SCECategory", Enabled = false)]
        public virtual SCECategories SCECategory
        {
            get { return _SCECategory; }
            set
            {
                if (_SCECategory != value)
                {
                    _SCECategory = value;
                    OnPropertyChanged("SCECategory");
                    if (objectSpace != null && objectSpace.IsModified && _SCECategory != null)
                    {
                        SCESubCategory = null;
                    }
                }
            }
        }
        private SCESubCategories _SCESubCategory;
        [XafDisplayName("SCE Sub Category"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        [Index(62), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DataSourceProperty("SCESubCategoryList", DataSourcePropertyIsNullMode.SelectNothing)]
        [Appearance("SCESubCategory", Enabled = false)]
        public virtual SCESubCategories SCESubCategory
        {
            get { return _SCESubCategory; }
            set
            {
                if (_SCESubCategory != value)
                {
                    _SCESubCategory = value;
                    OnPropertyChanged("SCESubCategory");
                }
            }
        }
        private string _Model;
        [XafDisplayName("Model"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(70), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Model
        {
            get { return _Model; }
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        private string _SerialNo;
        [XafDisplayName("Serial No"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(71), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string SerialNo
        {
            get { return _SerialNo; }
            set
            {
                if (_SerialNo != value)
                {
                    _SerialNo = value;
                    OnPropertyChanged("SerialNo");
                }
            }
        }

        private string _Make;
        [XafDisplayName("Make"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(72), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Make
        {
            get { return _Make; }
            set
            {
                if (_Make != value)
                {
                    _Make = value;
                    OnPropertyChanged("Make");
                }
            }
        }

        private string _LifeSpan;
        [XafDisplayName("Life Span"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(73), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string LifeSpan
        {
            get { return _LifeSpan; }
            set
            {
                if (_LifeSpan != value)
                {
                    _LifeSpan = value;
                    OnPropertyChanged("LifeSpan");
                }
            }
        }

        private string _Manufacture;
        [XafDisplayName("Manufacture"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(74), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Manufacture
        {
            get { return _Manufacture; }
            set
            {
                if (_Manufacture != value)
                {
                    _Manufacture = value;
                    OnPropertyChanged("Manufacture");
                }
            }
        }

        private string _Remarks;
        [XafDisplayName("Remarks"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [Index(75), VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                if (_Remarks != value)
                {
                    _Remarks = value;
                    OnPropertyChanged("Remarks");
                }
            }
        }


        private bool _IsApproved;
        [XafDisplayName("Approved"), ToolTip("Enter Text")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
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

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

        [XafDisplayName("Component")]
        public virtual IList<EquipmentComponents> EquipmentComponent { get; set; }

        [XafDisplayName("Attachment")]
        public virtual IList<EquipmentAttachments> EquipmentAttachment { get; set; }

        [XafDisplayName("Photo")]
        public virtual IList<EquipmentPhotos> EquipmentPhoto { get; set; }

        [XafDisplayName("Property")]
        [Browsable(false)]
        public virtual IList<EquipmentProperties> EquipmentProperty { get; set; }

        [XafDisplayName("PM Schedule")]
        [Appearance("PMScheduleEquipment", Enabled = false)]
        public virtual IList<PMScheduleEquipments> PMScheduleEquipment { get; set; }

        [XafDisplayName("Work Request")]
        [Appearance("WorkRequestEquipment", Enabled = false)]
        public virtual IList<WorkRequestEquipments> WorkRequestEquipment { get; set; }

        [XafDisplayName("Work Order")]
        [Appearance("WorkOrderEquipment", Enabled = false)]
        public virtual IList<WorkOrderEquipments> WorkOrderEquipment { get; set; }



        //public void refPMSchedule (ref BindingList<PMScheduleDC> mylist)
        //{
        //    foreach(PMScheduleDC obj in _PMSchedule)
        //    {
        //        mylist.Add(obj);
        //    }
        //}

        #region PMSchedule
        
        private BindingList<PMScheduleDC> _PMSchedule;
        [XafDisplayName("Schedule List")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Appearance("PMSchedule", Enabled = false)]
        public virtual IList<PMScheduleDC> PMSchedule
        {
            get
            {
                if (objectSpace == null || this == null)
                {
                    _PMSchedule = null;
                }
                else
                {
                    if (IsApproved && IsActive)
                    {
                        int myyear = DateTime.Today.Year;
                        RefreshSchedule(myyear, this);
                    }
                    else
                        _PMSchedule = null;

                }
                return _PMSchedule;
            }
        }
        private void RefreshSchedule(int myyear, Equipments eq)
        {
            try
            {
                if (eq != null)
                {
                    //IList<PMSchedules> schs = objectSpace.GetObjects<PMSchedules>(new ContainsOperator("Detail", new BinaryOperator("ID", eq.ID, BinaryOperatorType.Equal)));
                    _PMSchedule = new BindingList<PMScheduleDC>();

                    //IList<PMSchedules> schs = objectSpace.GetObjects<PMSchedules>(CriteriaOperator.Parse("not [IsNested] and [IsApproved] and [IsActive] and [Detail][[Equipment.ID]=?]", eq.ID));
                    //IEnumerable<PMSchedules> sortedEnum = schs.OrderBy(t => t.SortingBy);
                    //IList<PMSchedules> schsList = sortedEnum.ToList();

                    //IList<PMSchedules> schsnest = objectSpace.GetObjects<PMSchedules>(CriteriaOperator.Parse("[IsNested] and [IsApproved] and [IsActive] and [Detail][[Equipment.ID]=?]", eq.ID));
                    //IEnumerable<PMSchedules> nestsortedEnum = schsnest.OrderBy(t => t.SortingBy);
                    //IList<PMSchedules> nestschsList = nestsortedEnum.ToList();

                    //if (schsList.Count > 0)
                    //{
                    //    assignschedule(myyear, eq, schsList, false);
                    //}
                    //if (nestschsList.Count > 0)
                    //{
                    //    assignschedule(myyear, eq, nestschsList, true);
                    //}

                    IList<PMSchedules> schs = objectSpace.GetObjects<PMSchedules>(CriteriaOperator.Parse("[IsApproved] and [IsActive] and [Detail][[Equipment.ID]=?]", eq.ID)).OrderBy(t => t.SortingBy).ToList();
                    //IEnumerable<PMSchedules> sortedEnum = schs.OrderBy(t => t.SortingBy);
                    //IList<PMSchedules> schsList = sortedEnum.ToList();
                    //IList<PMSchedules> schsList = schs.OrderBy(t => t.SortingBy).ToList();

                    if (schs.Count > 0)
                    {
                        assignschedule(myyear, eq, schs);
                    }

                }
            }
            catch { }
        }
        private void assignschedule(int myyear, Equipments eq, IList<PMSchedules> schs)
        {
            try
            {
                PMScheduleDC schcurrent = new PMScheduleDC();
                bool current = false;
                DateTime datefrom;
                int cycle = 0;
                foreach (PMSchedules sch in schs)
                {
                    current = false;

                    schcurrent = new PMScheduleDC();

                    if (sch.FromDate != null)
                    {
                        datefrom = (DateTime)sch.FromDate;
                    }
                    else
                    {
                        datefrom = (DateTime)GeneralSettings.pmstartdate;
                    }
                    if (sch.PMFrequency.Frequency == MaintenanceFrequency.MONTH || sch.PMFrequency.Frequency == MaintenanceFrequency.DAY)
                    {
                        if (sch.PMFrequency.Frequency == MaintenanceFrequency.DAY)
                            cycle = 1;
                        else
                            cycle = sch.PMFrequency.CycleCount;

                        datefrom = datefrom.AddMonths(cycle);
                        do
                        {
                            if (datefrom.Year == myyear || datefrom.Year == myyear + 1)
                            {
                                if (!current)
                                {
                                    schcurrent.PMYear = myyear.ToString() + " to " + (myyear + 1).ToString();
                                    schcurrent.Equipment = objectSpace.GetObjectByKey<Equipments>(eq.ID);
                                    schcurrent.IsNested = sch.IsNested;
                                    schcurrent.PMSchedule = objectSpace.GetObjectByKey<PMSchedules>(sch.ID);
                                    current = true;
                                }
                                assignMonth(datefrom, ref schcurrent, sch, myyear);
                            }
                            datefrom = datefrom.AddMonths(cycle);

                        } while (datefrom.Year <= myyear + 1);

                        if (current)
                        {
                            _PMSchedule.Add(schcurrent);
                        }
                    }
                }

                if (_PMSchedule.Count > 0)
                {
                    foreach (PMScheduleDC curr in _PMSchedule.OrderByDescending(pp => pp.PMSchedule.SortingBy).ToList())
                    {
                        int sortingby = curr.PMSchedule.SortingBy;
                        foreach (PMScheduleDC next in _PMSchedule.Where(pp => pp.PMSchedule.SortingBy < sortingby).OrderByDescending(pp => pp.PMSchedule.SortingBy).ToList())
                        {
                            if (!next.IsNested)
                            {
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule101))
                                    next.PMSchedule101 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule102))
                                    next.PMSchedule102 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule103))
                                    next.PMSchedule103 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule104))
                                    next.PMSchedule104 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule105))
                                    next.PMSchedule105 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule106))
                                    next.PMSchedule106 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule107))
                                    next.PMSchedule107 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule108))
                                    next.PMSchedule108 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule109))
                                    next.PMSchedule109 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule110))
                                    next.PMSchedule110 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule111))
                                    next.PMSchedule111 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule112))
                                    next.PMSchedule112 = null;


                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule201))
                                    next.PMSchedule201 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule202))
                                    next.PMSchedule202 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule203))
                                    next.PMSchedule203 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule204))
                                    next.PMSchedule204 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule205))
                                    next.PMSchedule205 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule206))
                                    next.PMSchedule206 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule207))
                                    next.PMSchedule207 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule208))
                                    next.PMSchedule208 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule209))
                                    next.PMSchedule209 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule210))
                                    next.PMSchedule210 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule211))
                                    next.PMSchedule211 = null;
                                if (!string.IsNullOrWhiteSpace(curr.PMSchedule212))
                                    next.PMSchedule212 = null;
                            }
                        }
                    }
                }
            }
            catch { }
        }
        private void assignMonth(DateTime mydate, ref PMScheduleDC schdc, PMSchedules mysch, int myyear)
        {
            try
            {
                if (mydate.Year == myyear)
                    switch (mydate.Month)
                    {
                        case 1:
                            schdc.PMSchedule101 = mysch.PMFrequency.BoShortName;
                            break;
                        case 2:
                            schdc.PMSchedule102 = mysch.PMFrequency.BoShortName;
                            break;
                        case 3:
                            schdc.PMSchedule103 = mysch.PMFrequency.BoShortName;
                            break;
                        case 4:
                            schdc.PMSchedule104 = mysch.PMFrequency.BoShortName;
                            break;
                        case 5:
                            schdc.PMSchedule105 = mysch.PMFrequency.BoShortName;
                            break;
                        case 6:
                            schdc.PMSchedule106 = mysch.PMFrequency.BoShortName;
                            break;
                        case 7:
                            schdc.PMSchedule107 = mysch.PMFrequency.BoShortName;
                            break;
                        case 8:
                            schdc.PMSchedule108 = mysch.PMFrequency.BoShortName;
                            break;
                        case 9:
                            schdc.PMSchedule109 = mysch.PMFrequency.BoShortName;
                            break;
                        case 10:
                            schdc.PMSchedule110 = mysch.PMFrequency.BoShortName;
                            break;
                        case 11:
                            schdc.PMSchedule111 = mysch.PMFrequency.BoShortName;
                            break;
                        case 12:
                            schdc.PMSchedule112 = mysch.PMFrequency.BoShortName;
                            break;
                    }
                else if (mydate.Year == myyear + 1)
                    switch (mydate.Month)
                    {
                        case 1:
                            schdc.PMSchedule201 = mysch.PMFrequency.BoShortName;
                            break;
                        case 2:
                            schdc.PMSchedule202 = mysch.PMFrequency.BoShortName;
                            break;
                        case 3:
                            schdc.PMSchedule203 = mysch.PMFrequency.BoShortName;
                            break;
                        case 4:
                            schdc.PMSchedule204 = mysch.PMFrequency.BoShortName;
                            break;
                        case 5:
                            schdc.PMSchedule205 = mysch.PMFrequency.BoShortName;
                            break;
                        case 6:
                            schdc.PMSchedule206 = mysch.PMFrequency.BoShortName;
                            break;
                        case 7:
                            schdc.PMSchedule207 = mysch.PMFrequency.BoShortName;
                            break;
                        case 8:
                            schdc.PMSchedule208 = mysch.PMFrequency.BoShortName;
                            break;
                        case 9:
                            schdc.PMSchedule209 = mysch.PMFrequency.BoShortName;
                            break;
                        case 10:
                            schdc.PMSchedule210 = mysch.PMFrequency.BoShortName;
                            break;
                        case 11:
                            schdc.PMSchedule211 = mysch.PMFrequency.BoShortName;
                            break;
                        case 12:
                            schdc.PMSchedule212 = mysch.PMFrequency.BoShortName;
                            break;
                    }
            }
            catch { }
        }

        #endregion

        #region listing
        /*
        private IList<SCECategories> _SCECategoryList;
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IList<SCECategories> SCECategoryList
        {
            get
            {
                if (Criticality == null || objectSpace == null)
                {
                    _SCECategoryList = null;
                }
                else
                {
                    RefreshSCECategoryList();
                }
                return _SCECategoryList;
            }
        }
        private void RefreshSCECategoryList()
        {
            _SCECategoryList = objectSpace.GetObjects<SCECategories>(CriteriaOperator.Parse("[Criticality.ID]=?", Criticality.ID));
        }

        private IList<SCESubCategories> _SCESubCategoryList;
        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public IList<SCESubCategories> SCESubCategoryList
        {
            get
            {
                if (SCECategory == null || objectSpace == null)
                {
                    _SCESubCategoryList = null;
                }
                else
                {
                    RefreshSCESubCategoryList();
                }
                return _SCESubCategoryList;
            }
        }
        private void RefreshSCESubCategoryList()
        {
            _SCESubCategoryList = objectSpace.GetObjects<SCESubCategories>(CriteriaOperator.Parse("[SCECategory.ID]=?", SCECategory.ID));
        }
        */
        #endregion
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
            IsApproved = false;
            IsActive = false;
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
            if (IsNew)
            {
                long temp = 0;
                EqClassDocs doc = objectSpace.FindObject<EqClassDocs>(CriteriaOperator.Parse("EqClass.ID=?", this.EqClass.ID));
                if (doc == null)
                {
                    temp = 1;
                    doc = objectSpace.CreateObject<EqClassDocs>();
                    doc.EqClass = objectSpace.FindObject<EqClasses>(CriteriaOperator.Parse("ID=?", this.EqClass.ID));
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

                BoCode = temp.ToString().PadLeft(3, '0');
                BoInt = (int)temp;
                
            }

            UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
            UpdateDate = DateTime.Now;

            IsNew = false;

            string rtn = "";
            rtn += Area == null || Area.BoShortName == null ? "" : Area.BoShortName.Trim() == "-" ? "" : Area.BoShortName;
            rtn += Location == null || Location.BoShortName == null ? "" : Location.BoShortName.Trim() == "" ? "" : "-" + Location.BoShortName;
            rtn += SubLocation == null || SubLocation.BoShortName == null ? "" : SubLocation.BoShortName.Trim() == "" ? "" : "-" + SubLocation.BoShortName;
            rtn += EqClass == null || EqClass.BoShortName == null ? "" : EqClass.BoShortName.Trim() == "" ? "-" : "-" + EqClass.BoShortName;
            rtn += BoCode;
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
