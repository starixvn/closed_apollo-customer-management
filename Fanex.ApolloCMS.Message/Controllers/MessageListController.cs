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
    using Fanex.ApolloCMS.Message.Criteria.MessageList;
    using Fanex.ApolloCMS.Message.Models.MessageList;
    using Fanex.ApolloCMS.Message.Services.Implements;
    using Fanex.ApolloCMS.Message.Services.Interfaces;

    /// <summary>
    /// TODO: Update summary.
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
        /// <returns>Index view.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the message list.
        /// </summary>
        /// <param name="getMessageListCriteria">The get message list criteria.</param>
        /// <returns>List Message.</returns>
        [HttpPost]
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
        public ActionResult DeleteMessage(DeleteMessageCriteria deleteMessageCriteria)
        {
            deleteMessageCriteria.UserLoginId = this.CurrentSession.UserId;
            this._messageService.DeleteMessage(deleteMessageCriteria);
            return Json(new { success = true });
        }
    }
}
