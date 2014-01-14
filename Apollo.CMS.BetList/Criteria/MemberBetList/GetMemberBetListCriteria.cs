// --------------------------------------------------------------------------
// <copyright file="GetMemberBetListCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Criteria.MemberBetList
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get the bet list.
    /// </summary>
    public class GetMemberBetListCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public DateTime MoveBetsDay { get; set; }

        [SpParam(Ignored = true)]
        public bool IsBackupData { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            if (IsBackupData)
            {
                return "Acc_BetList_GetByCustomer_Old";
            }
            else
            {
                return "Acc_BetList_GetByCustomer";
            }
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