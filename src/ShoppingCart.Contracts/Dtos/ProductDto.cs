namespace ShoppingCart.Contracts.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public string Unit { get; set; }
}
