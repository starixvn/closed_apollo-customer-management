// --------------------------------------------------------------------------
// <copyright file="TreeViewCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Criteria.TreeView
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TreeViewCriteria : CriteriaBase
    {
        public int AgentId { get; set; }

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