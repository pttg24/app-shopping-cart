using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Domain;

public class Item
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
}
