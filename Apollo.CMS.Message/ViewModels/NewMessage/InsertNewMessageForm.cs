// --------------------------------------------------------------------------
// <copyright file="InsertNewMessageForm.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.17">Created</change>
// </history>

namespace Apollo.CMS.Message.ViewModels.NewMessage
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Apollo.CMS.Core.Resources;

    /// <summary>
    /// Insert new message form.
    /// </summary>
    public class InsertNewMessageForm
    {
        public byte MsgType { get; set; }

        public bool IsSpecialMsg { get; set; }

        public int UserLoginId { get; set; }

        public string UserLoginName { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewLang), ErrorMessageResourceName = "RequireEnglishContent")]
        public string ContentEn { get; set; }

        public string ContentCh { get; set; }

        public string ContentCs { get; set; }

        public string ContentTh { get; set; }

        public string ContentJp { get; set; }

        public string ContentKo { get; set; }

        public string ContentVn { get; set; }

        public IEnumerable<Recipient> RecipientList { get; set; }
    }
}