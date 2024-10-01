namespace ShoppingCart.Contracts.Dtos;

public class CartItemDto
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
}
