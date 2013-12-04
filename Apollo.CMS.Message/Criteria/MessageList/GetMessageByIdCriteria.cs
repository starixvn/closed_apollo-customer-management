// -----------------------------------------------------------------------
// <copyright file="GetMessageByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.14">Created</change>
// </history>

namespace Apollo.CMS.Message.Criteria.MessageList
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get message by identity.
    /// </summary>
    public class GetMessageByIdCriteria : CriteriaBase
    {
        public int MessageId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Acc_Msg_GetMessageById";
        }
    }
}
