// --------------------------------------------------------------------------
// <copyright file="AccountListService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.18">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.Account.Criteria.AccountList;
    using Apollo.CMS.Account.Models.AccountList;
    using Apollo.CMS.Account.Services.Interfaces;
    using Apollo.CMS.Account.ViewModels.AccountList;
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
        public IEnumerable<AgentInfo> GetAgentList(GetAgentListCriteria criteria)
        {
            int totalItems;
            IEnumerable<RawAgentInfo> rawData = this.GetListData<RawAgentInfo>(criteria, out totalItems);
            IEnumerable<AgentInfo> agentList = null;

            if (rawData != null && rawData.Any())
            {
                agentList = rawData.Select(obj => new AgentInfo(obj));
                criteria.TotalItems = totalItems;
            }

            return agentList;
        }

        /// <summary>
        /// Gets member list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct members.</returns>
        public IEnumerable<MemberInfo> GetMemberList(GetMemberListCriteria criteria)
        {
            int totalItems;
            IEnumerable<RawMemberInfo> rawData = this.GetListData<RawMemberInfo>(criteria, out totalItems);
            IEnumerable<MemberInfo> memberList = null;

            if (rawData != null && rawData.Any())
            {
                memberList = rawData.Select(obj => new MemberInfo(obj));
                criteria.TotalItems = totalItems;
            }

            return memberList;
        }

        /// <summary>
        /// Gets shadow list of a specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of shadows.</returns>
        public IEnumerable<ShadowInfo> GetShadowList(GetShadowListCriteria criteria)
        {
            int totalItems;
            IEnumerable<RawShadowInfo> rawData = this.GetListData<RawShadowInfo>(criteria, out totalItems);
            IEnumerable<ShadowInfo> shadowList = null;

            if (rawData != null && rawData.Any())
            {
                shadowList = rawData.Select(obj => new ShadowInfo(obj));
                criteria.TotalItems = totalItems;
            }

            return shadowList;
        }
    }
}