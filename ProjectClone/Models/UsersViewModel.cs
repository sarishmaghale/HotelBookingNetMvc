using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectClone.Models
{
    public class UsersViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Usernaame is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email address")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid phone Number")]
        public string UserPhone { get; set; }
        public Nullable<int> status { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}