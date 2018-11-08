namespace BookRentalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenreAndBookModelsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ISBN = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Author = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Availibility = c.Int(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        GenreId = c.Int(nullable: false),
                        DateAdded = c.DateTime(),
                        PublicationDate = c.DateTime(nullable: false),
                        ProductDimensions = c.String(nullable: false),
                        Pages = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "GenreId", "dbo.Genres");
            DropIndex("dbo.Books", new[] { "GenreId" });
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
        }
    }
}
