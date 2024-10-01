using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastructure.Models;

public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public double TotalAmount { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}
