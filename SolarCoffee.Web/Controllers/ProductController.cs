
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data.Dtos;
using SolarCoffee.Services.IServices;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Adds a new product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost("/api/product")]
        public ActionResult AddProduct([FromBody] ProductDto productDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            _logger.LogInformation("Adding product");
            return Ok(_productService.CreateProduct(productDto));
        }

        /// <summary>
        /// Returns all products
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Getting all products");
            return Ok(_productService.GetAllProducts());
        }

        /// <summary>
        /// Returns an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/api/product/{productId}")]
        public ActionResult GetProductById(int productId)
        {
            _logger.LogInformation("Getting a product by productId");
            return Ok(_productService.GetProductById(productId));
        }

        /// <summary>
        /// Archives an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPatch("/api/product/{productId}")]
        public ActionResult ArchiveProduct(int productId)
        {
            _logger.LogInformation("Archiving product");
            return Ok(_productService.ArchiveProduct(productId));
        }
    }
}