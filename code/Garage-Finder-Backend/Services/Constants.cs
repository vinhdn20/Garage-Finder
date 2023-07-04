﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Constants
    {
        public const string STATUS_ORDER_OPEN = "open";
        public const string STATUS_ORDER_CONFIRMED = "confirmed";
        public const string STATUS_ORDER_CANCELED = "canceled";
        public const string STATUS_ORDER_DONE = "done";
        public const string STATUS_ORDER_REJECT = "reject";

        public const string BLOCK_STAFF = "block";
        public const string OPEN_STAFF = "open";
        public const string ROLE_USER = "user";
        public const string ROLE_STAFF = "staff";
        public static readonly List<string> STAFF_STATUS = new List<string>() {BLOCK_STAFF, OPEN_STAFF};
    }
}
