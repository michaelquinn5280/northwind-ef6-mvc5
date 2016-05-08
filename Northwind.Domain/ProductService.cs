using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Northwind.Domain.Model;

namespace Northwind.Domain
{
    /// <summary>
    /// Product Service
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// Northwind EF Context
        /// </summary>
        private readonly NorthwindContext _context;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductService() : this(null)
        {
            
        }

        /// <summary>
        /// DI Contructor
        /// </summary>
        /// <param name="context"></param>
        public ProductService(NorthwindContext context)
        {
            _context = context ?? new NorthwindContext();
        }

        /// <summary>
        /// Get All Products With Child Supplier Details
        /// </summary>
        /// <returns>IEnumerable Products with Supplier</returns> 
        public IEnumerable<Product> GetAllProductsWithSupplierDetails()
        {
            // Supplier ID is options, include is left outer join, could also be done with view in db
            var query = from p in _context.Products.Include(p => p.Supplier)
                        select p;
            return query.ToList();
        }

        /// <summary>
        ///  Get All Products With Child Supplier Details Async
        /// </summary>
        /// <returns>Task IEnumerable Products with Supplier</returns>
        public async Task<IEnumerable<Product>> GetAllProductsWithSupplierDetailsAsync()
        {
            // Supplier ID is options, include is left outer join, could also be done with view in db
            var query = from p in _context.Products.Include(p => p.Supplier)
                        select p;
            return await query.ToListAsync();
        }
    }
}
