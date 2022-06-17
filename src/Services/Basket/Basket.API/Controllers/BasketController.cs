using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly IDiscountGrpcService _service;

        public BasketController(IBasketRepository repository, IDiscountGrpcService service)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet("{userName}", Name = nameof(GetBasket))]
        public async Task<IActionResult> GetBasket(string userName)
        {
            return Ok(await _repository.GetBasket(userName));
        }

        [HttpPost()]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            _service.DiscountCalculate(basket);

            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = nameof(DeleteBasket))]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);

            return NoContent();
        }
    }
}
