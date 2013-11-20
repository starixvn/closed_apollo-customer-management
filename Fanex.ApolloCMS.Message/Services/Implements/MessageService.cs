// -----------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Services.Implements
{
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.ApolloCMS.Message.Criteria.MessageList;
    using Fanex.ApolloCMS.Message.Models.MessageList;
    using Fanex.ApolloCMS.Message.Repositories.Implements;
    using Fanex.ApolloCMS.Message.Repositories.Interfaces;
    using Fanex.ApolloCMS.Message.Services.Interfaces;
    using Fanex.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;

        public MessageService()
            : this(null)
        {
        }

        public MessageService(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository ?? new MessageRepository();
        }

        /// <summary>
        /// Gets the list message.
        /// </summary>
        /// <param name="getMessageListCriteria">The get message list criteria.</param>
        /// <returns>List message.</returns>
        public List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria)
        {
            var result = new List<Message>();

            if (Converter.ToString2(getMessageListCriteria.MsgType) == "All")
            {
                getMessageListCriteria.MsgType = null;
            }

            if (Converter.ToString2(getMessageListCriteria.Special) == "All")
            {
                getMessageListCriteria.Special = null;
            }

            getMessageListCriteria.ToDate = getMessageListCriteria.ToDate.AddDays(1).AddMilliseconds(-1);

            var rawData = _messageRepository.GetListMessage(getMessageListCriteria);

            if (rawData == null)
            {
                return result;
            }

            foreach (var row in rawData)
            {
                var messageInfo = new Message()
                {
                    PostTime = row.PostTime,
                    ContentCh = row.ContentCh,
                    ContentCs = row.ContentCs,
                    ContentEn = row.ContentEn,
                    ContentJp = row.ContentJp,
                    ContentKo = row.ContentKo,
                    ContentTh = row.ContentTh,
                    ContentVn = row.ContentVn,
                    MsgType = row.MsgType,
                    IsSpecial = row.IsSpecial,
                    Creator = row.Creator,
                    Recipient = row.Recipient,
                    TargetId = row.TargetId,
                    MessageId = row.MessageId
                };
                result.Add(messageInfo);
            }

            return result;
        }

        /// <summary>
        /// Gets the message by identifier.
        /// </summary>
        /// <param name="getMessageByIdCriteria">The get message by identifier criteria.</param>
        /// <returns>The message.</returns>
        public Message GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria)
        {
            var result = new Message();
            result = this._messageRepository.GetMessageById(getMessageByIdCriteria);
            return result;
        }

        /// <summary>
        /// Updates the message.
        /// </summary>
        /// <param name="updateMessageCriteria">The update message criteria.</param>
        public void UpdateMessage(UpdateMessageCriteria updateMessageCriteria)
        {
            this._messageRepository.UpdateMessage(updateMessageCriteria);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="deleteMessageCriteria">The delete message criteria.</param>
        public void DeleteMessage(DeleteMessageCriteria deleteMessageCriteria)
        {
            this._messageRepository.DeleteMessage(deleteMessageCriteria);
        }
    }
}
