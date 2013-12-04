// --------------------------------------------------------------------------
// <copyright file="TreeViewController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Controllers
{
    using System.Web.Mvc;
    using Apollo.CMS.Core.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TreeViewController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.AgentId = 62;
            return View();
        }
    }
}
