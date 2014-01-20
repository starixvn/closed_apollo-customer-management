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
    using Apollo.CMS.BetList.Constants;
    using Apollo.CMS.BetList.Models.GeneralBetTicket;
    using Apollo.CMS.BetList.ViewModels.GeneralBetTicket;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Common;

    /// <summary>
    /// Bet list helper.
    /// </summary>
    public static class BetListHelper
    {
        /// <summary>
        /// Converts raw data queried from database to bet list table format.
        /// </summary>
        /// <param name="data">The raw data.</param>
        /// <returns>BetList object represents bet list.</returns>
        public static BetList ConvertToBetList(IEnumerable<BetTicket> data)
        {
            var result = new BetList();

            if (data != null && data.Any())
            {
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                TicketHelper ticketHelper = new TicketHelper();
                IEnumerable<ITicket> tickets = BetListHelper.Map(data);
                string ticketStatus = string.Empty;

                result.TotalItems = data.Count();

                foreach (ITicket ticket in tickets)
                {
                    ticketStatus = ticket.Status.ToLower();
                    result.Add(new BetItem
                    {
                        Username = ticket.UserName,
                        TransId = ticket.TransId,
                        TransDate = ticket.TransDate,
                        Stake = ticket.Stake,
                        WinLoss = ticket.Winlost,
                        Comm = ticket.Comm,
                        CompanyWinLoss = ticket.SuperWinlost,
                        CompanyShare = ticket.SuperPositionTaking,
                        CompanyComm = ticket.SuperComm,
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
        /// Converts raw data queried from database to the parlay sub ticket list.
        /// </summary>
        /// <param name="rawData">The raw data.</param>
        /// <returns>The HTML formatted string that represents the sub-tickets.</returns>
        public static string ConvertParlaySubTicketList(IEnumerable<ParlaySubTicket> rawData)
        {
            string result = string.Empty;

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
        /// Gets the name of the odd type.
        /// </summary>
        /// <param name="id">The identifier of the odd.</param>
        /// <returns>The odd name.</returns>
        public static string GetOddTypeName(object id)
        {
            var oddName = CacheRef.GetOddsTypeName(id);

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
                        MasterOdds = Converter.ToDecimal(item.MOdds),
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
                        SuperComm = Converter.ToDecimal(item.CompanyComm),
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