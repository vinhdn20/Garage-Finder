﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Subscription
{
    public class InvoicesDTO
    {
        public int InvoicesID { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
