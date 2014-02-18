// -----------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
//   <change who="Jason.Ly" date="2014.2.18">Refactor code</change>
// </history>

namespace Apollo.CMS.Message.Services.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.Message.Criteria.MessageList;
    using Apollo.CMS.Message.Models.MessageList;
    using Apollo.CMS.Message.ViewModels.MessageList;
    using Fanex.NAP.Framework.Command;

    public interface IMessageService
    {
        /// <summary>
        /// Gets the list of messages that satisfied the passed query conditions.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of messages.</returns>
        IEnumerable<MessageInfo> GetMessageList(GetMessageListCriteria criteria);

        /// <summary>
        /// Gets the message by identifier.
        /// </summary>
        /// <param name="getMessageByIdCriteria">The get message by identifier criteria.</param>
        /// <returns>The message.</returns>
        MessageDetails GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria);

        /// <summary>
        /// Updates the message.
        /// </summary>
        /// <param name="updateMessageCriteria">The update message criteria.</param>
        /// <returns>The query result.</returns>
        ICommandResult UpdateMessage(UpdateMessageCriteria updateMessageCriteria);

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="deleteMessageCriteria">The delete message criteria.</param>
        /// <returns>The query result.</returns>
        ICommandResult DeleteMessage(DeleteMessageCriteria deleteMessageCriteria);
    }
}