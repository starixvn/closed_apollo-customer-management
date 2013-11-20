// -----------------------------------------------------------------------
// <copyright file="GetMessageListCriteria.cs" company="Nexcel Solutions Vietnam">
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
    /// Get message list criteria.
    /// </summary>
    public class GetMessageListCriteria : CriteriaBase
    {
        public int TopNewMsg { get; set; }

        public byte? MsgType { get; set; }

        public bool? Special { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Acc_Msg_GetListMessage";
        }
    }
}
