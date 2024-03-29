﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class Brand
    {
        [Key]
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public string? Note { get; set; }
        public string? ImageLink { get; set; }

        public ICollection<Car> Cars { get; set; }
        public ICollection<GarageBrand> GarageBrands { get; set; }
        public ICollection<GuestOrder> GuestOrders { get; set; }
    }
}
