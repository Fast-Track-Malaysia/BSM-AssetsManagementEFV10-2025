namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Contractor_DocFullCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contractors", "DocFullCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contractors", "DocFullCode");
        }
    }
}
