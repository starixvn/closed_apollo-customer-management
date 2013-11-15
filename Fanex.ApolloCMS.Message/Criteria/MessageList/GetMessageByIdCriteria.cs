// -----------------------------------------------------------------------
// <copyright file="GetMessageByIdCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.14">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Criteria.MessageList
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
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
            return "Message_MessageList_GetMessageById";
        }
    }
}
