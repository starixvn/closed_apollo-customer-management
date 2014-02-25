// --------------------------------------------------------------------------
// <copyright file="BetAndCommSettingRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Repositories.Implements
{
    using System.Collections;
    using System.Collections.Generic;
    using Apollo.CMS.CustomerSetting.Models.BetAndCommSetting;
    using Apollo.CMS.CustomerSetting.Repositories.Interfaces;
    using Fanex.Common;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Bet and commission setting repository.
    /// </summary>
    public class BetAndCommSettingRepository : IBetAndCommSettingRepository
    {
        /// <summary>
        /// Gets the bet limit information.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <param name="maxBetLimit">Output parameter of MaxBetLimit.</param>
        /// <returns>List of BetLimitInfo objects.</returns>
        public IEnumerable<BetLimitInfo> GetBetLimitData(ICriteria criteria, ref decimal maxBetLimit)
        {
            IEnumerable<BetLimitInfo> result = null;
            Hashtable param = criteria.GetSpParams();

            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                result = db.Query<BetLimitInfo>(param);
                param = db.GetOuputParams();

                if (param.ContainsKey("maxbetlimit"))
                {
                    maxBetLimit = Converter.ToDecimal(param["maxbetlimit"]);
                }
            }

            return result;
        }
    }
}