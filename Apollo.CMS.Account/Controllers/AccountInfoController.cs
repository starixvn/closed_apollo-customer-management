// -----------------------------------------------------------------------
// <copyright file="AccountInfoController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.04">Created</change>
// </history>

namespace Apollo.CMS.Account.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Apollo.CMS.Account.Criteria.AccountInfo;
    using Apollo.CMS.Account.Models.AccountInfo;
    using Apollo.CMS.Account.Services.AccountInfo.Implements;
    using Apollo.CMS.Account.Services.AccountInfo.Interfaces;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Resources;
    using Fanex.Common;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Common controller.
    /// </summary>
    public class AccountInfoController : BaseController
    {
        private readonly IAccountInfoService _accountInfoService;

        public AccountInfoController() :
            this(null)
        {
        }

        public AccountInfoController(IAccountInfoService accountInfoService)
        {
            this._accountInfoService = accountInfoService ?? new AccountInfoService();
            ViewBag.HasCustomerSettingPermission = this.HasRight(ApolloCMSFunctionKeys.CustomerSetting, Constant.Policy.Read);
        }

        /// <summary>
        /// Get Left Pan Partial view.
        /// </summary>
        /// <param name="isMember">Whether the passed ID is of a member or not.</param>
        /// <param name="id">The ID of the customer.</param>
        /// <param name="username">The username of the customer.</param>
        /// <returns>Partial menu view.</returns>
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public PartialViewResult LeftPanel(bool isMember = false, int? id = null, string username = null)
        {
            string partialViewName = "AgentLeftPanel";

            if ((id == null) && (username == null))
            {
                id = Converter.ToInt(Session["customerId"]);
                isMember = Converter.ToBool(Session["isMember"]);
            }

            if ((id != null && id != 0) || !string.IsNullOrEmpty(username))
            {
                if (isMember)
                {
                    var customer = this._accountInfoService.GetMemberInfo(new GetMemberInfoCriteria
                    {
                        CustId = id,
                        Username = username
                    });

                    customer = customer == null ? new MemberInfo() : customer;
                    id = customer.CustId;
                    username = customer.Username;
                    ViewBag.Customer = customer;
                    partialViewName = "MemberLeftPanel";
                }
                else
                {
                    var customer = this._accountInfoService.GetAgentInfo(new GetAgentInfoCriteria
                    {
                        AgentId = id,
                        Username = username
                    });

                    customer = customer == null ? new AgentInfo() : customer;
                    id = customer.AgentId;
                    username = customer.Username;
                    ViewBag.Customer = customer;
                }
            }

            Session.Add("customerId", id);
            Session.Add("username", username);
            Session.Add("isMember", isMember);

            return PartialView(partialViewName);
        }

        /// <summary>
        /// Get Search Partial view.
        /// </summary>
        /// <param name="isMember">Whether the passed ID is of a member or not.</param>
        /// <param name="username">The username of the customer.</param>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult Search(bool isMember = false, string username = null)
        {
            var memberTypes = new List<SelectListItem>();

            if (username == null)
            {
                username = Converter.ToString2(Session["username"]);
                isMember = Converter.ToBool(Session["isMember"]);
            }

            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Agent,
                Value = "false"
            });
            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Member,
                Value = "true",
                Selected = isMember
            });

            ViewBag.MemberTypes = memberTypes;
            ViewBag.CustomerUsername = username;

            return PartialView();
        }

        /// <summary>
        /// Gets suggested customer list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object represents the list of suggested customers.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public JsonResult GetSuggestedCustomer(GetSuggestedCustomersByUsernameCriteria criteria)
        {
            return Json(this._accountInfoService.GetSuggestedCustomers(criteria));
        }

        /// <summary>
        /// Gets up line agent list.
        /// </summary>
        /// <param name="agentId">The ID of the agent.</param>
        /// <returns>Partial view represents the up line popup.</returns>
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public PartialViewResult UplineAgents(int? agentId)
        {
            ViewBag.UplineList = this._accountInfoService.GetUplineAgents(new GetAgentUplineCriteria
            {
                AgentId = Converter.ToInt(agentId)
            });

            return PartialView("Upline");
        }

        /// <summary>
        /// Gets down line agent list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>Partial view represents the list of down line agent.</returns>
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public PartialViewResult GetDownlineAgents(GetDownlineAgentsCriteria criteria)
        {
            ViewBag.Agents = this._accountInfoService.GetDownlineAgents(criteria);
            return PartialView("AgentList");
        }

        /// <summary>
        /// Gets down line member list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>Partial view represents the list of down line member.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.SearchAccount, Policy = Constant.Policy.Read)]
        public PartialViewResult GetDownlineMembers(GetDownlineMembersCriteria criteria)
        {
            ViewBag.Members = this._accountInfoService.GetDownlineMembers(criteria);
            return PartialView("MemberList");
        }
    }
}