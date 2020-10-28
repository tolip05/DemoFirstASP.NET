namespace MVCDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargoes",
                c => new
                    {
                        CargoId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PricePerKilometer = c.Double(nullable: false),
                        FromLocationId = c.Int(nullable: false),
                        ForLocationId = c.Int(nullable: false),
                        CargoType = c.Int(nullable: false),
                        TransportType = c.Int(nullable: false),
                        ForwarderId = c.Int(nullable: false),
                        CarrierId = c.Int(),
                        ForLocation_LocationId = c.Int(),
                        FromLocation_LocationId = c.Int(),
                    })
                .PrimaryKey(t => t.CargoId)
                .ForeignKey("dbo.Carriers", t => t.CarrierId)
                .ForeignKey("dbo.Locations", t => t.ForLocation_LocationId)
                .ForeignKey("dbo.Forwarders", t => t.ForwarderId, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.FromLocation_LocationId)
                .Index(t => t.ForwarderId)
                .Index(t => t.CarrierId)
                .Index(t => t.ForLocation_LocationId)
                .Index(t => t.FromLocation_LocationId);
            
            CreateTable(
                "dbo.Carriers",
                c => new
                    {
                        CarrierId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Emeil = c.String(),
                        CarrierIdentificator = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.CarrierId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.LocationId);
            
            CreateTable(
                "dbo.Forwarders",
                c => new
                    {
                        ForwarderId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Emeil = c.String(),
                        ForwarderIdentificator = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ForwarderId);
            
            CreateTable(
                "dbo.Identificators",
                c => new
                    {
                        IdentificatorId = c.Int(nullable: false, identity: true),
                        IdentificatorType = c.Int(nullable: false),
                        IdentificatorNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdentificatorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cargoes", "FromLocation_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Cargoes", "ForwarderId", "dbo.Forwarders");
            DropForeignKey("dbo.Cargoes", "ForLocation_LocationId", "dbo.Locations");
            DropForeignKey("dbo.Cargoes", "CarrierId", "dbo.Carriers");
            DropIndex("dbo.Cargoes", new[] { "FromLocation_LocationId" });
            DropIndex("dbo.Cargoes", new[] { "ForLocation_LocationId" });
            DropIndex("dbo.Cargoes", new[] { "CarrierId" });
            DropIndex("dbo.Cargoes", new[] { "ForwarderId" });
            DropTable("dbo.Identificators");
            DropTable("dbo.Forwarders");
            DropTable("dbo.Locations");
            DropTable("dbo.Carriers");
            DropTable("dbo.Cargoes");
        }
    }
}
