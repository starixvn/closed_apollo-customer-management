// --------------------------------------------------------------------------
// <copyright file="IMemberBetListRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.15">Created</change>
// </history>

namespace Apollo.CMS.BetList.Repositories.Interfaces
{
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Fanex.BetList.Core.Entities;

    public interface IMemberBetListRepository
    {
        /// <summary>
        /// Gets the system parlay data.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>SystemParlayData object wraps all needed data to generate system parlay details.</returns>
        SystemParlayData GetSystemParlayData(GetSystemParlayDetailsCriteria criteria);
    }
}