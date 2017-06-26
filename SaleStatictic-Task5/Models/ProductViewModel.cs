using System.ComponentModel.DataAnnotations;

namespace SaleStatictic_Task5.Models
{
    public class ProductViewModel
    {
        [Required]
        [Display(Name = "Id товара")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название товара")]
        [Display(Name = "Название товара")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Введите цену товара, в качестве разделителя дробной и целой части используется запятая")]
        [Display(Name = "Цена товара")]
        [Range(typeof(decimal), "0,01", "9999,99",ErrorMessage = "Наименьшая цена - 1 копейка")]
        public decimal ProductCost { get; set; }
    }
}
