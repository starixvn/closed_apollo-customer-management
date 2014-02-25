// --------------------------------------------------------------------------
// <copyright file="SettingConfiguration.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Configurations
{
    using System.Web;
    using System.Xml.Linq;

    /// <summary>
    /// Gets setting configuration from XML.
    /// </summary>
    public static class SettingConfiguration
    {
        public const string BetLimitSportNamesFilePath = "~/Areas/CustomerSetting/Configurations/BetLimitSportNames.xml";

        static SettingConfiguration()
        {
            BettingLimitSportNameConfig = XDocument.Load(HttpContext.Current.Server.MapPath(BetLimitSportNamesFilePath));
        }

        public static XDocument BettingLimitSportNameConfig { get; set; }
    }
}