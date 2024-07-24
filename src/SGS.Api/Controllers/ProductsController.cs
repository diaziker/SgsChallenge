using Microsoft.AspNetCore.Mvc;
using SGS.Domain.Entities;
using SGS.Domain.Enums;
using SGS.Domain.Services;

namespace SGS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        /// <summary>
        /// Retrieves a paginated list of products.
        /// </summary>
        /// <param name="pageNumber">Page number (default is 1).</param>
        /// <param name="pageSize">Number of products per page (default is 10).</param>
        /// <param name="sortBy">Field to sort by (default is Name).</param>
        /// <param name="ascending">Sort direction (true for ascending, false for descending).</param>
        /// <returns>A list of products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityProduct>>> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] ProductSortBy sortBy = ProductSortBy.Name,
            [FromQuery] bool ascending = true)
        {
            var products = await _productServices.GetAllAsync(pageNumber, pageSize, sortBy.ToString(), ascending);
            return Ok(products);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<EntityProduct>> GetById(string id)
        {
            var product = await _productServices.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Filters products based on category, price range, stock, and discount.
        /// </summary>
        /// <param name="category">The category of the product.</param>
        /// <param name="minPrice">Minimum price of the product.</param>
        /// <param name="maxPrice">Maximum price of the product.</param>
        /// <param name="isActive">Filter by active status.</param>
        /// <param name="stock">Filter by stock quantity.</param>
        /// <param name="hasDiscount">Filter by products with discount.</param>
        /// <param name="pageNumber">Page number (default is 1).</param>
        /// <param name="pageSize">Number of products per page (default is 10).</param>
        /// <param name="sortBy">Field to sort by (default is Name).</param>
        /// <param name="ascending">Sort direction (true for ascending, false for descending).</param>
        /// <returns>A list of filtered products.</returns>
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<EntityProduct>>> Filter(
            [FromQuery] string category,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] bool? isActive,
            [FromQuery] int? stock,
            [FromQuery] bool? hasDiscount,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] ProductSortBy sortBy = ProductSortBy.Name,
            [FromQuery] bool ascending = true)
        {
            var products = await _productServices.GetFilteredProductsAsync(category, minPrice, maxPrice, isActive, stock, hasDiscount, pageNumber, pageSize, sortBy.ToString(), ascending);
            return Ok(products);
        }
    }
}
