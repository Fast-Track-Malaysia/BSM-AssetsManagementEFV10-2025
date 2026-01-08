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
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetsManagementEF.Module.BusinessObjects
{
    // Register this entity in the DbContext using the "public DbSet<Areas> Areass { get; set; }" syntax.
    [DefaultClassOptions]
    [NavigationItem("Purchase Requests")]
    [XafDisplayName("Purchase Request Status")]
    [Appearance("HideNew", AppearanceItemType = "Action", TargetItems = "New", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideDelete", AppearanceItemType = "Action", TargetItems = "Delete", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideLink", AppearanceItemType = "Action", TargetItems = "Link", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideUnlink", AppearanceItemType = "Action", TargetItems = "Unlink", Context = "ListView", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    [Appearance("HideEdit", AppearanceItemType = "Action", TargetItems = "SwitchToEditMode;Edit", Context = "Any", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("Name")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class vw_SAP_pr
    {
        public vw_SAP_pr()
        {
            // In the constructor, initialize collection properties, e.g.: 
            // this.AssociatedEntities = new List<AssociatedEntityObject>();
        }
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Browsable(false)]
        public string keycolumn { get; set; }

        [Browsable(false)]
        [XafDisplayName("PR Create Date")]
        public DateTime? CreationDate { get; set; }

        [Appearance("PRDate", Enabled = false)]
        [XafDisplayName("SAP PR Date")]
        public DateTime? PRDate { get; set; }

        [Browsable(false)]
        public int? PRNo { get; set; }
        [XafDisplayName("SAP PR No")]
        public string PRNoS { get { return PRNo == null ? "" : PRNo.ToString(); } }


        [Appearance("CardCode", Enabled = false)]
        [XafDisplayName("Vendor")]
        public string CardCode { get; set; }

        [Appearance("CardName", Enabled = false)]
        [XafDisplayName("Vendor Name")]
        public string CardName { get; set; }

        [Appearance("Currency", Enabled = false)]
        [XafDisplayName("Currency")]
        public string Currency { get; set; }

        [Appearance("PRTotal", Enabled = false)]
        [XafDisplayName("SAP PR Total")]
        public decimal? PRTotal { get; set; }

        [Appearance("PODate", Enabled = false)]
        [XafDisplayName("SAP PO Date")]
        public DateTime? PODate { get; set; }

        [Browsable(false)]
        public int? PONo { get; set; }
        [XafDisplayName("SAP PO No")]
        public string PONoS { get { return PONo == null ? "" : PONo.ToString(); } }

        [Appearance("POTotal", Enabled = false)]
        [XafDisplayName("SAP PO Total")]
        [ModelDefault("DisplayFormat","{n:2}")]
        public decimal? POTotal { get; set; }

        [Browsable(false)]
        [XafDisplayName("SAP PO Create User")]
        public string POCreateUser { get; set; }

        [Appearance("AMSPRNo", Enabled = false)]
        [XafDisplayName("AMS PR No")]
        public string AMSPRNo { get; set; }

        [Browsable(false)]
        public int? AMSPRID { get; set; }

        [Appearance("AMSWONo", Enabled = false)]
        [XafDisplayName("AMS WO No")]
        public string AMSWONo { get; set; }

        [Appearance("AMSWRNo", Enabled = false)]
        [XafDisplayName("AMS WR No")]
        public string AMSWRNo { get; set; }

        [Appearance("AMSPG", Enabled = false)]
        [XafDisplayName("AMS Planner Group")]
        public string AMSPG { get; set; }

        [Appearance("AMSPRTotal", Enabled = false)]
        [XafDisplayName("AMS PR Total")]
        public decimal? AMSPRTotal { get; set; }

        [Appearance("AMSCardCode", Enabled = false)]
        [XafDisplayName("AMS Vendor")]
        public string AMSCardCode { get; set; }

        [Appearance("AMSCardName", Enabled = false)]
        [XafDisplayName("AMS Vendor Name")]
        public string AMSCardName { get; set; }

    }
}
