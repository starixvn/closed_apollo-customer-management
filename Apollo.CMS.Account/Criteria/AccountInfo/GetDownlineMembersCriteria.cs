// --------------------------------------------------------------------------
// <copyright file="GetDownlineMembersCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Criteria that are used to get down line agents of current customer.
    /// </summary>
    public class GetDownlineMembersCriteria : CriteriaBase
    {
        public int ParentId { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Acc_Customer_GetCustomerByAgentId";
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