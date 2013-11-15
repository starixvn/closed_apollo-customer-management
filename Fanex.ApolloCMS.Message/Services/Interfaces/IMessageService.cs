// -----------------------------------------------------------------------
// <copyright file="IMessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using Fanex.ApolloCMS.Message.Criteria.MessageList;
    using Fanex.ApolloCMS.Message.Models.MessageList;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMessageService
    {
        List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria);

        Message GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria);
    }
}
