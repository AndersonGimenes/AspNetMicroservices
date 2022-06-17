using Basket.API.Entities;
using Discount.Grpc.Protos;

namespace Basket.API.Services
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public void DiscountCalculate(ShoppingCart cart)
        {
            foreach(var item in cart.Items)
            {
                var coupon = _discountProtoService.GetDiscount(new GetDiscountRequest { ProductName = item.ProductName });
                item.Price -= coupon.Amount;
            }
        }
    }
}
