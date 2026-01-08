namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_contractordtl_currency : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractDocDtls", "Currency", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractDocDtls", "Currency");
        }
    }
}
