using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectClone.Models
{
    public class RoomsViewModel
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "RoomName Is Required")]
        public string RoomName { get; set; }

        [Required(ErrorMessage = "Availability is required")]
        public Nullable<int> Availability { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public string Price { get; set; }

        public string Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
        public Nullable<int> Capacity { get; set; }
        public Nullable<int> Beds { get; set; }
        public string Description { get; set; }
    }
}