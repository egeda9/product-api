using Moq;
using FluentAssertions;
using Product.Repository;
using Product.Service.Implementations;

namespace Product.Test
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;

        private IList<Model.Product> _products;

        public ProductServiceTest()
        {
            this._productRepositoryMock = new Mock<IProductRepository>();
            this.Initialize();
        }

        [Fact]
        public async Task Get_Products_OK_Test()
        {
            // Given
            this._productRepositoryMock
                .Setup(x => x.GetAsync())
                .ReturnsAsync(this._products);

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            var result = await productService.GetAsync();

            // Then
            result.Count.Should().Be(3);
            this._productRepositoryMock.Verify(x => x.GetAsync(), Times.Once);
        }

        [Fact]
        public async Task Get_Product_OK_Test()
        {
            // Given
            this._productRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(this._products[0]);

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            var result = await productService.GetAsync(1);

            // Then
            result?.Id.Should().Be(1);
            this._productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_Product_Null_Test()
        {
            Model.Product? myProduct = null;

            // Given
            this._productRepositoryMock
                .Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(myProduct);

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            var result = await productService.GetAsync(1);

            // Then
            result.Should().BeNull();
            this._productRepositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Create_Product_OK_Test()
        {
            // Given
            this._productRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Model.Product>()))
                .ReturnsAsync(1);

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            var result = await productService.CreateAsync(this._products[0]);

            // Then
            result.Should().Be(1);
            this._productRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<Model.Product>()), Times.Once);
        }

        [Fact]
        public async Task Update_Product_OK_Test()
        {
            // Given
            this._productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Model.Product>()))
                .ReturnsAsync(this._products[0]);

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            var result = await productService.UpdateAsync(1, this._products[0]);

            // Then
            result?.Id.Should().Be(1);
            this._productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Model.Product>()), Times.Once);
        }

        [Fact]
        public async Task Update_Product_Null_Test()
        {
            Model.Product? myProduct = null;

            // Given
            this._productRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Model.Product>()))
                .ReturnsAsync(myProduct);

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            var result = await productService.UpdateAsync(6, this._products[0]);

            // Then
            result.Should().BeNull();
            this._productRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Model.Product>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Product_OK_Test()
        {
            // Given
            this._productRepositoryMock
                .Setup(x => x.DeleteAsync(It.IsAny<int>()));

            // When
            var productService = new ProductService(this._productRepositoryMock.Object);
            await productService.DeleteAsync(1);

            // Then
            this._productRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<int>()), Times.Once);
        }

        private void Initialize()
        {
            this._products = new List<Model.Product>();

            var product1 = new Model.Product
            {
                Id = 1,
                IsAvailable = true,
                Name = "test",
                StockQuantity = 10,
                ReleaseDate = DateTime.Now,
                Category = "test",
                CreatedAt = DateTime.Now,
                Description = "test",
                IsActive = true,
                Manufacturer = "test",
                Price = 100
            };

            var product2 = new Model.Product
            {
                Id = 2,
                IsAvailable = true,
                Name = "test2",
                StockQuantity = 20,
                ReleaseDate = DateTime.Now,
                Category = "test2",
                CreatedAt = DateTime.Now,
                Description = "test2",
                IsActive = true,
                Manufacturer = "test2",
                Price = 1000
            };

            var product3 = new Model.Product
            {
                Id = 3,
                IsAvailable = true,
                Name = "test3",
                StockQuantity = 30,
                ReleaseDate = DateTime.Now,
                Category = "test3",
                CreatedAt = DateTime.Now,
                Description = "test3",
                IsActive = true,
                Manufacturer = "test3",
                Price = 2000
            };

            this._products.Add(product1);
            this._products.Add(product2);
            this._products.Add(product3);
        }
    }
}
