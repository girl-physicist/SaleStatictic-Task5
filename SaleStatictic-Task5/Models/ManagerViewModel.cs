using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaleStatictic_Task5.Models
{
    public class ManagerViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Имя менеджера")]
        [Display(Name = "Имя менеджера")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string ManagerName { get; set; }
    }
}