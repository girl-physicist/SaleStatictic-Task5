using System.Collections.Generic;

namespace DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal ProductCost { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
