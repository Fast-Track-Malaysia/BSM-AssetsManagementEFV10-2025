namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_contractor_currency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractDocs", "Currency", c => c.String());
            AddColumn("dbo.Contractors", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractors", "Currency");
            DropColumn("dbo.ContractDocs", "Currency");
        }
    }
}
