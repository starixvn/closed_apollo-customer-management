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
    using Fanex.ApolloCMS.Core.Web;
    using Fanex.ApolloCMS.Message.Constants;
    using Fanex.ApolloCMS.Message.Criteria.NewMessage;
    using Fanex.ApolloCMS.Message.Services.Implements;
    using Fanex.ApolloCMS.Message.Services.Interfaces;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Framework.Data;

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
            this._newMessageService = newMessageService ?? new NewMessageService();
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <returns>Index view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.AddNewMessage, Policy = Constant.Policy.Host)]
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
            ViewBag.HasViewMessageRight = this.HasRight(ApolloCMSFunctionKeys.ViewMessageList, Constant.Policy.Read);

            return View();
        }

        /// <summary>
        /// Adds the new public message.
        /// </summary>
        /// <param name="criteria">The criteria that contains the information of the new message.</param>
        /// <returns>Whether or not the new public message is added successfully or not.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.AddNewMessage, Policy = Constant.Policy.Host)]
        public JsonResult AddNewMessage(InsertMessageCriteria criteria)
        {
            if (!ModelState.IsValid)
            {
                return this.GetInvalidModelErrors();
            }
            else
            {
                criteria.UserLoginId = this.CurrentSession.UserId;
                criteria.UserLoginName = this.CurrentSession.Username;
                criteria.MsgType = (byte)MessageTypes.Public;

                return Json(this._newMessageService.InsertMessage(criteria));
            }
        }
    }
}
