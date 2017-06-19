using System;

namespace SaleStatictic_Task5.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
    }
}