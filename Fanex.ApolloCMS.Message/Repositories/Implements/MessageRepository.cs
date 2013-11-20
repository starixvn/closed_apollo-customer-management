// -----------------------------------------------------------------------
// <copyright file="MessageRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Repositories.Implements
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using Fanex.ApolloCMS.Core.Repositories;
    using Fanex.ApolloCMS.Message.Criteria.MessageList;
    using Fanex.ApolloCMS.Message.Models.MessageList;
    using Fanex.ApolloCMS.Message.Repositories.Interfaces;
    using Fanex.Common;
    using Fanex.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        /// <summary>
        /// Gets the list message.
        /// </summary>
        /// <param name="getMessageListCriteria">The get message list criteria.</param>
        /// <returns>List message.</returns>
        public List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria)
        {
            IEnumerable<Message> result = null;

            using (var db = new ObjectDb(getMessageListCriteria.GetSettingKey()))
            {
                result = db.Query<Message>(getMessageListCriteria.GetSpParams());
            }

            return result.ToList();
        }

        /// <summary>
        /// Gets the message by identifier.
        /// </summary>
        /// <param name="getMessageByIdCriteria">The get message by identifier criteria.</param>
        /// <returns>The message.</returns>
        public Message GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria)
        {
            using (var db = new ObjectDb(getMessageByIdCriteria.GetSettingKey()))
            {
                var param = getMessageByIdCriteria.GetSpParams();
                
                return db.QueryEntity<Message>(param);
            }
        }

        /// <summary>
        /// Updates the message.
        /// </summary>
        /// <param name="updateMessageCriteria">The update message criteria.</param>
        public void UpdateMessage(UpdateMessageCriteria updateMessageCriteria)
        {
            using (var db = new ObjectDb(updateMessageCriteria.GetSettingKey()))
            {
                var param = updateMessageCriteria.GetSpParams();

                db.ExecuteNonQuery(param);
            }
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="deleteMessageCriteria">The delete message criteria.</param>
        public void DeleteMessage(DeleteMessageCriteria deleteMessageCriteria)
        {
            using (var db = new ObjectDb(deleteMessageCriteria.GetSettingKey()))
            {
                var param = deleteMessageCriteria.GetSpParams();

                db.ExecuteNonQuery(param);
            }
        }
    }
}
