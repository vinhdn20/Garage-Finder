using System;
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
        public const string ROLE_ADMIN = "admin";
        public const string USER_ACTIVE = "active";
        public const string USER_LOCKED = "locked";
        public const string GARAGE_ACTIVE = "active";
        public const string GARAGE_LOCKED = "locked";
        public const int ROLE_ADMIN_ID = 1;
        public const int ROLE_USER_ID = 2;

        public static readonly List<string> STAFF_STATUS = new List<string>() {BLOCK_STAFF, OPEN_STAFF};
        public const string DELETE_GARAGE = "delete";
        public const string INVOICE_WAITING = "waiting";
        public const string INVOICE_PAID = "paid";
        public const string INVOICE_FAIL = "fail";
        public const string VNPAY_SUCCESS = "00";
        public const string DELETE_SUBCRIPTION = "block";
        public const string OPEN_SUBCRIPTION = "open";
    }
}
