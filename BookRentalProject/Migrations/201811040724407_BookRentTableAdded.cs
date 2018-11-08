namespace BookRentalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookRentTableAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookRents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false),
                        BookId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        ActualEndDate = c.DateTime(),
                        ScheduledEndDate = c.DateTime(),
                        RentalPrice = c.Double(nullable: false),
                        RentalDuration = c.String(nullable: false),
                        AdditionalCharge = c.Double(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookRents");
        }
    }
}
