// --------------------------------------------------------------------------
// <copyright file="MemberInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.5">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountInfo
{
    using System;

    /// <summary>
    /// Member information model.
    /// </summary>
    public class MemberInfo
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public string Currency { get; set; }

        public byte? Status { get; set; }

        public byte? BetStatus { get; set; }

        public byte? UplineStatus { get; set; }

        public byte? UplineBetStatus { get; set; }

        public decimal? MatchLimit { get; set; }

        public decimal? VirtualMultiplier { get; set; }

        public byte? Danger { get; set; }

        public DateTime? CreateDate { get; set; }

        public int? Creator { get; set; }

        public DateTime? LastLogged { get; set; }

        public string IPLogin { get; set; }

        public string UGroup { get; set; }

        public bool? DoubleCommLosing { get; set; }

        public int AgentId { get; set; }

        public bool? IsUGroupChanged { get; set; }

        public decimal? SportCredit { get; set; }

        public string AgentUsername { get; set; }

        public short? AgentLevel { get; set; }

        public byte? AgentStatus { get; set; }
    }
}