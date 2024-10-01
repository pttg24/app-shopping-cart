namespace ShoppingCart.Contracts.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Update { get; set; }
    public DateTime Delivery { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; }
    public ICollection<OrderItemDto> OrderItems { get; set; }
}
