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
        public long? RowNumber { get; set; }

        public DateTime? PostDate { get; set; }

        public string Content_CH { get; set; }

        public string Content_CS { get; set; }

        public string Content_EN { get; set; }

        public string Content_JP { get; set; }

        public string Content_KO { get; set; }

        public string Content_TH { get; set; }

        public string Content_VN { get; set; }

        public byte? MsgType { get; set; }

        public bool? IsSpecial { get; set; }

        public string Creator { get; set; }

        public int TargetId { get; set; }

        public string Recipient { get; set; }

        public int MessageId { get; set; }

        public string MessageType
        {
            get
            {
                switch ((int)MsgType)
                {
                    case (int)MessageTypeConstants.Public:
                        return Labels.Public;
                    case (int)MessageTypeConstants.Private:
                        return Labels.Private;
                    default:
                        return string.Empty;
                }
            }
        }

        public string MessageIsSpecial
        {
            get
            {
                if (IsSpecial == true)
                {
                    return Labels.Yes;
                }
                else
                {
                    return Labels.No;
                }
            }
        }
    }
}
