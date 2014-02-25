// --------------------------------------------------------------------------
// <copyright file="BetLimitInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Models.BetAndCommSetting
{
    /// <summary>
    /// Bet Limit information.
    /// </summary>
    public class BetLimitInfo
    {
        public short SportType { get; set; }

        public short BetType { get; set; }

        public decimal? MinBet { get; set; }

        public decimal? MaxBet { get; set; }

        public decimal? MaxPerMatch { get; set; }

        public decimal? MaxMinBet { get; set; }

        public decimal? MinMaxBet { get; set; }

        public decimal? MinMaxPerMatch { get; set; }
    }
}