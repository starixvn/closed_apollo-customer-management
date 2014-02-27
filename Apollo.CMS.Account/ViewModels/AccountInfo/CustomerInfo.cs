// --------------------------------------------------------------------------
// <copyright file="CustomerInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.Account.ViewModels.AccountInfo
{
    using Apollo.CMS.Account.Models.AccountInfo;
    using Fanex.Common;

    /// <summary>
    /// Common information of a customer.
    /// </summary>
    public class CustomerInfo
    {
        public CustomerInfo()
        {
        }

        public CustomerInfo(AgentInfo agent)
        {
            if (agent != null)
            {
                this.CustId = agent.AgentId;
                this.Level = Converter.ToInt(agent.AgentLevel);
                this.UplineId = agent.ParentId;
                this.Username = agent.Username;
            }
        }

        public CustomerInfo(MemberInfo member)
        {
            if (member != null)
            {
                this.CustId = member.CustId;
                this.UplineId = member.AgentId;
                this.Username = member.Username;
                this.IsMember = true;
            }
        }

        public int CustId { get; set; }

        public int Level { get; set; }

        public int? UplineId { get; set; }

        public string Username { get; set; }

        public bool IsMember { get; set; }
    }
}