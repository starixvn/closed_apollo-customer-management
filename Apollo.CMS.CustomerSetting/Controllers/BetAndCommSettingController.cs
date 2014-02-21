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
    }
}