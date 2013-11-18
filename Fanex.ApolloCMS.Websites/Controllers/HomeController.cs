// --------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.04">Created</change>
// </history>

namespace Fanex.ApolloCMS.Websites.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using Fanex.ApolloCMS.Core.Controllers;
    using Fanex.ApolloCMS.Core.Resources;
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

        /// <summary>
        /// Get Search Partial view.
        /// </summary>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult _Search()
        {
            var memberTypes = new List<SelectListItem>();

            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Agent,
                Value = "true"
            });
            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Member,
                Value = "false"
            });

            ViewBag.MemberTypes = memberTypes;
            return PartialView("~/Views/Shared/_Search.cshtml");
        }

        /// <summary>
        /// Get Left Pan Partial view.
        /// </summary>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult _LeftPan()
        {
            return PartialView("~/Views/Shared/_LeftPan.cshtml");
        }
    }
}