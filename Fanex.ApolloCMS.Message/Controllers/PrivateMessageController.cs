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
    using Fanex.ApolloCMS.Core.Controllers;
    using Fanex.ApolloCMS.Core.Resources;
    using Fanex.ApolloCMS.Message.Services.Implements;
    using Fanex.ApolloCMS.Message.Services.Interfaces;

    /// <summary>
    /// New private message controller.
    /// </summary>
    public class PrivateMessageController : BaseController
    {
        private readonly INewMessageService _newMessageService;
        private readonly int _maxRowsLimit = 20;

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
            ViewBag.MaxRowsLimit = this._maxRowsLimit;
            return View();
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
