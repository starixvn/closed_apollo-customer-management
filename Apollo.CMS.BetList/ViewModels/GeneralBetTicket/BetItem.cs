// --------------------------------------------------------------------------
// <copyright file="BetItem.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.ViewModels.GeneralBetTicket
{
    using System;

    /// <summary>
    /// Bet ticket view model.
    /// </summary>
    [Serializable]
    public class BetItem
    {
        public string Username { get; set; }

        public long TransId { get; set; }

        public DateTime TransDate { get; set; }

        public decimal Stake { get; set; }

        public decimal WinLoss { get; set; }

        public decimal Comm { get; set; }

        public decimal CompanyWinLoss { get; set; }

        public decimal CompanyShare { get; set; }

        public decimal CompanyComm { get; set; }

        public bool IsHandicap { get; set; }

        public bool IsRunning { get; set; }

        /// <summary>
        /// Gets or sets format of the transaction time column in the view.
        /// </summary>
        /// <value>
        /// HTML string represents the transaction time column.
        /// </value>
        public string TransTimeCol { get; set; }

        /// <summary>
        /// Gets or sets format of the choice column in the view.
        /// </summary>
        /// <value>
        /// HTML string represents the choice column.
        /// </value>
        public string ChoiceCol { get; set; }

        /// <summary>
        /// Gets or sets format of the odds column in the view.
        /// </summary>
        /// <value>
        /// HTML string represents the odds column.
        /// </value>
        public string OddsCol { get; set; }

        /// <summary>
        /// Gets or sets format of the status column in the view.
        /// </summary>
        /// <value>
        /// HTML string represents the status column.
        /// </value>
        public string StatusCol { get; set; }
    }
}