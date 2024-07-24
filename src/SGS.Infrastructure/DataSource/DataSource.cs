using MongoDB.Driver;
using SGS.Domain.DataSource;
using SGS.Domain.Entities;
using SGS.Infrastructure.Databases;

namespace SGS.Infrastructure.DataSource
{
    public class DataSource : IDataSource
    {
        private readonly MongoDbContext _context;

        public DataSource(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EntityProduct>> GetAll(int pageNumber, int pageSize, string sortBy, bool ascending)
        {
            var sortDefinition = ascending
                    ? Builders<EntityProduct>.Sort.Ascending(sortBy)
                    : Builders<EntityProduct>.Sort.Descending(sortBy);

            return await _context.Products
                            .Find(_ => true)
                            .Sort(sortDefinition)
                            .Skip((pageNumber - 1) * pageSize)
                            .Limit(pageSize)
                            .ToListAsync();
        }

        public async Task<EntityProduct> GetById(string id)
        {
            var product = await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();

            return product;
        }

        public async Task<IEnumerable<EntityProduct>> GetFilteredProducts(string category,
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
            var filter = Builders<EntityProduct>.Filter.Empty;

            if (!string.IsNullOrEmpty(category))
            {
                filter &= Builders<EntityProduct>.Filter.Eq(p => p.Category, category);
            }

            if (minPrice.HasValue)
            {
                filter &= Builders<EntityProduct>.Filter.Gte(p => p.Price, minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filter &= Builders<EntityProduct>.Filter.Lte(p => p.Price, maxPrice.Value);
            }

            if (isActive.HasValue)
            {
                filter &= Builders<EntityProduct>.Filter.Eq(p => p.IsActive, isActive.Value);
            }

            if (stock.HasValue)
            {
                filter &= Builders<EntityProduct>.Filter.Gte(p => p.Stock, stock.Value);
            }

            if (hasDiscount.HasValue)
            {
                filter &= Builders<EntityProduct>.Filter.Eq(p => p.Discount.Status, hasDiscount.Value);
            }

            var sortDefinition = ascending
                    ? Builders<EntityProduct>.Sort.Ascending(sortBy)
                    : Builders<EntityProduct>.Sort.Descending(sortBy);

            return await _context.Products
                            .Find(filter)
                            .Sort(sortDefinition)
                            .Skip((pageNumber - 1) * pageSize)
                            .Limit(pageSize)
                            .ToListAsync();
        }
    }
}
