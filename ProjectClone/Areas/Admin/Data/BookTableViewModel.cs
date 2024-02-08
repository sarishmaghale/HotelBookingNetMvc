using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectClone.Areas.Admin.Data
{
    public class BookTableViewModel
    {
        public int BookId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> Quantity { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Date { get; set; }
        public string Total { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public string Price { get; set; }
        public string Phone { get; set; }
        public Nullable<int> status { get; set; }
    }
}