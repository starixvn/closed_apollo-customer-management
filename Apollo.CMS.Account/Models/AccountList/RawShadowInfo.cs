// --------------------------------------------------------------------------
// <copyright file="RawShadowInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.19">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountList
{
    using System;
    using System.Web.Script.Serialization;
    using Apollo.CMS.Core.Helpers;

    /// <summary>
    /// Show model.
    /// </summary>
    public class RawShadowInfo
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime? CreatedDate { get; set; }

        [ScriptIgnore]
        public byte? Status { get; set; }

        public string ActualStatus
        {
            get
            {
                return StatusHelper.GetShadowStatus(this.Status);
            }
        }
    }
}