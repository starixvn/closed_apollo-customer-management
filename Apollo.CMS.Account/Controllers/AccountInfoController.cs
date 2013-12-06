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
    using Fanex.NAP.Core.Business;

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
        public PartialViewResult LeftPanel(bool isMember = false, int? id = null, string username = null)
        {
            if (isMember)
            {
                var customer = this._accountInfoService.GetMemberInfo(new GetMemberInfoCriteria
                {
                    CustId = id,
                    Username = username
                });

                ViewBag.Customer = customer == null ? new MemberInfo() : customer;

                return PartialView("MemberLeftPanel");
            }
            else
            {
                var customer = this._accountInfoService.GetAgentInfo(new GetAgentInfoCriteria
                {
                    AgentId = id,
                    Username = username
                });

                ViewBag.Customer = customer == null ? new AgentInfo() : customer;

                return PartialView("AgentLeftPanel");
            }
        }

        /// <summary>
        /// Get Search Partial view.
        /// </summary>
        /// <returns>Partial menu view.</returns>
        [ChildActionOnly]
        public PartialViewResult Search()
        {
            var memberTypes = new List<SelectListItem>();

            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Agent,
                Value = "false"
            });
            memberTypes.Add(new SelectListItem
            {
                Text = ViewLang.Member,
                Value = "true"
            });

            ViewBag.MemberTypes = memberTypes;

            return PartialView();
        }

        /// <summary>
        /// Gets suggested customer list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>JSON object represents the list of suggested customers.</returns>
        [HttpPost]
        public JsonResult GetSuggestedCustomer(GetSuggestedCustomersByUsernameCriteria criteria)
        {
            return Json(this._accountInfoService.GetSuggestedCustomers(criteria));
        }
    }
}