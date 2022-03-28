using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _repository.GetProducts());
        }

        [HttpGet("{id:length(24)}", Name = nameof(GetProduct))]
        public async Task<IActionResult> GetProduct(string id)
        {
            var product = await _repository.GetProduct(id);
            if(product is null)
                return LogError(id);

            return Ok(product);
        }

        [HttpGet("[action]/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            return Ok(await _repository.GetProductsByCategory(category));
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            return Ok(await _repository.GetProductsByName(name));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            await _repository.CreateProduct(product);
            return CreatedAtRoute(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var updateResult = await _repository.UpdateProduct(product);
            if (!updateResult)
                return LogError(product.Id);

            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> UpdateProduct(string id)
        {
            var updateResult = await _repository.DeleteProduct(id);
            if (!updateResult)
                return LogError(id);
            
            return NoContent();
        }

        #region[PRVATE METHODS]
        private NotFoundResult LogError(string id)
        {
            _logger.LogError($"Product with id: {id}, not found.");
            return NotFound();
        }
        #endregion
    }
}
