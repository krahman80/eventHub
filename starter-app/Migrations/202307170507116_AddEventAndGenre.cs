namespace starter_app.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventAndGenre : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        GenreId = c.Byte(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Events", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Events", new[] { "GenreId" });
            DropIndex("dbo.Events", new[] { "ArtistId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Events");
        }
    }
}
