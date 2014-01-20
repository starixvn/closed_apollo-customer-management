// -----------------------------------------------------------------------
// <copyright file="SystemParlayDetailsHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.17">Created</change>
// </history>

namespace Apollo.CMS.BetList.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.BetList.Contants;
    using Apollo.CMS.BetList.Models.GeneralBetTicket;
    using Apollo.CMS.BetList.ViewModels.GeneralBetTicket;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.Core.Utils;

    /// <summary>
    /// Class that helps to generate details table of system parlay.
    /// </summary>
    public static class SystemParlayDetailsHelper
    {
        /// <summary>
        /// Maps the raw system parlay data queried from database to table format.
        /// </summary>
        /// <param name="systemParlayData">The system parlay data queried from database.</param>
        /// <returns>SystemParlayDetails object represents system parlay's details table.</returns>
        public static SystemParlayDetails Map(SystemParlayData systemParlayData)
        {
            SystemParlayDetails result = new SystemParlayDetails();

            if (systemParlayData != null)
            {
                SystemParlayTicketItem ticket = null;
                SystemParlayDailyTicketList dailyTicketList = null;
                IEnumerable<SystemParlayTicket> ticketList = null;
                IEnumerable<SystemParlaySerial> eventList = null;
                TicketHelper ticketHelper = new TicketHelper();
                IEnumerable<DateTime> dateList = systemParlayData.TicketList.Select(t => t.WinLossDate).Distinct().OrderBy(date => date);
                int dateCount = dateList.Count();

                result.TotalWinLoss = Formatter.HighlightNegativeNumber(systemParlayData.TicketList.Sum(t => t.WinLoss));
                result.TotalComm = Formatter.HighlightNegativeNumber(systemParlayData.TicketList.Sum(t => t.PlayerComm));
                result.Outstanding = Formatter.HighlightNegativeNumber(systemParlayData.Outstanding);

                foreach (DateTime date in dateList)
                {
                    ticketList = systemParlayData.TicketList.Where(t => t.WinLossDate == date);

                    dailyTicketList = new SystemParlayDailyTicketList();
                    dailyTicketList.WinLossDate = date.ToShortDateString();
                    dailyTicketList.TotalWinLoss = Formatter.HighlightNegativeNumber(ticketList.Sum(t => t.WinLoss));
                    dailyTicketList.TotalComm = Formatter.HighlightNegativeNumber(ticketList.Sum(t => t.PlayerComm));

                    foreach (SystemParlayTicket item in ticketList)
                    {
                        eventList = systemParlayData.SerialList.Where(d => d.Serial == item.Serial);
                        ticket = GroupTicketData(item, eventList, ticketHelper);
                        dailyTicketList.TicketItems.Add(ticket);
                    }

                    result.DailyTicketLists.Add(dailyTicketList);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="statusId">The status identifier.</param>
        /// <returns>The status name.</returns>
        public static string GetStatus(int statusId)
        {
            string result = string.Empty;

            switch ((BetStatusId)statusId)
            {
                case BetStatusId.Running:
                    result = CoreBetList.running;
                    break;

                case BetStatusId.Reject:
                    result = CoreBetList.reject;
                    break;

                case BetStatusId.Won:
                    result = CoreBetList.won;
                    break;

                case BetStatusId.Lose:
                    result = CoreBetList.lose;
                    break;

                case BetStatusId.Draw:
                    result = CoreBetList.draw;
                    break;

                case BetStatusId.Waiting:
                    result = CoreBetList.waiting;
                    break;

                case BetStatusId.Void:
                    result = CoreBetList.voided;
                    break;

                case BetStatusId.Refund:
                    result = CoreBetList.refund;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Groups events of a ticket.
        /// </summary>
        /// <param name="data">The ticket data.</param>
        /// <param name="eventList">The event list.</param>
        /// <param name="ticketHelper">The ticket helper that is used to get team name.</param>
        /// <returns>SystemParlayTicketItem represents for a ticket.</returns>
        private static SystemParlayTicketItem GroupTicketData(SystemParlayTicket data, IEnumerable<SystemParlaySerial> eventList, TicketHelper ticketHelper)
        {
            SystemParlayTicketItem ticket = new SystemParlayTicketItem();

            ticket.WinLossDate = data.WinLossDate.ToShortDateString();
            ticket.WinLoss = Formatter.HighlightNegativeNumber(data.WinLoss);
            ticket.Comm = Formatter.HighlightNegativeNumber(data.PlayerComm);
            ticket.Odds = data.MOdds == 0 ? "-" : Formatter.DecFormat(data.MOdds, 2);
            ticket.Stake = Formatter.DecFormat(data.Stake, 2);
            ticket.Status = GetStatus(data.StatusId);

            if (eventList.Any())
            {
                foreach (SystemParlaySerial eventItem in eventList)
                {
                    string eventName = string.Format(
                        "{0} -vs- {1}",
                        ticketHelper.GetTeamNameById(eventItem.HomeId),
                        ticketHelper.GetTeamNameById(eventItem.AwayId));

                    ticket.EventNames.Add(eventName);
                }
            }
            else
            {
                ticket.EventNames.Add(CoreLang.TotalLosingTicket);
            }

            return ticket;
        }
    }
}