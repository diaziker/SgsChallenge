using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SGS.Domain.Entities
{
    public class EntityProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("id")]
        public string ProductId { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("picture")]
        public string Picture { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("stock")]
        public int Stock { get; set; }

        [BsonElement("discount")]
        public EntityDiscount Discount { get; set; }
    }
}
