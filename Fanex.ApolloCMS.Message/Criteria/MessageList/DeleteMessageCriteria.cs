// -----------------------------------------------------------------------
// <copyright file="DeleteMessageCriteria.cs" company="Nexcel Solutions Vietnam">
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
    public class DeleteMessageCriteria : CriteriaBase
    {
        public int MsgId { get; set; }

        public int TargetId { get; set; }

        public int UserLoginId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Acc_Msg_DeleteMessage";
        }
    }
}
