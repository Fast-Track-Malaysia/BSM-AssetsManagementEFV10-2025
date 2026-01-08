namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database30 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vw_SAP_pr", "AMSCardCode", c => c.String());
            AddColumn("dbo.vw_SAP_pr", "AMSCardName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.vw_SAP_pr", "AMSCardName");
            DropColumn("dbo.vw_SAP_pr", "AMSCardCode");
        }
    }
}
