using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public BasketController(IDiscountGrpcService service, IBasketRepository repository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _repository = repository;
            _service = service;
            _publishEndpoint = publishEndpoint;
            _mapper = mapper;
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

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasket(basketCheckout.UserName);
            
            if (basket is null)
                return BadRequest();

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;

            await _publishEndpoint.Publish(eventMessage);

            await _repository.DeleteBasket(basket.UserName);

            return Accepted();
        }
    }
}
