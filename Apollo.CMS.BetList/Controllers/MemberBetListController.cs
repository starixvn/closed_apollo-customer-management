// -----------------------------------------------------------------------
// <copyright file="MemberBetListController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Controllers
{
    using System.Web.Mvc;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Services.Implements;
    using Apollo.CMS.BetList.Services.Interfaces;
    using Apollo.CMS.Core.Configurations;
    using Apollo.CMS.Core.Controllers;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Web.Attribute;

    /// <summary>
    /// Member's bet list controller.
    /// </summary>
    public class MemberBetListController : BaseController
    {
        private readonly IMemberBetListService _memberBetListService;

        public MemberBetListController() :
            this(null)
        {
        }

        public MemberBetListController(IMemberBetListService memberBetListService)
        {
            this._memberBetListService = memberBetListService ?? new MemberBetListService();
            ViewBag.PageSize = AppSettings.DefaultPageSize;
            ViewBag.StartDate = AppSettings.StartDate.ToShortDateString();
        }

        /// <summary>
        /// Redirect index page.
        /// </summary>
        /// <param name="custId">Customer ID.</param>
        /// <param name="username">Customer's username.</param>
        /// <returns>Index view.</returns>
        [HttpGet]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewBetList, Policy = Constant.Policy.Read)]
        public ActionResult Index(int custId, string username)
        {
            ViewBag.CustId = custId;
            ViewBag.Username = username;

            return View("~/Areas/Account/Views/MemberBetList/Index.cshtml");
        }

        /// <summary>
        /// Gets the member bet list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The JSON list contains bet list of member.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewBetList, Policy = Constant.Policy.Read)]
        public JsonResult GetMemberBetList(MemberBetListFilter criteria)
        {
            return Json(this._memberBetListService.GetPagedBetList(criteria));
        }

        /// <summary>
        /// Gets the detailed win loss of a whole line.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The JSON list contains win loss list of a ticket.</returns>
        [HttpPost]
        [NapAuth(FunctionKey = ApolloCMSFunctionKeys.ViewBetList, Policy = Constant.Policy.Read)]
        public JsonResult GetTicketDetail(DetailedWinLossFilter criteria)
        {
            return Json(this._memberBetListService.GetDetailWinLossList(criteria));
        }
    }
}