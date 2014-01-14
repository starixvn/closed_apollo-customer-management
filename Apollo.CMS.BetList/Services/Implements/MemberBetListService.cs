// -----------------------------------------------------------------------
// <copyright file="MemberBetListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Helpers;
    using Apollo.CMS.BetList.Models.MemberBetList;
    using Apollo.CMS.BetList.Services.Interfaces;
    using Apollo.CMS.BetList.ViewModels.MemberBetList;
    using Apollo.CMS.Core.Models;
    using Apollo.CMS.Core.Services;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;

    /// <summary>
    /// Member's bet list service.
    /// </summary>
    public class MemberBetListService : BaseService, IMemberBetListService
    {
        private static List<BetTicket> _ticketList;

        /// <summary>
        /// Gets the paged bet list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The bet items of a page.</returns>
        public BetList GetPagedBetList(GetMemberBetListCriteria criteria)
        {
            List<BetTicket> pageData = null;
            BetList result = null;

            if (criteria.IsLoadNew || _ticketList == null)
            {
                GetBetTicket(criteria);
            }

            if (_ticketList.Any())
            {
                pageData = _ticketList.GetRange(criteria.PageIndex * criteria.PageSize, criteria.PageSize);
                result = GetPagedBetList(pageData);
            }

            return result;
        }

        /// <summary>
        /// Converts raw bet list data from database to appropriate format.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns>Formatted bet list.</returns>
        private BetList GetPagedBetList(List<BetTicket> rawData)
        {
            var result = new BetList();

            if (rawData != null && rawData.Any())
            {
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                ITicketHelper ticketHelper = new TicketHelper { IsShowOddsWithChoice = true };
                IEnumerable<ITicket> tickets = BetListHelper.Map(rawData);

                foreach (ITicket ticket in tickets)
                {
                    result.Add(new BetItem
                    {
                        RefNo = ticket.RefNo,
                        TransDate = ticket.TransDate,
                        Stake = ticket.Stake,
                        WinLoss = ticket.Winlost,
                        Comm = ticket.Comm,
                        CompanyWinLoss = ticket.SuperWinlost,
                        CompanyShare = ticket.SuperPositionTaking,
                        TransTimeCol = builder.BuildTrans(ticket),
                        ChoiceCol = builder.BuildChoice(ticket, ticketHelper, null, false),
                        OddsCol = builder.BuildOdds(ticket, null, BetListHelper.GetOddTypeName),
                        StatusCol = builder.BuildStatus(ticket, ticketHelper, null, true)
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the bet ticket list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        private void GetBetTicket(GetMemberBetListCriteria criteria)
        {
            BalanceUp balanceUp = this.BalanceUp;
            _ticketList = new List<BetTicket>();

            if (balanceUp.BalanceUpDay < criteria.FromDate)
            {
                var newBetListCriteria = new GetNewMemberBetListCriteria
                {
                    FromDate = criteria.FromDate,
                    ToDate = criteria.ToDate,
                    MoveBetsDay = balanceUp.MoveBetsDay,
                    CustId = criteria.CustId
                };

                _ticketList.AddRange(this.GetListData<BetTicket>(newBetListCriteria));
            }
            else if (criteria.ToDate < balanceUp.BalanceUpDay)
            {
                var oldBetListCriteria = new GetOldMemberBetListCriteria
                {
                    FromDate = criteria.FromDate,
                    ToDate = criteria.ToDate,
                    CustId = criteria.CustId
                };

                _ticketList.AddRange(this.GetListData<BetTicket>(oldBetListCriteria));
            }
            else
            {
                var newBetListCriteria = new GetNewMemberBetListCriteria
                {
                    FromDate = balanceUp.BalanceUpDay,
                    ToDate = criteria.ToDate,
                    MoveBetsDay = balanceUp.MoveBetsDay,
                    CustId = criteria.CustId
                };
                var oldBetListCriteria = new GetOldMemberBetListCriteria
                {
                    FromDate = criteria.FromDate,
                    ToDate = balanceUp.BalanceUpDay.AddDays(-1),
                    CustId = criteria.CustId
                };

                _ticketList.AddRange(this.GetListData<BetTicket>(newBetListCriteria));
                _ticketList.AddRange(this.GetListData<BetTicket>(newBetListCriteria));
                _ticketList = _ticketList.OrderBy(obj => obj.TransId).ToList();
            }
        }
    }
}