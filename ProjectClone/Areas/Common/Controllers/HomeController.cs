﻿
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProjectClone.Areas.Common.Controllers
{
   
    public class HomeController : Controller
    {
        // GET: Common/Home
    
        public ActionResult Index()
        {
           
            return View();
        }
       
    }
}