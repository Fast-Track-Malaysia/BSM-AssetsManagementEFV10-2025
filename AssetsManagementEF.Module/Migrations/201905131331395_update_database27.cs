namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database27 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.vw_SAP_pr",
                c => new
                    {
                        keycolumn = c.String(nullable: false, maxLength: 128),
                        CreationDate = c.DateTime(),
                        PRDate = c.DateTime(),
                        PRNo = c.Int(),
                        CardCode = c.String(),
                        CardName = c.String(),
                        Currency = c.String(),
                        PRTotal = c.Decimal(precision: 18, scale: 2),
                        PODate = c.DateTime(),
                        PONo = c.Int(),
                        POTotal = c.Decimal(precision: 18, scale: 2),
                        POCreateUser = c.String(),
                        AMSPRNo = c.String(),
                        AMSPRID = c.Int(),
                    })
                .PrimaryKey(t => t.keycolumn);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.vw_SAP_pr");
        }
    }
}
