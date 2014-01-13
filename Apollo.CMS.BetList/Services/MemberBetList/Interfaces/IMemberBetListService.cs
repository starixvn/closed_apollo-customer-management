// -----------------------------------------------------------------------
// <copyright file="IMemberBetListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.MemberBetList.Interfaces
{
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.ViewModels.MemberBetList;

    public interface IMemberBetListService
    {
        /// <summary>
        /// Gets the paged bet list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The bet items of a page.</returns>
        BetList GetPagedBetList(GetMemberBetListCriteria criteria);
    }
}