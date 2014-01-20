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
    using Apollo.CMS.BetList.Models.GeneralBetTicket;
    using Apollo.CMS.BetList.Models.MemberBetList;
    using Apollo.CMS.BetList.Repositories.Implements;
    using Apollo.CMS.BetList.Repositories.Interfaces;
    using Apollo.CMS.BetList.Services.Interfaces;
    using Apollo.CMS.BetList.ViewModels.GeneralBetTicket;
    using Apollo.CMS.BetList.ViewModels.MemberBetList;
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.Models;
    using Apollo.CMS.Core.Services;
    using Fanex.Common;

    /// <summary>
    /// Member's bet list service.
    /// </summary>
    public class MemberBetListService : BaseService, IMemberBetListService
    {
        private static List<BetTicket> _ticketList;
        private readonly IMemberBetListRepository _memberBetListRepository;

        public MemberBetListService() :
            this(null)
        {
        }

        public MemberBetListService(IMemberBetListRepository memberBetListRepository)
        {
            this._memberBetListRepository = memberBetListRepository ?? new MemberBetListRepository();
        }

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
            IEnumerable<ParlaySubTicket> rawData = this.GetListData<ParlaySubTicket>(criteria);

            return BetListHelper.ConvertParlaySubTicketList(rawData);
        }

        /// <summary>
        /// Gets the system parlay details.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The SystemParlayDetails object that represents the system parlay details.</returns>
        public SystemParlayDetails GetSystemParlayDetails(GetSystemParlayDetailsCriteria criteria)
        {
            BalanceUp balanceUp = this.BalanceUp;
            string result = string.Empty;

            criteria.BalanceUpDay = balanceUp.BalanceUpDay;
            criteria.MoveBetsDay = balanceUp.MoveBetsDay;

            return SystemParlayDetailsHelper.Map(this._memberBetListRepository.GetSystemParlayData(criteria));
        }

        /// <summary>
        /// Separate the bet list into pages.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>Formatted bet list.</returns>
        private BetList SeparateBetListToPage(MemberBetListFilter criteria)
        {
            BetList result = null;
            List<BetTicket> pagedData = null;
            int startIndex = criteria.PageIndex * criteria.PageSize;
            int endIndex = startIndex + criteria.PageSize;

            endIndex = endIndex > _ticketList.Count ? _ticketList.Count : endIndex;
            pagedData = _ticketList.GetRange(startIndex, endIndex - startIndex);
            result = BetListHelper.ConvertToBetList(pagedData);
            result.TotalItems = _ticketList.Count;

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