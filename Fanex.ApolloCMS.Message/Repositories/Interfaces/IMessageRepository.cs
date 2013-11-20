// -----------------------------------------------------------------------
// <copyright file="IMessageRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Repositories.Interfaces
{
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.ApolloCMS.Message.Criteria.MessageList;
    using Fanex.ApolloCMS.Message.Models.MessageList;

    public interface IMessageRepository
    {
        List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria);

        Message GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria);

        void UpdateMessage(UpdateMessageCriteria updateMessageCriteria);

        void DeleteMessage(DeleteMessageCriteria deleteMessageCriteria);
    }
}
