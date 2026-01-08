namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.vw_SAP_pr", "AMSWONo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.vw_SAP_pr", "AMSWONo");
        }
    }
}
