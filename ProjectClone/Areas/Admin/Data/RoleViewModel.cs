using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectClone.Areas.Admin.Data
{
    public class RoleViewModel
    {
        public int RoleId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string UserRole { get; set; }

        public virtual tbUser tbUser { get; set; }
    }
}