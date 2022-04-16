using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}", Name = nameof(GetDiscount))]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            return Ok(await _repository.GetDiscount(productName));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);
            return CreatedAtRoute(nameof(GetDiscount), new {productName = coupon.ProductName}, coupon);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            return Ok(await _repository.UpdateDiscount(coupon));
        }

        [HttpDelete("{productName}", Name = nameof(DeleteDiscount))]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteDiscount(productName));
        }
    }
}
