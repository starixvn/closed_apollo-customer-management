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
        /// <param name="outputParam">The output parameter.</param>
        /// <returns>List message.</returns>
        public List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria, ref Hashtable outputParam)
        {
            DataSet result = null;

            using (var db = new ObjectDb(getMessageListCriteria.GetSettingKey()))
            {
                result = db.ExecuteDataSet(getMessageListCriteria.GetSpParams());
                outputParam = db.GetOuputParams();
            }

            var messageListTable = result.Tables[0];
            var messageList = new List<Message>();

            foreach (DataRow row in messageListTable.Rows)
            {
                messageList.Add(new Message()
                {
                    PostTime = Converter.ToDateTime(row["PostTime"]),
                    ContentCh = Converter.ToString2(row["Content_ch"]),
                    ContentCs = Converter.ToString2(row["Content_cs"]),
                    ContentEn = Converter.ToString2(row["Content_en"]),
                    ContentJp = Converter.ToString2(row["Content_jp"]),
                    ContentKo = Converter.ToString2(row["Content_ko"]),
                    ContentTh = Converter.ToString2(row["Content_th"]),
                    ContentVn = Converter.ToString2(row["Content_vn"]),
                    Creator = Converter.ToString2(row["CreatorName"]),
                    IsSpecial = Converter.ToBool(row["IsSpecial"]),
                    MsgType = Convert.ToByte(row["MsgType"]),
                    Recipient = Converter.ToString2(row["Recipient"]),
                    TargetId = Converter.ToInt(row["TargetId"]),
                    MessageId = Converter.ToInt(row["MessageId"])
                });
            }

            return messageList;
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
        /// <returns>Message update.</returns>
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
        /// <param name="deleteMessageCriterial">The delete message criteria.</param>
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
