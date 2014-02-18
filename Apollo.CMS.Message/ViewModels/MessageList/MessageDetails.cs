// --------------------------------------------------------------------------
// <copyright file="MessageDetails.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.17">Created</change>
// </history>

namespace Apollo.CMS.Message.ViewModels.MessageList
{
    using System.ComponentModel.DataAnnotations;
    using Apollo.CMS.Core.Resources;
    using Apollo.CMS.Message.Models.MessageList;

    /// <summary>
    /// View model for a message item in edit popup.
    /// </summary>
    public class MessageDetails
    {
        public MessageDetails()
        {
        }

        public MessageDetails(RawMessage rawMessage)
        {
            if (rawMessage != null)
            {
                this.ContentCh = rawMessage.ContentCh;
                this.ContentCs = rawMessage.ContentCs;
                this.ContentEn = rawMessage.ContentEn;
                this.ContentJp = rawMessage.ContentJp;
                this.ContentKo = rawMessage.ContentKo;
                this.ContentTh = rawMessage.ContentTh;
                this.ContentVn = rawMessage.ContentVn;
            }
        }

        public string ContentCh { get; set; }

        public string ContentCs { get; set; }

        [Required(ErrorMessageResourceType = typeof(ViewLang), ErrorMessageResourceName = "RequireEnglishContent")]
        public string ContentEn { get; set; }

        public string ContentJp { get; set; }

        public string ContentKo { get; set; }

        public string ContentTh { get; set; }

        public string ContentVn { get; set; }
    }
}