using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Northwind.Domain;
using Northwind.Domain.Model;
using Northwind.Mvc.Controllers;
using Northwind.Mvc.Models;

namespace Northwind.Mvc.Tests
{
    /// <summary>
    /// Unit Tests for Products Controller
    /// </summary>
    [TestClass]
    public class ProductsControllerUnitTest
    {
        /// <summary>
        /// Test Products View Data
        /// </summary>
        [TestMethod]
        public void TestProductsViewData()
        {
            //test valid result set
            var data = MockDataProductsWithSuppliers();
            var mockService = MockProductService(data);
            var controller = new ProductsController(mockService.Object);

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);

            var productsViewModel = (ProductsViewModel)result.ViewData.Model;
            Assert.AreEqual("Chai", productsViewModel.ProductOverviewList.First().ProductName);
            Assert.AreEqual(3, productsViewModel.ProductOverviewList.Count());
            Assert.IsNull(productsViewModel.ProductOverviewList.First(p => p.ProductId.Equals(17)).SupplierDetails);  //test null child

            //test empty result set
            var emptyData = MockDataEmptyProductsWithSuppliers();
            var emptyMockService = MockProductService(emptyData);
            var emptyController = new ProductsController(emptyMockService.Object);

            var emptyResult = emptyController.Index() as ViewResult;
            Assert.IsNotNull(emptyResult);

            var emptyProductsViewModel = (ProductsViewModel)emptyResult.ViewData.Model;
            Assert.AreEqual(0, emptyProductsViewModel.ProductOverviewList.Count());
        }

        /// <summary>
        /// Get Mock Product Service
        /// </summary>
        /// <param name="data">IEnumerable Product Data</param>
        /// <returns>Mock IProductService</returns>
        private static Mock<IProductService> MockProductService(IEnumerable<Product> data)
        {
            Mock<IProductService> mockProductService = new Mock<IProductService>();
            mockProductService.Setup(s => s.GetAllProductsWithSupplierDetails()).Returns(data);
            return mockProductService;
        }

        /// <summary>
        /// Get Mock Data for Products With Suppliers
        /// </summary>
        /// <returns>IEnumberable Product Mock Data</returns>
        private static IEnumerable<Product> MockDataProductsWithSuppliers()
        {
            var data = new List<Product>
            {
                new Product
                {
                    ProductID = 1,
                    ProductName = "Chai",
                    SupplierID = 1,
                    CategoryID = 1,
                    QuantityPerUnit = "10 boxes x 20 bags",
                    UnitPrice = (decimal?) 18.00,
                    UnitsInStock = 39,
                    UnitsOnOrder = 0,
                    ReorderLevel = 10,
                    Discontinued = false,
                    Supplier = new Supplier
                    {
                        SupplierID = 1,
                        CompanyName = "Exotic Liquids",
                        ContactName = "Charlotte Cooper",
                        ContactTitle = "Purchasing Manager",
                        Address = "49 Gilbert St.",
                        City = "London",
                        Region = null,
                        PostalCode = "EC1 4SD",
                        Country = "UK",
                        Phone = "(171) 555-2222",
                        Fax = null,
                        HomePage = null
                    }
                },
                new Product
                {
                    ProductID = 8,
                    ProductName = "Northwoods Cranberry Sauce",
                    SupplierID = 3,
                    CategoryID = 2,
                    QuantityPerUnit = "12 - 12 oz jars",
                    UnitPrice = (decimal?) 40.00,
                    UnitsInStock = 6,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = false,
                    Supplier = new Supplier
                    {
                        SupplierID = 3,
                        CompanyName = "Grandma Kelly's Homestead",
                        ContactName = "Regina Murphy",
                        ContactTitle = "Sales Representative",
                        Address = "707 Oxford Rd.",
                        City = "Ann Arbor",
                        Region = "MI",
                        PostalCode = "48104",
                        Country = "USA",
                        Phone = "(313) 555-5735",
                        Fax = "(313) 555-3349",
                        HomePage = null
                    }
                },
                new Product
                {
                    ProductID = 17,
                    ProductName = "Alice Mutton",
                    CategoryID = 6,
                    QuantityPerUnit = "20 - 1 kg tins",
                    UnitPrice = (decimal?) 39.00,
                    UnitsInStock = 0,
                    UnitsOnOrder = 0,
                    ReorderLevel = 0,
                    Discontinued = true
                }
            }.AsEnumerable();
            return data;
        }

        /// <summary>
        /// Get Mock Data Empty Products with Suppliers
        /// </summary>
        /// <returns>IEnumerable Products Mock Data</returns>
        private static IEnumerable<Product> MockDataEmptyProductsWithSuppliers()
        {
            return new List<Product>().AsEnumerable();
        }
    }
}
