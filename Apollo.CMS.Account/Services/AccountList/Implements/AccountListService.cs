// --------------------------------------------------------------------------
// <copyright file="AccountListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.18">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.AccountList.Implements
{
    using System.Collections.Generic;
    using Apollo.CMS.Account.Criteria.AccountList;
    using Apollo.CMS.Account.Models.AccountList;
    using Apollo.CMS.Account.Services.AccountList.Interfaces;
    using Apollo.CMS.Core.Services;

    /// <summary>
    /// Account list service.
    /// </summary>
    public class AccountListService : BaseService, IAccountListService
    {
        /// <summary>
        /// Gets navigation path.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of navigation paths.</returns>
        public IEnumerable<NavigationPath> GetNavigationPaths(GetNavigationPathCriteria criteria)
        {
            return this.GetListData<NavigationPath>(criteria);
        }

        /// <summary>
        /// Gets direct down line agent list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct down line agent list.</returns>
        public IEnumerable<Agent> GetAgentList(GetAgentListCriteria criteria)
        {
            int totalItems;
            IEnumerable<Agent> agentList = this.GetListData<Agent>(criteria, out totalItems);

            criteria.TotalItems = totalItems;

            return agentList;
        }

        /// <summary>
        /// Gets member list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct members.</returns>
        public IEnumerable<Member> GetMemberList(GetMemberListCriteria criteria)
        {
            int totalItems;
            IEnumerable<Member> memberList = this.GetListData<Member>(criteria, out totalItems);

            criteria.TotalItems = totalItems;

            return memberList;
        }

        /// <summary>
        /// Gets shadow list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of shadows.</returns>
        public IEnumerable<Shadow> GetShadowList(GetShadowListCriteria criteria)
        {
            int totalItems;
            IEnumerable<Shadow> shadowList = this.GetListData<Shadow>(criteria, out totalItems);

            criteria.TotalItems = totalItems;

            return shadowList;
        }
    }
}