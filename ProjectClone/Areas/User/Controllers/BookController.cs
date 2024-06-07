using ProjectClone.Areas.User.Data;
using ProjectClone.Areas.User.Utility;
using ProjectClone.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjectClone.Areas.User.Controllers
{
    public class BookController : Controller
    {
        // GET: User/Book
        private IUserRepo booking;
        private SharedRepository content;
        private PaymentRepo paymentService;
        public BookController(IUserRepo _booking, SharedRepository _content, PaymentRepo _paymentService)
        {
            booking = _booking;
            content = _content;
            paymentService = _paymentService;
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
        [HttpPost]
        public ActionResult Payment(BookingViewModel model)
        {

                    EsewaPaymentRequest paymentModel = new EsewaPaymentRequest()
                    {
                        amt = model.Total,
                        psc = "0",
                        pdc = "0",
                        txAmt = "0",
                        tAmt = model.Total,
                        pid = model.BookId.ToString(),
                        scd = "EPAYTEST",
                        su = Url.Action("UpdateBookings", "Book", null, Request.IsSecureConnection ? "https" : "http"),
                        fu = Url.Action("ConfirmBooking", "Book", null, Request.IsSecureConnection ? "https" : "http")
                    };
                    var paymentUrl = paymentService.InitiatePayment(paymentModel);
            TempData["BookId"] = model.BookId;
                    return Redirect(paymentUrl);                
            
        }
        public ActionResult UpdateBookings()
        {
            int BookId =(int)TempData["BookId"];
            int id = booking.UpdateBookings(BookId);
            return RedirectToAction("BookingDetails", new { BookId = id});
        }
    }
}