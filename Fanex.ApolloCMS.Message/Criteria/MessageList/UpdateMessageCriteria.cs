// -----------------------------------------------------------------------
// <copyright file="UpdateMessageCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Criteria.MessageList
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UpdateMessageCriteria : CriteriaBase
    {
        public int MsgId { get; set; }

        public int UserLoginId { get; set; }

        public string UserLoginName { get; set; }

        public string Content_en { get; set; }

        public string Content_ch { get; set; }

        public string Content_cs { get; set; }

        public string Content_th { get; set; }

        public string Content_jp { get; set; }

        public string Content_ko { get; set; }

        public string Content_vn { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Message_MessageList_UpdateMessage";
        }
    }
}
