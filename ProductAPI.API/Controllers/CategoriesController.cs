using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.Dto;
using ProductAPI.Application.Interface;

namespace ProductAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryApplicationService _categoryApplicationService;

        public CategoriesController(ICategoryApplicationService categoryApplicationService)
        {
            _categoryApplicationService = categoryApplicationService;
        }

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>Collection of categories.</returns>
        /// <response code="200">Returns the collection of category.</response>
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDto>> GetAll()
        {
            IEnumerable<CategoryDto> result = _categoryApplicationService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <response code="200">Returns the requested category.</response>
        /// <response code="404">Category not found.</response>
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> GetById(int id)
        {
            CategoryDto result = _categoryApplicationService.GetById(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="productDto">Category data.</param>
        /// <response code="201">Category created successfully.</response>
        /// <response code="400">Invalid request.</response>
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<CategoryDto> Create([FromBody] CategoryDto categoryDto)
        {
            CategoryDto createdCategory = _categoryApplicationService.Add(categoryDto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdCategory.Id },
                createdCategory);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="categoryDto">Updated category data.</param>
        /// <response code="200">Returns the updated category.</response>
        /// <response code="400">Category ID mismatch.</response>
        /// <response code="404">Category not found.</response>
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult<CategoryDto> Update(int id, [FromBody] CategoryDto categoryDto)
        {
            if (id != categoryDto.Id)
                return BadRequest("Category ID mismatch.");

            var existingCategory = _categoryApplicationService.GetById(id);

            if (existingCategory == null)
                return NotFound();

            CategoryDto updatedCategory = _categoryApplicationService.Update(categoryDto);

            return Ok(updatedCategory);
        }

        /// <summary>
        /// Deletes a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <response code="204">Category deleted successfully.</response>
        /// <response code="404">Category not found.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CategoryDto categoryDto = _categoryApplicationService.GetById(id);

            if (categoryDto == null)
                return NotFound();

            _categoryApplicationService.Delete(categoryDto.Id);

            return NoContent();
        }
    }
}
