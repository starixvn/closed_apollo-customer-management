// -----------------------------------------------------------------------
// <copyright file="PublicMessageController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Fanex.ApolloCMS.Core.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class PublicMessageController : BaseController
    {
        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <returns>Index view.</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}
