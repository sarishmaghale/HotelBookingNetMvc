
using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ProjectClone.Areas.Common.Utility
{
    public class CommonRepo : ICommonRepo
    {
        private HotelDbEntities1 db;
        public CommonRepo(HotelDbEntities1 _db)
        {
            this.db = _db;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool CheckUser(UsersViewModel users)
        {
            int row = db.tbUsers.Where(x => x.UserName == users.UserName).Count();
            bool res = (row == 0) ? true : false;
            return res;
        }
        public UsersViewModel GetUserData(string UserName)
        {
            var data = db.tbUsers.Where(x => x.UserName == UserName).Select(m => new UsersViewModel()
            {
                UserId = m.UserId,
                UserName = m.UserName,
                UserPassword = m.UserPassword,
            }).FirstOrDefault();
            return data;
        }

        public string GetUserRole(int UserId)
        {
            var Role = db.Roles.Where(x => x.UserId == UserId).Select(m => m.UserRole).FirstOrDefault();
            return Role;
        }

        public void InsertDefaultRole(int UserId)
        {
            Role roles = new Role()
            {
                UserId = UserId,
                UserRole = "User",
            };
            db.Roles.Add(roles);
            db.SaveChanges();
        }

        public bool LogInData(UsersViewModel users)
        {
            var row = db.tbUsers.Where(x => x.UserName == users.UserName && x.UserPassword == users.UserPassword).FirstOrDefault();
            bool res = (row != null) ? true : false;
            return res;
        }

        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool Register(UsersViewModel users)
        {
            tbUser data = new tbUser()
            {
                UserName = users.UserName,
                UserPassword = users.UserPassword,
                UserPhone = users.UserPhone,
                status = 1,
            };
            db.tbUsers.Add(data);
            int a = db.SaveChanges();
            bool res = (a > 0) ? true : false;
            return res;
        }
        public IEnumerable<RoomsViewModel> GetOtherRooms(int id)
        {
            var data=db.RoomTables.Where(x => x.RoomId != id).Select(m => new RoomsViewModel() {
                RoomId = m.RoomId,
                RoomName = m.RoomName,
                Availability = m.Availability,
                Price = m.Price,
                Image = m.Image,
                Capacity = m.Capacity,
                Beds = m.Beds,

            }).ToList();
            return data;
        }
        public int GetAvailByDate(DateTime date, int id)
        {
            int num = 0;
            int rem = 0;
            var row = db.RoomTables.Where(x => x.RoomId == id).ToList();
            foreach (var rowvalue in row)
            {
                var rooms = rowvalue.Availability;
                var values = db.BookTables.Where(x => x.RoomId == id && x.Date == date && x.status == 1).ToList();
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