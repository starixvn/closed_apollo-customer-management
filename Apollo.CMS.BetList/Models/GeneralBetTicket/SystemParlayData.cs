// --------------------------------------------------------------------------
// <copyright file="SystemParlayData.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.17">Created</change>
// </history>

namespace Apollo.CMS.BetList.Models.GeneralBetTicket
{
    using System.Collections.Generic;

    /// <summary>
    /// System parlay data model.
    /// </summary>
    public class SystemParlayData
    {
        public IEnumerable<SystemParlayTicket> TicketList { get; set; }

        public IEnumerable<SystemParlaySerial> SerialList { get; set; }

        public decimal Outstanding { get; set; }
    }
}