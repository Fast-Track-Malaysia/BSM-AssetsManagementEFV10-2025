namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class null_reviewdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeviationReviewers", "ReviewDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DeviationReviewers", "ReviewDate", c => c.DateTime(nullable: false));
        }
    }
}
