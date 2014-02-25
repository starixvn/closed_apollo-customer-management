// --------------------------------------------------------------------------
// <copyright file="BetAndCommSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.Core.Services;
    using Apollo.CMS.CustomerSetting.Criteria.BetAndCommSetting;
    using Apollo.CMS.CustomerSetting.Models.BetAndCommSetting;
    using Apollo.CMS.CustomerSetting.Repositories.Implements;
    using Apollo.CMS.CustomerSetting.Repositories.Interfaces;
    using Apollo.CMS.CustomerSetting.Services.Interfaces;
    using Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Bet and commission setting service.
    /// </summary>
    public class BetAndCommSettingService : BaseService, IBetAndCommSettingService
    {
        private readonly IBetAndCommSettingRepository _repository;

        public BetAndCommSettingService() :
            this(null)
        {
        }

        public BetAndCommSettingService(IBetAndCommSettingRepository repository)
        {
            this._repository = repository ?? new BetAndCommSettingRepository();
        }

        /// <summary>
        /// Gets the member discount information.
        /// </summary>
        /// <param name="filter">The filters that are passed from user.</param>
        /// <returns>MemberDiscountItem object.</returns>
        public MemberDiscountItem GetMemberDiscount(BetAndCommSettingFilter filter)
        {
            var getDiscountCriteria = new GetCustomerDiscountForUpdateCriteria
            {
                CustId = filter.CustId
            };
            var getGroupCriteria = new GetGroupByCustIdCriteria
            {
                CustId = filter.CustId
            };
            CustomerDiscountInfo[] customerDiscountList = this.GetListData<CustomerDiscountInfo>(getDiscountCriteria).ToArray();
            CustomerGroupInfo customerGroupInfo = this.GetData<CustomerGroupInfo>(getGroupCriteria);

            return new MemberDiscountItem(customerGroupInfo.UGroup, customerDiscountList);
        }

        /// <summary>
        /// Gets the agent discount information.
        /// </summary>
        /// <param name="filter">The filters that are passed from user.</param>
        /// <returns>AgentDiscountItem object.</returns>
        public AgentDiscountItem GetAgentDiscount(BetAndCommSettingFilter filter)
        {
            var getAgentDiscount = new GetAgentDiscountForUpdateCriteria
            {
                AgentId = filter.CustId
            };
            AgentDiscountInfo[] discountList = this.GetListData<AgentDiscountInfo>(getAgentDiscount).ToArray();

            return new AgentDiscountItem(discountList);
        }

        /// <summary>
        /// Gets the bet limits.
        /// </summary>
        /// <param name="filter">The filters that are passed from user.</param>
        /// <returns>List of BetLimitItem objects.</returns>
        public IEnumerable<BetLimitItem> GetBetLimits(BetAndCommSettingFilter filter)
        {
            IEnumerable<BetLimitInfo> rawData = null;
            IEnumerable<BetLimitItem> result = null;
            ICriteria criteria = null;
            decimal maxBetlimit = 0;

            if (filter.IsMember)
            {
                criteria = new GetCustomerBetLimitForUpdateCriteria
                {
                    CustId = filter.CustId
                };
            }
            else
            {
                criteria = new GetAgentBetLimitForUpdateCriteria
                {
                    AgentId = filter.CustId
                };
            }

            rawData = this._repository.GetBetLimitData(criteria, ref maxBetlimit);

            if (rawData != null && rawData.Any())
            {
                result = rawData.Select(obj => new BetLimitItem(obj, maxBetlimit));
            }

            return result;
        }
    }
}