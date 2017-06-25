using System.ComponentModel.DataAnnotations;

namespace SaleStatictic_Task5.Models
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Id товара")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название товара")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Цена товара")]
        public decimal ProductCost { get; set; }
    }
}