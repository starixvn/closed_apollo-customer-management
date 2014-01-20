// -----------------------------------------------------------------------
// <copyright file="SystemParlayDetails.cs" company="Nexcel Solutions Vietnam">
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
    /// View model for system parlay details.
    /// </summary>
    [Serializable]
    public class SystemParlayDetails
    {
        public SystemParlayDetails()
        {
            this.DailyTicketLists = new List<SystemParlayDailyTicketList>();
        }

        public string TotalWinLoss { get; set; }

        public string TotalComm { get; set; }

        public string Outstanding { get; set; }

        public List<SystemParlayDailyTicketList> DailyTicketLists { get; set; }
    }
}