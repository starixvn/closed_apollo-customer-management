// --------------------------------------------------------------------------
// <copyright file="GetCustomerBetLimitForUpdateCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// The criteria that using to get customer's bet limit information.
    /// </summary>
    public class GetCustomerBetLimitForUpdateCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public decimal MaxBetLimit { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Age_Customer_GetBetLimitForUpdate";
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