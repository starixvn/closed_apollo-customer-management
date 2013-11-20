// -----------------------------------------------------------------------
// <copyright file="MessageListController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Fanex.ApolloCMS.Core.Controllers;
    using Fanex.ApolloCMS.Core.Web;
    using Fanex.ApolloCMS.Message.Criteria.MessageList;
    using Fanex.ApolloCMS.Message.Models.MessageList;
    using Fanex.ApolloCMS.Message.Services.Implements;
    using Fanex.ApolloCMS.Message.Services.Interfaces;
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
            var result = this._messageService.GetListMessage(getMessageListCriteria);
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
            var result = this._messageService.GetMessageById(getMessageByIdCriteria);
            return PartialView("EditMessage", result);
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
            this._messageService.UpdateMessage(updateMessageCriteria);
            return Json(new { success = true });
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
            this._messageService.DeleteMessage(deleteMessageCriteria);
            return Json(new { success = true });
        }
    }
}
