using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Models;
using ShopBridge.Respository.Interfaces;
using Shouldly;

namespace ShopBridge.UnitTestProject
{
    [TestClass]
    public class ControllerUnitTest
    {
        private Mock<IProductRespository> _mockRepository;

        List<Product> products = null;
        Product product1 = null;
        Product product2 = null;
        Product product3 = null;
        Product product4 = null;
        Product product5 = null;

        [TestInitialize]
        public void Setup ()
        {
            _mockRepository = new Mock<IProductRespository>();

        }

        private ProductController CreateController ()
        {
            // Sample Products
            product1 = new Product { productId = 1, productCode = "abc", productName = "Aaa", productDescription = "Phone", productPrice = "10", productAvailableCount = 10 };
            product2 = new Product { productId = 2, productCode = "bcd", productName = "Aaa", productDescription = "Phone", productPrice = "10", productAvailableCount = 10 };
            product3 = new Product { productId = 3, productCode = "cde", productName = "Aaa", productDescription = "Phone", productPrice = "10", productAvailableCount = 10 };
            product4 = new Product { productId = 4, productCode = "abc", productName = "Aaa", productDescription = "Phone", productPrice = "10", productAvailableCount = 10 };
            product5 = new Product { productId = 5, productCode = "def", productName = "Aaa", productDescription = "Phone", productPrice = "10", productAvailableCount = 10 };

            products = new List<Product>
        {
            product1,
            product2,
            product3,
            product4,
            product5
        };
            return new ProductController();
        }

        [TestMethod]
        public void Test_GetAllProducts_Success ()
        {

            var controller = CreateController();
            _mockRepository.Setup(x => x.GetProducts()).Returns(products);
            var jsonResult = controller.GetAllProducts();
            jsonResult.ShouldNotBeNull();
            
        }

    }
}
