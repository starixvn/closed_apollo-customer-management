// --------------------------------------------------------------------------
// <copyright file="GetDetailedWinLossCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Criteria.MemberBetList
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get win loss details of the whole line.
    /// </summary>
    public class GetDetailedWinLossCriteria : CriteriaBase
    {
        public long TransId { get; set; }

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
                return "Acc_BetList_GetDetailByTransId_Old";
            }
            else
            {
                return "Acc_BetList_GetDetailByTransId";
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