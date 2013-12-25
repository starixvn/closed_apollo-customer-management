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
    using System.Web.Script.Serialization;
    using Apollo.CMS.Core.Helpers;

    /// <summary>
    /// Member model.
    /// </summary>
    public class Member
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        [ScriptIgnore]
        public byte? Status { get; set; }

        [ScriptIgnore]
        public byte? BetStatus { get; set; }

        public int AgentId { get; set; }

        [ScriptIgnore]
        public string UGroup { get; set; }

        [ScriptIgnore]
        public byte? UplineStatus { get; set; }

        [ScriptIgnore]
        public byte? UplineBetStatus { get; set; }

        public decimal? CashBalance { get; set; }

        public string ActualUserGroup
        {
            get
            {
                string userGroup = string.Empty;

                if (!string.IsNullOrEmpty(this.UGroup))
                {
                    switch (this.UGroup.ToLowerInvariant())
                    {
                        case "a":
                            userGroup = "D";
                            break;

                        case "b":
                            userGroup = "C";
                            break;

                        case "c":
                            userGroup = "B";
                            break;

                        case "d":
                            userGroup = "A";
                            break;
                    }
                }

                return userGroup;
            }
        }

        public string ActualStatus
        {
            get
            {
                return StatusHelper.GetStatus(this.Status, this.UplineStatus);
            }
        }

        public string ActualBetStatus
        {
            get
            {
                return StatusHelper.GetStatus(this.BetStatus, this.UplineBetStatus);
            }
        }
    }
}