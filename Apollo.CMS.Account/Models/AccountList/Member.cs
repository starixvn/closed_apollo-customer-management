// --------------------------------------------------------------------------
// <copyright file="Member.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.19">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountList
{
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.Helpers;

    /// <summary>
    /// Member model.
    /// </summary>
    public class Member
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public byte? Status { get; set; }

        public byte? BetStatus { get; set; }

        public int AgentId { get; set; }

        public string UGroup { get; set; }

        public byte? UplineStatus { get; set; }

        public byte? UplineBetStatus { get; set; }

        public decimal? CashBalance { get; set; }

        public string RealStatus
        {
            get
            {
                return StatusHelper.GetStatus((AccountStatus)this.Status, (AccountStatus)this.UplineStatus);
            }
        }

        public string RealBetStatus
        {
            get
            {
                return StatusHelper.GetStatus((AccountStatus)this.BetStatus, (AccountStatus)this.UplineBetStatus);
            }
        }
    }
}