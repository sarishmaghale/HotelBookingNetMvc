using ProjectClone.Areas.User.Data;
using ProjectClone.Areas.User.Utility;
using ProjectClone.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectClone.Areas.User.Controllers
{
    public class BookController : Controller
    {
        // GET: User/Book
        private IUserRepo booking;
        private SharedRepository content;
        public BookController(IUserRepo _booking, SharedRepository _content)
        {
            booking = _booking;
            content = _content;
        }
        public ActionResult ConfirmBooking(int id)
        {
            string UserName = User.Identity.Name;
            var img = content.GetRoomDetails(id);
            ViewBag.Image = img.Image;
            var data = booking.GetDetails(id,UserName);
            return View(data);
 
            
        }

        [HttpPost]
        public ActionResult ConfirmBooking(BookingViewModel books)
        {
            if (ModelState.IsValid)
            {
                bool result = booking.VerifyRequest(books);
                if (result == true)
                {
                    int id = booking.ConfirmBooking(books);
                    TempData["BookingRequest"] = "<script>alert('Successfully booked!')</script>";
                    return RedirectToAction("BookingDetails", "Book", new { BookId = id });
                }
                else
                {
                    TempData["FailRequest"] = "<script>alert('Requested rooms not available')</script>";
                    return RedirectToAction("ConfirmBooking", new { id = books.RoomId });
                }
            }
            else
            {
                TempData["FailRequest"] = "<script>alert('Invalid data')</script>";
                return RedirectToAction("ConfirmBooking", new { id = books.RoomId });
            }
                
        }
        public ActionResult BookingDetails(int BookId)
        {
            var bookingDetails = booking.GetBookingDetails(BookId);
            return View(bookingDetails);
        }
        public ActionResult MyBookings()
        {
            string name = User.Identity.Name;

                var data = booking.GetUserBookings(name);

                return View(data);
        
           
        }
    }
}