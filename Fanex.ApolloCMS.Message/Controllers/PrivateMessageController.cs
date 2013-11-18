// -----------------------------------------------------------------------
// <copyright file="PrivateMessageController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.12">Created</change>
//   <change who="Jason.Ly" date="2013.11.14">Implement</change>
// </history>
namespace Fanex.ApolloCMS.Message.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.ApolloCMS.Core.Configurations;
    using Fanex.ApolloCMS.Core.Controllers;
    using Fanex.ApolloCMS.Core.Resources;
    using Fanex.ApolloCMS.Message.Constants;
    using Fanex.ApolloCMS.Message.Criteria.NewMessage;
    using Fanex.ApolloCMS.Message.Services.Implements;
    using Fanex.ApolloCMS.Message.Services.Interfaces;

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
            this._newMessageService = newMessageService ?? new NewMessageService { ServiceContext = this.ServiceContext };
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <returns>Index view.</returns>
        public ActionResult Index()
        {
            ViewBag.MemberTypes = CreateMemberTypes();
            ViewBag.MaxRowsLimit = AppSettings.NewPrivateMessageMaxRowsLimit;
            return View();
        }

        /// <summary>
        /// Gets the recipient information.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object contains recipient information.</returns>
        public JsonResult GetRecipientInfo(GetRecipientInfoCriteria criteria)
        {
            return Json(this._newMessageService.GetRecipientInfo(criteria));
        }

        /// <summary>
        /// Adds the new public message.
        /// </summary>
        /// <param name="criteria">The criteria that contains the information of the new message.</param>
        /// <returns>Whether or not the new public message is added successfully or not.</returns>
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
