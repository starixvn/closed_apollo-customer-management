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
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.Models;
    using Apollo.CMS.Core.Services;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Common;

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
        public BetList GetPagedBetList(MemberBetListFilter criteria)
        {
            BetList result = null;

            if (criteria.IsLoadNew || _ticketList == null)
            {
                GetBetTicket(criteria);
            }

            if (_ticketList.Any())
            {
                result = SeparateBetListToPage(criteria);
            }

            return result;
        }

        /// <summary>
        /// Gets the detailed win loss of the whole line.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The win loss list.</returns>
        public IEnumerable<DetailedWinLossItem> GetDetailWinLossList(DetailedWinLossFilter criteria)
        {
            BalanceUp balanceUp = this.BalanceUp;
            var detailedWinLossCriteria = new GetDetailedWinLossCriteria
            {
                TransId = criteria.TransId,
                IsBackupData = balanceUp.BalanceUpDay > criteria.TransDate
            };
            List<DetailedWinLossItem> result = null;
            IEnumerable<DetailedWinLoss> rawData = this.GetListData<DetailedWinLoss>(detailedWinLossCriteria);

            if ((rawData != null) && rawData.Any())
            {
                result = new List<DetailedWinLossItem>();

                foreach (DetailedWinLoss record in rawData)
                {
                    result.Add(new DetailedWinLossItem
                    {
                        Username = record.Username,
                        PositionTaking = record.PositionTaking,
                        WinLoss = record.WinLoss,
                        Discount = record.WinLoss,
                        Comm = record.Comm,
                        IsSuper = Converter.ToInt(record.AgentLevel) == (int)HighLevelAgents.Super
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the parlay sub tickets.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The HTML formatted string that represents the sub-tickets.</returns>
        public string GetParlaySubTickets(GetParlaySubTicketsCriteria criteria)
        {
            string result = string.Empty;
            IEnumerable<ParlaySubTicket> rawData = this.GetListData<ParlaySubTicket>(criteria);

            if ((rawData != null) && rawData.Any())
            {
                List<ITicket> tickets = BetListHelper.Map(rawData);
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                ITicketHelper ticketHelper = new TicketHelper { IsShowOddsWithChoice = true };
                string line = "<div class='line'></div>";

                for (int i = 0; i < tickets.Count; i++)
                {
                    if (i == tickets.Count - 1)
                    {
                        line = "<div></div>";
                    }

                    builder.AddChoice(tickets[i], ticketHelper, null, string.Empty, line, false);
                }

                result = builder.GetBetList();
            }

            return result;
        }

        /// <summary>
        /// Gets the system parlay details.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The HTML formatted string that represents the system parlay details.</returns>
        public string GetSystemParlayDetails(GetSystemParlayDetailsCriteria criteria)
        {
            string result = string.Empty;

            return result;
        }

        /// <summary>
        /// Separate the bet list into pages.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>Formatted bet list.</returns>
        private BetList SeparateBetListToPage(MemberBetListFilter criteria)
        {
            var result = new BetList();
            List<BetTicket> pagedData = null;
            int startIndex = criteria.PageIndex * criteria.PageSize;
            int endIndex = startIndex + criteria.PageSize;

            endIndex = endIndex > _ticketList.Count ? _ticketList.Count : endIndex;
            pagedData = _ticketList.GetRange(startIndex, endIndex - startIndex);
            result.TotalItems = _ticketList.Count;

            if (pagedData != null && pagedData.Any())
            {
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                ITicketHelper ticketHelper = new TicketHelper();
                IEnumerable<ITicket> tickets = BetListHelper.Map(pagedData);
                string ticketStatus = string.Empty;

                foreach (ITicket ticket in tickets)
                {
                    ticketStatus = ticket.Status.ToLower();
                    result.Add(new BetItem
                    {
                        TransId = ticket.TransId,
                        TransDate = ticket.TransDate,
                        Stake = ticket.Stake,
                        WinLoss = ticket.Winlost,
                        Comm = ticket.Comm,
                        CompanyWinLoss = ticket.SuperWinlost,
                        CompanyShare = ticket.SuperPositionTaking,
                        TransTimeCol = builder.BuildTrans(ticket),
                        ChoiceCol = builder.BuildChoice(ticket, ticketHelper, null, false),
                        OddsCol = builder.BuildOdds(ticket, null, BetListHelper.GetOddTypeName),
                        StatusCol = builder.BuildStatus(ticket, ticketHelper, null, true),
                        IsHandicap = (ticket.BetTypeId == (int)BetTypes.Handicap) || (ticket.BetTypeId == (int)BetTypes._3_Way_Handicap),
                        IsRunning = BetStatus.Running.Equals(ticketStatus) || BetStatus.Waiting.Equals(ticketStatus)
                    });
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the bet ticket list.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        private void GetBetTicket(MemberBetListFilter criteria)
        {
            BalanceUp balanceUp = this.BalanceUp;
            var betListCriteria = new GetMemberBetListCriteria
            {
                MoveBetsDay = balanceUp.MoveBetsDay,
                CustId = criteria.CustId
            };

            _ticketList = new List<BetTicket>();

            if ((criteria.FromDate < balanceUp.BalanceUpDay) && (balanceUp.BalanceUpDay < criteria.ToDate))
            {
                // Gets data from archive database.
                betListCriteria.FromDate = criteria.FromDate;
                betListCriteria.ToDate = balanceUp.BalanceUpDay.AddDays(-1);
                betListCriteria.IsBackupData = true;
                _ticketList.AddRange(this.GetListData<BetTicket>(betListCriteria));

                // Gets data from current using database.
                betListCriteria.FromDate = balanceUp.BalanceUpDay;
                betListCriteria.ToDate = criteria.ToDate;
                betListCriteria.IsBackupData = false;
                _ticketList.AddRange(this.GetListData<BetTicket>(betListCriteria));
            }
            else
            {
                betListCriteria.FromDate = criteria.FromDate;
                betListCriteria.ToDate = criteria.ToDate;
                betListCriteria.IsBackupData = balanceUp.BalanceUpDay > criteria.ToDate;
                _ticketList.AddRange(this.GetListData<BetTicket>(betListCriteria));
                _ticketList = _ticketList.OrderBy(obj => obj.TransId).ToList();
            }
        }
    }
}