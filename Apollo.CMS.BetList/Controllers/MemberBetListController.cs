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
    using Apollo.CMS.BetList.Services.MemberBetList.Implements;
    using Apollo.CMS.BetList.Services.MemberBetList.Interfaces;
    using Apollo.CMS.Core.Controllers;

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
        }
    }
}