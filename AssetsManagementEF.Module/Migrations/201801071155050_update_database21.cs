namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Positions", "IsPRApprove", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Positions", "IsPRApprove");
        }
    }
}
