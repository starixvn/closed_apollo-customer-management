// --------------------------------------------------------------------------
// <copyright file="NavigationPath.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.19">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountList
{
    /// <summary>
    /// Navigation path model.
    /// </summary>
    public class NavigationPath
    {
        public int? AgentId { get; set; }

        public string Username { get; set; }

        public short? AgentLevel { get; set; }

        public string Currency { get; set; }
    }
}