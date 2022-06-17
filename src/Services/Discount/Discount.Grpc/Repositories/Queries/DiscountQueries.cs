namespace Discount.Grpc.Repositories.Queries
{
    public static class DiscountQueries
    {
        public static string CreateDiscount() =>
            "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)";

        public static string UpdateDiscount() =>
            "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";

        public static string GetDiscount() =>
            "SELECT productName, description, amount FROM Coupon WHERE ProductName = @ProductName";

        public static string DeleteDiscount() =>
            "DELETE FROM Coupon WHERE ProductName = @ProductName";
    }

}
