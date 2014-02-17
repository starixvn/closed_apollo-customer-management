// --------------------------------------------------------------------------
// <copyright file="InsertMessageCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Criteria.NewMessage
{
    using Apollo.CMS.Message.ViewModels.NewMessage;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Insert new message criteria.
    /// </summary>
    public class InsertMessageCriteria : CriteriaBase
    {
        public InsertMessageCriteria()
        {
        }

        public InsertMessageCriteria(InsertNewMessageForm form)
        {
            if (form != null)
            {
                this.MsgType = form.MsgType;
                this.IsSpecialMsg = form.IsSpecialMsg;
                this.UserLoginId = form.UserLoginId;
                this.UserLoginName = form.UserLoginName;
                this.ContentEn = form.ContentEn;
                this.ContentCh = form.ContentCh;
                this.ContentCs = form.ContentCs;
                this.ContentTh = form.ContentTh;
                this.ContentJp = form.ContentJp;
                this.ContentKo = form.ContentKo;
                this.ContentVn = form.ContentVn;
            }
        }

        public byte MsgType { get; set; }

        public bool IsSpecialMsg { get; set; }

        public int UserLoginId { get; set; }

        public string UserLoginName { get; set; }

        public string Recipient { get; set; }

        public string ContentEn { get; set; }

        public string ContentCh { get; set; }

        public string ContentCs { get; set; }

        public string ContentTh { get; set; }

        public string ContentJp { get; set; }

        public string ContentKo { get; set; }

        public string ContentVn { get; set; }

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