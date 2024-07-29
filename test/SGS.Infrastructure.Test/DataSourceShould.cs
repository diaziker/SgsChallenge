using MongoDB.Driver;
using SGS.Domain.Entities;

namespace SGS.Test
{
    public class DataSourceShould
    {
        private readonly IMongoCollection<EntityProduct> _products;

        public DataSourceShould()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("sgs");
            _products = mongoDatabase.GetCollection<EntityProduct>("Products");
        }

        [Fact(Skip = "Local tests")]
        public async Task Should_AddDataToMongoDb()
        {
            var products = new List<EntityProduct>();
            var totalProducts = 15;

            for (int i = 1; i <= totalProducts; i++)
            {
                products.Add(new EntityProduct
                {
                    ProductId = Guid.NewGuid().ToString(),
                    Category = $"Category{i}",
                    Description = $"Description for product {i}",
                    IsActive = i % 2 == 0,
                    Name = $"Product{i}",
                    Picture = $"https://picsum.photos/700/700?random={i}",
                    Price = i * 10.5,
                    Stock = i * 10,
                    Discount = new EntityDiscount
                    {
                        Status = i % 2 == 0,
                        Value = i % 2 == 0 ? 10 : 0
                    }
                });
            }

            await _products.InsertManyAsync(products);
        }

        [Fact(Skip = "Local tests")]
        public async Task Should_DeleteDataToMongoDb()
        {
            await _products.DeleteManyAsync(Builders<EntityProduct>.Filter.Empty);
        }
    }
}