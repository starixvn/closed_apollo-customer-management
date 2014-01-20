// --------------------------------------------------------------------------
// <copyright file="BetList.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.ViewModels.GeneralBetTicket
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Bet list view model.
    /// </summary>
    [Serializable]
    public class BetList
    {
        public BetList()
        {
            this.BetItems = new List<BetItem>();
        }

        public List<BetItem> BetItems { get; set; }

        public decimal SumStake { get; set; }

        public decimal SumWinLoss { get; set; }

        public decimal SumComm { get; set; }

        public decimal SumCompanyWinLoss { get; set; }

        public decimal SumCompanyShare { get; set; }

        public decimal SumCompanyComm { get; set; }

        public int Counter { get; set; }

        public int TotalItems { get; set; }

        /// <summary>
        /// Adds a specified item to the bet list.
        /// </summary>
        /// <param name="item">The item that needs to be added.</param>
        public void Add(BetItem item)
        {
            if (item != null)
            {
                this.BetItems.Add(item);
                this.SumStake += item.Stake;
                this.SumWinLoss += item.WinLoss;
                this.SumComm += item.Comm;
                this.SumCompanyShare += item.CompanyShare;
                this.SumCompanyComm += item.CompanyComm;
                this.SumCompanyWinLoss += item.CompanyWinLoss;
            }
        }
    }
}