using Dapper;
using Discount.API.Entities;
using Discount.API.IoC.Factories;
using Discount.API.Repositories.Queries;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _confirguration;
        
        public DiscountRepository(IConfiguration configuration)
        {
            _confirguration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using (var context = NpgsqlConnectionFactory.GetInstance(_confirguration))
            {
                var result = await context.QueryFirstAsync<Coupon>(DiscountQueries.GetDiscount(), new { ProductName = productName });

                if (result is null)
                    return new Coupon { ProductName = "No Discount", Description = "No Discount Desc" };

                return result;
            }
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using(var context = NpgsqlConnectionFactory.GetInstance(_confirguration))
            {
                var result = await context.ExecuteAsync(DiscountQueries.CreateDiscount(),
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

                if (result == 0)
                    return false;
                
                return true;
            }
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using (var context = NpgsqlConnectionFactory.GetInstance(_confirguration))
            {
                var result = await context.ExecuteAsync(DiscountQueries.UpdateDiscount(),
                    new {Id = coupon.Id, ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

                if (result == 0)
                    return false;

                return true;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using (var context = NpgsqlConnectionFactory.GetInstance(_confirguration))
            {
                var result = await context.ExecuteAsync(DiscountQueries.DeleteDiscount(),
                    new { ProductName = productName });

                if (result == 0)
                    return false;

                return true;
            }
        }             
    }
}
