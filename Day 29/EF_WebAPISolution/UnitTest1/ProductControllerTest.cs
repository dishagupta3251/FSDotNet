using AutoMapper;


using EFCoreWebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

using Microsoft.AspNetCore.Mvc;
using EFCoreWebAPI.Services;
using Microsoft.AspNetCore.Http;
using EF_WebAPI.Repositories;
using EFCoreFirstAPI.Contexts;
using EF_WebAPI.Interfaces;
using EF_WebAPI.Models.DTO;
using EF_WebAPI.Models;
using EF_WebAPI.Controllers;

namespace UnitTest1
{
    public class ProductControllerTest
    {
        DbContextOptions options;
        ProductRepository repository;
        ShoppingContext shoppingContext;
        private Mock<ILogger<ProductControllerTest>> loggerController;
        Mock<IMapper> mapper;
        IProductService productService;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
                .UseInMemoryDatabase("Test")
                .Options;

            shoppingContext = new ShoppingContext(options);
            repository = new ProductRepository(shoppingContext);
            loggerController = new Mock<ILogger<ProductControllerTest>>();
            mapper = new Mock<IMapper>();
            productService = new ProductService(repository, mapper.Object);
        }

        [Test]
        [TestCase("Test Product1", 300, 10)]
        [TestCase("Test Product2", 200, 20)]
        public async Task AddProductControllerTest(string name, float price, int qty)
        {
            //Arrange
            var product = new ProductDTO
            {
                Name = name,
                Price = price,
                Quantity = qty,
            };
            var productEntity = new Product
            {
                Name = name,
                Price = price,
                Quantity = qty
            };
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            var controller = new ProductController(productService, loggerController.Object);
            //Act
            var result = await controller.CreateProduct(product);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        [Test]
        [TestCase("Test Product1", 300, 10, 200, 1)]
        [TestCase("Test Product2", 200, 20, 300, 2)]
        public async Task updateProductPriceControllerTest(string name, float price, int qty, float updatePrice, int id)
        {
            //Arrange
            var product = new ProductDTO
            {
                Name = name,
                Price = price,
                Quantity = qty,
            };
            var productEntity = new Product
            {
                Name = name,
                Price = price,
                Quantity = qty
            };
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);
            var controller = new ProductController(productService, loggerController.Object);
            //Act
            await controller.CreateProduct(product);
            var result = await controller.UpdateProductPrice(updatePrice, id);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }

        //Exceptions Testing

        [Test]
        public async Task CreateProduct_ReturnsInternalServerError_OnException()
        {
            // Arrange
            var mockProductService = new Mock<IProductService>();
            var mockLogger = new Mock<ILogger<ProductControllerTest>>();
            var controller = new ProductController(mockProductService.Object, mockLogger.Object);

            var productDto = new ProductDTO
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };

            mockProductService.Setup(service => service.CreateProduct(productDto))
                .ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await controller.CreateProduct(productDto) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
        }

        [Test]
        public async Task UpdateProductPrice_ReturnsInternalServerError_OnException()
        {
            // Arrange

            var productDto = new ProductDTO
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };

            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            mapper.Setup(m => m.Map<Product>(productDto)).Returns(productEntity);
            var controller = new ProductController(productService, loggerController.Object);
            // Act
            await controller.CreateProduct(productDto);
            var result = await controller.UpdateProductPrice(1900, 7) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.AreEqual("Product Price Update Fail", result.Value);
        }

    }
}