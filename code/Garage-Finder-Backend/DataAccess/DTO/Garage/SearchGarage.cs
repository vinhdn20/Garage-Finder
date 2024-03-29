﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.Garage
{
    public class SearchGarage
    {
        public string? keyword { get; set; }
        public int[]? provinceID { get; set; }
        public int[]? districtsID { get; set; }
        public int[]? categoriesID { get; set; }
        public int[]? brandsID { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
