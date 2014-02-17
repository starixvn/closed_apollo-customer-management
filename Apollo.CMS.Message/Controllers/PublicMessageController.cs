// -----------------------------------------------------------------------
// <copyright file="PublicMessageController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
//   <change who="Jason.Ly" date="2013.11.14">Implement</change>
// </history>

namespace Apollo.CMS.Message.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Resources;
    using Apollo.CMS.Message.Constants;
    using Apollo.CMS.Message.Criteria.NewMessage;
    using Apollo.CMS.Message.Models.NewMessage;
    using Apollo.CMS.Message.Services.Implements;
    using Apollo.CMS.Message.Services.Interfaces;
    using Apollo.CMS.Message.ViewModels.NewMessage;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

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
            IEnumerable<Site> sites = this._newMessageService.GetAllSites(new GetAllSitesCriteria());
            List<SelectListItem> recipients = sites.Select(obj => new SelectListItem
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
        /// <param name="form">The criteria that contains the information of the new message.</param>
        /// <returns>Whether or not the new public message is added successfully or not.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.AddNewMessage, Policy = Constant.Policy.Host)]
        public JsonResult AddNewMessage(InsertNewMessageForm form)
        {
            if (!ModelState.IsValid)
            {
                return this.GetInvalidModelErrors();
            }
            else
            {
                form.UserLoginId = this.CurrentSession.UserId;
                form.UserLoginName = this.CurrentSession.Username;
                form.MsgType = (byte)MessageTypes.Public;

                return Json(this._newMessageService.InsertMessage(form));
            }
        }
    }
}