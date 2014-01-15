// -----------------------------------------------------------------------
// <copyright file="IMemberBetListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.ViewModels.MemberBetList;

    public interface IMemberBetListService
    {
        /// <summary>
        /// Gets the paged bet list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The bet items of a page.</returns>
        BetList GetPagedBetList(MemberBetListFilter criteria);

        /// <summary>
        /// Gets the detailed win loss of the whole line.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The win loss list.</returns>
        IEnumerable<DetailedWinLossItem> GetDetailWinLossList(DetailedWinLossFilter criteria);

        /// <summary>
        /// Gets the parlay sub tickets.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The HTML formatted string that represents the sub-tickets.</returns>
        string GetParlaySubTickets(GetParlaySubTicketsCriteria criteria);

        /// <summary>
        /// Gets the system parlay details.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The HTML formatted string that represents the system parlay details.</returns>
        string GetSystemParlayDetails(GetSystemParlayDetailsCriteria criteria);
    }
}