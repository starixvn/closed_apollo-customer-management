// --------------------------------------------------------------------------
// <copyright file="AgentDiscountInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Models.BetAndCommSetting
{
    /// <summary>
    /// Agent discount information.
    /// </summary>
    public class AgentDiscountInfo
    {
        public short SportType { get; set; }

        public short BetType { get; set; }

        public decimal DiscountA { get; set; }

        public decimal DiscountB { get; set; }

        public decimal DiscountC { get; set; }

        public decimal DiscountD { get; set; }

        public decimal? UplineMinDiscountA { get; set; }

        public decimal? UplineMinDiscountB { get; set; }

        public decimal? UplineMinDiscountC { get; set; }

        public decimal? UplineMinDiscountD { get; set; }
    }
}