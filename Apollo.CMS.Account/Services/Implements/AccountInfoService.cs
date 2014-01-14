// --------------------------------------------------------------------------
// <copyright file="AccountInfoService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.Implements
{
    using System.Collections.Generic;
    using Apollo.CMS.Account.Criteria.AccountInfo;
    using Apollo.CMS.Account.Models.AccountInfo;
    using Apollo.CMS.Account.Services.Interfaces;
    using Apollo.CMS.Core.Services;

    /// <summary>
    /// Account information service.
    /// </summary>
    public class AccountInfoService : BaseService, IAccountInfoService
    {
        /// <summary>
        /// Gets suggested customers by passed username.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of suggested customers that have the username like the input username.</returns>
        public IEnumerable<SuggestedMember> GetSuggestedCustomers(GetSuggestedCustomersByUsernameCriteria criteria)
        {
            return this.GetListData<SuggestedMember>(criteria);
        }

        /// <summary>
        /// Gets the information of an agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>AgentInfo object represents the information of an agent.</returns>
        public AgentInfo GetAgentInfo(GetAgentInfoCriteria criteria)
        {
            return this.GetData<AgentInfo>(criteria);
        }

        /// <summary>
        /// Gets the information of a member.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>MemberInfo object represents the information of a member.</returns>
        public MemberInfo GetMemberInfo(GetMemberInfoCriteria criteria)
        {
            return this.GetData<MemberInfo>(criteria);
        }

        /// <summary>
        /// Gets the direct down line agents of a customer.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct down line agents.</returns>
        public IEnumerable<SuggestedAgent> GetDownlineAgents(GetDownlineAgentsCriteria criteria)
        {
            return this.GetListData<SuggestedAgent>(criteria);
        }

        /// <summary>
        /// Gets the direct down line members of a customer.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct down line members.</returns>
        public IEnumerable<SuggestedMember> GetDownlineMembers(GetDownlineMembersCriteria criteria)
        {
            return this.GetListData<SuggestedMember>(criteria);
        }

        /// <summary>
        /// Gets up line agents of an specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of up line agents.</returns>
        public IEnumerable<UplineAgent> GetUplineAgents(GetAgentUplineCriteria criteria)
        {
            return this.GetListData<UplineAgent>(criteria);
        }
    }
}