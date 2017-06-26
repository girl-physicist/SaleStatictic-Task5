using System.Collections.Generic;

namespace DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
