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
    using Apollo.CMS.Message.Constants;
    using Apollo.CMS.Message.Criteria.NewMessage;
    using Apollo.CMS.Message.Services.Implements;
    using Apollo.CMS.Message.Services.Interfaces;
    using Apollo.CMS.Message.ViewModels.NewMessage;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

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
                form.MsgType = (byte)MessageTypes.Private;

                return Json(this._newMessageService.InsertMessage(form));
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