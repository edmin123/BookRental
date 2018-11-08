namespace BookRentalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMembershipTypes : DbMigration
    {
        public override void Up()
        {
            Sql("Insert into [dbo].[MembershipTypes](Name,SignUpFee,ChargeRateOneMonth,ChargeRateSixMonth) values('Pay per Rental',0,25,50)");
            Sql("Insert into [dbo].[MembershipTypes](Name,SignUpFee,ChargeRateOneMonth,ChargeRateSixMonth) values('Member',150,10,20)");
            Sql("Insert into [dbo].[MembershipTypes](Name,SignUpFee,ChargeRateOneMonth,ChargeRateSixMonth) values('Admin',0,0,0)");
        }
        
        public override void Down()
        {
        }
    }
}
