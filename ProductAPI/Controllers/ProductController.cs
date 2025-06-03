using Microsoft.AspNetCore.Mvc;
using ProductAPI.Services.Products;
using ProductAPI.Services.Products.Dto;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productService;

        public ProductController(IProductAppService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<ProductDto>>> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<ProductDto>> CreateOrUpdate(ProductDto productDto)
        {
            var result = await _productService.CreateAsync(productDto);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ProductDto>> Update(ProductDto productDto)
        {
            var result = await _productService.CreateAsync(productDto);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
