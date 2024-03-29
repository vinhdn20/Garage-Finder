﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFData.Models.Entity
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public DateTime DateTime { get; set; }
        public string Content { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public bool IsRead { get; set; }

        public Users User { get; set; }
    }
}
