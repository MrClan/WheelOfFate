namespace RichmondGroupTechnicalTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Engineers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Shift = c.Int(nullable: false),
                        EngineerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Engineers", t => t.EngineerId, cascadeDelete: true)
                .Index(t => t.EngineerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "EngineerId", "dbo.Engineers");
            DropIndex("dbo.Schedules", new[] { "EngineerId" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Engineers");
        }
    }
}
