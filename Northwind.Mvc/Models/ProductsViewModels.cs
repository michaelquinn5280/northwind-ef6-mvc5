using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Northwind.Domain.Model;

namespace Northwind.Mvc.Models
{
    /// <summary>
    /// List Entity for All products Frid
    /// </summary>
    public class ProductOverview
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Quantity")]
        public string QuantityPerUnit { get; set; }
        [Display(Name = "Price")]
        public string UnitPrice { get; set; }
        [Display(Name = "In Stock")]
        public int UnitsInStock { get; set; }
        [Display(Name = "On Order")]
        public int UnitsOnOrder { get; set; }
        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }
        [Display(Name = "Discontinued")]
        public bool Discontinued { get; set; }
        public string SupplierId { get; set; }
        [Display(Name = "Supplier Name")]
        public string SupplierCompanyName { get; set; }
        public Supplier SupplierDetails { get; set; }
    }

    /// <summary>
    /// Products View Model
    /// </summary>
    public class ProductsViewModel
    {
        /// <summary>
        /// Products for data tables
        /// </summary>
        public IEnumerable<ProductOverview> ProductOverviewList { get; set; }

        /// <summary>
        /// Implicit Opterator to Handle Mappings 
        /// </summary>
        /// <param name="products"></param>
        public static implicit operator ProductsViewModel(List<Product> products)
        {
            var productsViewModel = new ProductsViewModel
            {
                ProductOverviewList = products.Select(MapProductOverview)
            };
            return productsViewModel;
        }

        /// <summary>
        /// Model to View Model Map for Product and Child Supplier
        /// </summary>
        /// <param name="product">Product</param>
        /// <returns>Product Overview</returns>
        private static ProductOverview MapProductOverview(Product product)
        {
            if (product == null) return null;
            return new ProductOverview
            {
                ProductId = product.ProductID,
                ProductName = product.ProductName,
                QuantityPerUnit = product.QuantityPerUnit,
                UnitPrice = $"{product.UnitPrice:C}",
                UnitsInStock = product.UnitsInStock != null ? (int)product.UnitsInStock : 0,
                UnitsOnOrder = product.UnitsOnOrder != null ? (int) product.UnitsOnOrder : 0,
                ReorderLevel = product.ReorderLevel != null ? (int) product.ReorderLevel : 0,
                Discontinued = product.Discontinued,
                SupplierId = product.Supplier?.SupplierID.ToString() ?? string.Empty,
                SupplierCompanyName = product.Supplier?.CompanyName ?? string.Empty,
                SupplierDetails = product.Supplier
            };
        }
    }
}