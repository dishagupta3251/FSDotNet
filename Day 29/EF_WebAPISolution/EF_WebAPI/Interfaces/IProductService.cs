using EF_WebAPI.Models.DTO;
using EF_WebAPI.Models;

namespace EF_WebAPI.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateProduct(ProductDTO product);

        Task<Product> UpdateProductPrice(float price, int Id);

        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProduct(int Id);
    }
}
