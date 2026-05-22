namespace DomainModels
{
    public class RestaurantTable : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsOccupied { get; set; } = false;
    }
}
