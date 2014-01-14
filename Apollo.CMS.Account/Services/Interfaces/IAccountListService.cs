// --------------------------------------------------------------------------
// <copyright file="IAccountListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.18">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.Account.Criteria.AccountList;
    using Apollo.CMS.Account.Models.AccountList;

    public interface IAccountListService
    {
        /// <summary>
        /// Gets navigation path.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of navigation paths.</returns>
        IEnumerable<NavigationPath> GetNavigationPaths(GetNavigationPathCriteria criteria);

        /// <summary>
        /// Gets direct down line agent list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct down line agent list.</returns>
        IEnumerable<Agent> GetAgentList(GetAgentListCriteria criteria);

        /// <summary>
        /// Gets member list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct members.</returns>
        IEnumerable<Member> GetMemberList(GetMemberListCriteria criteria);

        /// <summary>
        /// Gets shadow list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of shadows.</returns>
        IEnumerable<Shadow> GetShadowList(GetShadowListCriteria criteria);
    }
}