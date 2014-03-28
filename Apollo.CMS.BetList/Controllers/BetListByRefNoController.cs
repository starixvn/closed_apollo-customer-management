// -----------------------------------------------------------------------
// <copyright file="BetListByRefNoController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.21">Created</change>
// </history>

namespace Apollo.CMS.BetList.Controllers
{
    using System.Web.Mvc;
    using Apollo.CMS.BetList.Criteria.BetListByRefNo;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Services.Implements;
    using Apollo.CMS.BetList.Services.Interfaces;
    using Apollo.CMS.BetList.ViewModels.MemberBetList;
    using Apollo.CMS.Core.Controllers;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Member's bet list controller.
    /// </summary>
    public class BetListByRefNoController : BaseController
    {
        private readonly IBetListByRefNoService _betListByRefNoService;
        private readonly IMemberBetListService _memberBetListService;

        public BetListByRefNoController() :
            this(null, null)
        {
        }

        public BetListByRefNoController(IBetListByRefNoService betListByRefNoService, IMemberBetListService memberBetListService)
        {
            this._betListByRefNoService = betListByRefNoService ?? new BetListByRefNoService();
            this._memberBetListService = memberBetListService ?? new MemberBetListService();
            ViewBag.HasSearchAccountPermission = this.HasRight(ApolloCMSFunctionKeys.SearchAccount, Constant.Policy.Read);
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <returns>Index view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.BetListByRef, Policy = Constant.Policy.Read)]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the member bet list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The JSON list contains bet list of member.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.BetListByRef, Policy = Constant.Policy.Read)]
        public JsonResult GetBetListByRefNo(GetBetListByRefNoCriteria criteria)
        {
            return Json(this._betListByRefNoService.GetBetListByRefNo(criteria));
        }

        /// <summary>
        /// Gets the detailed win loss of a whole line.
        /// </summary>
        /// <param name="filter">The criteria that are used to query data.</param>
        /// <returns>The JSON list contains win loss list of a ticket.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.BetListByRef, Policy = Constant.Policy.Read)]
        public JsonResult GetTicketDetail(DetailedWinLossFilter filter)
        {
            return Json(this._memberBetListService.GetDetailWinLossList(filter));
        }

        /// <summary>
        /// Gets system parlay details.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The JSON list contains system parlay details.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.BetListByRef, Policy = Constant.Policy.Read)]
        public JsonResult GetSystemParlayDetails(GetSystemParlayDetailsCriteria criteria)
        {
            return Json(this._memberBetListService.GetSystemParlayDetails(criteria));
        }
    }
}