using ProjectClone.Models;
using ProjectClone.Areas.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ProjectClone.Utility;

namespace ProjectClone.Areas.Common.Controllers
{
    public class RoomController : Controller
    {

        private ICommonRepo rooms;
        private SharedRepository context;
        public RoomController(ICommonRepo _rooms, SharedRepository _context)
        {
            rooms = _rooms;
            context = _context;
        }
        public ActionResult Rooms()
        {
            
            return View(context.GetRooms());
        }

        public ActionResult RoomDetails(int id)
        {
            var data= context.GetRoomDetails(id);
            TempData["AvailableRooms"] = context.TodayAvailability(id);
            ViewBag.OtherRooms = rooms.GetOtherRooms(id);
            return View(data);
        }
        public ActionResult AvailByDate(DateTime Date, int id)
        {
            var data = rooms.GetAvailByDate(Date, id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}