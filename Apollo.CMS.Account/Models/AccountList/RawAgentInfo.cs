// --------------------------------------------------------------------------
// <copyright file="RawAgentInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.19">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountList
{
    /// <summary>
    /// Agent model.
    /// </summary>
    public class RawAgentInfo
    {
        public int AgentId { get; set; }

        public string Username { get; set; }

        public short? AgentLevel { get; set; }

        public byte? BetStatus { get; set; }

        public byte? UplineStatus { get; set; }

        public byte? UplineBetStatus { get; set; }

        public byte? Status { get; set; }

        public int? NumberOfAgent { get; set; }

        public int? NumberOfMember { get; set; }

        public int? NumberOfShadow { get; set; }

        public string Currency { get; set; }
    }
}