// -----------------------------------------------------------------------
// <copyright file="UpdateMessageCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Criteria.MessageList
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Update message criteria.
    /// </summary>
    public class UpdateMessageCriteria : CriteriaBase
    {
        public int MsgId { get; set; }

        public int UserLoginId { get; set; }

        public string UserLoginName { get; set; }

        public string ContentEn { get; set; }

        public string ContentCh { get; set; }

        public string ContentCs { get; set; }

        public string ContentTh { get; set; }

        public string ContentJp { get; set; }

        public string ContentKo { get; set; }

        public string ContentVn { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Acc_Msg_UpdateMessage";
        }
    }
}
