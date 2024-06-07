using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectClone.Areas.User.Data
{
    public class EsewaPaymentResponse
    {
        public string amt { get; set; }
        public string refid { get; set; }
        public string pid { get; set; }
        public string scd { get; set; }
    }
}