using System;

namespace BLL.DTO
{
   public class OrderDTO : DTO
    {
        public DateTime? Date { get; set; }

        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
    }
}
