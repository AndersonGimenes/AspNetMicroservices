using Basket.API.Entities;

namespace Basket.API.Services
{
    public interface IDiscountGrpcService
    {
        void DiscountCalculate(ShoppingCart cart);
    }
}
