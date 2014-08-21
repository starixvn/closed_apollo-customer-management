// -----------------------------------------------------------------------
// <copyright file="BetListByRefNoService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.21">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.BetList.Constants;
    using Apollo.CMS.BetList.Criteria.BetListByRefNo;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Helpers;
    using Apollo.CMS.BetList.Models.GeneralBetTicket;
    using Apollo.CMS.BetList.Services.Interfaces;
    using Apollo.CMS.BetList.ViewModels.BetListByRefNo;
    using Apollo.CMS.BetList.ViewModels.GeneralBetTicket;
    using Apollo.CMS.Core.Services;
    using Fanex.Common;

    /// <summary>
    /// Member's bet list service.
    /// </summary>
    public class BetListByRefNoService : BaseService, IBetListByRefNoService
    {
        private readonly IMemberBetListService _memberBetListService;

        public BetListByRefNoService() :
            this(null)
        {
        }

        public BetListByRefNoService(IMemberBetListService memberBetListService)
        {
            this._memberBetListService = memberBetListService ?? new MemberBetListService();
        }

        /// <summary>
        /// Gets the bet list by reference number.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>BetListByRefNoItem object represents the searched data.</returns>
        public BetListByRefNoItem GetBetListByRefNo(GetBetListByRefNoCriteria criteria)
        {
            var result = new BetListByRefNoItem();
            List<BetTicket> rawData = this.GetListData<BetTicket>(criteria).ToList();

            if (rawData.Any())
            {
                //// Fix ticket 7-Over, 4-Over
                MemberBetListService.FixTotalGoalBetteam(rawData);

                BetTicket mainTickets = rawData.ElementAt(0);
                BetList tickets = BetListHelper.ConvertToBetList(rawData);

                result.Ticket = tickets.BetItems[0];
                result.IsParlayTicket = (mainTickets.BetType == (int)BetTypes.MixParlay) ||
                                        (mainTickets.BetType == (int)BetTypes.Combine_Mix_Parlay);

                if (result.IsParlayTicket)
                {
                    result.ParlaySubTicket = this._memberBetListService.GetParlaySubTickets(new GetParlaySubTicketsCriteria
                    {
                        RefNo = Converter.To<long>(mainTickets.RefNo)
                    });
                }
            }

            return result;
        }
    }
}