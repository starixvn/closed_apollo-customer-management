// -----------------------------------------------------------------------
// <copyright file="MessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
//   <change who="Jason.Ly" date="2014.2.18">Refactor code</change>
// </history>

namespace Apollo.CMS.Message.Services.Implements
{
    using System.Collections.Generic;
    using System.Linq;
    using Apollo.CMS.Core.Services;
    using Apollo.CMS.Message.Criteria.MessageList;
    using Apollo.CMS.Message.Models.MessageList;
    using Apollo.CMS.Message.Services.Interfaces;
    using Apollo.CMS.Message.ViewModels.MessageList;
    using Fanex.NAP.Framework.Command;

    /// <summary>
    /// Message services.
    /// </summary>
    public class MessageService : BaseService, IMessageService
    {
        /// <summary>
        /// Gets the list of messages that satisfied the passed query conditions.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of messages.</returns>
        public IEnumerable<MessageInfo> GetMessageList(GetMessageListCriteria criteria)
        {
            IEnumerable<RawMessage> rawData = null;
            IEnumerable<MessageInfo> result = null;

            criteria.ToDate = criteria.ToDate.AddDays(1).AddMilliseconds(-1);
            rawData = GetListData<RawMessage>(criteria);

            if (rawData != null && rawData.Any())
            {
                result = rawData.Select(obj => new MessageInfo(obj));
            }

            return result;
        }

        /// <summary>
        /// Gets the message by identifier.
        /// </summary>
        /// <param name="getMessageByIdCriteria">The get message by identifier criteria.</param>
        /// <returns>The message.</returns>
        public MessageDetails GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria)
        {
            RawMessage rawMessage = GetData<RawMessage>(getMessageByIdCriteria);

            return new MessageDetails(rawMessage);
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