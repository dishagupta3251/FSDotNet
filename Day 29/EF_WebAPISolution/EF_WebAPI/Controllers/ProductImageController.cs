using EF_WebAPI.Models.DTO;
using EF_WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EF_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly ProductImageService _service;

        public ProductImageController(ProductImageService productImageService)
        {
            _service = productImageService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(ProductImageDTO productImage)
        {
            try
            {
                var newProductImage = await _service.CreateProductImage(productImage);
                return Ok(newProductImage);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }

}
