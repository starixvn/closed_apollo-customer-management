// --------------------------------------------------------------------------
// <copyright file="Recipient.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Models.NewMessage
{
    /// <summary>
    /// Recipient view model that is used to store recipient passed from user.
    /// </summary>
    public class Recipient
    {
        public int TargetId { get; set; }

        public string TargetName { get; set; }

        public bool IsAgent { get; set; }

        public bool InformUpline { get; set; }

        public int? AgentLevel { get; set; }
    }
}