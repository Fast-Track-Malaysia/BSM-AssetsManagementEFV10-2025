namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ContractDocs", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ContractDocs", "IsActive");
        }
    }
}
