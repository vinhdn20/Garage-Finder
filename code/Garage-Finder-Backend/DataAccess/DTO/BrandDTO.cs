﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class BrandDTO
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string Note { get; set; }
        public string? ImageLink { get; set; }
    }
}
