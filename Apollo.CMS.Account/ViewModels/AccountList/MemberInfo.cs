// --------------------------------------------------------------------------
// <copyright file="MemberInfo.cs" company="Nexcel Solutions Vietnam">
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
    /// View model for a member item in the member list.
    /// </summary>
    [Serializable]
    public class MemberInfo
    {
        public MemberInfo()
        {
        }

        public MemberInfo(RawMemberInfo rawData)
        {
            if (rawData != null)
            {
                this.CustId = rawData.CustId;
                this.AgentId = rawData.CustId;
                this.Username = rawData.Username;
                this.CashBalance = Converter.ToDecimal(rawData.CashBalance);
                this.Status = StatusHelper.GetStatus(rawData.Status, rawData.UplineStatus);
                this.BetStatus = StatusHelper.GetStatus(rawData.BetStatus, rawData.UplineBetStatus);
                this.UserGroup = GetUserGroup(rawData.UGroup);
            }
        }

        public int CustId { get; set; }

        public int AgentId { get; set; }

        public string Username { get; set; }

        public decimal CashBalance { get; set; }

        public string UserGroup { get; set; }

        public string Status { get; set; }

        public string BetStatus { get; set; }

        /// <summary>
        /// Gets the actual user group.
        /// </summary>
        /// <param name="group">The group got from database.</param>
        /// <returns>The actual group which user belongs to.</returns>
        private string GetUserGroup(string group)
        {
            string userGroup = string.Empty;

            if (!string.IsNullOrEmpty(group))
            {
                switch (group.ToLowerInvariant())
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
}