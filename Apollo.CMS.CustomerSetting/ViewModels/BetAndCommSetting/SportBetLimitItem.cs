// --------------------------------------------------------------------------
// <copyright file="SportBetLimitItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    /// <summary>
    /// Bet limit item for a sport.
    /// </summary>
    public class SportBetLimitItem
    {
        public string SportName { get; set; }

        public decimal SingleGame { get; set; }

        public decimal MinOfSingleGame { get; set; }

        public decimal MaxOfSingleGame { get; set; }

        public decimal SingleBet { get; set; }

        public decimal MinOfSingleBet { get; set; }

        public decimal MaxOfSingleBet { get; set; }

        public decimal MinSingleBet { get; set; }

        public decimal MinOfMinSingleBet { get; set; }
    }
}