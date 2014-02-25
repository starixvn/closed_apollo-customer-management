// --------------------------------------------------------------------------
// <copyright file="CustomerDiscountInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Models.BetAndCommSetting
{
    /// <summary>
    /// Customer's discount information.
    /// </summary>
    public class CustomerDiscountInfo
    {
        public short SportType { get; set; }

        public short BetType { get; set; }

        public decimal Discount { get; set; }

        public decimal UplineMinDiscountA { get; set; }

        public decimal UplineMinDiscountB { get; set; }

        public decimal UplineMinDiscountC { get; set; }

        public decimal UplineMinDiscountD { get; set; }
    }
}