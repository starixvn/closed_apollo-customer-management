// --------------------------------------------------------------------------
// <copyright file="GetAgentUplineCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Criteria.AccountInfo
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get up line agent of current customer.
    /// </summary>
    public class GetAgentUplineCriteria : CriteriaBase
    {
        public int AgentId { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Acc_Agent_GetAgentUplinePath";
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