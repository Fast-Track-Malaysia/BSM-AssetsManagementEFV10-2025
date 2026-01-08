namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vw_SAP_pr", "AMSWRNo", c => c.String());
            AddColumn("dbo.vw_SAP_pr", "AMSPG", c => c.String());
            AddColumn("dbo.vw_SAP_pr", "AMSPRTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.vw_SAP_pr", "AMSPRTotal");
            DropColumn("dbo.vw_SAP_pr", "AMSPG");
            DropColumn("dbo.vw_SAP_pr", "AMSWRNo");
        }
    }
}
