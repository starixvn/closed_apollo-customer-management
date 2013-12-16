// -----------------------------------------------------------------------
// <copyright file="Message.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Models.MessageList
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Apollo.CMS.Message.Constants;
    using Apollo.CMS.Message.Resources;

    /// <summary>
    /// Message model.
    /// </summary>
    public class Message
    {
        public DateTime? PostTime { get; set; }

        public string ContentCh { get; set; }

        public string ContentCs { get; set; }

        [Required(ErrorMessageResourceType = typeof(Labels), ErrorMessageResourceName = "RequireEnglishContent")]
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
