// --------------------------------------------------------------------------
// <copyright file="BetLimitSportName.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Configurations.Entities
{
    /// <summary>
    /// Bet limit sport name.
    /// </summary>
    public class BetLimitSportName
    {
        public int SportId { get; set; }

        public int BetTypeId { get; set; }

        public string Name { get; set; }

        public string ResourceKey { get; set; }
    }
}