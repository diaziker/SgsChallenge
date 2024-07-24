using MongoDB.Bson.Serialization.Attributes;

namespace SGS.Domain.Entities
{
    public class EntityDiscount
    {
        [BsonElement("status")]
        public bool Status { get; set; }

        [BsonElement("value")]
        public int Value { get; set; }
    }
}