// --------------------------------------------------------------------------
// <copyright file="SettingConstants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Constants
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using Apollo.CMS.CustomerSetting.Configurations;
    using Apollo.CMS.CustomerSetting.Configurations.Entities;
    using Fanex.Common;

    /// <summary>
    /// Setting constants.
    /// </summary>
    public static class SettingConstants
    {
        public const string DecimalFormat = "#,##0.##";

        static SettingConstants()
        {
            LoadBetLimitSportNames();
        }

        public static Dictionary<string, BetLimitSportName> BetLimitSportNames { get; set; }

        /// <summary>
        /// Loads the bet limit sport names.
        /// </summary>
        private static void LoadBetLimitSportNames()
        {
            XElement root = SettingConfiguration.BettingLimitSportNameConfig.Root;
            IEnumerable<XElement> sports = root.Elements();

            BetLimitSportNames = new Dictionary<string, BetLimitSportName>();

            foreach (XElement sport in sports)
            {
                var betLimit = new BetLimitSportName
                {
                    SportId = Converter.ToInt(sport.Attribute("sportId").Value),
                    BetTypeId = Converter.ToInt(sport.Attribute("betTypeId").Value),
                    Name = sport.Attribute("name").Value,
                    ResourceKey = sport.Attribute("resourceKey").Value
                };

                BetLimitSportNames.Add(string.Format("{0}_{1}", betLimit.SportId, betLimit.BetTypeId), betLimit);
            }
        }
    }
}