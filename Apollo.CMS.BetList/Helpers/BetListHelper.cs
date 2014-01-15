// -----------------------------------------------------------------------
// <copyright file="BetListHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.BetList.Models.MemberBetList;
    using Apollo.CMS.Core.Constants;
    using Fanex.BetList.Core.Entities;
    using Fanex.Common;

    /// <summary>
    /// Bet list helper.
    /// </summary>
    public static class BetListHelper
    {
        /// <summary>
        /// Gets the name of the odd type.
        /// </summary>
        /// <param name="id">The identifier of the odd.</param>
        /// <returns>The odd name.</returns>
        public static string GetOddTypeName(object id)
        {
            var oddName = string.Empty;

            switch ((OddTypes)id)
            {
                case OddTypes.Dec:
                    oddName = OddTypeNames.Dec;
                    break;

                case OddTypes.Hk:
                    oddName = OddTypeNames.Hk;
                    break;

                case OddTypes.Indo:
                    oddName = OddTypeNames.Indo;
                    break;

                case OddTypes.My:
                    oddName = OddTypeNames.My;
                    break;

                case OddTypes.Us:
                    oddName = OddTypeNames.Us;
                    break;
            }

            return oddName;
        }

        /// <summary>
        /// Maps the list of BetItem objects queried from DB to Ticket objects which will be used to generate bet list.
        /// </summary>
        /// <param name="data">The bet data that need to be mapped to Ticket objects.</param>
        /// <returns>List of Ticket objects.</returns>
        public static List<ITicket> Map(IEnumerable<BetTicket> data)
        {
            List<ITicket> result = new List<ITicket>();
            if ((data != null) && (data.Count() > 0))
            {
                foreach (var item in data)
                {
                    ITicket ticket = new Ticket
                    {
                        CustId = Converter.ToInt(item.CustId),
                        UserName = item.Username,
                        BetId = item.BetId,
                        RefNo = Converter.ToString2(item.RefNo),
                        TransId = Converter.To<long>(item.TransId),
                        TransDate = Converter.ToDateTime(item.TransDate),
                        BetTypeId = Converter.ToInt(item.BetType),
                        HomeId = Converter.ToInt(item.HomeId),
                        AwayId = Converter.ToInt(item.AwayId),
                        Handicap1 = Converter.ToDecimal(item.Hdp1),
                        Handicap2 = Converter.ToDecimal(item.Hdp2),
                        Odds = Converter.ToDecimal(item.Odds),
                        OddsType = Converter.ToString2(item.OddsType),
                        BetTeam = item.BetTeam,
                        EventStatus = item.BetStatus,
                        LiveHomeScore = Converter.ToInt(item.LiveHomeScore),
                        LiveAwayScore = Converter.ToInt(item.LiveAwayScore),
                        IsLive = Converter.ToBool(item.LiveIndicator),
                        LeagueId = Converter.ToInt(item.LeagueId),
                        SportTypeId = Converter.ToInt(item.SportType),
                        IsNeutral = Converter.ToBool(item.IsNeutral),
                        MatchId = Converter.ToInt(item.MatchId),
                        MatchCode = item.MatchCode,
                        Status = item.Status,
                        StatusId = item.StatusId,
                        Stake = Converter.ToDecimal(item.Stake),
                        Winlost = Converter.ToDecimal(item.WinLoss),
                        Comm = Converter.ToDecimal(item.PlayerComm),
                        SuperPositionTaking = Converter.ToDecimal(item.CompanyPositionTaking),
                        SuperWinlost = Converter.ToDecimal(item.CompanyWinLoss),
                        IP = item.IP
                    };

                    result.Add(ticket);
                }
            }

            return result;
        }

        /// <summary>
        /// Maps the list of ParlaySubTicket objects queried from DB to Ticket objects which will be used to generate bet list.
        /// </summary>
        /// <param name="data">The bet data that need to be mapped to Ticket objects.</param>
        /// <returns>List of Ticket objects.</returns>
        public static List<ITicket> Map(IEnumerable<ParlaySubTicket> data)
        {
            List<ITicket> result = new List<ITicket>();
            if ((data != null) && (data.Count() > 0))
            {
                foreach (var item in data)
                {
                    ITicket ticket = new Ticket
                    {
                        RefNo = Converter.ToString2(item.RefNo),
                        TransId = Converter.To<long>(item.TransId),
                        BetTypeId = Converter.ToInt(item.BetType),
                        HomeId = Converter.ToInt(item.HomeId),
                        AwayId = Converter.ToInt(item.AwayId),
                        Handicap1 = Converter.ToDecimal(item.Hdp1),
                        Handicap2 = Converter.ToDecimal(item.Hdp2),
                        Odds = Converter.ToDecimal(item.Odds),
                        BetTeam = item.BetTeam,
                        EventStatus = item.BetStatus,
                        LiveHomeScore = Converter.ToInt(item.LiveHomeScore),
                        LiveAwayScore = Converter.ToInt(item.LiveAwayScore),
                        IsLive = Converter.ToBool(item.LiveIndicator),
                        LeagueId = Converter.ToInt(item.LeagueId),
                        SportTypeId = Converter.ToInt(item.SportType),
                        IsNeutral = Converter.ToBool(item.IsNeutral),
                        MatchId = Converter.ToInt(item.MatchId),
                        StatusId = item.StatusId
                    };

                    result.Add(ticket);
                }
            }

            return result;
        }
    }
}