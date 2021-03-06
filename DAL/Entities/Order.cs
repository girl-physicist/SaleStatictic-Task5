﻿using System;

namespace DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int ProductId { get; set; }
        public int ClientId { get; set; }
        public int ManagerId { get; set; }

        public Product Product { get; set; }
        public Manager Manager { get; set; }
        public Client Client { get; set; }
    }
}
