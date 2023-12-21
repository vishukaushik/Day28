using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using Moq;
using ProductsAPI.Repository;
using ProductsAPI.Model;

namespace ProductsAPI_Test.RepositoryTest
{
    [TestFixture]
    internal class ProductRepoTest
    {
        private const string TestFilePath = "testProducts.json";

        [SetUp]
        public void SetUp()
        {
            // Optionally, you can set up any necessary conditions before each test.
            // For example, you might want to create a clean test file for each test.
            File.WriteAllText(TestFilePath, "[]");
        }

        [TearDown]
        public void TearDown()
        {
            // Optionally, you can perform cleanup after each test.
            // For example, you might want to delete the test file after the tests are done.
            File.Delete(TestFilePath);
        }

        [Test]
        public void AddProduct_ShouldIncreaseProductCount()
        {
            // Arrange
            var repo = new ProductRepo();
            var initialCount = repo.GetAllProducts().Count;
            var newProduct = new Product { ProductId = 1, ProductName = "TestProduct", ProductPrice = 1000 };

            // Act
            repo.AddProduct(newProduct);

            // Assert
            var updatedCount = repo.GetAllProducts().Count;
            Assert.AreEqual(initialCount + 1, updatedCount);
        }

        [Test]
        public void GetProductById_ShouldReturnCorrectProduct()
        {
            // Arrange
            var repo = new ProductRepo();
            var expectedProduct = new Product { ProductId = 1, ProductName = "TestProduct", ProductPrice = 1000 };
            repo.AddProduct(expectedProduct);

            // Act
            var actualProduct = repo.GetProductById(1);

            // Assert
            Assert.AreEqual(expectedProduct, actualProduct);
        }

        [Test]
        public void UpdateProduct_ShouldModifyExistingProduct()
        {
            // Arrange
            var repo = new ProductRepo();
            var originalProduct = new Product { ProductId = 1, ProductName = "OriginalProduct", ProductPrice = 1000 };
            repo.AddProduct(originalProduct);

            var updatedProduct = new Product { ProductId = 1, ProductName = "UpdatedProduct", ProductPrice = 1500 };

            // Act
            repo.UpdateProduct(1, updatedProduct);

            // Assert
            var products = repo.GetAllProducts();
            var modifiedProduct = products.FirstOrDefault(p => p.ProductId == 1);
            Assert.IsNotNull(modifiedProduct);
            Assert.AreEqual(updatedProduct.ProductName, modifiedProduct.ProductName);
            Assert.AreEqual(updatedProduct.ProductPrice, modifiedProduct.ProductPrice);
        }

        [Test]
        public void DeleteProduct_ShouldRemoveProduct()
        {
            // Arrange
            var repo = new ProductRepo();
            var productToDelete = new Product { ProductId = 1, ProductName = "ProductToDelete", ProductPrice = 20000 };
            repo.AddProduct(productToDelete);

            // Act
            repo.DeleteProduct(1);

            // Assert
            var products = repo.GetAllProducts();
            var deletedProduct = products.FirstOrDefault(p => p.ProductId == 1);
            Assert.IsNull(deletedProduct);
        }
    }
}
