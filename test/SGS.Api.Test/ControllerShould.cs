using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SGS.Controllers;
using SGS.Domain.Models;
using SGS.Domain.Services;

namespace SGS.Api.Test
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductServices> _mockProductServices;
        private readonly ProductsController _controller;

        public ProductsControllerTests()
        {
            _mockProductServices = new Mock<IProductServices>();
            _controller = new ProductsController(_mockProductServices.Object);
        }

        [Fact]
        public async Task Get_ReturnsProducts_WhenProductsExist()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Category = "Category1",
                    Description = "Description1",
                    IsActive = true,
                    Name = "Product 1",
                    Picture = "picture1.jpg",
                    Price = 10.5m,
                    Stock = 100,
                    Discount = new Discount { Status = true, Value = 10 }
                }
            };
            _mockProductServices.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                                .ReturnsAsync(products);

            var sut = await _controller.Get();

            var expected = new OkObjectResult(products);
            Assert.Equal(StatusCodes.Status200OK, expected.StatusCode);
            Assert.Equal(products, expected.Value);
        }

        [Fact]
        public async Task GetById_ReturnsProduct_WhenProductExists()
        {
            var product = new Product
            {
                Id = "1",
                Category = "Category1",
                Description = "Description1",
                IsActive = true,
                Name = "Product 1",
                Picture = "picture1.jpg",
                Price = 10.5m,
                Stock = 100,
                Discount = new Discount { Status = true, Value = 10 }
            };
            _mockProductServices.Setup(s => s.GetByIdAsync("1")).ReturnsAsync(product);

            var sut = await _controller.GetById("1");

            var expected = new OkObjectResult(product);
            Assert.Equal(StatusCodes.Status200OK, expected.StatusCode);
            Assert.Equal(product, expected.Value);
        }

        [Fact]
        public async Task Filter_ReturnsFilteredProducts_WhenProductsMatchFilter()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Category = "Category1",
                    Description = "Description1",
                    IsActive = true,
                    Name = "Product 1",
                    Picture = "picture1.jpg",
                    Price = 10.5m,
                    Stock = 100,
                    Discount = new Discount { Status = true, Value = 10 }
                }
            };
            _mockProductServices.Setup(s => s.GetFilteredProductsAsync(It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<decimal?>(), It.IsAny<bool?>(), It.IsAny<int?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                                .ReturnsAsync(products);

            var sut = await _controller.Filter(null, null, null, null, null, null);

            var expected = new OkObjectResult(products);
            Assert.Equal(StatusCodes.Status200OK, expected.StatusCode);
            Assert.Equal(products, expected.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenNoProductsExist()
        {
            var products = new List<Product>();
            _mockProductServices.Setup(s => s.GetAllAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                                .ReturnsAsync(products);

            var sut = await _controller.Get();

            var result = sut.Result as NotFoundObjectResult;
            var expected = new NotFoundObjectResult("No products found matching the criteria.");

            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal(expected.Value, result.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            _mockProductServices.Setup(s => s.GetByIdAsync("1")).ReturnsAsync((Product)null);

            var sut = await _controller.GetById("1");

            var result = sut.Result as NotFoundObjectResult;
            var expected = new NotFoundObjectResult("Product with ID 1 not found.");

            Assert.Equal(StatusCodes.Status404NotFound, expected.StatusCode);
            Assert.Equal(expected.Value, result.Value);
        }

        [Fact]
        public async Task Filter_ReturnsNotFound_WhenNoProductsMatchFilter()
        {
            var products = new List<Product>();
            _mockProductServices.Setup(s => s.GetFilteredProductsAsync(It.IsAny<string>(), It.IsAny<decimal?>(), It.IsAny<decimal?>(), It.IsAny<bool?>(), It.IsAny<int?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                                .ReturnsAsync(products);

            var sut = await _controller.Filter(null, null, null, null, null, null);

            var result = sut.Result as NotFoundObjectResult;
            var expected = new NotFoundObjectResult("No products found matching the criteria.");

            Assert.Equal(StatusCodes.Status404NotFound, expected.StatusCode);
            Assert.Equal(expected.Value, result.Value);
        }
    }
}
