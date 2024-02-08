using ProjectClone.Areas.Admin.Data;
using ProjectClone.Areas.User.Data;
using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace ProjectClone.Areas.User.Utility
{
    public class UserRepo : IUserRepo
    {
        private HotelDbEntities1 db;
        public UserRepo(HotelDbEntities1 _db)
        {
            this.db = _db;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        public int ConfirmBooking(BookingViewModel books)
        {
            var row = db.tbUsers.Where(x => x.UserId == books.UserId).FirstOrDefault();
            if (books.Name == null)
            {
                books.Name = row.UserName;
            }
            if (books.Phone == null)
            {
                books.Phone = row.UserPhone;
            }
            var roominfo = db.RoomTables.Where(x => x.RoomId == books.RoomId).FirstOrDefault();
            //get total cost based on stay days and quantity booked
            var total = Int32.Parse(roominfo.Price) * (int)books.Days*(int)books.Quantity;
            BookTable data = new BookTable()
            {
                
                RoomId = books.RoomId,
                UserId = books.UserId,
                Quantity = books.Quantity,
                Date = books.Date,
                Name = books.Name,
                Phone = books.Phone,
                status = 1,
                Days=books.Days,
                Total= total.ToString()
            };
            db.BookTables.Add(data);
            int res = db.SaveChanges();
            bool result = (res > 0) ? true : false;
            return data.BookId;
        }

        public BookingViewModel GetDetails(int id, string UserName)
        {
            var Bookingdata = db.RoomTables.Where(x => x.RoomId == id).Select(x => new BookingViewModel()
            {
                RoomId = x.RoomId,
                Quantity = x.Availability,
                RoomName = x.RoomName,
                UserName = UserName,
                UserId = db.tbUsers.Where(a => a.UserName == UserName).Select(b => b.UserId).FirstOrDefault(),
                Phone = db.tbUsers.Where(c => c.UserName == UserName).Select(c => c.UserPhone).FirstOrDefault()
            }).FirstOrDefault();
            return Bookingdata;
        }

        public bool VerifyRequest(BookingViewModel books)
        {
            bool res = AvailDate(books.RoomId, books.Quantity, books.Date, books.Days);//calls the function to check if the date is available for booking
            if (res)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

     public bool AvailDate(int?roomId, int?bookedqty, DateTime? date, int?staydays)
        {
            
            var rooms = db.RoomTables.Where(x => x.RoomId == roomId).FirstOrDefault();
           
            if (rooms != null)
            {
                //initialize availability 
                int avail=0;
                DateTime updatedDate =(DateTime)date;
                for (int i=0; i < staydays; i++)
                {
                    avail = AvailRoom(roomId, bookedqty, updatedDate, (int)rooms.Availability,avail);//calls the function to check if the room is available
                    updatedDate = updatedDate.AddDays(1);
                 
                };
                if (avail == staydays)
                {

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            return false;
        }
        public int AvailRoom(int? roomId, int? bookedqty, DateTime? updatedDate,int Totalrooms, int avail)
        {
            var booklist = db.BookTables.Where(x => x.RoomId == roomId && x.Date == updatedDate && x.status == 1).Count();
            if (booklist != 0)
            {
                
                var booklists = db.BookTables.Where(x => x.RoomId == roomId && x.Date == updatedDate && x.status == 1).ToList();
                int Availnum = 0;
                foreach (var list in booklists)
                {
                    var qtys = list.Quantity;
                    Availnum = (int)(Availnum + qtys + bookedqty);
                }
                if (Availnum <= Totalrooms)
                {
                    avail = avail + 1;
                }
            }
            else if (bookedqty <= Totalrooms)
            {
                avail = avail + 1;
            }
            return avail;

        }

        public BookingViewModel GetBookingDetails(int Id)
        {
            var data = (from books in db.BookTables
                            join rooms in db.RoomTables
                            on books.RoomId equals rooms.RoomId
                            where books.BookId == Id
                            select (new BookingViewModel()
                            {
                                Quantity=books.Quantity,
                                Date=books.Date,
                               BookId=books.BookId,
                                Days=books.Days,
                                Phone=books.Phone,
                                Name= books.Name,
                                RoomName=rooms.RoomName,
                                Image=rooms.Image,
                                Price=rooms.Price,
                                Total=books.Total
                            })).FirstOrDefault();
            return data;
        }
       
        public IEnumerable<BookingViewModel> GetUserBookings(string Username)
        {
            int UserId = GetUserId(Username);
            var data= db.BookTables.Where(x => x.UserId == UserId && x.status == 1).Select(m => new BookingViewModel() { 
            BookId=m.BookId,
            Date=m.Date,
                DateString =m.Date.ToString(),
            Days =m.Days,
            }).ToList();

            return data;


        }
        public int GetUserId(string Username)
        {
           
                var values = db.tbUsers.Where(x => x.UserName == Username).SingleOrDefault();
            if (values != null) {
                int id = values.UserId;
                return id;
            }
            else
            {
                return 0;
            }
               


            
        }
    }
}