// --------------------------------------------------------------------------
// <copyright file="AgentCommissionItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    /// <summary>
    /// Commission item for customer of agent level.
    /// </summary>
    public class AgentCommissionItem
    {
        public decimal DiscountA { get; set; }

        public decimal DiscountB { get; set; }

        public decimal DiscountC { get; set; }

        public decimal DiscountD { get; set; }

        public decimal _1x2Comm { get; set; }

        public decimal OtherComm { get; set; }

        public AgentCommissionItem Upline { get; set; }
    }
}