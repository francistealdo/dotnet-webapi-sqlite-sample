using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Dto;

namespace ProductAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<ProductDto>> GetAll()
        {
            var result = GetProducts();

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var result = GetProducts().Where(p => p.Id == id).FirstOrDefault();

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public ActionResult<string> Create([FromBody] string product)
        {
            return Ok($"Create product: {product}");
        }

        private List<ProductDto> GetProducts()
        {
            List<ProductDto> result = new List<ProductDto>();

            for (int i = 1; i < 100; i++)
            {
                result.Add(new ProductDto
                {
                    Id = i,
                    Name = $"Product {i}",
                    Description = $"Description for product {i}"
                });
            }
            return result;
        }
    }
}
