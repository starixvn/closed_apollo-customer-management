// --------------------------------------------------------------------------
// <copyright file="AgentInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.5">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountInfo
{
    /// <summary>
    /// Agent information model.
    /// </summary>
    public class AgentInfo
    {
        public int AgentId { get; set; }

        public short AgentLevel { get; set; }

        public string Username { get; set; }

        public byte Currency { get; set; }

        public byte Status { get; set; }

        public byte BetStatus { get; set; }

        public byte UplineStatus { get; set; }

        public byte UplineBetStatus { get; set; }

        public decimal MatchLimit { get; set; }

        public decimal VirtualMultiplier { get; set; }

        public bool DoubleCommLosing { get; set; }

        public decimal SportCredit { get; set; }

        public int ParentId { get; set; }

        public string ParentUsername { get; set; }
    }
}