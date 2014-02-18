// -----------------------------------------------------------------------
// <copyright file="RawMessage.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
//   <change who="Jason.Ly" date="2014.2.18">Change class name</change>
// </history>

namespace Apollo.CMS.Message.Models.MessageList
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Apollo.CMS.Core.Resources;

    /// <summary>
    /// Message model.
    /// </summary>
    public class RawMessage
    {
        public DateTime? PostTime { get; set; }

        public string ContentCh { get; set; }

        public string ContentCs { get; set; }

        public string ContentEn { get; set; }

        public string ContentJp { get; set; }

        public string ContentKo { get; set; }

        public string ContentTh { get; set; }

        public string ContentVn { get; set; }

        public byte? MsgType { get; set; }

        public bool? IsSpecial { get; set; }

        public string Creator { get; set; }

        public int TargetId { get; set; }

        public string Recipient { get; set; }

        public int MessageId { get; set; }

        public int MsgTargetId { get; set; }
    }
}