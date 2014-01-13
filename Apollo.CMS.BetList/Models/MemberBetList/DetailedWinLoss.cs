// --------------------------------------------------------------------------
// <copyright file="DetailedWinLoss.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Models.MemberBetList
{
    using System;

    /// <summary>
    /// Win loss of each level in the line.
    /// </summary>
    public class DetailedWinLoss
    {
        public long? TransID { get; set; }

        public int? AgentID { get; set; }

        public string Status { get; set; }

        public decimal? PositionTaking { get; set; }

        public decimal? Discount { get; set; }

        public decimal? WinLoss { get; set; }

        public decimal? Comm { get; set; }

        public string Username { get; set; }

        public short? AgentLevel { get; set; }

        public byte? StatusId { get; set; }
    }
}