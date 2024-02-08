
using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectClone.Utility
{
    public class SharedRepository 
    {
        private HotelDbEntities1 db;
        public SharedRepository(HotelDbEntities1 _db)
        {
            this.db = _db;
        }
        public RoomsViewModel GetRoomDetails(int id)
        {
            var data = db.RoomTables.Where(x => x.RoomId == id).Select(x => new RoomsViewModel()
            {
                RoomId = x.RoomId,
                RoomName = x.RoomName,
                Price = x.Price,
                Availability = x.Availability,
                Image = x.Image,
                Capacity=x.Capacity,
                Beds=x.Beds,
                Description=x.Description
            }).FirstOrDefault();
            return data;
        }

        public IEnumerable<RoomsViewModel> GetRooms()
        {
            IEnumerable<RoomsViewModel> data = db.RoomTables.Select(x => new RoomsViewModel()
            {
                RoomId = x.RoomId,
                RoomName = x.RoomName,
                Availability = x.Availability,
                Price = x.Price,
                Image = x.Image,
                Capacity=x.Capacity,
                Beds=x.Beds,
               

            }).ToList();
            return data;
        }

        public int TodayAvailability(int id)
        {
            int num = 0;
            int rem = 0;
            var row = db.RoomTables.Where(x => x.RoomId == id).ToList();
            foreach (var rowvalue in row)
            {
                var rooms = rowvalue.Availability;
                var values = db.BookTables.Where(x => x.RoomId == id && x.Date == DateTime.Today && x.status == 1).ToList();
                foreach (var v in values)
                {
                    var qty = v.Quantity;
                    num = (int)(num + qty);
                }
                rem = (int)(rooms - num);
            }
            return rem;
        }
    }
}