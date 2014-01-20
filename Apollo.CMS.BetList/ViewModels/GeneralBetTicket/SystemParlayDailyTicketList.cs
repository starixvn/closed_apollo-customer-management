// -----------------------------------------------------------------------
// <copyright file="SystemParlayDailyTicketList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.17">Created</change>
// </history>

namespace Apollo.CMS.BetList.ViewModels.GeneralBetTicket
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// View model for daily system parlay tickets.
    /// </summary>
    [Serializable]
    public class SystemParlayDailyTicketList
    {
        public SystemParlayDailyTicketList()
        {
            this.TicketItems = new List<SystemParlayTicketItem>();
        }

        public string WinLossDate { get; set; }

        public string TotalWinLoss { get; set; }

        public string TotalComm { get; set; }

        public List<SystemParlayTicketItem> TicketItems { get; set; }
    }
}