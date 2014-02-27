// --------------------------------------------------------------------------
// <copyright file="IBetAndCommSettingService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Services.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting;
    using Fanex.NAP.Framework.Command;

    public interface IBetAndCommSettingService
    {
        /// <summary>
        /// Gets the member discount information.
        /// </summary>
        /// <param name="filter">The filters that are passed from user.</param>
        /// <returns>MemberDiscountItem object.</returns>
        MemberDiscountItem GetMemberDiscount(BetAndCommSettingFilter filter);

        /// <summary>
        /// Gets the agent discount information.
        /// </summary>
        /// <param name="filter">The filters that are passed from user.</param>
        /// <returns>AgentDiscountItem object.</returns>
        AgentDiscountItem GetAgentDiscount(BetAndCommSettingFilter filter);

        /// <summary>
        /// Gets the bet limits.
        /// </summary>
        /// <param name="filter">The filters that are passed from user.</param>
        /// <returns>List of BetLimitItem objects.</returns>
        IEnumerable<BetLimitItem> GetBetLimits(BetAndCommSettingFilter filter);

        /// <summary>
        /// Updates the bet and commission setting.
        /// </summary>
        /// <param name="filter">The filter contains updated data.</param>
        /// <returns>ICommandResult object shows that the update process is successful or not.</returns>
        ICommandResult UpdateBetAndCommSetting(UpdateBetAndCommFilter filter);
    }
}