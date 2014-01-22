// -----------------------------------------------------------------------
// <copyright file="IBetListByRefNoService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.21">Created</change>
// </history>

namespace Apollo.CMS.BetList.Services.Interfaces
{
    using Apollo.CMS.BetList.Criteria.BetListByRefNo;
    using Apollo.CMS.BetList.ViewModels.BetListByRefNo;

    public interface IBetListByRefNoService
    {
        /// <summary>
        /// Gets the bet list by reference number.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>BetListByRefNoItem object represents the searched data.</returns>
        BetListByRefNoItem GetBetListByRefNo(GetBetListByRefNoCriteria criteria);
    }
}