// --------------------------------------------------------------------------
// <copyright file="GetAgentListCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.19">Created</change>
// </history>

namespace Apollo.CMS.Account.Criteria.AccountList
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get the agent list.
    /// </summary>
    public class GetAgentListCriteria : CriteriaBase
    {
        public int AgentId { get; set; }

        public string UserName { get; set; }

        public byte Status { get; set; }

        public byte BetStatus { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsOrderByAsc { get; set; }

        public int TotalItems { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Acc_Agent_GetByAgent";
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