// --------------------------------------------------------------------------
// <copyright file="BetTicket.cs" company="Nexcel Solutions Vietnam">
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
    /// Bet ticket model.
    /// </summary>
    public class BetTicket
    {
        public long? TransId { get; set; }

        public long? RefNo { get; set; }

        public int? MatchId { get; set; }

        public string MatchCode { get; set; }

        public int? HomeId { get; set; }

        public int? AwayId { get; set; }

        public byte? SportType { get; set; }

        public int? LeagueId { get; set; }

        public bool? IsNeutral { get; set; }

        public string BetStatus { get; set; }

        public string Username { get; set; }

        public short? BetType { get; set; }

        public int? CustId { get; set; }

        public decimal? Hdp1 { get; set; }

        public decimal? Hdp2 { get; set; }

        public decimal? Odds { get; set; }

        public decimal? MOdds { get; set; }

        public string Status { get; set; }

        public decimal? Stake { get; set; }

        public decimal? ActualStake { get; set; }

        public DateTime? TransDate { get; set; }

        public string BetTeam { get; set; }

        public long? BetId { get; set; }

        public decimal? PlayerDiscount { get; set; }

        public decimal? WinLoss { get; set; }

        public decimal? PlayerComm { get; set; }

        public decimal? CompanyPositionTaking { get; set; }

        public decimal? CompanyDiscount { get; set; }

        public decimal? CompanyWinLoss { get; set; }

        public decimal? CompanyComm { get; set; }

        public short? LiveHomeScore { get; set; }

        public short? LiveAwayScore { get; set; }

        public bool? LiveIndicator { get; set; }

        public short? HtHomeScore { get; set; }

        public short? HtAwayScore { get; set; }

        public short? FinalHomeScore { get; set; }

        public short? FinalAwayScore { get; set; }

        public string IP { get; set; }

        public byte? OddsType { get; set; }

        public bool? DoubleCommLosing { get; set; }

        public byte? StatusId { get; set; }
    }
}