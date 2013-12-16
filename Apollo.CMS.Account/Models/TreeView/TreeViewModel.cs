// --------------------------------------------------------------------------
// <copyright file="TreeViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.TreeView
{
    /// <summary>
    /// Tree view model.
    /// </summary>
    public class TreeViewModel
    {
        public int? AgentId { get; set; }

        public string Username { get; set; }

        public int? ParentId { get; set; }

        public short? AgentLevel { get; set; }

        public bool? HasAgentDownline { get; set; }
    }
}