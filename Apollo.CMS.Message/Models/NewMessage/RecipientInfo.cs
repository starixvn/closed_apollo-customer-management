// --------------------------------------------------------------------------
// <copyright file="RecipientInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Models.NewMessage
{
    /// <summary>
    /// Recipient information model.
    /// </summary>
    public class RecipientInfo
    {
        public int AgentId { get; set; }

        public int CustId { get; set; }

        public string UserName { get; set; }
    }
}