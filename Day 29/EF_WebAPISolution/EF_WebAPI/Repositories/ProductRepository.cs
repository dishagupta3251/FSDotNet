using EF_WebAPI.Models;
using EFCoreFirstAPI.Contexts;
using EFCoreFirstAPI.Exceptions;
using EFCoreFirstAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EF_WebAPI.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly ShoppingContext _shoppingContext;
        public ProductRepository(ShoppingContext shoppingContext)
        {
            _shoppingContext=shoppingContext;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                _shoppingContext.Products.Add(entity);
                await _shoppingContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex) {
                throw new CouldNotAddException("Product");
            }

        }

        public async Task<Product> Delete(int key)
        {
            var product=await Get(key);
            try
            {
                 _shoppingContext.Products.Remove(product);
                await _shoppingContext.SaveChangesAsync();
                return product;
            }
            catch (Exception ex) {
                throw new NotFoundException("Could not found Products");
            }
        }

        public async Task<Product> Get(int key)
        {
            var product= await _shoppingContext.Products.FirstOrDefaultAsync(p=>p.Id==key);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _shoppingContext.Products.ToListAsync();
            if (products.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return products;
        }

        public async Task<Product> Update(int key, Product entity)
        {
            var product = await Get(key);
            if (product != null)
            {
                product.OrderDetails=entity.OrderDetails;
                product.Name=entity.Name;
                product.Description=entity.Description;
                product.Price=entity.Price;
                product.Quantity=entity.Quantity;
                product.BasicImage=entity.BasicImage;
                await _shoppingContext.SaveChangesAsync();
            }
            throw new NotFoundException("Product");
        }
    }
}
