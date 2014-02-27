// --------------------------------------------------------------------------
// <copyright file="UpdatedbetLimitItem.cs" company="Nexcel Solutions Vietnam">
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
    /// Used to save updated bet limit information from client.
    /// </summary>
    [Serializable]
    public class UpdatedbetLimitItem
    {
        public short SportType { get; set; }

        public short BetType { get; set; }

        public decimal MaxPerMatch { get; set; }

        public decimal Max { get; set; }

        public decimal Min { get; set; }
    }
}