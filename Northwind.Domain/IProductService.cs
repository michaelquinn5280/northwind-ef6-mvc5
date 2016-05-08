using System.Collections.Generic;
using System.Threading.Tasks;
using Northwind.Domain.Model;

namespace Northwind.Domain
{
    /// <summary>
    /// Interface for Product Service
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get All Products With Supplier Details
        /// </summary>
        /// <returns>IEnumerable Products</returns>
        IEnumerable<Product> GetAllProductsWithSupplierDetails();

        /// <summary>
        /// Get All Products With Supplier Details Async
        /// </summary>
        /// <returns>Task IEnumerable Product</returns>
        Task<IEnumerable<Product>> GetAllProductsWithSupplierDetailsAsync();
    }
}