// -----------------------------------------------------------------------
// <copyright file="MessageAreaRegistration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
// </history>
// -----------------------------------------------------------------------

namespace Fanex.ApolloCMS.Message
{
    using System.Web.Mvc;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Message";
            }
        }

        /// <summary>
        /// Registers the area.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
            name: "Publish_message",
            url: "Message/AddNewPublicMessage",
            defaults: new { controller = "PublicMessage", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "Fanex.ApolloCMS.Message.Controllers" });

            context.MapRoute(
            name: "Private_message",
            url: "Message/AddNewPrivateMessage",
            defaults: new { controller = "PrivateMessage", action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "Fanex.ApolloCMS.Message.Controllers" });

            context.MapRoute(
                "Message_list",
                "Message/{controller}/{action}/{id}",
                new { controller = "MessageList", action = "Index", id = UrlParameter.Optional },
                new[] { "Fanex.ApolloCMS.Message.Controllers" });
        }
    }
}
