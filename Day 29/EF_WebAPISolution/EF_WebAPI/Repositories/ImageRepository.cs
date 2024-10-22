
using EF_WebAPI.Models;
using EFCoreFirstAPI.Contexts;
using EFCoreFirstAPI.Exceptions;
using EFCoreFirstAPI.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace EFCoreWebAPI.Repositories
{
    public class ImageRepository : IRepository<int, ProductImage>
    {
        private readonly ShoppingContext _context;

        public ImageRepository(ShoppingContext shoppingContext)
        {
            _context = shoppingContext;
        }

        public async Task<ProductImage> Add(ProductImage entity)
        {
            try
            {
                await _context.ProductImages.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw new CouldNotAddException("Product Image");
            }
        }

        public async Task<ProductImage> Delete(int key)
        {
            var productImage = await Get(key);
            if (productImage != null)
            {
                _context.ProductImages.Remove(productImage);
                return productImage;
            }
            throw new NotFoundException("Product Image for delete");
        }

        public async Task<ProductImage> Get(int key)
        {
            var productImages = await GetAll();
            var productImage = productImages.FirstOrDefault(pi => pi.Id == key);
            if (productImage == null)
            {
                throw new NotFoundException("Product Image");
            }
            return productImage;
        }

        public async Task<IEnumerable<ProductImage>> GetAll()
        {
            var productImages = await _context.ProductImages.ToListAsync();
            if (productImages.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return productImages;
        }

        public Task<ProductImage> Update(int key, ProductImage entity)
        {
            throw new NotImplementedException();
        }
    }
}
