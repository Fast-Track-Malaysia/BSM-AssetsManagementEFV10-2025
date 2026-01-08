namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.ItemMasters", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ContractDocs", "StartDate", c => c.DateTime());
            AlterColumn("dbo.ContractDocs", "EndDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ContractDocs", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ContractDocs", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ItemMasters", "IsActive");
            DropColumn("dbo.Contractors", "IsActive");
        }
    }
}
