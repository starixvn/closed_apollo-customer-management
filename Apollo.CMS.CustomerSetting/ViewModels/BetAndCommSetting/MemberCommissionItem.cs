// --------------------------------------------------------------------------
// <copyright file="MemberCommissionItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    /// <summary>
    /// Commission item for customer who is a member.
    /// </summary>
    public class MemberCommissionItem
    {
        public decimal HandicapComm { get; set; }

        public decimal _1x2Comm { get; set; }

        public decimal OtherComm { get; set; }

        public AgentCommissionItem Upline { get; set; }
    }
}