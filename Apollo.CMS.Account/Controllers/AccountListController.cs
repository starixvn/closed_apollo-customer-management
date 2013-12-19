// -----------------------------------------------------------------------
// <copyright file="AccountListController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.18">Created</change>
// </history>

namespace Apollo.CMS.Account.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Apollo.CMS.Account.Services.AccountList.Implements;
    using Apollo.CMS.Account.Services.AccountList.Interfaces;
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Resources;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Account list controller.
    /// </summary>
    public class AccountListController : BaseController
    {
        private readonly IAccountListService _accountListService;

        public AccountListController() :
            this(null)
        {
        }

        public AccountListController(IAccountListService accountListService)
        {
            this._accountListService = accountListService ?? new AccountListService();
        }

        /// <summary>
        /// Redirects agent list page.
        /// </summary>
        /// <param name="agentId">The ID of searched agent.</param>
        /// <param name="agentLevel">The level of searched agent.</param>
        /// <returns>Agent list view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public ActionResult AgentList(int agentId = 0, int agentLevel = 0)
        {
            ViewBag.AgentId = agentId;
            ViewBag.AccountStatuses = GetAccountStatusList();
            ViewBag.BetStatuses = GetBetStatusList();

            return View();
        }

        /// <summary>
        /// Redirects member list page.
        /// </summary>
        /// <param name="agentId">The ID of searched agent.</param>
        /// <returns>Member list view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public ActionResult MemberList(int agentId = 0)
        {
            ViewBag.AgentId = agentId;
            ViewBag.AccountStatuses = GetAccountStatusList();
            ViewBag.BetStatuses = GetBetStatusList();

            return View();
        }

        /// <summary>
        /// Redirects shadow list page.
        /// </summary>
        /// <param name="agentId">The ID of searched agent.</param>
        /// <returns>Shadow list view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public ActionResult ShadowList(int agentId = 0)
        {
            ViewBag.AgentId = agentId;

            return View();
        }

        /// <summary>
        /// Creates breadcrumb.
        /// </summary>
        /// <param name="agentId">The ID of searched agent.</param>
        /// <returns>Breadcrumb partial view.</returns>
        [ChildActionOnly]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public PartialViewResult Breadcrumb(int agentId = 0)
        {
            ViewBag.AgentId = agentId;

            return PartialView();
        }

        /// <summary>
        /// Gets the bet status list.
        /// </summary>
        /// <returns>List of SelectListItem objects represents the bet statuses.</returns>
        private IEnumerable<SelectListItem> GetBetStatusList()
        {
            var statusList = new List<SelectListItem>();

            statusList.Add(new SelectListItem
            {
                Text = ViewLang.All,
                Value = "null"
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Active,
                Value = BetStatus.Active.ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Stopped,
                Value = BetStatus.Stopped.ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.CLock,
                Value = BetStatus.CLock.ToString()
            });

            return statusList;
        }

        /// <summary>
        /// Gets the account status list.
        /// </summary>
        /// <returns>List of SelectListItem objects represents the account statuses.</returns>
        private IEnumerable<SelectListItem> GetAccountStatusList()
        {
            var statusList = new List<SelectListItem>();

            statusList.Add(new SelectListItem
            {
                Text = ViewLang.All,
                Value = "null"
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Active,
                Value = AccountStatus.Active.ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Stopped,
                Value = AccountStatus.Stopped.ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Locked,
                Value = AccountStatus.Locked.ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.CLock,
                Value = AccountStatus.CLock.ToString()
            });

            return statusList;
        }
    }
}