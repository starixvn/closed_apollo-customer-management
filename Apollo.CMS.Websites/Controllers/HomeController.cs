// --------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.04">Created</change>
// </history>

namespace Apollo.CMS.Websites.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Resources;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Home controller.
    /// </summary>
    [NapSuperAuth]
    public class HomeController : BaseController
    {
        public HomeController()
        {
        }

        /// <summary>
        /// Gets Index page.
        /// </summary>
        /// <returns>Return Index View.</returns>
        public ActionResult Index()
        {
            Response.Cookies.Add(new HttpCookie("menu-id") { Expires = DateTime.Now.AddDays(-1d) });

            return View();
        }

        /// <summary>
        /// Get Menu Partial view.
        /// </summary>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult _Menu()
        {
            ViewBag.MenuItemList = CurrentSession.Menu;
            return PartialView("~/Views/Shared/_Menu.cshtml");
        }
    }
}