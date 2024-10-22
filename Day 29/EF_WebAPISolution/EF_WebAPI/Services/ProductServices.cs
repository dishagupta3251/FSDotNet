
﻿using AutoMapper;

using EF_WebAPI.Interfaces;
using EF_WebAPI.Models;
using EF_WebAPI.Models.DTO;
using EFCoreFirstAPI.Exceptions;
using EFCoreFirstAPI.Interfaces;


namespace EFCoreWebAPI.Services
    {
        public class ProductService : IProductService
        {
            private readonly IRepository<int, Product> _productRepo;
            private readonly IMapper _mapper;

            public ProductService(IRepository<int, Product> productRepository, IMapper mapper)
            {
                _productRepo = productRepository;
                _mapper = mapper;
            }
           

        public async Task<int> CreateProduct(ProductDTO product)
        {
            Product newProduct = _mapper.Map<Product>(product);
            var addProduct = await _productRepo.Add(newProduct);
            return addProduct.Id;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
            {
                try
                {
                    var products = await _productRepo.GetAll();
                    return products;
                }
                catch (Exception ex)
                {
                    throw new CollectionEmptyException("Product");
                }
            }

            public async Task<Product> GetProduct(int Id)
            {
                try
                {
                    var product = await _productRepo.Get(Id);
                    return product;
                }
                catch (Exception ex)
                {
                    throw new NotFoundException("Product");
                }

            }

            public Task<Product> UpdateProductPrice(float price, int Id)
            {
                var oldProduct = _productRepo.Get(Id);
                if (oldProduct != null)
                {
                    oldProduct.Result.Price = price;
                    return oldProduct;
                }
                throw new NotUpdateException("Product Price Update Fail");
            }


        }
    }

