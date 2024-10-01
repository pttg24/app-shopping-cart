namespace ShoppingCart.Contracts.Dtos;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double TotalAmount { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; }
}
