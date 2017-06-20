using System;
using System.ComponentModel.DataAnnotations;

namespace SaleStatictic_Task5.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
    }
}