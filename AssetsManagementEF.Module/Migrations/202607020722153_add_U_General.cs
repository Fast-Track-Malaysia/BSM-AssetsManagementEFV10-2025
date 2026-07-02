namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_U_General : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractDocDtls", "U_General", c => c.Boolean(nullable: false));
            AddColumn("dbo.PurchaseRequestDtls", "U_General", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PurchaseRequestDtls", "U_General");
            DropColumn("dbo.ContractDocDtls", "U_General");
        }
    }
}
