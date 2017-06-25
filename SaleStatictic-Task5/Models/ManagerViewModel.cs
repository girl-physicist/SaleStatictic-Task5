using System.ComponentModel.DataAnnotations;

namespace SaleStatictic_Task5.Models
{
    public class ManagerViewModel
    {
        [Required]
        [Display(Name = "Id менеджера")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Имя менеджера")]
        public string ManagerName { get; set; }
    }
}