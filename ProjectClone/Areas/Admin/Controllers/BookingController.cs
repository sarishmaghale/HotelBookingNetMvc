using ProjectClone.Areas.Admin.Data;
using ProjectClone.Areas.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectClone.Areas.Admin.Controllers
{
    public class BookingController : Controller
    {
        // GET: Admin/Booking
        private IAdminRepo bookings;
        public BookingController(IAdminRepo _bookings)
        {
            bookings = _bookings;
        }
        public ActionResult Index()
        {
            var data=bookings.GetBookings();
            return View(data);
        }
        public JsonResult BookingDetails(int BookId, int UserId, int RoomId)
        {
            var data = bookings.GetBookingDetails(BookId,UserId,RoomId);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public ActionResult CheckOut(int BookedId)
        {
            bookings.DeleteRecord(BookedId);
            return RedirectToAction("Index", "Booking", new {area="Admin"});
        }
       
    }
}