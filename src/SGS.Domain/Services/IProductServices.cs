using SGS.Domain.Models;

namespace SGS.Domain.Services
{
    public interface IProductServices
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, string sortBy, bool ascending);
        Task<Product> GetByIdAsync(string id);
        Task<IEnumerable<Product>> GetFilteredProductsAsync(string category, double? minPrice, double? maxPrice, bool? isActive, int? stock, bool? hasDiscount, int pageNumber, int pageSize, string sortBy, bool ascending);
    }
}
