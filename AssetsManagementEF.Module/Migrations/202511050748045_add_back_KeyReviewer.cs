namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_back_KeyReviewer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeviationReviewers", "KeyReviewer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeviationReviewers", "KeyReviewer");
        }
    }
}
