// -----------------------------------------------------------------------
// <copyright file="MemberBetListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.MemberBetList.Implements
{
    using System.Collections.Generic;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Models.MemberBetList;
    using Apollo.CMS.BetList.Services.MemberBetList.Interfaces;
    using Apollo.CMS.Core.Models;
    using Apollo.CMS.Core.Services;

    /// <summary>
    /// Member's bet list service.
    /// </summary>
    public class MemberBetListService : BaseService, IMemberBetListService
    {
        private static List<BetTicket> _ticketList;

        /// <summary>
        /// Gets the bet ticket.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        private void GetBetTicket(GetBetListCriteria criteria)
        {
            BalanceUp balanceUp = this.BalanceUp;
            _ticketList = new List<BetTicket>();
            IEnumerable<BetTicket> temTicketList = null;

            if (balanceUp.BalanceUpDay < criteria.FromDate)
            {
                temTicketList = this.GetListData<BetTicket>(criteria);
                _ticketList.AddRange(temTicketList);
            }
        }
    }
}