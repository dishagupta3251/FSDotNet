using AutoMapper;
using EF_WebAPI.Models.DTO;
using EF_WebAPI.Models;
using EFCoreFirstAPI.Interfaces;
using EF_WebAPI.Interfaces;

namespace EF_WebAPI.Services
{
    public class ProductImageService : IProductImage
    {
        private readonly IRepository<int, ProductImage> _productImageRepo;
        private readonly IMapper _mapper;

        public ProductImageService(IRepository<int, ProductImage> productImageRepository, IMapper mapper)
        {
            _productImageRepo = productImageRepository;
            _mapper = mapper;
        }

        public async Task<ProductImage> CreateProductImage(ProductImageDTO productImage)
        {
            ProductImage newProductImage = _mapper.Map<ProductImage>(productImage);
            var addedProductImage = await _productImageRepo.Add(newProductImage);
            return addedProductImage;
        }
    }
}
