// --------------------------------------------------------------------------
// <copyright file="UplineAgent.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountInfo
{
    using System;

    /// <summary>
    /// Up line agent model.
    /// </summary>
    public class UplineAgent
    {
        public int? AgentId { get; set; }

        public string Username { get; set; }

        public short? AgentLevel { get; set; }

        public int? ParentId { get; set; }
    }
}