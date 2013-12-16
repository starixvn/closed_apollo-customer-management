// --------------------------------------------------------------------------
// <copyright file="GetAllAgentAndAgentDownlineByIdCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Get all agent and agent down line by id class.
    /// </summary>
    public class GetAllAgentAndAgentDownlineByIdCriteria : CriteriaBase
    {
        public int? AgentId { get; set; }

        public string Username { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_Agent_GetHierarchyById";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}