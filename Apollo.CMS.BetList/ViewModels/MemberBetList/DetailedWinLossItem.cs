// --------------------------------------------------------------------------
// <copyright file="DetailedWinLossItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.ViewModels.MemberBetList
{
    using System;

    /// <summary>
    /// Detailed win loss item view model.
    /// </summary>
    [Serializable]
    public class DetailedWinLossItem
    {
        public string Username { get; set; }

        public decimal? PositionTaking { get; set; }

        public decimal? Discount { get; set; }

        public decimal? WinLoss { get; set; }

        public decimal? Comm { get; set; }

        public bool IsSuper { get; set; }
    }
}
