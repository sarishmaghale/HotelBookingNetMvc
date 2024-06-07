using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectClone.Areas.User.Data
{
    public class EsewaPaymentRequest
    {
        public string amt { get; set; }
        public string psc { get; set; }
        public string pdc { get; set; }
        public string txAmt { get; set; }
        public string tAmt { get; set; }
        public string pid { get; set; }
        public string scd { get; set; } // Merchant Code
        public string su { get; set; } // Success URL
        public string fu { get; set; }
    }
}