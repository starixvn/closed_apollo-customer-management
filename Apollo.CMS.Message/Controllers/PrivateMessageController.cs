// -----------------------------------------------------------------------
// <copyright file="PrivateMessageController.cs" company="Nexcel Solutions Vietnam">
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
    using System.Web.Mvc;
    using Apollo.CMS.Core.Configurations;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Resources;
    using Apollo.CMS.Core.Web;
    using Apollo.CMS.Message.Constants;
    using Apollo.CMS.Message.Criteria.NewMessage;
    using Apollo.CMS.Message.Services.Implements;
    using Apollo.CMS.Message.Services.Interfaces;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// New private message controller.
    /// </summary>
    public class PrivateMessageController : BaseController
    {
        private readonly INewMessageService _newMessageService;

        public PrivateMessageController() :
            this(null)
        {
        }

        public PrivateMessageController(INewMessageService newMessageService)
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
            ViewBag.MemberTypes = CreateMemberTypes();
            ViewBag.MaxRowsLimit = AppSettings.NewPrivateMessageMaxRowsLimit;
            ViewBag.HasViewMessageRight = this.HasRight(ApolloCMSFunctionKeys.ViewMessageList, Constant.Policy.Read);
            return View();
        }

        /// <summary>
        /// Gets the recipient information.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains recipient information.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.AddNewMessage, Policy = Constant.Policy.Host)]
        public JsonResult GetRecipientInfo(GetRecipientInfoCriteria criteria)
        {
            return Json(this._newMessageService.GetRecipientInfo(criteria));
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
                criteria.MsgType = (byte)MessageTypes.Private;

                return Json(this._newMessageService.InsertMessage(criteria));
            }
        }

        /// <summary>
        /// Creates the member types.
        /// </summary>
        /// <returns>List of SelectListItem.</returns>
        private IEnumerable<SelectListItem> CreateMemberTypes()
        {
            var memberTypes = new List<SelectListItem>();

            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Agent,
                Value = "true",
                Selected = true
            });
            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Member,
                Value = "false"
            });

            return memberTypes;
        }
    }
}
