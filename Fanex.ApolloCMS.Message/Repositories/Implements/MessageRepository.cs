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
        public List<Message> GetListMessage(GetMessageListCriteria getMessageListCriteria, ref System.Collections.Hashtable outputParam)
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
                    RowNumber = (long)Convert.ToDouble(row["RowNumber"]),
                    PostDate = Converter.ToDateTime(row["PostTime"]),
                    Content_CH = Converter.ToString2(row["Content_ch"]),
                    Content_CS = Converter.ToString2(row["Content_cs"]),
                    Content_EN = Converter.ToString2(row["Content_en"]),
                    Content_JP = Converter.ToString2(row["Content_jp"]),
                    Content_KO = Converter.ToString2(row["Content_ko"]),
                    Content_TH = Converter.ToString2(row["Content_th"]),
                    Content_VN = Converter.ToString2(row["Content_vn"]),
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

        public Message GetMessageById(GetMessageByIdCriteria getMessageByIdCriteria)
        {
            using (var db = new ObjectDb(getMessageByIdCriteria.GetSettingKey()))
            {
                var param = getMessageByIdCriteria.GetSpParams();
                
                return db.QueryEntity<Message>(param);
            }
        }
    }
}
