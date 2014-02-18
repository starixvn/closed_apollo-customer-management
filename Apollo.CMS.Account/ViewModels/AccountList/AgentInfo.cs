// --------------------------------------------------------------------------
// <copyright file="AgentInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.18">Created</change>
// </history>

namespace Apollo.CMS.Account.ViewModels.AccountList
{
    using System;
    using Apollo.CMS.Account.Models.AccountList;
    using Apollo.CMS.Core.Helpers;
    using Fanex.Common;

    /// <summary>
    /// View model for an agent item in the agent list.
    /// </summary>
    [Serializable]
    public class AgentInfo
    {
        public AgentInfo()
        {
        }

        public AgentInfo(RawAgentInfo rawData)
        {
            if (rawData != null)
            {
                this.AgentId = rawData.AgentId;
                this.Username = rawData.Username;
                this.AgentLevel = Converter.To<short>(rawData.AgentLevel);
                this.NumberOfAgent = Converter.ToInt(rawData.NumberOfAgent);
                this.NumberOfMember = Converter.ToInt(rawData.NumberOfMember);
                this.NumberOfShadow = Converter.ToInt(rawData.NumberOfShadow);
                this.Currency = rawData.Currency;
                this.Status = StatusHelper.GetStatus(rawData.Status, rawData.UplineStatus);
                this.BetStatus = StatusHelper.GetStatus(rawData.BetStatus, rawData.UplineBetStatus);
            }
        }

        public int AgentId { get; set; }

        public string Username { get; set; }

        public short AgentLevel { get; set; }

        public int NumberOfAgent { get; set; }

        public int NumberOfMember { get; set; }

        public int NumberOfShadow { get; set; }

        public string Currency { get; set; }

        public string Status { get; set; }

        public string BetStatus { get; set; }
    }
}