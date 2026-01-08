namespace AssetsManagementEF.Module.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_database19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PMScheduleCalenders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IsNested = c.Boolean(nullable: false),
                        PMYear = c.String(),
                        Equipment_ID = c.Int(),
                        EquipmentComponent_ID = c.Int(),
                        PMSchedule_ID = c.Int(),
                        PMSchedule101_ID = c.Int(),
                        PMSchedule102_ID = c.Int(),
                        PMSchedule103_ID = c.Int(),
                        PMSchedule104_ID = c.Int(),
                        PMSchedule105_ID = c.Int(),
                        PMSchedule106_ID = c.Int(),
                        PMSchedule107_ID = c.Int(),
                        PMSchedule108_ID = c.Int(),
                        PMSchedule109_ID = c.Int(),
                        PMSchedule110_ID = c.Int(),
                        PMSchedule111_ID = c.Int(),
                        PMSchedule112_ID = c.Int(),
                        PMSchedule201_ID = c.Int(),
                        PMSchedule202_ID = c.Int(),
                        PMSchedule203_ID = c.Int(),
                        PMSchedule204_ID = c.Int(),
                        PMSchedule205_ID = c.Int(),
                        PMSchedule206_ID = c.Int(),
                        PMSchedule207_ID = c.Int(),
                        PMSchedule208_ID = c.Int(),
                        PMSchedule209_ID = c.Int(),
                        PMSchedule210_ID = c.Int(),
                        PMSchedule211_ID = c.Int(),
                        PMSchedule212_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipments", t => t.Equipment_ID)
                .ForeignKey("dbo.EquipmentComponents", t => t.EquipmentComponent_ID)
                .ForeignKey("dbo.PMSchedules", t => t.PMSchedule_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule101_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule102_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule103_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule104_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule105_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule106_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule107_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule108_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule109_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule110_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule111_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule112_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule201_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule202_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule203_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule204_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule205_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule206_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule207_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule208_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule209_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule210_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule211_ID)
                .ForeignKey("dbo.PMFrequencies", t => t.PMSchedule212_ID)
                .Index(t => t.Equipment_ID)
                .Index(t => t.EquipmentComponent_ID)
                .Index(t => t.PMSchedule_ID)
                .Index(t => t.PMSchedule101_ID)
                .Index(t => t.PMSchedule102_ID)
                .Index(t => t.PMSchedule103_ID)
                .Index(t => t.PMSchedule104_ID)
                .Index(t => t.PMSchedule105_ID)
                .Index(t => t.PMSchedule106_ID)
                .Index(t => t.PMSchedule107_ID)
                .Index(t => t.PMSchedule108_ID)
                .Index(t => t.PMSchedule109_ID)
                .Index(t => t.PMSchedule110_ID)
                .Index(t => t.PMSchedule111_ID)
                .Index(t => t.PMSchedule112_ID)
                .Index(t => t.PMSchedule201_ID)
                .Index(t => t.PMSchedule202_ID)
                .Index(t => t.PMSchedule203_ID)
                .Index(t => t.PMSchedule204_ID)
                .Index(t => t.PMSchedule205_ID)
                .Index(t => t.PMSchedule206_ID)
                .Index(t => t.PMSchedule207_ID)
                .Index(t => t.PMSchedule208_ID)
                .Index(t => t.PMSchedule209_ID)
                .Index(t => t.PMSchedule210_ID)
                .Index(t => t.PMSchedule211_ID)
                .Index(t => t.PMSchedule212_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule212_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule211_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule210_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule209_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule208_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule207_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule206_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule205_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule204_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule203_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule202_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule201_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule112_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule111_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule110_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule109_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule108_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule107_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule106_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule105_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule104_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule103_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule102_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule101_ID", "dbo.PMFrequencies");
            DropForeignKey("dbo.PMScheduleCalenders", "PMSchedule_ID", "dbo.PMSchedules");
            DropForeignKey("dbo.PMScheduleCalenders", "EquipmentComponent_ID", "dbo.EquipmentComponents");
            DropForeignKey("dbo.PMScheduleCalenders", "Equipment_ID", "dbo.Equipments");
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule212_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule211_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule210_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule209_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule208_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule207_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule206_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule205_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule204_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule203_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule202_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule201_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule112_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule111_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule110_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule109_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule108_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule107_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule106_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule105_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule104_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule103_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule102_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule101_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "PMSchedule_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "EquipmentComponent_ID" });
            DropIndex("dbo.PMScheduleCalenders", new[] { "Equipment_ID" });
            DropTable("dbo.PMScheduleCalenders");
        }
    }
}
