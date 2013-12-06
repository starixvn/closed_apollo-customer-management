// --------------------------------------------------------------------------
// <copyright file="GetSuggestedCustomersByUsernameCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// The criteria that are used to get suggested members by the input username.
    /// </summary>
    public class GetSuggestedCustomersByUsernameCriteria : CriteriaBase
    {
        public string Username { get; set; }

        [SpParam(Ignored = true)]
        public bool IsMember { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return IsMember ? "Age_Customer_GetByUsername" : "Age_Agent_SuggestByUsername";
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