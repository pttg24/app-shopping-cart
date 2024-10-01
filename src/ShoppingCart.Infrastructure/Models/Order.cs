using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace ShoppingCart.Infrastructure.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Created { get; set; }
    public DateTime Update { get; set; }
    public DateTime Delivery { get; set; }
    public double TotalAmount { get; set; }
    public string Status { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
