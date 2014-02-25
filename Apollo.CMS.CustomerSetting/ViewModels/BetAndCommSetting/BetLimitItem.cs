// --------------------------------------------------------------------------
// <copyright file="BetLimitItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.CustomerSetting.Configurations.Entities;
    using Apollo.CMS.CustomerSetting.Constants;
    using Apollo.CMS.CustomerSetting.Models.BetAndCommSetting;
    using Fanex.Common;
    using Fanex.NAP.Core.Web;

    /// <summary>
    /// Bet limit item for a sport.
    /// </summary>
    public class BetLimitItem
    {
        public BetLimitItem()
        {
        }

        public BetLimitItem(BetLimitInfo limitInfo, decimal maxBetLimit)
        {
            this.SportType = limitInfo.SportType;
            this.BetType = limitInfo.BetType;
            this.SportTypeName = GetSportName();
            this.IsSpecialSport = limitInfo.SportType == SportTypes.Soccer || limitInfo.SportType == SportTypes.Basketball;
            this.MinSingleBet = Converter.ToDecimal(limitInfo.MinBet).Format();
            this.MinOfMinSingleBet = Converter.ToDecimal(limitInfo.MaxMinBet).Format();
            this.SingleBet = Converter.ToDecimal(limitInfo.MaxBet).Format();
            this.MinOfSingleBet = maxBetLimit.Format();
            this.MaxOfSingleBet = Converter.ToDecimal(limitInfo.MinMaxBet).Format();
            this.SingleGame = Converter.ToDecimal(limitInfo.MaxPerMatch).Format();
            this.MinOfSingleGame = maxBetLimit.Format();
            this.MaxOfSingleGame = Converter.ToDecimal(limitInfo.MinMaxPerMatch).Format();
        }

        public short SportType { get; set; }

        public string SportTypeName { get; set; }

        public short BetType { get; set; }

        public bool IsSpecialSport { get; set; }

        public string MinSingleBet { get; set; }

        public string MinOfMinSingleBet { get; set; }

        public string SingleBet { get; set; }

        public string MinOfSingleBet { get; set; }

        public string MaxOfSingleBet { get; set; }

        public string SingleGame { get; set; }

        public string MinOfSingleGame { get; set; }

        public string MaxOfSingleGame { get; set; }

        /// <summary>
        /// Gets the name which associates with the sport type and bet type.
        /// </summary>
        /// <returns>The sport name.</returns>
        private string GetSportName()
        {
            string sportName = string.Empty;
            string dictionaryKey = string.Format("{0}_{1}", this.SportType, this.BetType);
            BetLimitSportName configSportName = null;

            if (SettingConstants.BetLimitSportNames.ContainsKey(dictionaryKey))
            {
                configSportName = SettingConstants.BetLimitSportNames[dictionaryKey];
            }

            if (configSportName != null)
            {
                sportName = configSportName.Name;
            }

            return sportName;
        }
    }
}