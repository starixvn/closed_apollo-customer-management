// -----------------------------------------------------------------------
// <copyright file="PublicMessageController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
//   <change who="Jason.Ly" date="2013.11.14">Implement</change>
// </history>

namespace Fanex.ApolloCMS.Message.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Fanex.ApolloCMS.Core.Controllers;
    using Fanex.ApolloCMS.Core.Resources;
    using Fanex.ApolloCMS.Message.Criteria.NewMessage;
    using Fanex.ApolloCMS.Message.Services.Implements;
    using Fanex.ApolloCMS.Message.Services.Interfaces;

    /// <summary>
    /// New public message controller.
    /// </summary>
    public class PublicMessageController : BaseController
    {
        private readonly INewMessageService _newMessageService;

        public PublicMessageController() :
            this(null)
        {
        }

        public PublicMessageController(INewMessageService newMessageService)
        {
            this._newMessageService = newMessageService ?? new NewMessageService { ServiceContext = this.ServiceContext };
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <returns>Index view.</returns>
        public ActionResult Index()
        {
            var sites = this._newMessageService.GetAllSites(new GetAllSitesCriteria());
            var recipients = sites.Select(obj => new SelectListItem
            {
                Text = obj.SiteName,
                Value = obj.SiteId
            }).ToList();

            recipients.Insert(
                           0, 
                           new SelectListItem
                           {
                               Text = ViewLang.All,
                               Value = "null",
                               Selected = true
                           });

            ViewBag.Recipient = recipients;
            return View();
        }
    }
}
