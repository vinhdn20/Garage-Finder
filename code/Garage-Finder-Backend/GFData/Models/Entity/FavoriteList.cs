﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class FavoriteList
    {
        [Key]
        public int FavoriteID { get; set; }
        public int UserID { get; set; }
        public int GarageID { get; set; }
    }
}
