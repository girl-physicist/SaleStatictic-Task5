﻿using System;

namespace BLL.DTO
{
   public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }
    }
}