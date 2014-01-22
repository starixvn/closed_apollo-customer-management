// --------------------------------------------------------------------------
// <copyright file="BetListByRefNoItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.21">Created</change>
// </history>

namespace Apollo.CMS.BetList.ViewModels.BetListByRefNo
{
    using System;
    using Apollo.CMS.BetList.ViewModels.GeneralBetTicket;

    /// <summary>
    /// View model for a ticket that is searched by Bet List By Ref No function.
    /// </summary>
    [Serializable]
    public class BetListByRefNoItem
    {
        public BetItem Ticket { get; set; }

        public bool IsParlayTicket { get; set; }

        public string ParlaySubTicket { get; set; }
    }
}