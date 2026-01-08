namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database33 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Priorities", "FullCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Priorities", "FullCode");
        }
    }
}
