using System;
using System.ComponentModel.DataAnnotations;

namespace SaleStatictic_Task5.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display (Name = "Дата")]
        public DateTime Date { get; set; }
        
        [Required]
        [Display(Name = "Имя клиента")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Имя менеджера")]
        public string ManagerName { get; set; }

        [Required]
        [Display(Name = "Название товара")]
        public string ProductName { get; set; }
    }
}