﻿using System.Web.Mvc;

namespace ProjectClone.Areas.Common
{
    public class CommonAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Common";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Common_default",
                "Common/{controller}/{action}/{id}",
                new { Controller="Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ProjectClone.Areas.Common.Controllers" }
            );
        }
    }
}