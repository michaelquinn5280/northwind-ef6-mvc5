using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;
using Northwind.Domain.Model;

namespace Northwind.Domain.Test
{
    /// <summary>
    /// Class for Product Service Unit Tests
    /// </summary>
    [TestClass]
    public class ProductServiceUnitTest
    {
        /// <summary>
        /// Unit Test for Get All Products With Supplier Details
        /// </summary>
        [TestMethod]
        public void GetAllProductsWithSupplierDetails()
        {
            //valid result set
            var data = MockDataProductsWithSuppliers();
            var mockContext = MockProductContext(data);
            var products = new ProductService(mockContext.Object).GetAllProductsWithSupplierDetails().ToList();

            Assert.AreEqual(3, products.Count);
            Assert.AreEqual("Chai", products[0].ProductName);
            Assert.AreEqual("Northwoods Cranberry Sauce", products[1].ProductName);
            Assert.AreEqual("Alice Mutton", products[2].ProductName);
            Assert.IsNull(products[2].Supplier);

            //empty result set
            var emptyData = MockDataEmptyProductsWithSuppliers();
            var emptyMockContext = MockProductContext(emptyData);
            var emptyProducts = new ProductService(emptyMockContext.Object).GetAllProductsWithSupplierDetails().ToList();
            Assert.AreEqual(0, emptyProducts.Count);
        }

        /// <summary>
        /// Get Mock Product EF Context
        /// </summary>
        /// <param name="data">IQueryable Product Data</param>
        /// <returns>Mock NorthwindContext</returns>
        private static Mock<NorthwindContext> MockProductContext(IQueryable<Product> data)
        {
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //necessary to mock includes
            mockSet.Setup(s => s.Include(It.IsAny<string>())).Returns(mockSet.Object);
            var mockContext = new Mock<NorthwindContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);
            return mockContext;
        }

        /// <summary>
        /// Get Mock Data Products With Suppliers
        /// </summary>
        /// <returns></returns>
        private static IQueryable<Product> MockDataProductsWithSuppliers()
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
            }.AsQueryable();
            return data;
        }

        /// <summary>
        /// Get Mock Empty Data Products With Suppliers
        /// </summary>
        /// <returns></returns>
        private static IQueryable<Product> MockDataEmptyProductsWithSuppliers()
        {
            return new List<Product>().AsQueryable();
        }
    }
}
