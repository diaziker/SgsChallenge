using SGS.Domain.Entities;

namespace SGS.Domain.DataSource
{
    public interface IDataSource
    {
        Task<IEnumerable<EntityProduct>> GetAll(int pageNumber, int pageSize, string sortBy, bool ascending);
        Task<EntityProduct> GetById(string id);
        Task<IEnumerable<EntityProduct>> GetFilteredProducts(string category, double? minPrice, double? maxPrice, bool? isActive, int? stock, bool? hasDiscount, int pageNumber, int pageSize, string sortBy, bool ascending);
    }
}
