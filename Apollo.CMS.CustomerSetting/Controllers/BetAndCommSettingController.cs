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
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <param name="filter">Filter passed from user.</param>
        /// <returns>Index view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.EditSetting, Policy = Constant.Policy.Read)]
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
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.EditSetting, Policy = Constant.Policy.Read)]
        public PartialViewResult CommSetting(BetAndCommSettingFilter filter)
        {
            if (filter.IsMember)
            {
                MemberCommissionItem memberComm = CreateMemberComm();

                return PartialView("MemberCommSetting", memberComm);
            }
            else
            {
                AgentCommissionItem agentComm = CreateAgentComm();

                return PartialView("AgentCommSetting", agentComm);
            }
        }

        /// <summary>
        /// Redirect Commission Setting partial view.
        /// </summary>
        /// <param name="filter">Filter passed from user.</param>
        /// <returns>Partial view represents Bet Limit Setting session.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.EditSetting, Policy = Constant.Policy.Read)]
        public PartialViewResult BetLimitSetting(BetAndCommSettingFilter filter)
        {
            List<SportBetLimitItem> betLimitList = CreateBetLimitList();

            ViewBag.BetLimits = betLimitList;

            return PartialView(filter);
        }

        /// <summary>
        /// Creates a sample object of AgentCommissionItem for testing purpose.
        /// </summary>
        /// <returns>AgentCommissionItem object.</returns>
        private AgentCommissionItem CreateAgentComm()
        {
            var upline = new AgentCommissionItem
            {
                DiscountA = 2.25M,
                DiscountB = 1.25M,
                DiscountC = 0.75M,
                DiscountD = 0,
                _1x2Comm = 0,
                OtherComm = 0
            };
            var agent = new AgentCommissionItem
            {
                DiscountA = 2.25M,
                DiscountB = 1.25M,
                DiscountC = 0.75M,
                DiscountD = 0,
                _1x2Comm = 0,
                OtherComm = 0,
                Upline = upline
            };

            return agent;
        }

        /// <summary>
        /// Creates a sample object of MemberCommissionItem for testing purpose.
        /// </summary>
        /// <returns>MemberCommissionItem object.</returns>
        private MemberCommissionItem CreateMemberComm()
        {
            var upline = new AgentCommissionItem
            {
                DiscountA = 2.25M,
                DiscountB = 1.25M,
                DiscountC = 0.75M,
                DiscountD = 0,
                _1x2Comm = 0,
                OtherComm = 0
            };
            var member = new MemberCommissionItem
            {
                HandicapComm = 0.75M,
                _1x2Comm = 0,
                OtherComm = 0,
                Upline = upline
            };

            return member;
        }

        /// <summary>
        /// Creates the bet limit list.
        /// </summary>
        /// <returns>List of SportBetLimitItem.</returns>
        private List<SportBetLimitItem> CreateBetLimitList()
        {
            var list = new List<SportBetLimitItem>();
            var sportNames = new string[] 
            { 
                "Soccer", "Basketball", "Football", "Tennis", "Baseball", "Golf",
                "Motor Sports", "Other Sports", "Mix Sports Parlay"
            };

            foreach (string sportName in sportNames)
            {
                var betLimit = new SportBetLimitItem
                {
                    SingleGame = 1000M,
                    MinOfSingleGame = 200M,
                    MaxOfSingleGame = 3000M,
                    SingleBet = 1000M,
                    MinOfSingleBet = 200M,
                    MaxOfSingleBet = 3000M,
                    MinSingleBet = 50M,
                    MinOfMinSingleBet = 50M
                };

                betLimit.SportName = sportName;
                list.Add(betLimit);
            }

            return list;
        }
    }
}