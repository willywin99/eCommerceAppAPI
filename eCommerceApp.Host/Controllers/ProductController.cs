using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerceApp.Application.Services.Interfaces;

namespace eCommerceApp.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data = await productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound();
        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data = await productService.GetByIdAsync(id);
            return data != null ? Ok(data) : NotFound(data);
        }
    }
}
