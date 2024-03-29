﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Orders.RequestDTO
{
    public class DoneOrderDTO
    {
        public int GFOrderId { get; set; }
        public string Content { get; set; }
        public List<string>? ImageLinks { get; set; } = new List<string>();
        public List<string>? FileLinks { get; set; } = new List<string>();
    }
}
