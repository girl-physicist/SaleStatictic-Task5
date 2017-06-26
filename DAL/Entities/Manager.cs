using System.Collections.Generic;

namespace DAL.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string ManagerName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
