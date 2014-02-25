// --------------------------------------------------------------------------
// <copyright file="GetCustomerDiscountForUpdateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Criteria.BetAndCommSetting
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get customer's discount information.
    /// </summary>
    public class GetCustomerDiscountForUpdateCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Age_Customer_GetDiscountForUpdate";
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