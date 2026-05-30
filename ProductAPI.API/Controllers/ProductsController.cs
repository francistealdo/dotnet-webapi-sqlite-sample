using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application;
using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface;

namespace ProductAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly ICategoryApplicationService _categoryApplicationService;

        public ProductsController(IProductApplicationService productApplicationService, ICategoryApplicationService categoryApplicationService)
        {
            _productApplicationService = productApplicationService;
            _categoryApplicationService = categoryApplicationService;
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns>Collection of products.</returns>
        /// <response code="200">Returns the collection of products.</response>
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            IEnumerable<ProductDto> result = _productApplicationService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <response code="200">Returns the requested product.</response>
        /// <response code="404">Product not found.</response>
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            ProductDto result = _productApplicationService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="productDto">Product data.</param>
        /// <response code="201">Product created successfully.</response>
        /// <response code="400">Invalid request.</response>
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<ProductDto> Create([FromBody] ProductDto productDto)
        {
            CategoryDto category = _categoryApplicationService.GetById(productDto.CategoryId);

            if (category == null)
                return BadRequest("Category is invalid.");

            var createdProduct = _productApplicationService.Add(productDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdProduct.Id },
                createdProduct);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="productDto">Updated product data.</param>
        /// <response code="200">Returns the updated product.</response>
        /// <response code="400">Product ID mismatch.</response>
        /// <response code="404">Product not found.</response>
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<ProductDto> Update(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
                return BadRequest("Product ID mismatch.");

            var existingProduct = _productApplicationService.GetById(id);

            if (existingProduct == null)
                return NotFound();

            var updatedProduct = _productApplicationService.Update(productDto);

            return Ok(updatedProduct);
        }

        /// <summary>
        /// Deletes a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <response code="204">Product deleted successfully.</response>
        /// <response code="404">Product not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var productDto = _productApplicationService.GetById(id);

            if (productDto == null)
                return NotFound();

            _productApplicationService.Delete(productDto.Id);

            return NoContent();
        }
    }
}
