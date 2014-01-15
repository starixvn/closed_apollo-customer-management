// --------------------------------------------------------------------------
// <copyright file="ParlaySubTicket.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.15">Created</change>
// </history>

namespace Apollo.CMS.BetList.Models.MemberBetList
{
    /// <summary>
    /// Parlay sub ticket model.
    /// </summary>
    public class ParlaySubTicket
    {
        public long? RefNo { get; set; }

        public long? TransId { get; set; }

        public byte? SportType { get; set; }

        public int? LeagueId { get; set; }

        public int? BetType { get; set; }

        public int? MatchId { get; set; }

        public int? HomeId { get; set; }

        public int? AwayId { get; set; }

        public double? Hdp1 { get; set; }

        public double? Hdp2 { get; set; }

        public decimal? Odds { get; set; }

        public string BetTeam { get; set; }

        public int? StatusId { get; set; }

        public short? LiveHomeScore { get; set; }

        public short? LiveAwayScore { get; set; }

        public bool? LiveIndicator { get; set; }

        public short? HtHomeScore { get; set; }

        public short? HtAwayScore { get; set; }

        public short? FinalHomeScore { get; set; }

        public short? FinalAwayScore { get; set; }

        public string BetStatus { get; set; }

        public bool? IsNeutral { get; set; }
    }
}