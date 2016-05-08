using System.Linq;
using System.Web.Mvc;
using Northwind.Domain;
using Northwind.Mvc.Models;

namespace Northwind.Mvc.Controllers
{
    /// <summary>
    /// Products Controller
    /// </summary>
    public class ProductsController : Controller
    {
        /// <summary>
        /// Products Service
        /// </summary>
        private readonly IProductService _service;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ProductsController() : this(null)
        {

        }

        /// <summary>
        /// DI Contructor
        /// </summary>
        /// <param name="service">Product Service Interface</param>
        public ProductsController(IProductService service)
        {
            _service = service ?? new ProductService();
        }

        /// <summary>
        /// GET: Products
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult Index()
        {
            ProductsViewModel viewModel = _service.GetAllProductsWithSupplierDetails().ToList();
            return View(viewModel);
        }
    }
}
