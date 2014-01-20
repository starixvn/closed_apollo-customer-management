// --------------------------------------------------------------------------
// <copyright file="SystemParlaySerial.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.17">Created</change>
// </history>

namespace Apollo.CMS.BetList.Models.GeneralBetTicket
{
    /// <summary>
    /// System parlay serial model.
    /// </summary>
    public class SystemParlaySerial
    {
        public long Serial { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }
    }
}