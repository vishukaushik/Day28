using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using Moq;
using ProductsAPI.Repository;
using ProductsAPI.Model;
using ProductsAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ProductsAPI_Test.ControllersTest
{
    [TestFixture]
    internal class ProductControllerTest
    {
        private Mock<IProductRepo> rep;
        private ProductController controller;
        [SetUp]
        public void Setup()
        {
            rep = new Mock<IProductRepo>();
            controller = new ProductController(rep.Object);
        }

        [Test]
        public void GetProductById_Sucess()
        {
            var id = 1;
             
            rep.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(new Product { ProductId = id, ProductName = "mobile", ProductBrand="apple",ProductPrice=10000,ProductQunatity=10});
            var result = controller.GetProductById(id) as ActionResult<Product>;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var expected = result.Result as OkObjectResult;
            var okresult = expected.Value as Product;
            Assert.AreEqual("mobile", okresult.ProductName);

        }
        [Test]
        public void AddProduct_ValidProduct_ReturnsCreatedResult()
        {
            // Arrange
            var productToAdd = new Product { ProductId = 1, ProductName = "mobile", ProductBrand = "apple", ProductPrice = 10000, ProductQunatity = 10 };
            // Act
            rep.Setup(x => x.AddProduct(It.IsAny<Product>())).Returns(productToAdd);
            var result = controller.AddProduct(productToAdd) as ObjectResult;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);
            // Additional assertions if needed
        }

        [Test]
        public void UpdateProduct_ExistingProductId_ReturnsNoContentResult()
        {
            // Arrange
            var existingProductId = 1;
            var updatedProduct = new Product { ProductId = 1, ProductName = "mobile", ProductBrand = "apple", ProductPrice = 10000, ProductQunatity = 10 };
            rep.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(updatedProduct);
            rep.Setup(repo => repo.UpdateProduct(existingProductId, updatedProduct)).Returns(updatedProduct);
            // Act
            var expected = controller.UpdateProduct(existingProductId, updatedProduct) as OkObjectResult;
            // Assert
            Assert.IsNotNull(expected);
            Assert.AreEqual(200, expected.StatusCode);
            // Additional assertions if needed
        }

        [Test]
        public void DeleteProduct_ExistingProductId_ReturnsNoContentResult()
        {
            // Arrange
            var existingProductId = 1;
            rep.Setup(x => x.GetProductById(It.IsAny<int>())).Returns(new Product { ProductId = 1, ProductName = "mobile", ProductBrand = "apple", ProductPrice = 10000, ProductQunatity = 10 });
            rep.Setup(repo => repo.DeleteProduct(existingProductId));

            // Act
            var result = controller.DeleteProduct(existingProductId) as NoContentResult;

            // Assert
            //Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
            // Additional assertions if needed
        }

        [Test]
        public void GetProductById_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var nonExistentProductId = 999; // Choose an ID that does not exist in your mock data
            rep.Setup(repo => repo.GetProductById(nonExistentProductId)).Returns((Product)null);

            // Act
            var result = controller.GetProductById(nonExistentProductId) as ActionResult<Product>;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
            var expected = result.Result as NotFoundResult;
            
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, expected.StatusCode);
            // Additional assertions if needed
        }

        [Test]
        public void AddProduct_FailedToAdd_ReturnsBadRequest()
        {
            // Arrange
            var productToAdd = new Product { };
            // Set up the mock to simulate a failure to add (e.g., returning null)
            rep.Setup(repo => repo.AddProduct(It.IsAny<Product>())).Returns((Product)null);
            // Act
            var result = controller.AddProduct(productToAdd) as BadRequestResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            // Additional assertions if needed
        }

        [Test]
        public void UpdateProduct_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var nonExistentProductId = 999; // Choose an ID that does not exist in your mock data
            var updatedProduct = new Product { /* Initialize updated product properties */ };
            rep.Setup(repo => repo.UpdateProduct(nonExistentProductId, updatedProduct));

            // Act
            var result = controller.UpdateProduct(nonExistentProductId, updatedProduct) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            // Additional assertions if needed
        }

        [Test]
        public void DeleteProduct_ProductNotFound_ReturnsNotFound()
        {
            // Arrange
            var nonExistentProductId = 999; // Choose an ID that does not exist in your mock data
            rep.Setup(repo => repo.DeleteProduct(nonExistentProductId));

            // Act
            var result = controller.DeleteProduct(nonExistentProductId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            // Additional assertions if needed
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up resources if needed
        }
    }
}