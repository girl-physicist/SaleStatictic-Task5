using System.ComponentModel.DataAnnotations;

namespace SaleStatictic_Task5.Models
{
    public class ClientViewModel
    {
        [Required]
        [Display(Name = "Id клиента")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Имя клиента")]
        [StringLength(50, MinimumLength = 3)]
        public string ClientName { get; set; }
    }
}