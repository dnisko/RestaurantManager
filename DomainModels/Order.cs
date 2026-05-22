using DomainModels.Enums;

namespace DomainModels
{
    public class Order : BaseEntity
    {
        public int RestaurantTableId { get; set; }
        public RestaurantTable RestaurantTable { get; set; } = null!;

        public OrderStatus Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
