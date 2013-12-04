// --------------------------------------------------------------------------
// <copyright file="AccountAreaRegistration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.04">Created</change>
// </history>

namespace Apollo.CMS.Account
{
    using System.Web.Mvc;

    /// <summary>
    /// Registers account area.
    /// </summary>
    public class AccountAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Account";
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">The context that is used to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Account_default",
                "Account/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Apollo.CMS.Account.Controllers" });
        }
    }
}