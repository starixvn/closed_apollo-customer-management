// --------------------------------------------------------------------------
// <copyright file="IBetAndCommSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.CustomerSetting.Models.BetAndCommSetting;
    using Fanex.NAP.Framework.Data;

    public interface IBetAndCommSettingRepository
    {
        /// <summary>
        /// Gets the bet limit information.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <param name="maxBetLimit">Output parameter of MaxBetLimit.</param>
        /// <returns>List of BetLimitInfo objects.</returns>
        IEnumerable<BetLimitInfo> GetBetLimitData(ICriteria criteria, ref decimal maxBetLimit);
    }
}