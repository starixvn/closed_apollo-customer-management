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
    using Apollo.CMS.Account.Criteria.AccountList;
    using Apollo.CMS.Account.Models.AccountList;
    using Apollo.CMS.Account.Services.AccountList.Implements;
    using Apollo.CMS.Account.Services.AccountList.Interfaces;
    using Apollo.CMS.Core.Configurations;
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
        private readonly int[] _pageRanges = new int[5] { 10, 50, 100, 200, 500 };

        public AccountListController() :
            this(null)
        {
        }

        public AccountListController(IAccountListService accountListService)
        {
            this._accountListService = accountListService ?? new AccountListService();
            ViewBag.PageSize = AppSettings.DefaultPageSize;
            ViewBag.PageRanges = this._pageRanges;
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
            ViewBag.AgentLevel = agentLevel;
            ViewBag.AccountStatuses = GetAccountStatusList();
            ViewBag.BetStatuses = GetBetStatusList();

            return View();
        }

        /// <summary>
        /// Gets direct down line agent list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct agents.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public JsonResult GetAgentList(GetAgentListCriteria criteria)
        {
            return Json(new
            {
                AgentList = this._accountListService.GetAgentList(criteria),
                TotalItems = criteria.TotalItems
            });
        }

        /// <summary>
        /// Redirects member list page.
        /// </summary>
        /// <param name="agentId">The ID of searched agent.</param>
        /// <param name="agentLevel">The level of searched agent.</param>
        /// <returns>Member list view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public ActionResult MemberList(int agentId = 0, int agentLevel = 0)
        {
            ViewBag.AgentId = agentId;
            ViewBag.AgentLevel = agentLevel;
            ViewBag.AccountStatuses = GetAccountStatusList();
            ViewBag.BetStatuses = GetBetStatusList();

            return View();
        }

        /// <summary>
        /// Gets direct down line member list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct members.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public JsonResult GetMemberList(GetMemberListCriteria criteria)
        {
            return Json(new
            {
                MemberList = this._accountListService.GetMemberList(criteria),
                TotalItems = criteria.TotalItems
            });
        }

        /// <summary>
        /// Redirects shadow list page.
        /// </summary>
        /// <param name="agentId">The ID of searched agent.</param>
        /// <param name="agentLevel">The level of searched agent.</param>
        /// <returns>Shadow list view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public ActionResult ShadowList(int agentId = 0, int agentLevel = 0)
        {
            ViewBag.AgentId = agentId;
            ViewBag.AgentLevel = agentLevel;

            return View();
        }

        /// <summary>
        /// Gets shadow list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of shadows.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public JsonResult GetShadowList(GetShadowListCriteria criteria)
        {
            return Json(new
            {
                ShadowList = this._accountListService.GetShadowList(criteria),
                TotalItems = criteria.TotalItems
            });
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
            IEnumerable<NavigationPath> navigationPath = this._accountListService.GetNavigationPaths(new GetNavigationPathCriteria
            {
                AgentId = agentId
            });

            return PartialView(navigationPath);
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
                Value = ((int)BetStatus.All).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Active,
                Value = ((int)BetStatus.Active).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Stopped,
                Value = ((int)BetStatus.Stopped).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.CLock,
                Value = ((int)BetStatus.CLock).ToString()
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
                Value = ((int)AccountStatus.All).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Active,
                Value = ((int)AccountStatus.Active).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Stopped,
                Value = ((int)AccountStatus.Stopped).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.Locked,
                Value = ((int)AccountStatus.Locked).ToString()
            });
            statusList.Add(new SelectListItem
            {
                Text = ViewLang.CLock,
                Value = ((int)AccountStatus.CLock).ToString()
            });

            return statusList;
        }
    }
}