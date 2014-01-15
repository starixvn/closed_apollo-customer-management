// --------------------------------------------------------------------------
// <copyright file="SystemParlayDetail.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.15">Created</change>
// </history>

namespace Apollo.CMS.BetList.Models.MemberBetList
{
    using System;

    /// <summary>
    /// System parlay detail model.
    /// </summary>
    public class SystemParlayDetail
    {
        public DateTime? WinLossDate { get; set; }

        public int? TransId { get; set; }

        public long? Serial { get; set; }

        public int? StatusId { get; set; }

        public string Status { get; set; }

        public decimal? MOdds { get; set; }

        public decimal? Stake { get; set; }

        public decimal? WinLoss { get; set; }

        public decimal? PlayerComm { get; set; }
    }
}
