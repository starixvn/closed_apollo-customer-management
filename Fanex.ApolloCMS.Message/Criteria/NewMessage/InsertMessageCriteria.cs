// --------------------------------------------------------------------------
// <copyright file="InsertMessageCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Criteria.NewMessage
{
    using System;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Insert new message criteria.
    /// </summary>
    public class InsertMessageCriteria : CriteriaBase
    {
        public byte MsgType { get; set; }

        public bool IsSpecialMsg { get; set; }

        public int UserLoginId { get; set; }

        public string UserLoginName { get; set; }

        public string Recipient { get; set; }

        public string Content_En { get; set; }

        public string Content_Ch { get; set; }

        public string Content_Cs { get; set; }

        public string Content_Th { get; set; }

        public string Content_Jp { get; set; }

        public string Content_Ko { get; set; }

        public string Content_Vn { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Acc_Msg_InsertMessage";
        }

        /// <summary>
        /// Check validation of the criteria.
        /// </summary>
        /// <returns>Whether or not the criteria is valid.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}