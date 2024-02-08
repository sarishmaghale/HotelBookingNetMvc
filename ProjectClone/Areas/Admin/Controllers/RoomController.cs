using Microsoft.Ajax.Utilities;
using ProjectClone.Areas.Admin.Data;
using ProjectClone.Areas.Admin.Utility;
using ProjectClone.Models;
using ProjectClone.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectClone.Areas.Admin.Controllers
{
    public class RoomController : Controller
    {
        // GET: Admin/Room
        private IAdminRepo room;
        private SharedRepository context;
        public RoomController(IAdminRepo _room, SharedRepository _context)
        {
            room = _room;
            context = _context;
       
        }
        public ActionResult Rooms()
        {
           
            return View(context.GetRooms());
        }
       public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RoomsViewModel rooms)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(rooms.ImageFile.FileName);
                string extension = Path.GetExtension(rooms.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                rooms.Image = "~/images/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                rooms.ImageFile.SaveAs(fileName);
                bool res = room.AddRooms(rooms);
                if (res)
                {
                    ViewBag.AddMsg = "<script> alert('Room added successfully')</script>";
                    return RedirectToAction("Rooms", "Room", new { area = "Admin" });
                }
                else
                {
                    ViewBag.AddMsg = "<script> alert('Failed to add')</script>";
                    return RedirectToAction("Create", "Room", new { area = "Admin" });
                }
                
            }
            else
            {
                ViewBag.AddMsg = "<script> alert('Something went wrong')</script>";
                return View();
            }
            
        }
        public ActionResult Edit(int id)
        {
                var data = context.GetRoomDetails(id);
                return View(data);
        }

        [HttpPost]
        public ActionResult Edit(RoomsViewModel rooms)
        {
            if (ModelState.IsValid)
            {
                bool res = room.EditRooms(rooms);
                if (res)
                {
                    ViewBag.EditMsg = "<script> alert('successfully edited')</script>";
                    return RedirectToAction("Rooms", "Room", new { area = "Admin" });
                }
                else
                {
                    ViewBag.EditMsg = "<script> alert('failed to edit')</script>";
                    return View();
                }
            }
            else
            {
                ViewBag.EditMsg = "<script> alert('Invalid Data')</script>";
                return View();
            }
           
            
        }
        public ActionResult Details(int id)
        {
            ViewBag.AvailableRooms = context.TodayAvailability(id);
            return View(context.GetRoomDetails(id));
        }
    public ActionResult Delete(int id)
        {
            var row = context.GetRoomDetails(id);
            return View(row);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult ConfirmDelete(int id)
        {
           bool res= room.DeleteRooms(id);
            if (res)
            {
                ViewBag.DeleteMsg = "<script> alert('Rooms successfully deleted')</script>";
                return RedirectToAction("Rooms", "Room", new { area = "Admin" });
            }
            else
            {
                ViewBag.DeleteMsg = "<script> alert('Failed to delete!')</script>";
                return View();
            }
        }
        
    }
}