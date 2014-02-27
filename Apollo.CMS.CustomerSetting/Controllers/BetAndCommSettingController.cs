// --------------------------------------------------------------------------
// <copyright file="BetAndCommSettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.CustomerSetting.Services.Implements;
    using Apollo.CMS.CustomerSetting.Services.Interfaces;
    using Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Bet and commission setting controller.
    /// </summary>
    public class BetAndCommSettingController : BaseController
    {
        private readonly IBetAndCommSettingService _betAndCommSettingService;

        public BetAndCommSettingController() :
            this(null)
        {
        }

        public BetAndCommSettingController(IBetAndCommSettingService betAndCommSettingService)
        {
            this._betAndCommSettingService = betAndCommSettingService ?? new BetAndCommSettingService();
            ViewBag.HasEditSettingPermission = this.HasRight(ApolloCMSFunctionKeys.CustomerSetting, Constant.Policy.Host);
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <param name="filter">Filter passed from user.</param>
        /// <returns>Index view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.CustomerSetting, Policy = Constant.Policy.Read)]
        public ActionResult Index(BetAndCommSettingFilter filter)
        {
            return View(filter);
        }

        /// <summary>
        /// Redirect Commission Setting partial view.
        /// </summary>
        /// <param name="filter">Filter passed from user.</param>
        /// <returns>Partial view represents Commission Setting session.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.CustomerSetting, Policy = Constant.Policy.Read)]
        public PartialViewResult CommSetting(BetAndCommSettingFilter filter)
        {
            if (filter.IsMember)
            {
                MemberDiscountItem memberComm = this._betAndCommSettingService.GetMemberDiscount(filter);

                return PartialView("MemberCommSetting", memberComm);
            }
            else
            {
                AgentDiscountItem agentComm = this._betAndCommSettingService.GetAgentDiscount(filter);

                return PartialView("AgentCommSetting", agentComm);
            }
        }

        /// <summary>
        /// Redirect Commission Setting partial view.
        /// </summary>
        /// <param name="filter">Filter passed from user.</param>
        /// <returns>Partial view represents Bet Limit Setting session.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.CustomerSetting, Policy = Constant.Policy.Read)]
        public PartialViewResult BetLimitSetting(BetAndCommSettingFilter filter)
        {
            IEnumerable<BetLimitItem> betLimitList = this._betAndCommSettingService.GetBetLimits(filter);

            ViewBag.BetLimits = betLimitList;

            return PartialView(filter);
        }

        /// <summary>
        /// Redirect Commission Setting partial view.
        /// </summary>
        /// <param name="filter">Filter passed from user.</param>
        /// <returns>Partial view represents Bet Limit Setting session.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.CustomerSetting, Policy = Constant.Policy.Host)]
        public JsonResult UpdateCommAndBetLimit(UpdateBetAndCommFilter filter)
        {
            filter.AdminId = this.CurrentSession.UserId;

            return Json(this._betAndCommSettingService.UpdateBetAndCommSetting(filter));
        }
    }
}