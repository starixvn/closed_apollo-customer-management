// -----------------------------------------------------------------------
// <copyright file="Message.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Models.MessageList
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.ApolloCMS.Message.Constants;
    using Fanex.ApolloCMS.Message.Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Message
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

        public string PostDate
        {
            get
            {
                return PostTime.ToString();
            }
        }
    }
}
