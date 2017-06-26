using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaleStatictic_Task5.Models
{
    public class ClientViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Имя клиента")]
        [Display(Name = "Имя клиента")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string ClientName { get; set; }
    }
}