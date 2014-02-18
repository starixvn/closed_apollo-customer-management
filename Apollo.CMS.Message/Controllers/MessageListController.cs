// -----------------------------------------------------------------------
// <copyright file="MessageListController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
//   <change who="Jason.Ly" date="2014.2.18">Refactor code</change>
// </history>

namespace Apollo.CMS.Message.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Apollo.CMS.Core.Configurations;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Message.Criteria.MessageList;
    using Apollo.CMS.Message.Services.Implements;
    using Apollo.CMS.Message.Services.Interfaces;
    using Apollo.CMS.Message.ViewModels.MessageList;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Message list controller.
    /// </summary>
    public class MessageListController : BaseController
    {
        private readonly IMessageService _messageService;

        public MessageListController()
            : this(null)
        {
        }

        public MessageListController(IMessageService messageService)
        {
            this._messageService = messageService ?? new MessageService();
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <param name="isRedirected">Check whether the page is redirect from another page or not.</param>
        /// <returns>Index view.</returns>
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewMessageList, Policy = Constant.Policy.Read)]
        public ActionResult Index(bool isRedirected = false)
        {
            ViewBag.MinDate = DateTime.Now.AddMonths(AppSettings.MessageListMonthRange * -1).ToShortDateString();
            ViewBag.IsRedirected = isRedirected;
            ViewBag.HasHostRight = this.HasRight(ApolloCMSFunctionKeys.ViewMessageList, Constant.Policy.Host);
            return View();
        }

        /// <summary>
        /// Gets the message list.
        /// </summary>
        /// <param name="getMessageListCriteria">The get message list criteria.</param>
        /// <returns>List Message.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewMessageList, Policy = Constant.Policy.Read)]
        public JsonResult GetMessageList(GetMessageListCriteria getMessageListCriteria)
        {
            IEnumerable<MessageInfo> result = this._messageService.GetMessageList(getMessageListCriteria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets the message by identifier.
        /// </summary>
        /// <param name="getMessageByIdCriteria">The get message by identifier criteria.</param>
        /// <returns>The edit message view.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewMessageList, Policy = Constant.Policy.Host)]
        public ActionResult GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria)
        {
            MessageDetails result = this._messageService.GetMessageById(getMessageByIdCriteria);

            return PartialView("~/Areas/Message/Views/MessageList/EditMessage.cshtml", result);
        }

        /// <summary>
        /// Updates the message.
        /// </summary>
        /// <param name="updateMessageCriteria">The update message criteria.</param>
        /// <returns>Success message.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewMessageList, Policy = Constant.Policy.Host)]
        public ActionResult UpdateMessage(UpdateMessageCriteria updateMessageCriteria)
        {
            updateMessageCriteria.UserLoginName = this.CurrentSession.Username;
            updateMessageCriteria.UserLoginId = this.CurrentSession.UserId;

            return Json(this._messageService.UpdateMessage(updateMessageCriteria));
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="deleteMessageCriteria">The delete message criteria.</param>
        /// <returns>Success message.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewMessageList, Policy = Constant.Policy.Host)]
        public ActionResult DeleteMessage(DeleteMessageCriteria deleteMessageCriteria)
        {
            deleteMessageCriteria.UserLoginId = this.CurrentSession.UserId;

            return Json(this._messageService.DeleteMessage(deleteMessageCriteria));
        }
    }
}