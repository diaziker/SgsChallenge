using AutoMapper;
using Moq;
using SGS.Domain.DataSource;
using SGS.Domain.Entities;
using SGS.Domain.Models;
using SGS.Domain.Services;

namespace SGS.Domain.Test
{
    public class ProductServicesTests
    {
        private readonly Mock<IDataSource> _mockDataSource;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductServices _productServices;

        public ProductServicesTests()
        {
            _mockDataSource = new Mock<IDataSource>();
            _mockMapper = new Mock<IMapper>();
            _productServices = new ProductServices(_mockDataSource.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProducts_WhenProductsExist()
        {
            var products = new List<EntityProduct>
            {
                new EntityProduct
                {
                    Id = "1",
                    ProductId = "1",
                    Category = "Category1",
                    Description = "Description1",
                    IsActive = true,
                    Name = "Product 1",
                    Picture = "picture1.jpg",
                    Price = 10.5,
                    Stock = 100,
                    Discount = new EntityDiscount { Status = true, Value = 10 }
                },
                new EntityProduct
                {
                    Id = "2",
                    ProductId = "2",
                    Category = "Category2",
                    Description = "Description2",
                    IsActive = false,
                    Name = "Product 2",
                    Picture = "picture2.jpg",
                    Price = 20.5,
                    Stock = 200,
                    Discount = new EntityDiscount { Status = false, Value = 20 }
                }
            };

            _mockDataSource.Setup(ds => ds.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                           .ReturnsAsync(products);

            var mappedProducts = products.Select(p => new Product
            {
                Id = p.ProductId,
                Category = p.Category,
                Description = p.Description,
                IsActive = p.IsActive,
                Name = p.Name,
                Picture = p.Picture,
                Price = p.Price,
                Stock = p.Stock,
                Discount = new Discount { Status = p.Discount.Status, Value = p.Discount.Value }
            });

            _mockMapper.Setup(m => m.Map<IEnumerable<Product>>(It.IsAny<IEnumerable<EntityProduct>>()))
                       .Returns(mappedProducts);

            var sut = await _productServices.GetAllAsync(1, 10, "Name", true);

            Assert.Equal(2, sut.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            var product = new EntityProduct
            {
                Id = "1",
                ProductId = "1",
                Category = "Category1",
                Description = "Description1",
                IsActive = true,
                Name = "Product 1",
                Picture = "picture1.jpg",
                Price = 10.5,
                Stock = 100,
                Discount = new EntityDiscount { Status = true, Value = 10 }
            };

            _mockDataSource.Setup(ds => ds.GetById(It.IsAny<string>()))
                           .ReturnsAsync(product);

            var mappedProduct = new Product
            {
                Id = product.ProductId,
                Category = product.Category,
                Description = product.Description,
                IsActive = product.IsActive,
                Name = product.Name,
                Picture = product.Picture,
                Price = product.Price,
                Stock = product.Stock,
                Discount = new Discount { Status = product.Discount.Status, Value = product.Discount.Value }
            };

            _mockMapper.Setup(m => m.Map<Product>(It.IsAny<EntityProduct>()))
                       .Returns(mappedProduct);

            var sut = await _productServices.GetByIdAsync("1");

            Assert.NotNull(sut);
            Assert.Equal("Product 1", sut.Name);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_ShouldReturnFilteredProducts_WhenProductsExist()
        {
            var products = new List<EntityProduct>
            {
                new EntityProduct
                {
                    Id = "1",
                    ProductId = "1",
                    Category = "Category1",
                    Description = "Description1",
                    IsActive = true,
                    Name = "Product 1",
                    Picture = "picture1.jpg",
                    Price = 10.5,
                    Stock = 100,
                    Discount = new EntityDiscount { Status = true, Value = 10 }
                },
                new EntityProduct
                {
                    Id = "2",
                    ProductId = "2",
                    Category = "Category2",
                    Description = "Description2",
                    IsActive = false,
                    Name = "Product 2",
                    Picture = "picture2.jpg",
                    Price = 20.5,
                    Stock = 200,
                    Discount = new EntityDiscount { Status = false, Value = 20 }
                }
            };

            _mockDataSource.Setup(ds => ds.GetFilteredProducts(
                    It.IsAny<string>(),
                    It.IsAny<double?>(),
                    It.IsAny<double?>(),
                    It.IsAny<bool?>(),
                    It.IsAny<int?>(),
                    It.IsAny<bool?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(products);

            var mappedProducts = products.Select(p => new Product
            {
                Id = p.ProductId,
                Category = p.Category,
                Description = p.Description,
                IsActive = p.IsActive,
                Name = p.Name,
                Picture = p.Picture,
                Price = p.Price,
                Stock = p.Stock,
                Discount = new Discount { Status = p.Discount.Status, Value = p.Discount.Value }
            });

            _mockMapper.Setup(m => m.Map<IEnumerable<Product>>(It.IsAny<IEnumerable<EntityProduct>>()))
                       .Returns(mappedProducts);

            var sut = await _productServices.GetFilteredProductsAsync("Category1", null, null, null, null, null, 1, 10, "Name", true);

            Assert.Equal(2, sut.Count());
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmpty_WhenNoProductsExist()
        {
            var products = new List<EntityProduct>();

            _mockDataSource.Setup(ds => ds.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>()))
                           .ReturnsAsync(products);

            var mappedProducts = new List<Product>();

            _mockMapper.Setup(m => m.Map<IEnumerable<Product>>(It.IsAny<IEnumerable<EntityProduct>>()))
                       .Returns(mappedProducts);

            var sut = await _productServices.GetAllAsync(1, 10, "Name", true);

            Assert.Empty(sut);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            _mockDataSource.Setup(ds => ds.GetById(It.IsAny<string>()))
                           .ReturnsAsync((EntityProduct)null);

            _mockMapper.Setup(m => m.Map<Product>(It.IsAny<EntityProduct>()))
                       .Returns((Product)null);

            var sut = await _productServices.GetByIdAsync("1");

            Assert.Null(sut);
        }

        [Fact]
        public async Task GetFilteredProductsAsync_ShouldReturnEmpty_WhenNoProductsMatchFilters()
        {
            var products = new List<EntityProduct>();

            _mockDataSource.Setup(ds => ds.GetFilteredProducts(
                    It.IsAny<string>(),
                    It.IsAny<double?>(),
                    It.IsAny<double?>(),
                    It.IsAny<bool?>(),
                    It.IsAny<int?>(),
                    It.IsAny<bool?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(products);

            var mappedProducts = new List<Product>();

            _mockMapper.Setup(m => m.Map<IEnumerable<Product>>(It.IsAny<IEnumerable<EntityProduct>>()))
                       .Returns(mappedProducts);

            var sut = await _productServices.GetFilteredProductsAsync("NonExistingCategory", null, null, null, null, null, 1, 10, "Name", true);

            Assert.Empty(sut);
        }
    }
}
