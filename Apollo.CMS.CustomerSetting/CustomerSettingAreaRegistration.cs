// --------------------------------------------------------------------------
// <copyright file="CustomerSettingAreaRegistration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting
{
    using System.Web.Mvc;

    /// <summary>
    /// Registers customer setting area.
    /// </summary>
    public class CustomerSettingAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CustomerSetting";
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">The context that is used to register the area.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CustomerSetting_default",
                "CustomerSetting/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Apollo.CMS.CustomerSetting.Controllers" });
        }
    }
}