namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractDocDtls", "BaseLine", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractDocDtls", "BaseLine");
        }
    }
}
