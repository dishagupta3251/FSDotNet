using EF_WebAPI.Models.DTO;
using EF_WebAPI.Models;

namespace EF_WebAPI.Interfaces
{
    public interface IProductImage
    {
        Task<ProductImage> CreateProductImage(ProductImageDTO product);
    }
}
