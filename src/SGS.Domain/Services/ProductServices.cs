using AutoMapper;
using SGS.Domain.DataSource;
using SGS.Domain.Models;

namespace SGS.Domain.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IDataSource _dataSource;
        private readonly IMapper _mapper;

        public ProductServices(IDataSource dataSource, IMapper mapper)
        {
            _dataSource = dataSource;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, string sortBy, bool ascending)
        {
            var products = await _dataSource.GetAll(pageNumber, pageSize, sortBy, ascending);
            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var product = await _dataSource.GetById(id);
            return _mapper.Map<Product>(product);
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string category,
                                                                         decimal? minPrice,
                                                                         decimal? maxPrice,
                                                                         bool? isActive,
                                                                         int? stock,
                                                                         bool? hasDiscount,
                                                                         int pageNumber,
                                                                         int pageSize,
                                                                         string sortBy,
                                                                         bool ascending)
        {
            var products = await _dataSource.GetFilteredProducts(category, minPrice, maxPrice, isActive, stock, hasDiscount, pageNumber, pageSize, sortBy, ascending);
            return _mapper.Map<IEnumerable<Product>>(products);
        }
    }
}
