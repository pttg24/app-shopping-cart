namespace ShoppingCart.Contracts.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public string Status { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
}
