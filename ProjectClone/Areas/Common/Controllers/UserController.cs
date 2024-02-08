using ProjectClone.Models;
using ProjectClone.Areas.Common.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectClone.Areas.Common.Controllers
{
    public class UserController : Controller
    {
        private ICommonRepo user;
        public UserController(ICommonRepo _user)
        {
            user = _user;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UsersViewModel userdata)
        {
            bool row=user.CheckUser(userdata);
            if (row)
            {
                bool result=user.Register(userdata);
                if (result)
                {
                   var data= user.GetUserData(userdata.UserName);
                    int id = data.UserId;
                    user.InsertDefaultRole(id);
                    TempData["Register"] = "<script> alert('Successfully registered')</script>";
                }
                else
                {
                    TempData["Register"] = "<script> alert('Something went wrong')</script>";
                }
            }
            else
            {
                TempData["Register"] = "<script> alert('**UserName already exists!')</script>";
            }
            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(UsersViewModel users)
        {
            bool result=user.LogInData(users);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(users.UserName, false);
                var data= user.GetUserData(users.UserName);
                Session["UserId"] = data.UserId;
                Session["UserName"] = data.UserName;
                Session["UserPhone"] = data.UserPhone;
                var userRole = user.GetUserRole(data.UserId);
                if (userRole=="Admin")
                {
                    return RedirectToAction("Index", "Home", new { area="Admin"});
                }
                else
                {
                    return RedirectToAction("Rooms", "Room", "Common");
                }               
            }
            else
            {
                TempData["Login"] = "<script> alert('Invalid username or password')</script>";
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogInPopUp()
        {
            return PartialView("_loginForm");
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            user.LogOut();
            return RedirectToAction("Index", "Home");
        }

    }
}