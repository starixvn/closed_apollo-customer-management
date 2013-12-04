// -----------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.Core.Services.Implements;
    using Apollo.CMS.Message.Criteria.MessageList;
    using Apollo.CMS.Message.Models.MessageList;
    using Apollo.CMS.Message.Services.Interfaces;
    using Fanex.Common;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Message services.
    /// </summary>
    public class MessageService : BaseService, IMessageService
    {
        /// <summary>
        /// Gets the list message.
        /// </summary>
        /// <param name="getMessageListCriteria">The get message list criteria.</param>
        /// <returns>List message.</returns>
        public List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria)
        {
            IEnumerable<Message> result = null;

            if (Converter.ToString2(getMessageListCriteria.MsgType) == "All")
            {
                getMessageListCriteria.MsgType = null;
            }

            if (Converter.ToString2(getMessageListCriteria.Special) == "All")
            {
                getMessageListCriteria.Special = null;
            }

            getMessageListCriteria.ToDate = getMessageListCriteria.ToDate.AddDays(1).AddMilliseconds(-1);

            result = GetListData<Message>(getMessageListCriteria);

            return result.ToList();
        }

        /// <summary>
        /// Gets the message by identifier.
        /// </summary>
        /// <param name="getMessageByIdCriteria">The get message by identifier criteria.</param>
        /// <returns>The message.</returns>
        public Message GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria)
        {
            var result = new Message();
            result = GetData<Message>(getMessageByIdCriteria);
            return result;
        }

        /// <summary>
        /// Updates the message.
        /// </summary>
        /// <param name="updateMessageCriteria">The update message criteria.</param>
        /// <returns>The query result.</returns>
        public ICommandResult UpdateMessage(UpdateMessageCriteria updateMessageCriteria)
        {
            return UpdateData(updateMessageCriteria);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="deleteMessageCriteria">The delete message criteria.</param>
        /// <returns>The query result.</returns>
        public ICommandResult DeleteMessage(DeleteMessageCriteria deleteMessageCriteria)
        {
            return UpdateData(deleteMessageCriteria);
        }
    }
}