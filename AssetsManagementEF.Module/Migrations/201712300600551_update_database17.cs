namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PurchaseRequestDtls", "ContractDocDtl_ID", c => c.Int());
            CreateIndex("dbo.PurchaseRequestDtls", "ContractDocDtl_ID");
            AddForeignKey("dbo.PurchaseRequestDtls", "ContractDocDtl_ID", "dbo.ContractDocDtls", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchaseRequestDtls", "ContractDocDtl_ID", "dbo.ContractDocDtls");
            DropIndex("dbo.PurchaseRequestDtls", new[] { "ContractDocDtl_ID" });
            DropColumn("dbo.PurchaseRequestDtls", "ContractDocDtl_ID");
        }
    }
}
