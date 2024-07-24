using System.Runtime.Serialization;

namespace SGS.Domain.Enums
{
    public enum ProductSortBy
    {
        [EnumMember(Value = "Name")]
        Name,

        [EnumMember(Value = "Price")]
        Price,

        [EnumMember(Value = "Category")]
        Category,

        [EnumMember(Value = "Stock")]
        Stock,

        [EnumMember(Value = "IsActive")]
        IsActive
    }
}
