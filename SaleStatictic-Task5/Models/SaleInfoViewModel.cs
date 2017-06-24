using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;

namespace SaleStatictic_Task5.Models
{
    public class SaleInfoViewModel
    {
        public IEnumerable<OrderDTO> Orders { get; set; }
        public SelectList Managers { get; set; }
        public SelectList Products { get; set; }
        public SelectList Clients { get; set; }
        public SelectList Dates { get; set; }
    }
}