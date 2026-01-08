namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractDocs", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractDocs", "Remarks");
        }
    }
}
