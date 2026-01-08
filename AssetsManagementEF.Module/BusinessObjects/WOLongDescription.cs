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
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<WRLongDescription> WRLongDescriptions { get; set; }" syntax.
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("DisplayEntry")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class WOLongDescription : IXafEntityObject, IObjectSpaceLink, INotifyPropertyChanged
    {
        public WOLongDescription()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }
        [Browsable(false)]  // Hide the entity identifier from UI.
        public Int32 ID { get; protected set; }

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

        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DisplayEntry
        {
            get
            {
                string text = "";
                text = UpdateUser == null ? "" : UpdateUser.FullName + "[{0:dd/MM/yyyy}]";
                text = string.Format(text, UpdateDate == null? "": ((DateTime)UpdateDate).ToShortDateString());

                return text;
            }
        }

        // You can use the regular Code First syntax:
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string DisplayItem
        {
            get
            {

                var text = LongDescription;
                if (text != null && text.Length > 0)
                {
                    const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";//matches one or more (white space or line breaks) between '>' and '<'
                    const string stripFormatting = @"<[^>]*(>|$)";//match any character between '<' and '>', even when end tag is missing
                    const string lineBreak = @"<(br|BR|p|P)\s{0,1}\/{0,1}>";//matches: <br>,<br/>,<br />,<BR>,<BR/>,<BR />
                    var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
                    var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
                    var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);

                    //Decode html specific characters
                    text = System.Net.WebUtility.HtmlDecode(text);
                    //Remove tag whitespace/line breaks
                    text = tagWhiteSpaceRegex.Replace(text, "><");
                    //Replace <br /> with line breaks
                    text = lineBreakRegex.Replace(text, Environment.NewLine);
                    //Strip formatting
                    text = stripFormattingRegex.Replace(text, string.Empty);
                }
                else
                    text = "";

                return text;
            }
        }

        // Alternatively, specify more UI options: 
        private string _LongDescription;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Detail Description"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), VisibleInListView(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[FieldSizeAttribute(5000)]
        [EditorAlias("HTML")]
        public string LongDescription
        {
            get { return _LongDescription; }
            set
            {
                if (_LongDescription != value)
                {
                    _LongDescription = value;
                    OnPropertyChanged("LongDescription");
                }
            }
        }

        [Browsable(false)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [Browsable(false), System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool IsNew { get; protected set; }

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

            if (objectSpace != null)
            {
                UpdateUser = objectSpace.GetObjectByKey<SystemUsers>(SecuritySystem.CurrentUserId);
                UpdateDate = DateTime.Now;
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
