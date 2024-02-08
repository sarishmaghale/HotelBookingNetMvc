using ProjectClone.Areas.Admin.Data;
using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace ProjectClone.Areas.Admin.Utility
{
    public class AdminRepo : IAdminRepo
    {
        private HotelDbEntities1 db;
        public AdminRepo(HotelDbEntities1 _db)
        {
            this.db = _db;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public bool AddRooms(RoomsViewModel room)
        {
            RoomTable data = new RoomTable()
            {
                Availability = room.Availability,
                RoomName = room.RoomName,
                Price = room.Price,
                Image = room.Image,
                Capacity=room.Capacity,
                Beds=room.Beds,
                Description=room.Description
            };
            db.RoomTables.Add(data);
            int res = db.SaveChanges();
            bool val = (res > 0) ? true : false;
            return val;
        }

        //delete booking records
        public void DeleteRecord(int BookId)
        {
            var data = db.BookTables.Where(x => x.BookId == BookId).FirstOrDefault();
            //1= checkedIn 0=checkedOut 
            data.status = 0;
            db.Entry(data).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public bool DeleteRooms(int id)
        {
            var data = db.RoomTables.Where(x => x.RoomId == id).FirstOrDefault();
            if (data != null)
            {
                db.RoomTables.Remove(data);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditRooms(RoomsViewModel room)
        {
            var row = db.RoomTables.Where(x => x.RoomId == room.RoomId).FirstOrDefault();
            if (row != null)
            {
                row.RoomName = room.RoomName;
                row.Price = room.Price;
                row.Availability = room.Availability;
                row.Beds = room.Beds;
                row.Capacity = room.Capacity;
                row.Description = room.Description;
            }
            db.Entry(row).State = EntityState.Modified;
            int res = db.SaveChanges();
            bool result = (res > 0) ? true : false;
            return result;
        }

        public BookTableViewModel GetBookingDetails(int BookId, int UserId, int RoomId)
        {
            var data = db.BookTables.Where(x => x.BookId == BookId).Select(m => new BookTableViewModel()
            {
                Date = m.Date,
                UserName = m.Name,
                Phone = m.Phone,
                Total=m.Total,
                RoomName = db.RoomTables.Where(a => a.RoomId == RoomId).Select(a => a.RoomName).FirstOrDefault(),
       
            }).FirstOrDefault() ?? new BookTableViewModel();
            return data;
        }

        public IEnumerable<BookTableViewModel> GetBookings()
        {
            //1=booked 0=checkOut
            var data = db.BookTables.Where(x=>x.status==1).Select(x => new BookTableViewModel()
            {
                UserId = x.UserId,
                RoomId = x.RoomId,
                BookId = x.BookId,
                Date = x.Date,
                Quantity = x.Quantity
            }).ToList().OrderByDescending(x => x.BookId);
            return data;
        }
 


        public IEnumerable<RoleViewModel> getRoleList()
        {
            IEnumerable<RoleViewModel> data = (from user in db.tbUsers
                          join role in db.Roles
                          on user.UserId equals role.UserId
                          where user.status == 1
                          select new RoleViewModel
                          {
                              UserId=role.UserId,
                              RoleId=role.RoleId,
                              UserRole=role.UserRole,
                          }).ToList();
            return data;
        }

        public IEnumerable<UsersViewModel> getUserList()
        {
            IEnumerable<UsersViewModel> data = db.tbUsers.Where(m=> m.status==1).Select(x => new UsersViewModel()
            {
                UserId = x.UserId,
                UserName=x.UserName,
                UserPhone=x.UserPhone
            }).ToList();
            return data;
        }

        public void UpdateRole(int UserId, string UserRole)
        {
            var data=db.Roles.Where(x => x.UserId == UserId).FirstOrDefault();
            data.UserRole = UserRole;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteUser(int UserId)
        {
            var data = db.tbUsers.Where(x => x.UserId == UserId).FirstOrDefault();
            data.status = 0;
            db.Entry(data).State = EntityState.Modified;
            db.SaveChanges();

        }

     
    }
}