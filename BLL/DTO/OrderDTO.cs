using System;

namespace BLL.DTO
{
   public class OrderDTO
   {
        public int Id { get; set; }
        public DateTime Date { get; set; }

       public string ManagerName { get; set; }
       public string ClientName { get; set; }
       public string ProductName { get; set; }
    }
}
