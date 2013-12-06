// --------------------------------------------------------------------------
// <copyright file="IAccountInfoService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.AccountInfo.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.Account.Criteria.AccountInfo;
    using Apollo.CMS.Account.Models.AccountInfo;

    public interface IAccountInfoService
    {
        /// <summary>
        /// Gets suggested customers by passed username.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of suggested customers that have the username like the input username.</returns>
        IEnumerable<Member> GetSuggestedCustomers(GetSuggestedCustomersByUsernameCriteria criteria);

        /// <summary>
        /// Gets the information of an agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>AgentInfo object represents the information of an agent.</returns>
        AgentInfo GetAgentInfo(GetAgentInfoCriteria criteria);

        /// <summary>
        /// Gets the information of a member.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>MemberInfo object represents the information of a member.</returns>
        MemberInfo GetMemberInfo(GetMemberInfoCriteria criteria);

        /// <summary>
        /// Gets the direct down line agents of a customer.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct down line agents.</returns>
        IEnumerable<Agent> GetDownlineAgents(GetDownlineAgentsCriteria criteria);

        /// <summary>
        /// Gets the direct down line members of a customer.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of direct down line members.</returns>
        IEnumerable<Member> GetDownlineMembers(GetDownlineMembersCriteria criteria);

        /// <summary>
        /// Gets up line agents of an specific agent.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of up line agents.</returns>
        IEnumerable<UplineAgent> GetUplineAgents(GetAgentUplineCriteria criteria);
    }
}