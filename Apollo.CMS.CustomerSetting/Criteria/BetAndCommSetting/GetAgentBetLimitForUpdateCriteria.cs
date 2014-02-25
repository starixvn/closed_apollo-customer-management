// --------------------------------------------------------------------------
// <copyright file="GetAgentBetLimitForUpdateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Criteria.BetAndCommSetting
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get agent's bet limit information.
    /// </summary>
    public class GetAgentBetLimitForUpdateCriteria : CriteriaBase
    {
        public int AgentId { get; set; }

        public decimal MaxBetLimit { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Age_Agent_GetBetLimitForUpdate";
        }

        /// <summary>
        /// Check validation of the criteria.
        /// </summary>
        /// <returns>Whether or not the criteria is valid.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}