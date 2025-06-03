using Microsoft.AspNetCore.Mvc;
using ProductAPI.Services.Categories;
using ProductAPI.Services.Categories.Dto;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAppService _categoryService;

        public CategoryController(ICategoryAppService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CategoryDto>> Create(CategoryDto productDto)
        {
            var result = await _categoryService.CreateAsync(productDto);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<CategoryDto>> Update(CategoryDto productDto)
        {
            var result = await _categoryService.CreateAsync(productDto);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
