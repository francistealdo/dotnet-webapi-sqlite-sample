using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface;

namespace ProductAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;

        public ProductsController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        /// <summary>
        /// Get All Products.
        /// </summary>
        /// <returns>Get All Products.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            IEnumerable<ProductDto> result = _productApplicationService.GetAll();

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        /// Get a Product by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Get a Product by Id.</returns>
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            ProductDto result = _productApplicationService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Add new Product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Add new Product.</returns>
        [HttpPost]
        public ActionResult<string> Create([FromBody] ProductDto productDto)
        {
            // TODO: Add validation for the productDto object before calling the service layer.

            _productApplicationService.Add(productDto);
            return Ok("Post registered successfully.");
        }
    }
}
