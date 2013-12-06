// --------------------------------------------------------------------------
// <copyright file="GetAgentDownlineByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.6">Created</change>
// </history>

namespace Apollo.CMS.Account.Criteria.TreeView
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class GetAgentDownlineByIdCriteria : CriteriaBase
    {
        public int AgentId { get; set; }

        public override string GetSettingKey()
        {
            return "Age_Agent_GetAgentDownline";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}