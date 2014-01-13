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
    using System;

    /// <summary>
    /// The criteria that are used to get win loss details of the whole line from the current used database and backup database.
    /// </summary>
    public class GetDetailedWinLossCriteria
    {
        public long TransId { get; set; }

        public DateTime TranDate { get; set; }
    }
}