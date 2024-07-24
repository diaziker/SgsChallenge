namespace SGS.Domain.Entities
{
    public class EntityProduct
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public EntityDiscount Discount { get; set; }
    }
}
