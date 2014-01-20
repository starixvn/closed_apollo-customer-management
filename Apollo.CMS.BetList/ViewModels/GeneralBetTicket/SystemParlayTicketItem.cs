// -----------------------------------------------------------------------
// <copyright file="SystemParlayTicketItem.cs" company="Nexcel Solutions Vietnam">
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
    /// View model for each system parlay ticket.
    /// </summary>
    [Serializable]
    public class SystemParlayTicketItem
    {
        public SystemParlayTicketItem()
        {
            this.EventNames = new List<string>();
        }

        public string WinLossDate { get; set; }

        public List<string> EventNames { get; set; }

        public string Odds { get; set; }

        public string WinLoss { get; set; }

        public string Comm { get; set; }

        public string Stake { get; set; }

        public string Status { get; set; }
    }
}