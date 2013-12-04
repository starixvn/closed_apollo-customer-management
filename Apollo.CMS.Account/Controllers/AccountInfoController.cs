// -----------------------------------------------------------------------
// <copyright file="AccountInfoController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.04">Created</change>
// </history>

namespace Apollo.CMS.Account.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Resources;

    /// <summary>
    /// Common controller.
    /// </summary>
    public class AccountInfoController : BaseController
    {
        /// <summary>
        /// Get Left Pan Partial view.
        /// </summary>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult LeftPanel()
        {
            return PartialView();
        }

        /// <summary>
        /// Get Search Partial view.
        /// </summary>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult Search()
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
            return PartialView();
        }
    }
}