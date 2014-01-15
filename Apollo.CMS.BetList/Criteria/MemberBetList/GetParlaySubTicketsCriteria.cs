// --------------------------------------------------------------------------
// <copyright file="GetParlaySubTicketsCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.15">Created</change>
// </history>

namespace Apollo.CMS.BetList.Criteria.MemberBetList
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria that are used to get sub-tickets of a parlay ticket.
    /// </summary>
    public class GetParlaySubTicketsCriteria : CriteriaBase
    {
        public long RefNo { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Acc_BetList_GetMixParlayByRefNo";
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