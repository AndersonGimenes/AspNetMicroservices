namespace Discount.API.Repositories.MigrationQueries
{
    public static class Migration
    {
        public static string Query 
        { 
            get => @"CREATE TABLE IF NOT EXISTS Coupon
                    (
                        Id SERIAL PRIMARY KEY, 
                        ProductName VARCHAR(24) NOT NULL,
                        Description TEXT,
                        Amount INT
                    )"; 
        }
    }
}
