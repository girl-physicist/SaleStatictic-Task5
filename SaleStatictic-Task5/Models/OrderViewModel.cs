using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SaleStatictic_Task5.Models
{
    public class OrderViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
       
        [Required(ErrorMessage = "Введите дату")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display (Name = "Дата")]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Введите Имя клиента")]
        [Display(Name = "Имя клиента")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Введите Имя менеджера")]
        [Display(Name = "Имя менеджера")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string ManagerName { get; set; }

        [Required(ErrorMessage = "Введите название товара")]
        [Display(Name = "Название товара")]
        [StringLength(50, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 3)]
        public string ProductName { get; set; }
    }
}