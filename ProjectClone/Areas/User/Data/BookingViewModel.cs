using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectClone.Areas.User.Data
{
    public class BookingViewModel
    {
        public int BookId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RoomId { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public Nullable<int> Quantity { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Date { get; set; }

        public string Name { get; set; }
        public string DateString { get; set; }
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public string Total { get; set; }
        public string Price { get; set; }
        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid phone Number")]
        public string Phone { get; set; }
        public Nullable<int> Days { get; set; }

        public Nullable<int> PayStatus { get; set; }

        public string Image { get; set; }
    }
}