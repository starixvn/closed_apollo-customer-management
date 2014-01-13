// --------------------------------------------------------------------------
// <copyright file="BetListAreaRegistration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.04">Created</change>
// </history>

namespace Apollo.CMS.BetList
{
    using System.Web.Mvc;

    /// <summary>
    /// Registers bet list area.
    /// </summary>
    public class BetListAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BetList";
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">The context that is used to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BetList_default",
                "BetList/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Apollo.CMS.BetList.Controllers" });
        }
    }
}