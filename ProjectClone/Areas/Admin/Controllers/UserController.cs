using ProjectClone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ProjectClone.Areas.Admin.Utility;

namespace ProjectClone.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        private IAdminRepo manage;
        public UserController(IAdminRepo _manage)
        {
            manage = _manage;
        }
        // GET: Admin
        public ActionResult Manageusers()
        {
            return View();
        }
        public ActionResult ManageRoles()
        {
            
                ViewBag.Data = manage.getRoleList();
            return View();
        }
        public ActionResult EditDeleteUsers()
        {
            ViewBag.UserData = manage.getUserList();
            return View();
        }
        [HttpPost]
        public ActionResult UpdateRole(int UserId, string UserRole)
        {
            manage.UpdateRole(UserId, UserRole);
            return RedirectToAction("Manageusers");

        }

        [HttpPost]
        public ActionResult DeleteUser(int UserId)
        {
            manage.DeleteUser(UserId);
            return View();
        }
    }
}