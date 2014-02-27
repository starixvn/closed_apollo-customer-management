// --------------------------------------------------------------------------
// <copyright file="UpdatedCommItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.26">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    using System;

    /// <summary>
    /// Used to save updated commission information from client.
    /// </summary>
    [Serializable]
    public class UpdatedCommItem
    {
        public short SportType { get; set; }

        public short BetType { get; set; }

        public decimal DiscountA { get; set; }

        public decimal DiscountB { get; set; }

        public decimal DiscountC { get; set; }

        public decimal DiscountD { get; set; }
    }
}