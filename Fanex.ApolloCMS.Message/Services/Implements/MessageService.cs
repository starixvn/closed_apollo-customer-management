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
            var outputParam = new Hashtable();

            if (Converter.ToString2(getMessageListCriteria.MsgType) == "All")
            {
                getMessageListCriteria.MsgType = null;
            }

            if (Converter.ToString2(getMessageListCriteria.Special) == "All")
            {
                getMessageListCriteria.Special = null;
            }

            var rawData = _messageRepository.GetListMessage(getMessageListCriteria, ref outputParam);

            if (rawData == null)
            {
                return result;
            }

            foreach (var row in rawData)
            {
                var messageInfo = new Message()
                {
                    RowNumber = row.RowNumber,
                    PostDate = row.PostDate,
                    Content_CH = row.Content_CH,
                    Content_CS = row.Content_CS,
                    Content_EN = row.Content_EN,
                    Content_JP = row.Content_JP,
                    Content_KO = row.Content_KO,
                    Content_TH = row.Content_TH,
                    Content_VN = row.Content_VN,
                    MsgType = row.MsgType,
                    IsSpecial = row.IsSpecial,
                    Creator = row.Creator,
                    TargetId = row.TargetId 
                };
                result.Add(messageInfo);
            }

            return result;
        }
    }
}
