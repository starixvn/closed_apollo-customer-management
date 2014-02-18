// --------------------------------------------------------------------------
// <copyright file="MessageInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.17">Created</change>
// </history>

namespace Apollo.CMS.Message.ViewModels.MessageList
{
    using Apollo.CMS.Core.Resources;
    using Apollo.CMS.Message.Constants;
    using Apollo.CMS.Message.Models.MessageList;
    using Fanex.Common;
    using Fanex.NAP.Core.Web;

    /// <summary>
    /// View model for a message item in the message list.
    /// </summary>
    public class MessageInfo
    {
        public MessageInfo()
        {
        }

        public MessageInfo(RawMessage rawMessage)
        {
            if (rawMessage != null)
            {
                this.MessageId = rawMessage.MessageId;
                this.PostTime = Converter.ToDateTime(rawMessage.PostTime).ToFormatedDateTime();
                this.ContentEn = rawMessage.ContentEn;
                this.MessageType = GetMessageType(Converter.To<byte>(rawMessage.MsgType));
                this.IsSpecial = rawMessage.IsSpecial ?? false;
                this.Creator = rawMessage.Creator;
                this.Recipient = rawMessage.Recipient;
                this.MsgTargetId = rawMessage.MsgTargetId;
            }
        }

        public int MessageId { get; set; }

        public string PostTime { get; set; }

        public string ContentEn { get; set; }

        public string MessageType { get; set; }

        public bool IsSpecial { get; set; }

        public string Creator { get; set; }

        public string Recipient { get; set; }

        public int MsgTargetId { get; set; }

        /// <summary>
        /// Gets the type of the message.
        /// </summary>
        /// <param name="msgType">Message type ID.</param>
        /// <returns>Type of the message.</returns>
        private string GetMessageType(byte msgType)
        {
            var result = string.Empty;

            if (msgType == (byte)MessageTypes.Public)
            {
                result = ViewLang.Public;
            }
            else if (msgType == (byte)MessageTypes.Private)
            {
                result = ViewLang.Private;
            }

            return result;
        }
    }
}